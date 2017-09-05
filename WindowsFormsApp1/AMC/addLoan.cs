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
        int memid;
        private DatabaseConn _addloanconn;
        public DataTable loanmems;

        public AddLoan(int x)
        {
            InitializeComponent();
            memid = x;
            cbLoan.SelectedIndex = 0;
            cbRequest.SelectedIndex = 0;
            _addloanconn = new DatabaseConn();
            loadmembers();
        }


        private void loadmembers()
        {
            try
            {
                string[] taes = {"member_id",
                    "concat_ws(',', family_name, first_name) as name"};
                loanmems = _addloanconn.Select("members", taes).GetQueryData();
                foreach(DataRow r in loanmems.Rows)
                {
                    ComboboxContent cc = new ComboboxContent(int.Parse(r["member_id"].ToString()), r["name"].ToString());
                    //MessageBox.Show(cc.ToString());
                    cbBorrower.Items.Add(cc);
                }
                //cbBorrower.AutoCompleteSource = ;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void AddLoan_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cbBorrower.SelectedIndex = 1;
            cbLoan.SelectedIndex = 1;
            cbRequest.SelectedIndex = 1;
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
            tbTerm.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cbBorrower_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                cbBorrower.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else
            {
                cbBorrower.DropDownStyle = ComboBoxStyle.DropDown;
            }
        }

        private void cbBorrower_Leave(object sender, EventArgs e)
        {
        }
    }
}