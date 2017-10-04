using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace AMC
{
    public partial class UpdateCOA : Form
    {

        Dashboard sourceForm;
        string acctitle; int code, type; double amount, maxamt; int acctype;
        public MySqlConnection conn;

        private void button2_Click(object sender, EventArgs e)
        {
            sourceForm.Enabled = true;
            this.Close();
        }

        public UpdateCOA(Dashboard source, MySqlConnection con)
        {
            InitializeComponent();
            sourceForm = source;
            conn = con;
        }

        private void UpdateCOA_Load(object sender, EventArgs e)
        {
            refreshAccounts();
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            string query1, query2;
            query1 = "SELECT code AS Code, title AS 'Account Title', type FROM chart_of_accounts";
            query2 = " WHERE code LIKE \"%" + txtSearch.Text + "%\" OR title LIKE \"%" + txtSearch.Text + "%\"";
            loadAccounts(query1 + query2);
        }

        private void dgvAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                acctitle = dgvAccounts.Rows[e.RowIndex].Cells["Account Title"].Value.ToString();
                code = Int32.Parse(dgvAccounts.Rows[e.RowIndex].Cells["Code"].Value.ToString());
                acctype = Int32.Parse(dgvAccounts.Rows[e.RowIndex].Cells["type"].Value.ToString());
                lblSelected.Text = code.ToString() + " - " + acctitle;
                lblSelected.Visible = true;
            }
            catch (Exception ee)
            {

            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            txtSearch.Clear();
        }

        public static Boolean isNum(string strToCheck)
        {
            Regex rg = new Regex(@"^[0-9]+\.?[0-9]{0,2}$");
            return rg.IsMatch(strToCheck);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!fieldValidation())
                MessageBox.Show("Fill in all fields.");
            else if (!isNum(txtAmt.Text))
            {
                MessageBox.Show("Please enter a valid amount.");
            }
            else
            {
                type = getType();
                amount = double.Parse(txtAmt.Text);
                if (!amountValid())
                {
                    MessageBox.Show("You do not have sufficient funds on the selected account for this transaction.");
                    txtAmt.Clear();
                }
                
                else
                {
                    string q2 = "INSERT INTO chart_of_accounts_log (code, amount, date, type) VALUES " +
                        "('" + code + "','" + solveNewAmount().ToString() + "','" + DateTime.Today.ToString("yyyy-MM-dd") + "','" + acctype + "');";

                    try
                    {
                        conn.Open();                      
                        MySqlCommand ins2 = new MySqlCommand(q2, conn);
                        ins2.ExecuteNonQuery();
                        MessageBox.Show("Success! Transaction recorded.");
                        conn.Close();
                        sourceForm.Enabled = true;
                        sourceForm.loadAccs();
                        this.Close();
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.Message);
                        conn.Close();
                    }
                }
            }
            
        }

        private int getType()
        {
            if (rdCredit.Checked == true)
                return 1;
            else return 0;
        }

        private void refreshAccounts()
        {
            string query1;
            query1 = "SELECT code AS Code, title AS 'Account Title', type FROM chart_of_accounts";
            loadAccounts(query1);
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
                    dgvAccounts.Columns["type"].Visible = false;
                    conn.Close();
                }
                else
                {
                    dgvAccounts.DataSource = dt;
                    dgvAccounts.Columns["type"].Visible = false;
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

        private Boolean fieldValidation()
        {
            if (txtAmt.Text == "")
                return false;
            else if (lblSelected.Visible == false)
                return false;
            else
                return true;
        }

        private Boolean toIncrease()
        {
            if (acctype <= 1)
            {
                if (rdDebit.Checked)
                    return true;
                else return false;
            }
            else
            {
                if (rdCredit.Checked)
                    return true;
                else return false;
            }

        }

        private double solveNewAmount()
        {
            if (toIncrease())
                return amount;
            else
                return (amount * -1);
        }

        private Boolean amountValid()
        {
            string query = "SELECT SUM(amount) AS amount FROM chart_of_accounts_log WHERE code = '" + code.ToString() +
                             "' LIMIT 1";
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand(query, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    maxamt = double.Parse(dt.Rows[0]["amount"].ToString());
                }

                conn.Close();

                if (!toIncrease())
                {
                    if (amount > maxamt)
                        return false;
                }
                else
                {
                    return true;
                }
                return true;

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
                return false;
            }
        }

    }
}
