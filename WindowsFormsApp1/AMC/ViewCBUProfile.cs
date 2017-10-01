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
    public partial class ViewCBUProfile : Form
    {

        public MainForm reftomain;
        public MySqlConnection conn;
        ViewCapitals source;
        string anum, mname; Int32 stat;

        private void ViewCBUProfile_Load(object sender, EventArgs e)
        {
            lblName.Text = "Account No. " + anum + " - " + mname;
            getBalance();
            loadTransactions(0);
        }

        public ViewCBUProfile(MainForm main, ViewCapitals src, MySqlConnection con, string no, string name)
        {
            InitializeComponent();
            reftomain = main;
            conn = con;
            anum = no;
            mname = name;
            source = src;
        }


        private void getBalance()
        {
            try
            {
                conn.Open();

                MySqlCommand comm2 = new MySqlCommand();
                comm2.Connection = conn;
                comm2.CommandType = CommandType.Text;
                comm2.CommandText = "SELECT amc.computeCapitalOutstandingBalance(@yr, @accountid) AS 'CurrBalance'";
                comm2.Parameters.AddWithValue("@yr", DateTime.Today.Year);
                comm2.Parameters.AddWithValue("@accountid", Convert.ToInt32(anum));
                double bal;
                bal = Convert.ToDouble(comm2.ExecuteScalar());
                lblBalance.Text = bal.ToString();
                comm2.CommandText = "SELECT account_status FROM capitals WHERE capital_account_id = " + anum;
                stat = Convert.ToInt32(comm2.ExecuteScalar());
                DateTime d8;
                comm2.CommandText = "SELECT opening_date FROM capitals WHERE capital_account_id = " + anum;
                d8 = Convert.ToDateTime(comm2.ExecuteScalar());
                lblDate.Text = "Opened on " + d8.ToString("MM/dd/yyyy");
                if (stat == 0)
                {
                    comm2.CommandText = "SELECT withdrawal_date FROM capitals WHERE capital_account_id = " + anum;
                    d8 = Convert.ToDateTime(comm2.ExecuteScalar());
                    lblClose.Visible = true;
                    lblClose.Text = "Withdrawn on " + d8.ToString("MM/dd/yyyy");
                    lblX.Text = "Final Balance: ₱";
                    btnDeactivate.Visible = false;
                }
                comm2.CommandText = "SELECT ics_no FROM capitals WHERE capital_account_id = " + anum;
                lblicsn.Text = comm2.ExecuteScalar().ToString();
                comm2.CommandText = "SELECT ics_amount FROM capitals WHERE capital_account_id = " + anum;
                lblics.Text = comm2.ExecuteScalar().ToString();
                comm2.CommandText = "SELECT ipuc_amount FROM capitals WHERE capital_account_id = " + anum;
                lblipuc.Text = comm2.ExecuteScalar().ToString();
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
        }

        private void rdAll_Click(object sender, EventArgs e)
        {
            loadTransactions(0);
        }

        private void rdDeposit_Click(object sender, EventArgs e)
        {
            loadTransactions(1);
        }

        private void rdWithdrawal_Click(object sender, EventArgs e)
        {
            loadTransactions(2);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            reftomain.Enabled = true;
            this.Close();
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to terminate this account? This cannot be undone.", "Terminate Account", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    string q = "UPDATE capitals SET account_status = 0, withdrawal_date = '" + DateTime.Today.ToString("yyyy-MM-dd") + "' WHERE capital_account_id = " + anum;
                    MySqlCommand comm = new MySqlCommand(q, conn);
                    comm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Account terminated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    conn.Close();
                }
                reftomain.Enabled = true;
                source.reopen();
                this.Close();
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void loadTransactions(int p)
        {
            string q = "SELECT date AS 'Date', total_amount * transaction_type AS 'Amount' FROM capitals_transaction WHERE capital_account_id = " + anum;
            if (p == 1)
                q += " AND transaction_type = 1 ";
            else if (p == 2)
                q += " AND transaction_type = -1 ";

            q += " ORDER BY date DESC";

            try
            {

                conn.Open();

                MySqlCommand comm = new MySqlCommand(q, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    dgvAccounts.DataSource = dt;
                    dgvAccounts.CurrentCell.Selected = false;
                    conn.Close();
                }
                else
                {
                    dgvAccounts.DataSource = dt;
                }
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
        }
    }
}
