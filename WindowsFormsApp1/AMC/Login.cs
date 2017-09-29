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
    public partial class Login : Form
    {
        private bool userCheck = true, passCheck = true;
        public string uid, uname;
        private DatabaseConn conn = new DatabaseConn();
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSubmit_Click(this, new EventArgs());
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSubmit_Click(this, new EventArgs());
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var x = conn.Select("users", "user_id", "concat_ws(',', last_name, first_name) as Name")
                        .Where("username", textBox1.Text, "password", tbSearch.Text)
                        .GetQueryData();
            if (x.Rows.Count == 1)
            {
                uid = x.Rows[0]["user_id"].ToString();
                uname = x.Rows[0]["Name"].ToString();
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Invalid Username or Password");
        }

        private void tbSearch_Enter(object sender, EventArgs e)
        {
            if (passCheck)
            {
                tbSearch.Text = "";
                tbSearch.Font = new Font("Wingdings", 10, FontStyle.Regular);
                tbSearch.PasswordChar = 'l';
                tbSearch.ForeColor = Color.DimGray;
            }
        }

        private void tbSearch_Leave(object sender, EventArgs e)
        {
            passCheck = tbSearch.Text.Equals("");
            if (passCheck)
            {
                tbSearch.Text = "Password";
                tbSearch.PasswordChar = '\0';
                tbSearch.Font = new Font("Segoe UI", 10, FontStyle.Italic);
                tbSearch.ForeColor = Color.FromArgb(148, 165, 165);
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            userCheck = textBox1.Text.Equals("");
            if (userCheck)
            {
                textBox1.Text = "Username";
                textBox1.Font = new Font("Segoe UI", 10, FontStyle.Italic);
                textBox1.ForeColor = Color.FromArgb(148, 165, 165);
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (userCheck)
            {
                textBox1.Text = "";
                textBox1.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                textBox1.ForeColor = Color.DimGray;
            }
        }
    }
}
