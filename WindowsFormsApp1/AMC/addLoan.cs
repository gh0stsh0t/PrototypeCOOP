using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AMC
{
    public partial class AddLoan : Form
    {
        public int memid;
        private DatabaseConn _addloanconn;
        public DataTable loanmems;
        public string memname;
        string today = DateTime.Today.ToString("yyyyMM");


        public AddLoan(int x)
        {
            InitializeComponent();
            memid = x;
            cbLoan.SelectedIndex = 0;
            cbRequest.SelectedIndex = 0;
            tbTerm.SelectedIndex = 0;
            _addloanconn = new DatabaseConn();
            this.ActiveControl = label3;
        }

        private void AddLoan_Load(object sender, EventArgs e)
        {
        }

        public void namerfrsh()
        {
            name.Text = memname;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cbBorrower_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void cbBorrower_Leave(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddLoan reftomain = this;
            Form popup = new addLoanM(reftomain);
            popup.Visible = true;
        }

        private void cbLoan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                try
                {
                    string[] taes = {"member_id", memid.ToString(), "loan_type", cbLoan.SelectedIndex.ToString(), "request_type"
                    , cbRequest.SelectedIndex.ToString(), "orig_amount", tbAmount.Text,};
                    _addloanconn.Insert("loans", taes);
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString());
                }
            } 
        }

        private bool validation()
        {
            if (memid == -1)
            {
                MessageBox.Show("Please select a member.");
                return false;
            }
            else if (tbAmount.Text == "")
            {
                MessageBox.Show("Please input an amount.");
                return false;
            }
            else if (tbInterest.Text == "")
            {
                MessageBox.Show("Please input an interest rate.");
                return false;
            }
            else if (tbAddress1.Text == "" || tbCompany1.Text == "" || tbName1.Text == "" || tbPosition1.Text == "")
            {
                MessageBox.Show("Please input details for Comaker 1.");
                return false;
            }
            else
                return true;
                
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            name.Text = "";
            memid = -1;
            cbLoan.SelectedIndex = 0;
            cbRequest.SelectedIndex = 0;
            tbAddress1.Clear();
            tbAddress2.Clear();
            tbAmount.Clear();
            tbCompany1.Clear();
            tbCompany2.Clear();
            tbInterest.Clear();
            tbName1.Clear();
            tbName2.Clear();
            tbPosition1.Clear();
            tbPosition2.Clear();
            tbPurpose.Clear();
            tbTerm.SelectedIndex = 0;
        }
    }
}