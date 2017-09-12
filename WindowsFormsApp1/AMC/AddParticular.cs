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
    public partial class AddParticular : Form
    {
        AddTransaction sourceForm;
        string acctitle; int code, type; double amount, maxamt; int acctype;
        public MySqlConnection conn;

        public AddParticular(AddTransaction source)
        {
            InitializeComponent();
            sourceForm = source;
            conn = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root;");
        }

        private void AddParticular_Load(object sender, EventArgs e)
        {
            refreshAccounts();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sourceForm.Enabled = true;
            this.Close();
        }

        private void refreshAccounts()
        {
            string query1;
            query1 = "SELECT code AS Code, title AS 'Account Title', type FROM chart_of_accounts";
            loadAccounts(query1);
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
            acctitle = dgvAccounts.Rows[e.RowIndex].Cells["Account Title"].Value.ToString();
            code = Int32.Parse(dgvAccounts.Rows[e.RowIndex].Cells["Code"].Value.ToString());
            acctype = Int32.Parse(dgvAccounts.Rows[e.RowIndex].Cells["type"].Value.ToString());
            lblSelected.Text = code.ToString() + " - " + acctitle;
            lblSelected.Visible = true;
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            txtSearch.Clear();
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            
            if (!fieldValidation())
                MessageBox.Show("Fill in all fields.");
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
                    reflectToDgv();
                    passToLists();
                    sourceForm.Enabled = true;
                    this.Close();
                }
            }
        }

        private int getType()
        {
            if (rdCredit.Checked == true)
                return 1;
            else return 0;
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

        private void reflectToDgv()
        {
            DataRow dr = sourceForm.particulars.NewRow();
            dr[0] = code;
            dr[1] = acctitle;
            if (type == 0)
                dr[2] = amount;
            if (type == 1)
                dr[3] = amount;
            sourceForm.particulars.Rows.Add(dr);
            sourceForm.loadParticulars(sourceForm.particulars);
        }

        private void passToLists()
        {
            sourceForm.code.Add(code);
            sourceForm.amount.Add(amount);
            sourceForm.debcred_type.Add(type);
        }

        private Boolean toIncrease()
        {
            if (acctype <= 1)
            {
                if (rdDebit.Checked)
                    return true;
                else return false;
            } else
            {
                if (rdCredit.Checked)
                    return true;
                else return false;
            }
            
        }

        private Boolean amountValid()
        {
            string query = "SELECT total_amount AS amount FROM chart_of_accounts_log WHERE code = '" + code.ToString() +
                            "' AND timestamp = (SELECT MAX(timestamp) FROM chart_of_accounts_log WHERE code = '" + code.ToString() + "')";
            try { 
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
               } else
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
