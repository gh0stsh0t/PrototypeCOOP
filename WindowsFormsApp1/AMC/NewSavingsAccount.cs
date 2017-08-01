using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AMC
{
    public partial class NewSavingsAccount : Form
    {
        public Form reftomain { get; set; }
        public int memid;
        public MySqlConnection conn;

        public NewSavingsAccount(int id, MySqlConnection c)
        {
            InitializeComponent();
            memid = id;
            conn = c;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reftomain.Enabled = true;
            this.Close();
        }

        private void NewSavingsAccount_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT * FROM members WHERE member_id = " + memid.ToString(), conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    lblName.Text = dt.Rows[0]["family_name"].ToString() + ", " + dt.Rows[0]["first_name"].ToString() + " " + dt.Rows[0]["middle_name"].ToString();
                    
                }
                /* else if (dt.Rows.Count > 1)
                {
                    for (int index = 0; index < dt.Rows.Count; index++)
                    {
                        string fn, ln;
                        fn = dt.Rows[index]["firstname"].ToString();
                        ln = dt.Rows[index]["lastname"].ToString();
                        MessageBox.Show("Hello " + ln + ", " + fn);
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect username and/or password.");
                } */

                conn.Close();

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
        }
    }
}
