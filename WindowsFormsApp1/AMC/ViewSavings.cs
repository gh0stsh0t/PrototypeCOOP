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
    public partial class ViewSavings : Form
    {
        public MySqlConnection conn;
        public MainForm reftomain;

        public ViewSavings(MainForm main)
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root;");
            reftomain = main;
        }

        private void ViewSavings_Load(object sender, EventArgs e)
        {
            cbxMonth.SelectedIndex = Convert.ToInt32(DateTime.Now.Month) - 1;
            cbxYear.SelectedText = DateTime.Now.Year.ToString();

            string q = createQuery(cbxMonth.SelectedIndex + 1, cbxYear.Text);
            loadAccounts(q);
        }

        private void loadAccounts(string query)
        {
            try
            {

                conn.Open();

                MySqlCommand comm = new MySqlCommand(query, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    dgvAccounts.DataSource = dt;
                    dgvAccounts.CurrentCell.Selected = false;
                    dgvAccounts.Columns["member_id"].Visible = false;
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
            dgvAccounts.ClearSelection();
        }

        private string createQuery(int mn, string yr)
        {
            string m = mn.ToString();
            if (rdMonth.Checked == true)
            {
                if(mn % 3 == 0)
                {
                    string q = "SELECT m.member_id, s.savings_account_id AS 'Account Number', " +
                        "CONCAT(m.family_name, ', ', m.first_name, ' ', m.middle_name) AS Name, " +
                        "amc.getMonthBeginningBalance(" + m + "," + yr + ", st.savings_account_id) as 'Beginning Balance', " + 
                        "amc.computeMonthOutstandingBalance(" + m + "," + yr + ",st.savings_account_id) as 'Outstanding Balance', " +
                        "amc.computeMonthInterest(" + m + "," + yr + ",st.savings_account_id) AS 'Computed Interest', " +
                        "amc.computeMonthInterestExpense(" + m + "," + yr + ",st.savings_account_id) AS 'Interest Expense for the Month', " +
                        "amc.computeQuarterInterest(" + m + "," + yr + ",st.savings_account_id) AS 'Interest Credited for the Quarter', " +
                        "amc.computeMonthEndBalance(" + m + "," + yr + ",st.savings_account_id) AS 'Month End Balance', " +
                        "amc.computeMonthAvgDailyBalance(" + m + "," + yr + ",st.savings_account_id) AS 'Average Daily Balance', " +
                        "amc.computeMonthBalanceDifference(" + m + "," + yr + ",st.savings_account_id) AS 'Increase (Decrease) for the Month' " +
                        "FROM savings s LEFT JOIN savings_transaction st ON s.savings_account_id = st.savings_account_id " +
                        "INNER JOIN members m ON s.member_id = m.member_id WHERE m.status = 1 GROUP BY s.savings_account_id";
                    // MessageBox.Show(q);
                    return q;
                }
                else
                {
                    return "SELECT m.member_id, s.savings_account_id AS 'Account Number', " + 
                        "CONCAT(m.family_name, ', ', m.first_name, ' ', m.middle_name) AS Name, " + 
                        "amc.getMonthBeginningBalance(" + mn.ToString() + "," + yr + ",st.savings_account_id) as 'Beginning Balance', " +
                        "amc.computeMonthOutstandingBalance(" + mn.ToString() + "," + yr + ",st.savings_account_id) as 'Outstanding Balance', " +
                        "amc.computeMonthInterest(" + mn.ToString() + "," + yr + ",st.savings_account_id) AS 'Computed Interest', " +
                        "amc.computeMonthInterestExpense(" + mn.ToString() + "," + yr + ",st.savings_account_id) AS 'Interest Expense for the Month', " +
                        "amc.computeMonthEndBalance(" + mn.ToString() + "," + yr + ",st.savings_account_id) AS 'Month End Balance', " +
                        "amc.computeMonthAvgDailyBalance(" + mn.ToString() + "," + yr + ",st.savings_account_id) AS 'Average Daily Balance', " +
                        "amc.computeMonthBalanceDifference(" + mn.ToString() + "," + yr + ",st.savings_account_id) AS 'Increase (Decrease) for the Month' " + 
                        "FROM savings s LEFT JOIN savings_transaction st ON s.savings_account_id = st.savings_account_id " + 
                        "INNER JOIN members m ON s.member_id = m.member_id WHERE m.status = 1 GROUP BY s.savings_account_id";
                }
            }
            else
            {
                return "";
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string q = createQuery(cbxMonth.SelectedIndex + 1, cbxYear.Text);
            loadAccounts(q);
        }
    }
}
