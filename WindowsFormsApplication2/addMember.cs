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
        int choice;
        public MySqlConnection conn;
        public addMember(Members x,int z)
        {
            InitializeComponent();
            upper = x;
            choice = z;
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
                MySqlCommand comm=new MySqlCommand("");
                switch (choice)
                {
                    case 0:
                        comm = new MySqlCommand("INSERT INTO members (family_name,first_name,middle_name) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')", conn);
                        break;
                    case 1:
                        comm = new MySqlCommand("INSERT INTO members (family_name,first_name,middle_name) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')", conn);
                        break;
                }
                comm.ExecuteNonQuery();
                conn.Close();
                upper.refrehasjkda();
                Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                conn.Close();
            }
        }

        private void addMember_Load(object sender, EventArgs e)
        {
            switch(choice)
            {
                case 0:
                    Text = "Add New Member";
                    break;
                case 1:
                    try
                    {
                        
                        conn.Open();
                        MySqlCommand comm = new MySqlCommand("SELECT * FROM members where id=" + upper.memberid, conn);
                        MySqlDataReader reader = comm.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            textBox1.Text = reader.GetString(1);
                            textBox2.Text = reader.GetString(2);
                            textBox3.Text = reader.GetString(3);
                        }
                        conn.Close();
                    }
                    catch(Exception ee)
                    {
                    }
                    Text = "Update Member Details";
                    button1.Text = "Update";
                    break;
            }
        }
    }
}
