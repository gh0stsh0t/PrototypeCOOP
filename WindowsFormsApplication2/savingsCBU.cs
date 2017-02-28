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
    public partial class savingsCBU : Form
    {
        fundsMenu upper;
        int menuOut;
        public savingsCBU(fundsMenu x,int choice)
        {
            InitializeComponent();
            upper = x;
            menuOut = choice;
        }

        private void savingsCBU_FormClosing(object sender, FormClosingEventArgs e)
        {
            upper.Show();
        }

        private void savingsCBU_Load(object sender, EventArgs e)
        {
            switch(menuOut)
            {
                case 0:
                    Text = "Savings Deposit";
                    break;
                case 1:
                    Text = "Savings Withdrawal";
                    label2.Show();
                    label5.Show();
                    label6.Show();
                    break;
                case 2:
                    Text = "Capital Build-Up Share";
                    break;
            }
        }
    }
}
