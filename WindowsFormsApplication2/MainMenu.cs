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
    public partial class MainMenu : Form
    {
        Form mem;
        Login upper;
        public MainMenu(Login x)
        {
            InitializeComponent();
            upper = x;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            mem = new Members(this);
            mem.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            mem = new fundsMenu(this);
            mem.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            mem = new Scheduling(this);
            mem.Show();
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            upper.Show();
        }
    }
}
