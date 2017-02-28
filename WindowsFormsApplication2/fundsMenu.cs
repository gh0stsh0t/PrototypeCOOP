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
    public partial class fundsMenu : Form
    {
        MainMenu upper;
        Form lower;
        public fundsMenu(MainMenu x)
        {
            InitializeComponent();
            upper = x;
        }

        private void fundsMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            upper.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lower = new loanPayment(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lower = new loanDisbursment(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lower = new savingsCBU(this, 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            lower = new savingsCBU(this, 1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lower = new savingsCBU(this, 2);
        }
    }
}
