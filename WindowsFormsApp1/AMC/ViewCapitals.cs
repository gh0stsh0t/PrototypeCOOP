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
    public partial class ViewCapitals : Form
    {
        public MySqlConnection conn;
        public MainForm reftomain;
        int accountstatus = 1;

        public ViewCapitals(MainForm main)
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root;");
            reftomain = main;
        }

        private void ViewCapitals_Load(object sender, EventArgs e)
        {
            cbxYear.SelectedText = DateTime.Now.Year.ToString();
            loadAccounts(cbxYear.Text);
        }

        private void loadAccounts(string yr)
        {
            lblDate.Text = "as of " + yr;
            accountstatus = checkStatus();
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand();
                comm.Connection = conn;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "displayCapitalsTable";
                comm.Parameters.AddWithValue("@yr", yr);
                comm.Parameters.AddWithValue("@accountstatus", accountstatus);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    dgvAccounts.DataSource = dt;
                    dgvAccounts.Columns["member_id"].Visible = false;
                    // dgvAccounts.CurrentCell.Selected = false;
                    dgvAccounts.Columns["Acc. No."].DefaultCellStyle.Format = null;
                    dgvAccounts.Columns["Member Name"].DefaultCellStyle.Format = null;
                    dgvAccounts.Columns[0].DefaultCellStyle.Format = null;
                    dgvAccounts.ClearSelection();
                    conn.Close();
                }
                else
                {
                    dgvAccounts.DataSource = dt;
                    dgvAccounts.ClearSelection();
                }
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
            dgvAccounts.ClearSelection();
        }

        private int checkStatus()
        {
            if (rdOpen.Checked == true)
                return 1;
            else
                return 0;
        }

        private void rdOpen_Click(object sender, EventArgs e)
        {
            loadAccounts(cbxYear.Text);
        }

        private void rdClosed_Click(object sender, EventArgs e)
        {
            loadAccounts(cbxYear.Text);
        }
    }
}
