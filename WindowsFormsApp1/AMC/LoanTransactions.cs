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
    public partial class LoanTransactions : Form
    {

        public MainForm reftomain;
        private int memId;
        private DatabaseConn db;
        public LoanTransactions(int member,string membern, MainForm parent)
        {
            InitializeComponent();
            memId = member;
            mname.Text = membern;
            db = new DatabaseConn();
            reftomain = parent;
            popup();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            reftomain.innerChild(new ViewMember(reftomain));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            popup();
        }
        private void popup()
        {
            using (var account = new LoanTransactionsA(memId))
            {
                if (account.ShowDialog() == DialogResult.OK)
                {
                    rifrish(account.acc);

                }
                else
                    MessageBox.Show("Choose a loan account to view");
            }
        }
        private void rifrish(int accId)
        {
            label5.Text = accId.ToString();
            dataGridView4.DataSource = db.Select("loan_transaction","loan_transaction_id", "Date", "total_amount as 'Total Amount'", "Principal", "Interest", "Penalty", "encoded_by as Encoder")
                                         .Where("loan_account_id", accId.ToString())
                                         .GetQueryData();
            dataGridView4.Columns["loan_transaction_id"].Visible = false;
            int height = 0;
            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                height += row.Height;
            }
            height += dataGridView4.ColumnHeadersHeight;
            dataGridView4.ClientSize = new Size(633, height + 2);
            foreach (DataGridViewColumn col in dataGridView4.Columns)
                dataGridView4.Columns[col.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
}
