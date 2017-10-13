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
        public int memid, loanid;
        private string transid;
        private bool edit= false;
        private float orig;
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

        public AddRepayment(string transaction, int memberid, int index):this()
        {
            edit = true;
            transid = transaction;
            memid = memberid;
            SetName(conn.Select("members", "concat_ws(',', family_name, first_name) as name")
                .Where("member_id", memid.ToString())
                .GetQueryData()
                .Rows[0][0].ToString(),index);

            var trans = conn.Select("loan_transaction",
                    "loan_account_id", "principal",
                    "interest", "penalty")
                            .Where("loan_transaction_id", transid.ToString())
                            .GetQueryData();
            orig = float.Parse(trans.Rows[0].Cells["interest"].Value.ToString());
            txtInterest.Text = trans.Rows[0].Cells["interest"].Value.ToString();
            txtPrincipal.Text = trans.Rows[0].Cells["principal"].Value.ToString();
            txtPenalty.Text = trans.Rows[0].Cells["penalty"].Value.ToString();

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
            if(edit)
                for (var x = 0;x<cbxAccount.Items.Count;x++)
                    if (cbxAccount.Items[x].ToString().Equals(transid))
                        index = x;
            cbxAccount.SelectedIndex = index;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string[] valueStrings =
                {
                    "loan_account_id", cbxAccount.SelectedItem.ToString(), "transaction_type", "1", "principal",
                    txtPrincipal.Text,
                    "interest", txtInterest.Text, "penalty", txtPenalty.Text, "total_amount", label12.Text,
                    "date", DateTime.Today.ToString("yyyy-MM-dd")
                };

                if (_value < 0 && !edit)
                    throw new Exception();
                if (float.Parse(txtPrincipal.Text) <= 0)
                {
                    if (edit)
                        conn.Update("loan_transactions", valueStrings).Where("loan_transaction_id", transid)
                            .GetQueryData();
                    else
                        conn.Insert("loan_transaction", valueStrings)
                            .GetQueryData();
                    string[] vals = { "outstanding_balance", _value.ToString() };
                    var z = new string[6];
                    if (Math.Abs(_value) < 1e-6)
                    {
                        string[] incase = { "loan_status", "3", "date_terminated", DateTime.Today.ToString("yyyy-MM-dd") };
                        vals.CopyTo(z, 0);
                        incase.CopyTo(z, vals.Length);
                    }
                    else
                    {
                        string[] nocase = { "loan_status", "1", "date_terminated", DateTime.Today.ToString("yyyy-MM-dd") };
                        vals.CopyTo(z, 0);
                        nocase.CopyTo(z, vals.Length);
                    }
                    conn.Update("loans", vals)
                        .Where("loan_account_id", cbxAccount.SelectedItem.ToString())
                        .GetQueryData();
                    SetName(label15.Text, index: cbxAccount.SelectedIndex);
                    MessageBox.Show("Transaction recorded");
                }
                else
                    MessageBox.Show("Principal value Must not be 0");
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
                _value = checkier(x.Content2) - checkier(txtPrincipal.Text) - (edit?orig:0);
                label18.ForeColor = _value < 0 ? Color.Red : Color.Black;
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
