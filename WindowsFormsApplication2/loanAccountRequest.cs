using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class loanAccountRequest : Form
    {
        Members upper;
        public loanAccountRequest(Members x)
        {
            InitializeComponent();
            upper = x;
        }

        private void loanAccountRequest_FormClosing(object sender, FormClosingEventArgs e)
        {
            upper.Show();
        }
    }
}
