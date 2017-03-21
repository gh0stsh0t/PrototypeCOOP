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
        int incr = 0;
        public MainMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            breaker();
            mem = new Members(this,BackColor);
            mem.TopLevel = false;
            mem.AutoScroll = true;
            panel4.Controls.Add(mem);
            mem.Show();
        }
        private void breaker()
        {
            try
            {
                mem.Close();
            }
            catch
            { }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            breaker();
            mem = new fundsMenu(this,BackColor);
            mem.TopLevel = false;
            mem.AutoScroll = true;
            panel4.Controls.Add(mem);
            mem.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            breaker();
            mem = new Scheduling(this);
            mem.TopLevel = false;
            mem.AutoScroll = true;
            panel4.Controls.Add(mem);
            mem.Show();
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            incr = 1;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel1.Size = new Size(panel1.Size.Width + 7 * incr, panel1.Size.Height);
            panel2.Size = new Size(panel2.Size.Width, panel2.Size.Height - 26 * incr);
            if (panel2.Size.Height <= 0 || incr < 0)
            {
                panel3.Location = new Point(panel3.Location.X, panel3.Location.Y + 3 * incr);
                if (panel3.Location.Y > 0)
                    panel3.Location = new Point(panel3.Location.X, 0);
                else if (panel3.Location.Y < -18)
                    panel3.Location = new Point(panel3.Location.X, -18);
            }
            if (panel2.Size.Height > 330)
                panel2.Size = new Size(panel2.Size.Width, 330);
            BackColor = System.Drawing.Color.FromArgb(BackColor.R + 10 * incr, BackColor.G + 11 * incr, BackColor.B + 10 * incr);
            if (panel1.Size.Width >= 126)
                timer1.Stop();
            else if (panel1.Size.Width == 0)
                timer1.Stop();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            breaker();
            incr = -1;
            timer1.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
