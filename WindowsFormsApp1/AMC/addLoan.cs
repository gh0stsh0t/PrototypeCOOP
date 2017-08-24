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
        public AddLoan(int x)
        {
            InitializeComponent();
            memid = x;
            MessageBox.Show(memid.ToString());
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
    }
}