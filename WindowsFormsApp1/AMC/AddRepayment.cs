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
    public partial class AddRepayment : Form
    {
        private DatabaseConn conn = new DatabaseConn();
        private addLoanM popup;
        public int memid;
        public AddRepayment()
        {
            InitializeComponent();
            MemberListRef();
        }

        public AddRepayment(int memberid) : this()
        {
            //cbxMember.SelectedIndex = memberid;
        }

        private void MemberListRef()
        {
            conn.Select("LoansM", "member_id", "concat_ws(',', family_name, first_name) as name")
                .Where("date_terminated", null);
            //cbxMember.DataSource = conn.GetQueryData();
        }

        private void cbxMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Select("Loans", "account_id", "type", "outstanding_balance")
                .Where("date_terminated", null);
            cbxAccount.DataSource = conn.GetQueryData();
            cbxAccount.SelectedIndex = 0;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            conn.Insert("loan_transactions", 
                        "principal", txtPrincipal.ToString(), "interest", txtInterest.ToString(), "penalty", txtPenalty.ToString(), 
                        "date_encoded", DateTime.Today.ToString());
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
            var reftomain = this;
            popup = new addLoanM(reftomain);
            popup.ShowDialog();
            DataTable dt = conn.Select("loans","member_id","loan_account_id").Where("member_id",memid.ToString()).GetQueryData();
            foreach (DataRow r in dt.Rows)
            {
                ComboboxContent cc = new ComboboxContent(int.Parse(r["loan_account_id"].ToString()), r["loan_account_id"].ToString());
                cbxAccount.Items.Add(cc);
            }
            
        }

        private void txtPenalty_TextChanged(object sender, EventArgs e)
        {

        }

        private void addition()
        {
            try
            {
                label12.Text = (int.Parse(txtInterest.ToString()) + int.Parse(txtPenalty.ToString()) + int.Parse(txtPrincipal.ToString())).ToString();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
