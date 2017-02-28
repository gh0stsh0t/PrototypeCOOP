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
    public partial class Members : Form
    {
        MainMenu upper;
        public Members(MainMenu x)
        {
            InitializeComponent();
            upper = x;
        }

        private void Members_FormClosing(object sender, FormClosingEventArgs e)
        {
            upper.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addMember lower = new addMember(this);
            lower.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //dataGridView1.SelectedRows
            viewMember lower = new viewMember(this);
            lower.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loanAccountRequest lower = new loanAccountRequest(this);
            lower.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("is Deactivated");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Report generated");
        }
    }
}
