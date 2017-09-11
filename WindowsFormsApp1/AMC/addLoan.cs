using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace AMC
{
    public partial class AddLoan : Form
    {
        public int memid;
        private DatabaseConn _addloanconn;
        public DataTable loanmems;
        public string memname;
        private Form popup;
        private double intrate = 5.00; //DO THIS SHIT GUYS      AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
        string today = DateTime.Today.ToString("yyyyMM");


        public AddLoan(int x)
        {
            InitializeComponent();
            memid = x;
            cbLoan.SelectedIndex = 0;
            cbRequest.SelectedIndex = 0;
            tbTerm.SelectedIndex = 0;
            _addloanconn = new DatabaseConn();
            tbInterest.Text = intrate.ToString();
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

        private void breaker()
        {
            try
            {
                popup.Close();
                popup.Dispose();
            }
            catch
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            breaker();
            AddLoan reftomain = this;
            popup = new addLoanM(reftomain);
            popup.ShowDialog();
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
            else if ()
            {

            }
            else
                return true;   
        }

        private bool co2check()
        {
            if ((tbAddress1.Text == "" || tbCompany1.Text == "" || tbName1.Text == "" || tbPosition1.Text == "") 
                && (tbAddress1.Text != "" || tbCompany1.Text != "" || tbName1.Text != "" || tbPosition1.Text != ""))
                return true;
            return false;
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

        private void tbAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.'))
            { e.Handled = true; }
            TextBox txtDecimal = sender as TextBox;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        public static Boolean isNum(string strToCheck)
        {
            Regex rg = new Regex(@"^[0-9]+\.?[0-9]{1,2}$");
            return rg.IsMatch(strToCheck);
        }

        private void tbInterest_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.'))
            { e.Handled = true; }
            TextBox txtDecimal = sender as TextBox;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        public static Boolean isAlphaNum(string strToCheck)
        {
            Regex rg = new Regex(@"^([a-zA-Z0-9]+[\s,\-\.]?)+$");
            return rg.IsMatch(strToCheck);
        }

        private void ctrlLeave(TextBox txtbox)
        {
            bool flag = (txtbox.Text == "");
            if (!flag)
            {
                if (!isAlphaNum(txtbox.Text))
                {
                    MessageBox.Show("Please make sure this field contains the valid format");
                    txtbox.Focus();
                }
            }
        }

        private void tbPurpose_Leave(object sender, EventArgs e)
        {
            ctrlLeave(tbPurpose);
        }

        private void tbName1_Leave(object sender, EventArgs e)
        {
            ctrlLeave(tbName1);
        }

        private void tbAddress1_Leave(object sender, EventArgs e)
        {
            ctrlLeave(tbAddress1);
        }

        private void tbCompany1_Leave(object sender, EventArgs e)
        {
            ctrlLeave(tbCompany1);
        }

        private void tbPosition1_Leave(object sender, EventArgs e)
        {
            ctrlLeave(tbPosition1);
        }

        private void tbName2_Leave(object sender, EventArgs e)
        {
            ctrlLeave(tbName2);
        }

        private void tbAddress2_Leave(object sender, EventArgs e)
        {
            ctrlLeave(tbAddress2);
        }

        private void tbCompany2_Leave(object sender, EventArgs e)
        {
            ctrlLeave(tbCompany2);
        }

        private void tbPosition2_Leave(object sender, EventArgs e)
        {
            ctrlLeave(tbPosition2);
        }

        private void tbAmount_Leave(object sender, EventArgs e)
        {
            if(!isNum(tbAmount.Text))
            {
                MessageBox.Show("Please make sure the amount contains no special characters and has no more than 2 decimal points.");
                tbAmount.Focus();
            }
        }

        private void tbInterest_Leave(object sender, EventArgs e)
        {
            if (!isNum(tbInterest.Text))
            {
                MessageBox.Show("Please make sure the interest contains no special characters and has no more than 2 decimal points.");
                tbInterest.Focus();
            }
            else if(float.Parse(tbInterest.Text) >= 100)
            {
                MessageBox.Show("Please make sure the interest is less than or equal to 100%.");
                tbInterest.Focus();
            }
        }
    }
   
}