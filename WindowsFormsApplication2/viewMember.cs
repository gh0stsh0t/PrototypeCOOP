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
    public partial class viewMember : Form
    {
        Members upper;
        MySqlConnection conn;
        public viewMember(Members x)
        {
            InitializeComponent();
            upper = x;
            conn = new MySqlConnection("Server=localhost;Database=test_db;Uid=root;Pwd=root;");
        }

        private void viewMember_FormClosing(object sender, FormClosingEventArgs e)
        {
            upper.Show();
        }

        private void viewMember_Load(object sender, EventArgs e)
        {
            try
            {

                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT * FROM members where id=" + upper.memberid, conn);
                MySqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    label2.Text= reader.GetString(1)+", "+reader.GetString(2)+" "+ reader.GetString(3);
                }
                conn.Close();
            }
            catch (Exception ee)
            {
            }
        }
    }
}
