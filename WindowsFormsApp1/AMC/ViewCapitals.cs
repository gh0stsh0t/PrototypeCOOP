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
            loadCbxYears();
            loadAccounts(cbxYear.Text, "%");
            loadValues(cbxYear.Text);
        }

        private void loadAccounts(string yr, string like)
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
                comm.Parameters.AddWithValue("@likephrase", like);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    // ADD TOTALS
                     DataRow dr = dt.NewRow();
                    dr[1] = "";
                    dr[2] = "TOTAL";
                    dr[3] = Convert.ToDouble(dt.Compute("Sum([Beginning Balance for the Year])", string.Empty));
                    dr[4] = Convert.ToDouble(dt.Compute("Sum([Outstanding Balance])", string.Empty));
                    dr[5] = Convert.ToDouble(dt.Compute("Sum([Average Monthly Balance])", string.Empty));
                    dr[6] = Convert.ToDouble(dt.Compute("Sum([Interest on Share Capital or Dividend])", string.Empty));
                    dr[7] = Convert.ToDouble(dt.Compute("Sum([Increase / Decrease in SC for the Year])", string.Empty));
                    dr[8] = Convert.ToDouble(dt.Compute("Sum([Targeted Increase for the Year])", string.Empty));
                    dr[9] = Convert.ToDouble(dt.Compute("Sum([% Accomplished])", string.Empty));
                    dr[10] = Convert.ToDouble(dt.Compute("Sum([Net Book Value of Share Capital])", string.Empty));
                    dt.Rows.Add(dr);

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
            loadAccounts(cbxYear.Text, "%");
        }

        private void rdClosed_Click(object sender, EventArgs e)
        {
            loadAccounts(cbxYear.Text, "%");
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            loadAccounts(cbxYear.Text, ("%" + tbSearch.Text + "%"));
        }

        private void loadValues(string yr)
        { /*
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand(query, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    sourceForm.lblAccount.Text = dt.Rows[0]["accountid"].ToString();
                    // sourceForm.lblBalance.Text = dt.Rows[0]["outstanding_balance"].ToString();
                } */

            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand();
                comm.Connection = conn;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "obtainHeaderValues";
                comm.Parameters.AddWithValue("@yr", yr);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    L1.Text = dt.Rows[0]["PUC1"].ToString();
                    R1.Text = dt.Rows[0]["PUC2"].ToString();
                    R2.Text = dt.Rows[0]["PUC1"].ToString();
                    D1.Text = dt.Rows[0]["DIFF1"].ToString();
                    R3.Text = dt.Rows[0]["NET"].ToString();
                    L4.Text = dt.Rows[0]["RF1"].ToString();
                    R4.Text = dt.Rows[0]["RF2"].ToString();
                    D2.Text = dt.Rows[0]["DIFF2"].ToString();
                    conn.Close();
                }
                else
                {
                    
                }
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }


        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadAccounts(cbxYear.Text, "%");
            loadValues(cbxYear.Text);
        }

        private void loadCbxYears()
        {
            string query; Int32 min = DateTime.Today.Year;
            cbxYear.Items.Clear();
            query = "SELECT YEAR(date) AS 'Year' FROM capitals_transaction ORDER BY date ASC LIMIT 1";

            try
            {

                conn.Open();

                MySqlCommand comm = new MySqlCommand(query, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    min = Convert.ToInt32(dt.Rows[0]["Year"]);
                }
                else
                { }
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }

            for (int i = DateTime.Today.Year; i >= min; i--)
                cbxYear.Items.Add(i.ToString());
            cbxYear.SelectedIndex = 0;
        }

        public void reopen()
        {
            cbxYear.SelectedText = DateTime.Now.Year.ToString();
            loadCbxYears();
            loadAccounts(cbxYear.Text, "%");
            loadValues(cbxYear.Text);
        }

        private void dgvAccounts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string mid, mname;
            mid = dgvAccounts.Rows[e.RowIndex].Cells["Acc. No."].Value.ToString();
            mname = dgvAccounts.Rows[e.RowIndex].Cells["Member Name"].Value.ToString();
            try
            {
                reftomain.Enabled = false;

                ViewCBUProfile cbu = new ViewCBUProfile(reftomain, this, conn, mid, mname);
                cbu.Show();
            }
            catch (Exception ee)
            {

            }
        }
    }
}
