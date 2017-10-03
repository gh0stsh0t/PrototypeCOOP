using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace AMC
{
    public partial class AddTransaction : Form
    {
        public string type;
        public string memname; public int memid;
        MySqlConnection conn = new MySqlConnection();
        string accid; Int32 newID = 0;
        public DataTable particulars = new DataTable();

        public List<int> code = new List<int>();
        public List<double> amount = new List<double>();
        public List<double> new_total = new List<double>();
        public List<int> debcred_type = new List<int>();
        int index;

        public AddTransaction(MainForm main, string t)
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root;");
            type = t;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void AddTransaction_Load(object sender, EventArgs e)
        {
            dtpDate.MaxDate = DateTime.Today;
            if (type=="savings")
            {
                lblTop.Text = "Add Savings Transaction";
            } else if (type == "capitals")
            {
                lblTop.Text = "Add Capitals Transaction";
            }
            loadMembers(type);

            dtpDate.Value = DateTime.Today;
            particulars.Columns.Add("Code", typeof(string));
            particulars.Columns.Add("Account Title", typeof(string));
            particulars.Columns.Add("Debit", typeof(double));
            particulars.Columns.Add("Credit", typeof(double));
            particulars.Columns.Add("NewAmt", typeof(double));

            loadParticulars(particulars);
            
        }

        private void loadMembers(string t)
        {
            
        }

        private void insertTLines()
        {
            string q1 = "", q2 = "";
            string columns = "", id = "", acode = "", typp = "", newtot = "", chartcol = "", cid = "";
            

            for(int i = 0; i<code.Count(); i++)
            {
                if (type == "savings")
                {
                    q1 = "INSERT INTO savings_transaction_line (savings_transaction_id, account_log_id) VALUES ";
                }
                else if (type == "capitals")
                {
                    q1 = "INSERT INTO capitals_transaction_line (capital_transaction_id, account_log_id) VALUES ";
                }
                q2 = "INSERT INTO chart_of_accounts_log (code, amount, date, type) VALUES ";


                id = newID.ToString();
                acode = code.ElementAt(i).ToString(); 
                typp = debcred_type.ElementAt(i).ToString();
                newtot = new_total.ElementAt(i).ToString();
                
                chartcol = "('" + acode + "','" + newtot + "','" + DateTime.Today.ToString("yyyy-MM-dd") + "','" + typp + "');";
                q2 = q2 + chartcol + "SELECT LAST_INSERT_ID();" ;
                try
                {
                    conn.Open();
                    MySqlCommand ins = new MySqlCommand(q2, conn);
                    cid = ins.ExecuteScalar().ToString();
                    columns = "('" + id + "','" + cid + "')";
                    q1 = q1 + columns;
                    MySqlCommand ins2 = new MySqlCommand(q1, conn);
                    ins2.ExecuteNonQuery();
                    // MessageBox.Show("Success! Transaction recorded.");
                    conn.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    conn.Close();
                }

            }



        }

        public static Boolean isNum(string strToCheck)
        {
            Regex rg = new Regex(@"^[0-9]+\.?[0-9]{0,2}$");
            return rg.IsMatch(strToCheck);
        }

        private void dgvMembers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnDelete.Enabled = true;
                index = e.RowIndex;
            }
            catch (Exception ee)
            {
                MessageBox.Show("Please select a row.");
            }
            
        }


        public void enableButtons()
        {
            dtpDate.Enabled = true;
            panelRadio.Enabled = true;
            txtAmt.Enabled = true;
            btnAdd.Enabled = true;
            btnClear.Enabled = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string query ="";
            string transtype = "";
            if(rdDep.Checked == true)
            {
                transtype = "1";
            } else if (rdWd.Checked == true)
            {
                transtype = "-1";
            }
            if (txtAmt.Text == "")
            {
                MessageBox.Show("Please fill in all fields.");
            } else if(!isNum(txtAmt.Text))
            {
                MessageBox.Show("Please enter a valid amount.");
            }
            else if(rdWd.Checked == true && Convert.ToDouble(lblBalance.Text) < Convert.ToDouble(txtAmt.Text))
            {
                MessageBox.Show("Account balance is insufficient for this transaction.");
            }
            else if (particulars.Rows.Count == 0)
            {
                MessageBox.Show("Please add particulars.");
            }
                else {
                int bal = balance();
                if(bal == 0)
                    MessageBox.Show("Double entries are not balanced.");
                else if (bal == 1)
                {
                    MessageBox.Show("Amount entered is not equal to the Particulars total.");
                }
                    
                else
                {
                    try
                    {
                        conn.Open();
                        if (type == "savings")
                        {
                            query = "INSERT INTO savings_transaction (savings_account_id, transaction_type, date, total_amount, encoded_by)" +
                                                "VALUES('" + lblAccount.Text + "', '" + transtype + "', '" + dtpDate.Value.ToString("yyyy-MM-dd") + "','" + txtAmt.Text + "','" + User.Name.id + "'); "
                                                + "SELECT LAST_INSERT_ID()";

                            
                        } else if (type =="capitals")
                        {
                            query = "INSERT INTO capitals_transaction (capital_account_id, transaction_type, date, total_amount, encoded_by)" +
                                                "VALUES('" + lblAccount.Text + "', '" + transtype + "', '" + dtpDate.Value.ToString("yyyy-MM-dd") + "','" + txtAmt.Text + "','" + User.Name.id + "'); "  
                                                + "SELECT LAST_INSERT_ID()" ;
                        }
                        MySqlCommand ins = new MySqlCommand(query, conn);
                        newID = Convert.ToInt32(ins.ExecuteScalar());
                        conn.Close();
                        insertTLines();
                        MessageBox.Show("Success! Transaction recorded.");
                        

                        refreshEverything(1);
                        
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.ToString());
                        conn.Close();
                    }
                }
            }
            

            
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectMember sel = new SelectMember(this, type);
            this.Enabled = false;
            sel.Show();
        }

        private int balance()
        {
            try
            {
                double credit = Convert.ToDouble(particulars.Compute("SUM(Credit)", string.Empty));
                double debit = Convert.ToDouble(particulars.Compute("SUM(Debit)", string.Empty));
                double transamt = double.Parse(txtAmt.Text);

                if (credit != debit)
                {
                    // MessageBox.Show("Double entries are not balanced.");
                    return 0;
                }
                else if (transamt != debit)
                {
                    // MessageBox.Show("Amount entered is not equal to the Particulars total.");
                    return 1;
                }
                else return 2;
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddParticular part = new AddParticular(this);
            this.Enabled = false;
            part.Show();
        }

        public void loadParticulars(DataTable tbl)
        {
            dgvParticulars.DataSource = tbl;
            dgvParticulars.Columns["NewAmt"].Visible = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            refreshEverything(0);
        }

        private void refreshEverything(int what)
        {
            try
            {
                particulars.Clear();
                loadParticulars(particulars);
                code.Clear();
                debcred_type.Clear();
                new_total.Clear();
                amount.Clear();
                if (what == 1)
                {
                    lblAccount.Text = "";
                    lblBalance.Text = "";
                    lblMember.Text = "";
                    txtAmt.Clear();
                    txtAmt.Enabled = false;
                    panelRadio.Enabled = false;
                    dtpDate.Value = DateTime.Today;
                    dtpDate.Enabled = false;
                }
            }
            catch (DataException ee)
            {
                // Process exception and return.
                Console.WriteLine("Exception of type {0} occurred.",
                    ee.GetType());
            }
        }

        private void dgvParticulars_Leave(object sender, EventArgs e)
        {
            // btnDelete.Enabled = false;
        }

        private void dgvParticulars_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // btnDelete.Enabled = true;
        }

        private void dgvParticulars_SelectionChanged(object sender, EventArgs e)
        {
            // btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string cd, amt, tot, dc = "";
            try
            {
                
                cd = dgvParticulars.Rows[index].Cells["Code"].Value.ToString();
                if (dgvParticulars.Rows[index].Cells["Debit"].Value.ToString() != "")
                {
                    amt = dgvParticulars.Rows[index].Cells["Debit"].Value.ToString();
                    dc = "0";
                }
                else
                {
                    amt = dgvParticulars.Rows[index].Cells["Credit"].Value.ToString();
                    dc = "1";
                }
                tot = dgvParticulars.Rows[index].Cells["NewAmt"].Value.ToString();

                code.Remove(int.Parse(cd));
                amount.Remove(Convert.ToDouble(amt));
                debcred_type.Remove(int.Parse(dc));
                new_total.Remove(Convert.ToDouble(tot));

                
                foreach (DataRow dr in particulars.Rows)
                {
                    if (dr[0].ToString() == cd)
                    {
                        dr.Delete();
                    }
                }
                loadParticulars(particulars);
            }
            catch (Exception ee)
            {
                // MessageBox.Show(ee.Message);
            }
        }
    }
}