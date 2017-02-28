using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication2
{
    public partial class addMember : Form
    {
        Members upper;
        public MySqlConnection conn;
        public addMember(Members x)
        {
            InitializeComponent();
            upper = x;
            conn = new MySqlConnection("Server=localhost;Database=test_db;Uid=root;Pwd=root;");
        }

        private void addMember_FormClosing(object sender, FormClosingEventArgs e)
        {
            upper.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("INSERT INTO members (fn,mn,ln,username,password,account_type,account_status) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "'," + status + "," + activ + ")", conn);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee);
                conn.Close();
            }
        }
    }
}
