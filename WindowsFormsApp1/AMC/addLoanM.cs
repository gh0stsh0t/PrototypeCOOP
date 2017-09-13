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
    public partial class addLoanM : Form
    {
        public Form reftomain;
        public int memid;
        public MySqlConnection conn;
        private DatabaseConn _addloanconn;
        public DataTable loanmems;

        public addLoanM(AddLoan main):this()
        {
            reftomain = main;
        }

        public addLoanM()
        {
            InitializeComponent();
             _addloanconn = new DatabaseConn();
        }
        public addLoanM(AddRepayment heh)
        {
            reftomain = heh;
        }

        private void addLoanM_Load(object sender, EventArgs e)
        {
            loadmembers();

        }

        private void loadmembers()
        {
            try
            {
                string[] taes = {"member_id",
                    "concat_ws(',', family_name, first_name) as name"};
                loanmems = _addloanconn.Select("members", taes).GetQueryData();
                mlist.DataSource = loanmems;
                mlist.Columns["member_id"].Visible = false;
                //cbBorrower.AutoCompleteSource = ;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void mlist_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void mlist_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            reftomain.memname = mlist.Rows[e.RowIndex].Cells["name"].Value.ToString();
            reftomain.memid = int.Parse(mlist.Rows[e.RowIndex].Cells["member_id"].Value.ToString());
            reftomain.namerfrsh();
            this.Hide();
        }
    }
}