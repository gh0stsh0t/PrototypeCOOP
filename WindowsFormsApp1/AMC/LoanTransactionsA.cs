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
    public partial class LoanTransactionsA : Form
    {
        public int acc;
        public LoanTransactionsA(int member)
        {
            InitializeComponent();
            var db = new DatabaseConn();
            mlist.DataSource = db.Select("loans", "loan_account_id", "date_granted", "orig_amount", "outstanding_balance")
                                 .Where("member_id", member.ToString())
                                 .GetQueryData();
            mlist.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void mlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            acc = (int)mlist.Rows[e.RowIndex].Cells["loan_account_id"].Value;
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
