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
        public AddRepayment()
        {
            InitializeComponent();
            MemberListRef();
        }

        public AddRepayment(int memberid):this()
        {
            cbxMember.SelectedIndex = memberid;
        }

        private void MemberListRef()
        {
            conn.Select("LoansM", "member_id", "concat_ws(',', family_name, first_name) as name")
                .Where("date_terminated", null);
            cbxMember.DataSource = conn.GetQueryData();
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
                
        }
    }
}
