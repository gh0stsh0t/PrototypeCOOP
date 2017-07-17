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
        public MySqlConnection conn;
        public ViewMember()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root;");
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (!advanceSearchVisible)
                panel1.Visible = !panel1.Visible;
        }
        private void Animations()
        {
            opaque.Location = new Point(0, 0);
            opaque.Size = this.Size;
            popupEnabler.Start();
        }
        private void ViewMember_Load(object sender, EventArgs e)
        {
            Rifrish();
        }
        private void Rifrish()
        {
            try
            {
                conn.Open();
                var comm = new MySqlCommand("SELECT member_id,concat_ws(',',family_name ,first_name) as name,gender,address,contact_no,type,status FROM members ", conn);
                var adp = new MySqlDataAdapter(comm);
                var dt = new DataTable();
                adp.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Height = dataGridView1.GetRowDisplayRectangle(0, true).Bottom * dataGridView1.RowCount + dataGridView1.ColumnHeadersHeight;
                dataGridView1.Columns["member_id"].Visible = false;
                conn.Close();
                var x = new DataGridViewButtonColumn();
                x.DefaultCellStyle.BackColor = button4.BackColor;
                var addColumn = new DataGridViewButtonColumn
                {
                    Name = "loans",
                    Text = "loans ",
                    FlatStyle = FlatStyle.Flat,
                    DefaultCellStyle=x.DefaultCellStyle
                };
                var editColumn = new DataGridViewButtonColumn
                {
                    Name = "savings",
                    Text = "savings ",
                    FlatStyle = FlatStyle.Flat,
                    DefaultCellStyle=x.DefaultCellStyle
                };
                var columnIndex = dataGridView1.ColumnCount;
                if (dataGridView1.Columns["loans"] == null)
                {
                    dataGridView1.Columns.Add(addColumn);
                    dataGridView1.Columns.Add(editColumn);
                }
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.ToString());
                conn.Close();
            }
            dataGridView1.ClearSelection();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                try
                {
                    conn.Open();
                    MySqlCommand comm = new MySqlCommand("SELECT concat_ws(',',family_name ,first_name) as name FROM members WHERE family_name LIKE '" + tbSearch.Text + "%'", conn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                    var dt = new DataTable();
                    adp.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Height = dataGridView1.GetRowDisplayRectangle(0, true).Bottom * dataGridView1.RowCount + dataGridView1.ColumnHeadersHeight;
                    conn.Close();
                    dataGridView1.Columns["loans"].DisplayIndex = dataGridView1.ColumnCount - 2;
                    dataGridView1.Columns["savings"].DisplayIndex = dataGridView1.ColumnCount - 1;
                }
                catch (Exception ee)
                {
                    //MessageBox.Show(ee.ToString());
                    conn.Close();
                }
            }
            else
            {
                Rifrish();
                Rifrish();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["loans"].Index)
            {
                MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells["member_id"].Value.ToString()+" loans");
            }
            else if (e.ColumnIndex == dataGridView1.Columns["savings"].Index)
            {
                MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells["member_id"].Value.ToString()+" savings");
            }
        }

        private void popupEnabler_Tick(object sender, EventArgs e)
        {
            //opaque.Opactiy
        }

        private void button3_Click(object sender, EventArgs e)
        {
            advanceSearchVisible = true;
            panel1.Visible = true;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("edit dialog of user " + dataGridView1.Rows[e.RowIndex].Cells["member_id"].Value.ToString() + " " + dataGridView1.Rows[e.RowIndex].Cells["name"].Value.ToString());
        }
    }
}
