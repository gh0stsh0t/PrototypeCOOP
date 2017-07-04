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
    public partial class AddMember : Form
    {
        private bool lnameFlag = true;
        private bool fnameFlag = true;
        private bool mnameFlag = true;
        public AddMember()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void AddMember_Load(object sender, EventArgs e)
        {
            this.ActiveControl = label3;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void lname_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void lname_Enter(object sender, EventArgs e)
        {
            if(lnameFlag)
            {
                lname.Text = "";
                lname.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void lname_Leave(object sender, EventArgs e)
        {
            lnameFlag = (lname.Text == "");
            if (lnameFlag)
            {
                lname.AppendText("Last Name");
                lname.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (fnameFlag)
                fname.Text = "";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            fnameFlag = (fname.Text == "");
            if (fnameFlag)
                fname.AppendText("First Name");
        }

        private void fname_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
