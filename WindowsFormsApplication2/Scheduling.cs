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
    public partial class Scheduling : Form
    {
        MainMenu upper;
        public Scheduling(MainMenu x)
        {
            InitializeComponent();
            Owner = x;
        }

        private void Scheduling_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
