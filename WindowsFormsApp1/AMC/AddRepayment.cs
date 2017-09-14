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
        public void SetName(string name)
        {
            label15.Text = name;
            conn.Select("loans", "member_id", "loan_account_id").Where("member_id", memid.ToString()).GetQueryData();
            foreach (DataRow r in conn.GetData().Rows)
            {
                var cc = new ComboboxContent(int.Parse(r["loan_account_id"].ToString()), r["loan_account_id"].ToString());
                cbxAccount.Items.Add(cc);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            conn.Insert("loan_transactions", 
                        "principal", txtPrincipal.ToString(), "interest", txtInterest.ToString(), "penalty", txtPenalty.ToString(), 
                        "date_encoded", DateTime.Today.ToString());
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
            try
            {
                
                label12.Text = (checkier(txtInterest.Text) + checkier(txtPenalty.Text) + checkier(txtPrincipal.Text)).ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private static int checkier(string text)
        {

            try
            {
                return int.Parse(text);
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
    }
}
