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
    public partial class ViewSavingsProfile : Form
    {
        public MainForm reftomain;
        public MySqlConnection conn;
        ViewSavings source;
        string anum, mname; Int32 stat;

        public ViewSavingsProfile(MainForm main, ViewSavings src, MySqlConnection con, string no, string name)
        {
            InitializeComponent();
            reftomain = main;
            conn = con;
            anum = no;
            mname = name;
            source = src;
        }

        private void ViewSavingsProfile_Load(object sender, EventArgs e)
        {
            lblName.Text = "Account No. " + anum + " - " + mname;
            getBalance();
            loadTransactions(0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            reftomain.Enabled = true;
            this.Close();
        }

        private void getBalance()
        {
            try
            {
                conn.Open();

                MySqlCommand comm2 = new MySqlCommand();
                comm2.Connection = conn;
                comm2.CommandType = CommandType.Text;
                comm2.CommandText = "SELECT amc.computeMonthEndBalance(@mn, @yr, @accountid) AS 'CurrBalance'";
                comm2.Parameters.AddWithValue("@mn", DateTime.Today.Month);
                comm2.Parameters.AddWithValue("@yr", DateTime.Today.Year);
                comm2.Parameters.AddWithValue("@accountid", Convert.ToInt32(anum));
                double bal;
                bal = Convert.ToDouble(comm2.ExecuteScalar());
                lblBalance.Text = bal.ToString("n2");
                comm2.CommandText = "SELECT account_status FROM savings WHERE savings_account_id = " + anum;
                stat = Convert.ToInt32(comm2.ExecuteScalar());
                DateTime d8;
                comm2.CommandText = "SELECT opening_date FROM savings WHERE savings_account_id = " + anum;
                d8 = Convert.ToDateTime(comm2.ExecuteScalar());
                lblDate.Text = "Opened on " + d8.ToString("MM/dd/yyyy");
                if(stat == 0)
                {
                    comm2.CommandText = "SELECT termination_date FROM savings WHERE savings_account_id = " + anum;
                    d8 = Convert.ToDateTime(comm2.ExecuteScalar());
                    lblClose.Visible = true;
                    lblClose.Text = "Closed since " + d8.ToString("MM/dd/yyyy");
                    lblX.Text = "Final Balance: ₱";
                    btnDeactivate.Visible = false;
                }
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
        }

        private void rdDeposit_Click(object sender, EventArgs e)
        {
            loadTransactions(1);
        }

        private void rdWithdrawal_Click(object sender, EventArgs e)
        {
            loadTransactions(2);
        }

        private void rdAll_Click(object sender, EventArgs e)
        {
            loadTransactions(0);
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to terminate this account? This cannot be undone.", "Terminate Account", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    string q = "UPDATE savings SET account_status = 0, termination_date = '" + DateTime.Today.ToString("yyyy-MM-dd") + "' WHERE savings_account_id = " + anum;
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
            string q = "SELECT date AS 'Date', total_amount * transaction_type AS 'Amount' FROM savings_transaction WHERE savings_account_id = " + anum;
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

