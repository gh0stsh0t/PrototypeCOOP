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
    public partial class ViewTransactions : Form
    {
        public MySqlConnection conn;
        public MainForm reftomain;
        char type;
        DataGridViewCellStyle dateCellStyle = new DataGridViewCellStyle();
        

        public ViewTransactions(MainForm main, char typ)
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root;");
            reftomain = main;
            type = typ;
            dateCellStyle.Format = "d";
        }

        private void ViewTransactions_Load(object sender, EventArgs e)
        {
            cbxMonth.SelectedIndex = DateTime.Today.Month - 1;
            cbxYear.Text = "2017";
            loadCbxYears();
            loadCbxDays();
            cbxDay.SelectedText = DateTime.Today.Day.ToString();

            if (type == 's')
            {
                load_Savings(1, filterTT(), 0);
                lblTop.Text = "View Savings Transactions";
            }
            else
            {
                load_Capitals(1, filterTT(), 0);
                lblTop.Text = "View Capital Build-Up Transactions";
            }
        }

        private void load_Savings(int o, int p, int r)
        {
            string q = "SELECT st.savings_transaction_id, CAST(s.savings_account_id AS CHAR(5)) AS 'Account No.', " +
         "CONCAT(m.family_name, ', ', m.first_name, ' ', m.middle_name) AS 'Member Name', " +
         "st.date AS 'Date', st.total_amount * st.transaction_type AS 'Amount', st.encoded_by AS 'Encoded' " +
         "FROM savings s LEFT JOIN savings_transaction st ON s.savings_account_id = st.savings_account_id " +
         "INNER JOIN members m ON s.member_id = m.member_id WHERE m.status = 1 AND s.account_status = 1 AND ";

            if (o == 1)
            {
                q = q + "st.date = '" + DateTime.Today.ToString("yyyy-MM-dd") + "'";
            }
            else if (o == 2)
            {
                q += "YEAR(st.date) = " + cbxYear.Text + " AND MONTH(st.date) = " + (cbxMonth.SelectedIndex + 1).ToString() + " AND DAY(st.date) = " + cbxDay.Text;
            }
            else if (o == 3)
            {
                q += "YEAR(st.date) = " + cbxYear.Text + " AND MONTH(st.date) = " + (cbxMonth.SelectedIndex + 1).ToString();
            }
            else if (o == 4)
            {
                q += "YEAR(st.date) = " + cbxYear.Text;
            }

            if (p == 1)
                q += " AND st.transaction_type = 1";
            else if (p == 2)
                q += " AND st.transaction_type = -1";

            string s = tbSearch.Text;
            if (r > 0)
                q += " AND (s.savings_account_id LIKE '%" + s + "%' OR m.first_name LIKE '%" + s + "%' OR m.family_name LIKE '%" + s + "%')";


            q += " ORDER BY st.date DESC";

            // MessageBox.Show(q);

            loadTransactions(q);
        }

        private void load_Capitals(int o, int p, int r)
        {
            string q = "SELECT ct.capital_transaction_id, CAST(c.capital_account_id AS CHAR(5)) AS 'Account No.', " +
         "CONCAT(m.family_name, ', ', m.first_name, ' ', m.middle_name) AS 'Member Name', " +
         "ct.date AS 'Date', ct.total_amount * ct.transaction_type AS 'Amount', ct.encoded_by AS 'Encoded' " +
         "FROM capitals c LEFT JOIN capitals_transaction ct ON c.capital_account_id = ct.capital_account_id " +
         "INNER JOIN members m ON c.member_id = m.member_id WHERE m.status = 1 AND c.account_status = 1 AND ";

            if (o == 1)
            {
                q = q + "ct.date = '" + DateTime.Today.ToString("yyyy-MM-dd") + "'";         
            } else if (o == 2)
            {
                q += "YEAR(ct.date) = " + cbxYear.Text + " AND MONTH(ct.date) = " + (cbxMonth.SelectedIndex + 1).ToString() + " AND DAY(ct.date) = " + cbxDay.Text;
            } else if(o == 3)
            {
                q += "YEAR(ct.date) = " + cbxYear.Text + " AND MONTH(ct.date) = " + (cbxMonth.SelectedIndex + 1).ToString();
            } else if(o == 4)
            {
                q += "YEAR(ct.date) = " + cbxYear.Text;
            }

            if(p == 1)
                q += " AND ct.transaction_type = 1";
            else if(p == 2)
                q += " AND ct.transaction_type = -1";

            string s = tbSearch.Text;
            if (r > 0)
                q += " AND (c.capital_account_id LIKE '%" + s + "%' OR m.first_name LIKE '%" + s + "%' OR m.family_name LIKE '%" + s + "%')";


            q += " ORDER BY ct.date DESC";

            // MessageBox.Show(q);

            loadTransactions(q);
        }

        private void loadTransactions(string query)
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
                    if(type=='c')
                    {
                        dgvAccounts.Columns["capital_transaction_id"].Visible = false;
                    } else
                    {
                        dgvAccounts.Columns["savings_transaction_id"].Visible = false;
                    }
                    dgvAccounts.Columns["Encoded"].Visible = false;
                    dgvAccounts.Columns["Date"].DefaultCellStyle = dateCellStyle;
                    // dgvAccounts.CurrentCell.Selected = false;
                    dgvAccounts.ClearSelection();
                    conn.Close();
                }
                else
                {
                    dgvAccounts.DataSource = dt;
                    if (type == 'c')
                    {
                        dgvAccounts.Columns["capital_transaction_id"].Visible = false;
                    }
                    else
                    {
                        dgvAccounts.Columns["savings_transaction_id"].Visible = false;
                    }
                    dgvAccounts.Columns["Encoded"].Visible = false;
                    dgvAccounts.ClearSelection();
                }
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
        }

        private int filterTT()
        {
            if (rdDeposit.Checked == true)
                return 1;
            else if (rdWithdrawal.Checked == true)
                return 2;
            else
                return 0;            
        }

        private void rdDay_Click(object sender, EventArgs e)
        {
            cbxMonth.Enabled = true;
            cbxDay.Enabled = true;
            cbxYear.Enabled = true;
            loadCbxDays();
            cbxDay.SelectedIndex = 0;
        }

        private void rdMonth_Click(object sender, EventArgs e)
        {
            cbxMonth.Enabled = true;
            cbxDay.Enabled = false;
            cbxYear.Enabled = true;
            cbxMonth.SelectedIndex = DateTime.Today.Month - 1;
            cbxYear.SelectedIndex = 0;
        }

        private void loadCbxDays()
        {
            cbxDay.Items.Clear();
            int yr;
            yr = Convert.ToInt32(cbxYear.Text);
            int max;
            max = DateTime.DaysInMonth(yr, (cbxMonth.SelectedIndex + 1));
            for (int i = 1; i <= max; i++)
                cbxDay.Items.Add(i.ToString());
            /* cbxDay.SelectedIndex = 0; */
        }

        private void cbxMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* if (rdDay.Checked == true)
                loadCbxDays(); */
        }

        private void cbxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* if (rdDay.Checked == true)
                loadCbxDays(); */
        }

        private void rdYear_Click(object sender, EventArgs e)
        {
            cbxDay.Enabled = false;
            cbxMonth.Enabled = false;
        }

        private void loadCbxYears()
        {
            string query; Int32 min = DateTime.Today.Year;
            cbxYear.Items.Clear();
            if (type == 'c')
                query = "SELECT YEAR(date) AS 'Year' FROM capitals_transaction ORDER BY date ASC LIMIT 1";
            else
                query = "SELECT YEAR(date) AS 'Year'  FROM savings_transaction ORDER BY date ASC LIMIT 1";

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

        private void cbxMonth_Leave(object sender, EventArgs e)
        {
            if (rdDay.Checked == true && cbxDay.Enabled == true)
                loadCbxDays();
            cbxDay.SelectedIndex = 0;
        }

        private void cbxYear_Leave(object sender, EventArgs e)
        {
            if (rdDay.Checked == true && cbxDay.Enabled == true)
                loadCbxDays();
            cbxDay.SelectedIndex = 0;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if(type == 'c')
            {
                if (rdDay.Checked == true)
                    load_Capitals(2, filterTT(), 0);
                else if (rdMonth.Checked == true)
                    load_Capitals(3, filterTT(), 0);
                else
                    load_Capitals(4, filterTT(), 0);
            } else
            {
                if (rdDay.Checked == true)
                    load_Savings(2, filterTT(), 0);
                else if (rdMonth.Checked == true)
                    load_Savings(3, filterTT(), 0);
                else
                    load_Savings(4, filterTT(), 0);
            }
            
        }

        private void rdAll_Click(object sender, EventArgs e)
        {
            if (type == 'c')
            {
                if (rdDay.Checked == true)
                    load_Capitals(2, 0, 0);
                else if (rdMonth.Checked == true)
                    load_Capitals(3, 0, 0);
                else
                    load_Capitals(4, 0, 0);
            } else
            {
                if (rdDay.Checked == true)
                    load_Savings(2, 0, 0);
                else if (rdMonth.Checked == true)
                    load_Savings(3, 0, 0);
                else
                    load_Savings(4, 0, 0);
            }
        }

        private void rdDeposit_Click(object sender, EventArgs e)
        {
            if (type == 'c')
            {
                if (rdDay.Checked == true)
                    load_Capitals(2, 1, 0);
                else if (rdMonth.Checked == true)
                    load_Capitals(3, 1, 0);
                else
                    load_Capitals(4, 1, 0);
            } else
            {
                if (rdDay.Checked == true)
                    load_Savings(2, 1, 0);
                else if (rdMonth.Checked == true)
                    load_Savings(3, 1, 0);
                else
                    load_Savings(4, 1, 0);
            }
        }

        private void rdWithdrawal_Click(object sender, EventArgs e)
        {
            if (type == 'c')
            {
                if (rdDay.Checked == true)
                    load_Capitals(2, 2, 0);
                else if (rdMonth.Checked == true)
                    load_Capitals(3, 2, 0);
                else
                    load_Capitals(4, 2, 0);
            } else
            {
                if (rdDay.Checked == true)
                    load_Savings(2, 2, 0);
                else if (rdMonth.Checked == true)
                    load_Savings(3, 2, 0);
                else
                    load_Savings(4, 2, 0);
            }
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (type == 'c')
            {
                if (rdDay.Checked == true)
                    load_Capitals(2, filterTT(), 1);
                else if (rdMonth.Checked == true)
                    load_Capitals(3, filterTT(), 1);
                else
                    load_Capitals(4, filterTT(), 1);
            } else
            {
                if (rdDay.Checked == true)
                    load_Savings(2, filterTT(), 1);
                else if (rdMonth.Checked == true)
                    load_Savings(3, filterTT(), 1);
                else
                    load_Savings(4, filterTT(), 1);
            }
        }
    }
}
