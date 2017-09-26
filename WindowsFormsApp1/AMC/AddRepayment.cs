using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMC
{
    public partial class AddRepayment : Form
    {
        private DatabaseConn conn = new DatabaseConn();
        private addLoanM popup;
        public int memid;
        public int loanid;
        private float _value;
        public AddRepayment()
        {
            InitializeComponent();
            MemberListRef();
        }
        /*
         * here lies the code for the data grid biew
         * SELECT SUM(interest) TOTAL_INTEREST, SUM(principal) total_principal, SUM(penalty) total_penalty), SUM(interest)-SUM(release) balance 
         * FROM loan_transactions
         * GROUP BY loan_account_id
         * 
         * STORED PROC:
         * SELECT date_granted-cutoffdate as age, age-term as taas name,  loans INNER JOIN loan_transactions
         */
        public AddRepayment(int memberid) : this()
        {
            //cbxMember.SelectedIndex = memberid;
            memid = memberid;
            SetName(conn.Select("members", "concat_ws(',', family_name, first_name) as name")
                        .Where("member_id",memid.ToString())
                        .GetQueryData()
                        .Rows[0][0].ToString());
        }
        private void MemberListRef()
        {
            conn.Select("LoansM", "member_id", "concat_ws(',', family_name, first_name) as name")
                .Where("date_terminated", null);
            //cbxMember.DataSource = conn.GetQueryData();
        }
        public void SetName(string name, int index = 0)
        {
            label15.Text = name;
            conn.Select("loans", "outstanding_balance", "loan_account_id").Where("member_id", memid.ToString()).GetQueryData();
            cbxAccount.Items.Clear();
            foreach (DataRow r in conn.GetData().Rows)
            {
                var cc = new ComboboxContent(int.Parse(r["loan_account_id"].ToString()), r["loan_account_id"].ToString(),r["outstanding_balance"].ToString());
                cbxAccount.Items.Add(cc);
            }
            cbxAccount.SelectedIndex = index;
            
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (_value < 0)
                    throw new Exception();
                conn.Insert("loan_transaction",
                        "loan_account_id", cbxAccount.SelectedItem.ToString(), "transaction_type", "1", "principal",
                        txtPrincipal.Text,
                        "interest", txtInterest.Text, "penalty", txtPenalty.Text, "total_amount", label12.Text,
                        "date", DateTime.Today.ToString("yyyy-MM-dd"))
                    .GetQueryData();
                string[] vals = {"outstanding_balance", _value.ToString()};
                string[] incase = {"loan_status", "3", "date_terminated", DateTime.Today.ToString("yyyy-MM-dd")};
                var z = new string[vals.Length + incase.Length];
                if (Math.Abs(_value) < 1e-6)
                {
                    vals.CopyTo(z, 0);
                    incase.CopyTo(z, vals.Length);
                }
                    conn.Update("loans", vals)
                        .Where("loan_account_id", cbxAccount.SelectedItem.ToString())
                        .GetQueryData();
                SetName(label15.Text,index:cbxAccount.SelectedIndex);
            }
            catch (Exception exception)
            {/*
                MessageBox.Show(exception.ToString());
                throw;*/
                MessageBox.Show("Final Balance is negative. Ensure Calculations are Correct");
            }
        }

        private void Breaker()
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
            Breaker();
            var reftomain = this;
            popup = new addLoanM(reftomain);
            popup.ShowDialog();
        }

        private void txtPenalty_TextChanged(object sender, EventArgs e)
        {
            Addition();
        }

        private void Addition()
        {
            var x = (ComboboxContent)cbxAccount.SelectedItem;
            try
            {
                
                label12.Text = (checkier(txtInterest.Text) + checkier(txtPenalty.Text) + checkier(txtPrincipal.Text)).ToString();
                _value = checkier(x.Content2) - checkier(txtPrincipal.Text);
                if (_value < 0)
                    label18.ForeColor = Color.Red;
                else
                    label18.ForeColor = Color.Black;
                label18.Text = _value.ToString("F");
            }
            catch (Exception ex)
            {
            }
        }

        private static float checkier(string text)
        {

            try
            {
                return float.Parse(text);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        private void txtPrincipal_TextChanged(object sender, EventArgs e)
        {
            Addition();
        }

        private void txtInterest_TextChanged(object sender, EventArgs e)
        {
            Addition();
        }

        private void keychecker(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.'))
            { e.Handled = true; }
            TextBox txtDecimal = sender as TextBox;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtPrincipal_KeyPress(object sender, KeyPressEventArgs e)
        {
            keychecker(sender,e);
        }

        private void txtInterest_KeyPress(object sender, KeyPressEventArgs e)
        {
            keychecker(sender,e);
        }

        private void txtPenalty_KeyPress(object sender, KeyPressEventArgs e)
        {
            keychecker(sender,e);
        }

        private void txtPrincipal_Leave(object sender, EventArgs e)
        {
            isNum(sender);
        }

        private void txtInterest_Leave(object sender, EventArgs e)
        {
            isNum(sender);
        }

        private void txtPenalty_Leave(object sender, EventArgs e)
        {
            isNum(sender);
        }

        public void isNum(object strToCheck)
        {
            Regex rg = new Regex(@"^[0-9]+\.?[0-9]{1,2}$");
            if (rg.IsMatch(((TextBox)strToCheck).Text)) return;
            MessageBox.Show("Please make sure the amount contains no special characters and has no more than 2 decimal points.");
            txtPrincipal.Focus();
        }

        private void cbxAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            var x = (ComboboxContent)cbxAccount.SelectedItem;
            label8.Text = "Current Balance: " + x.content2;
        }
    }
}
