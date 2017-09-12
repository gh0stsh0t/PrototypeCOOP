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
        string accid;
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

        private void dgvMembers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void loadAccountS(string memid)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT savings_account_id, outstanding_balance FROM savings WHERE member_id = " + memid, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    accid = dt.Rows[0]["savings_account_id"].ToString();
                    lblAccount.Text = accid;
                    lblBalance.Text = dt.Rows[0]["outstanding_balance"].ToString();
                }

                conn.Close();

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
        }

        private void loadAccountC(string memid)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT capital_account_id, outstanding_balance FROM capitals WHERE member_id = " + memid, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    accid = dt.Rows[0]["capital_account_id"].ToString();
                    lblAccount.Text = accid;
                    lblBalance.Text = dt.Rows[0]["outstanding_balance"].ToString();
                }

                conn.Close();

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
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
                transtype = "0";
            } else if (rdWd.Checked == true)
            {
                transtype = "1";
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
                                                "VALUES('" + lblAccount.Text + "', '" + transtype + "', '" + dtpDate.Value.ToString("yyyy-MM-dd") + "','" + txtAmt.Text + "')"; // INTEREST RATE FROM GENERAL

                            
                        } else if (type =="capitals")
                        {
                            query = "INSERT INTO capitals_transaction (capital_account_id, transaction_type, date, total_amount)" +
                                                "VALUES('" + lblAccount.Text + "', '" + transtype + "', '" + dtpDate.Value.ToString("yyyy-MM-dd") + "','" + txtAmt.Text + "')"; // INTEREST RATE FROM GENERAL

                        }
                        MySqlCommand ins = new MySqlCommand(query, conn);
                        ins.ExecuteNonQuery();
                        MessageBox.Show("Success! Transaction recorded.");
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

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectMember sel = new SelectMember(this, type);
            this.Enabled = false;
            sel.Show();
        }

        private int balance()
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
            try
            {
                particulars.Clear();
                loadParticulars(particulars);
            }
            catch (DataException ee)
            {
                // Process exception and return.
                Console.WriteLine("Exception of type {0} occurred.",
                    e.GetType());
            }
        }
    }
}