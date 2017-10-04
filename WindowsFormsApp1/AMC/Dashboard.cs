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
    public partial class Dashboard : Form
    {
        public MainForm reftomain;
        MySqlConnection conn;

        public Dashboard(MainForm main)
        {
            InitializeComponent();
            reftomain = main;
            conn = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root;");
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            loadBals();
            loadCoA();

            var db = new DatabaseConn();
            DataTable a = db.Select("members", "COUNT(member_id)").Where("status", "1").GetQueryData();
            label15.Text = a.Rows[0][0].ToString();
            a = db.Select("loans", "SUM(orig_amount) as tanan", "SUM(Case when date_terminated is null then 1 else 0 end) as notp", "SUM(Case when date_terminated is not null then 1 else 0 end) as vryp", "SUM(outstanding_balance) as outt").GetQueryData();
            label16.Text = a.Rows[0]["tanan"].ToString();
            label18.Text = a.Rows[0]["notp"].ToString();
            label19.Text = a.Rows[0]["vryp"].ToString();
            label20.Text = a.Rows[0]["outt"].ToString();

        }

        private void loadCoA()
        {
            string q = "SELECT c.code AS Code, c.title AS 'Account Title', SUM(cl.amount) AS 'Balance' " +
                "FROM chart_of_accounts c LEFT JOIN chart_of_accounts_log cl ON c.code = cl.code " +
                "WHERE c.status = 1 GROUP BY cl.code ";
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
                ////MessageBox.Show(ee.ToString());
                conn.Close();
            }
        }

        private void loadBals()
        {
            try
            {
                conn.Open();

                MySqlCommand comm2 = new MySqlCommand();
                comm2.Connection = conn;
                comm2.CommandType = CommandType.Text;
                comm2.CommandText = "SELECT interest_rate FROM amc.interest_rate_log ORDER BY date DESC LIMIT 1";
                double bal, bal1, bal2, bal3, bal4, bal5;
                bal = Convert.ToDouble(comm2.ExecuteScalar()) * 100;
                lblir.Text = bal.ToString() + "%";
                comm2.CommandText = "SELECT amount FROM amc.avg_daily_balance_log ORDER BY date DESC LIMIT 1";
                bal1 = Convert.ToDouble(comm2.ExecuteScalar());
                lblB.Text = bal1.ToString("n2");
                comm2.CommandText = "SELECT amount FROM amc.capital_general_log WHERE fund_type = 0 ORDER BY date DESC LIMIT 1";
                bal2 = Convert.ToDouble(comm2.ExecuteScalar());
                lblC.Text = bal2.ToString("n2");
                comm2.CommandText = "SELECT amount FROM amc.capital_general_log WHERE fund_type = 1 ORDER BY date DESC LIMIT 1";
                bal3 = Convert.ToDouble(comm2.ExecuteScalar()) * 100;
                lblD.Text = bal3.ToString() + "%";
                comm2.CommandText = "SELECT amount FROM amc.capital_general_log WHERE fund_type = 2 ORDER BY date DESC LIMIT 1";
                bal4 = Convert.ToDouble(comm2.ExecuteScalar());
                lblE.Text = bal4.ToString("n2");
                comm2.CommandText = "SELECT amount FROM amc.capital_general_log WHERE fund_type = 3 ORDER BY date DESC LIMIT 1";
                bal5 = Convert.ToDouble(comm2.ExecuteScalar());
                lblF.Text = bal5.ToString("n2");
                conn.Close();
            }
            catch (Exception ee)
            {
                ////MessageBox.Show(ee.ToString());
                conn.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAmount ua = new UpdateAmount(conn, reftomain, lblir.Text, lblB.Text, lblC.Text, lblD.Text, lblE.Text, lblF.Text, this);
            ua.Show();
            reftomain.Enabled = false;
        }

        public void reloadbals()
        {
            loadBals();
        }

        private void btnAddAcc_Click(object sender, EventArgs e)
        {
            NewCOA nc = new NewCOA(reftomain, this, conn);
            nc.Show();
            reftomain.Enabled = false;
        }

        public void loadAccs()
        {
            loadCoA();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            UpdateCOA coa = new UpdateCOA(this, conn);
            coa.Show();
            this.Enabled = false;
        }
    }
}
