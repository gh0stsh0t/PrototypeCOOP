using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace AMC
{
    public partial class ViewMember : Form
    {
        private bool advanceSearchVisible = false;
        private string filter = " where status=1";
        public MySqlConnection conn;
        public MainForm reftomain;
        public ViewMember(MainForm parent)
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root;");
            reftomain = parent;
            this.TopLevel = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (!advanceSearchVisible)
                panel1.Visible = !panel1.Visible;
        }
        private void ViewMember_Load(object sender, EventArgs e)
        {
            Rifrish();
        }
        private void Rifrish()
        {
            try
            {
                var tae = new DatabaseConn();
                if (filter.Equals(""))
                {
                    string[] taes = {"member_id",
                    "concat_ws(',', family_name, first_name) as name", "gender", "address", "contact_no", "type",
                    "status"};
                    dataGridView2.DataSource = tae.Select("members", taes).GetQueryData();
                }
                else
                {
                    MySqlCommand comm = new MySqlCommand("SELECT DISTINCT member_id, concat_ws(',',family_name ,first_name) as name, gender, address, contact_no, type, status FROM members " + filter, conn);

                    Console.Write(comm.CommandText);
                    MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                    var dt = new DataTable();
                    adp.Fill(dt);
                    dataGridView2.DataSource = dt;
                }
                //dataGridView1.DataSource = dt;
                dataGridView2.Columns["member_id"].Visible = false;
                int height = dataGridView2.Rows.Cast<DataGridViewRow>().Sum(row => row.Height);
                height += dataGridView2.ColumnHeadersHeight;
                int width = dataGridView2.Width;
                dataGridView2.ClientSize = new Size(width, height);
                conn.Close();
            }
            catch (Exception ee)
            {
                ////MessageBox.Show(ee.ToString());
                conn.Close();
            }
            dataGridView2.ClearSelection();
        }


        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                try
                {
                    conn.Open();
                    MySqlCommand comm = new MySqlCommand("SELECT members.member_id, concat_ws(',',family_name ,first_name) as name, gender, address, contact_no, type, status  FROM members WHERE family_name LIKE '" + tbSearch.Text + "%' AND " + filter, conn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                    var dt = new DataTable();
                    adp.Fill(dt);
                    dataGridView2.DataSource = dt;
                    int height = dataGridView2.Rows.Cast<DataGridViewRow>().Sum(row => row.Height);
                    height += dataGridView2.ColumnHeadersHeight;

                    dataGridView2.ClientSize = new Size(dataGridView2.Width, height);
                    conn.Close();
                }
                catch (Exception ee)
                {
                    ////MessageBox.Show(ee.ToString());
                    conn.Close();
                }
            }
            else
            {
                Rifrish();
            }
        }


        private void filterSet(int x)
        {
            switch (x)
            {
                case 0:
                    filter = "";
                    break;
                case 1:
                    filter = "WHERE date_terminated = NULL";
                    break;
                case 2:
                    filter = "WHERE date_terminated NOT IN(NULL)";
                    break;
                case 3:
                    filter = "WHERE status=0";
                    break;
                default:
                    filter = "WHERE status=1";
                    break;
            }
            Rifrish();
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            //filter
            filterSet(comboBox1.SelectedIndex);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //reset
            filterSet(7);
        }

        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int memid = int.Parse(dataGridView2.Rows[e.RowIndex].Cells["member_id"].Value.ToString());
                if (e.Button.Equals(MouseButtons.Left))
                    reftomain.innerChild(new ViewProfile(memid, reftomain));
                else
                    reftomain.innerChild(new AddMember(memid));
            }
            catch (System.ArgumentOutOfRangeException)
            {

            }
        }
    }
}
