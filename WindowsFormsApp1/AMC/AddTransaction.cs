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
            if(type=="savings")
            {
                lblTop.Text = "Add Savings Transaction";
            } else if (type == "capitals")
            {
                lblTop.Text = "Add Capitals Transaction";
            }
            loadMembers(type);

            dtpDate.Value = DateTime.Now;
            particulars.Columns.Add("Code", typeof(string));
            particulars.Columns.Add("Account Title", typeof(string));
            particulars.Columns.Add("Debit", typeof(double));
            particulars.Columns.Add("Credit", typeof(double));

            loadParticulars(particulars);
        }

        private void loadMembers(string t)
        {
            
        }

        private void insertTLines()
        {
            string q1 = "";
            if(type=="savings")
            {
                q1 = "INSERT INTO savings_transaction_line (savings_transaction_id, account_code, amount, type) VALUES ";
            } else if (type == "capitals")
            {
                q1 = "INSERT INTO capitals_transaction_line (capital_transaction_id, account_code, amount, type) VALUES ";
            }
            string q2 = "INSERT INTO chart_of_accounts_log (code, total_amount) VALUES ";
            string columns = "", id = "", acode = "", amtt = "", typp = "", newtot = "", chartcol = "";
            for(int i = 0; i<code.Count(); i++)
            {
                columns = "(";
                id = newID.ToString();
                acode = code.ElementAt(i).ToString(); 
                amtt = amount.ElementAt(i).ToString();
                typp = debcred_type.ElementAt(i).ToString();
                newtot = new_total.ElementAt(i).ToString();
                columns = "('" + id + "','" + acode + "','" + amtt + "','" + typp + "')";
                chartcol = "('" + acode + "','" + newtot + "')";
                if (i != code.Count() - 1)
                {
                    columns = columns + ",";
                    chartcol = chartcol + ",";
                }
                q1 = q1 + columns;
                q2 = q2 + chartcol;
            }

            try
            {
                MySqlCommand ins2 = new MySqlCommand(q1 + ";" + q2, conn);
                ins2.ExecuteNonQuery();
                // MessageBox.Show("Success! Transaction recorded.");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                conn.Close();
            }

        }

        private void dgvMembers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
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
            } if (particulars.Rows.Count == 0)
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
                            query = "INSERT INTO savings_transaction (savings_account_id, transaction_type, date, total_amount)" +
                                                "VALUES('" + lblAccount.Text + "', '" + transtype + "', '" + dtpDate.Value.ToString("yyyy-MM-dd") + "','" + txtAmt.Text + "'); "
                                                + "SELECT LAST_INSERT_ID()";

                            
                        } else if (type =="capitals")
                        {
                            query = "INSERT INTO capitals_transaction (capital_account_id, transaction_type, date, total_amount)" +
                                                "VALUES('" + lblAccount.Text + "', '" + transtype + "', '" + dtpDate.Value.ToString("yyyy-MM-dd") + "','" + txtAmt.Text + "')"  // INTEREST RATE FROM GENERAL
                                                + "SELECT LAST_INSERT_ID()" ;
                        }
                        MySqlCommand ins = new MySqlCommand(query, conn);
                        newID = Convert.ToInt32(ins.ExecuteScalar());
                        insertTLines();
                        MessageBox.Show("Success! Transaction recorded.");
                        conn.Close();

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
    }
}