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
    // YO IDEK

    public partial class SelectMember : Form
    {
        string memname, transtype; int id;
        public MySqlConnection conn;
        AddTransaction sourceForm;

        public SelectMember(AddTransaction refForm, string type)
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root;");
            sourceForm = refForm;
            transtype = type;
        }

        private void SelectMember_Load(object sender, EventArgs e)
        {
            refreshMembers();
        }

        private void refreshMembers()
        {
            string query1, query2;
            query1 = "SELECT member_id, CONCAT(family_name, ',', first_name, ' ', middle_name) AS Name FROM members WHERE status = 1";
            query2 = " AND member_id IN (SELECT member_id FROM " + transtype + " WHERE account_status = 1)";
            loadMembers(query1 + query2);

        }

        private void loadMembers(string query)
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
                    dgvMembers.DataSource = dt;
                    dgvMembers.CurrentCell.Selected = false;
                    dgvMembers.Columns["member_id"].Visible = false;
                    conn.Close();
                }
                else
                {
                    dgvMembers.DataSource = dt;
                    dgvMembers.Columns["member_id"].Visible = false;
                }
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
            dgvMembers.ClearSelection();
        }

        private void txtMem_KeyUp(object sender, KeyEventArgs e)
        {
            string query1, query2;
            query1 = "SELECT member_id, CONCAT(family_name, ', ' , first_name, ' ', middle_name) AS Name FROM members WHERE status = 1";
            query2 = " AND family_name LIKE \"%" + txtMem.Text + "%\" OR first_name LIKE \"%" + txtMem.Text + "%\" OR middle_name LIKE \"%" + txtMem.Text + "%\"";
            loadMembers(query1 + query2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sourceForm.Enabled = true;
            this.Close();
        }

        private void dgvMembers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                memname = dgvMembers.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                id = Int32.Parse(dgvMembers.Rows[e.RowIndex].Cells["member_id"].Value.ToString());
                lblSelected.Text = memname;
                lblSelected.Visible = true;
                sourceForm.memid = id;
                sourceForm.memname = memname;
            } catch (Exception ee)
            {

            }
            
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string q1 = "SELECT ";
            if (lblSelected.Visible == false)
            {
                MessageBox.Show("Please select a member.");
            }
            else
            {
                sourceForm.enableButtons();
                sourceForm.lblMember.Text = memname;
                if (transtype == "savings")
                {
                    q1 = q1 + "savings_account_id AS accountid FROM savings WHERE member_id = " + id.ToString();
                }
                else if (transtype == "capitals")
                {
                    q1 = q1 + "capital_account_id AS accountid FROM capitals WHERE member_id = " + id.ToString();
                }
                loadAcct(q1);
                sourceForm.btnSubmit.Enabled = true;
                sourceForm.Enabled = true;
                this.Close();
            }
        }

        private void txtMem_Enter(object sender, EventArgs e)
        {
            txtMem.Clear();
        }

        private void loadAcct(string query)
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
                    sourceForm.lblAccount.Text = dt.Rows[0]["accountid"].ToString();
                    // sourceForm.lblBalance.Text = dt.Rows[0]["outstanding_balance"].ToString();
                }

                MySqlCommand comm2 = new MySqlCommand();
                comm2.Connection = conn;
                comm2.CommandType = CommandType.Text;
                if (transtype == "savings")
                {
                    comm2.CommandText = "SELECT amc.computeMonthEndBalance(@mn, @yr, @accountid) AS 'CurrBalance'";
                    comm2.Parameters.AddWithValue("@mn", DateTime.Today.Month);
                }
                else
                {
                    comm2.CommandText = "SELECT amc.computeCapitalOutstandingBalance(@yr, @accountid) AS 'CurrBalance'";
                }                    
                comm2.Parameters.AddWithValue("@yr", DateTime.Today.Year);
                comm2.Parameters.AddWithValue("@accountid", Convert.ToInt32(dt.Rows[0]["accountid"]));
                double bal;
                bal = Convert.ToDouble(comm2.ExecuteScalar());
                sourceForm.lblBalance.Text = bal.ToString("n2");
                if(transtype != "savings")
                {
                    comm2.CommandText = "SELECT ics_amount FROM capitals WHERE member_id = " + id.ToString();
                    double ics;
                    ics = Convert.ToDouble(comm2.ExecuteScalar());
                    sourceForm.lblPr.Text = "(" + (bal / ics * 100).ToString("n2") + "% of Initial Capital Shares)";
                }
                /* MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt); */
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
        }
    } }
