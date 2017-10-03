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

namespace AMC
{
    public partial class NewCBUAccount : Form
    {
        public MainForm reftomain;
        public int memid;
        public MySqlConnection conn;
        string accid;

        public NewCBUAccount(int id, MySqlConnection c, MainForm main)
        {
            InitializeComponent();
            memid = id;
            conn = c;
            reftomain = main;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                conn.Open();
                string query = "INSERT INTO capitals (member_id, opening_date, account_status, ics_no, ics_amount, ipuc_amount)" +
                                "VALUES('" + memid + "','" + DateTime.Today.ToString("yyyy-MM-dd") + "', '1', '" + txticsn.Text + 
                                "','" + txticsa.Text + "','" + txtipuc.Text + "')";
                /* "SELECT LAST_INSERT_ID()" ;
            }
                        MySqlCommand ins = new MySqlCommand(query, conn);
            newID = Convert.ToInt32(ins.ExecuteScalar()); */

            MySqlCommand ins = new MySqlCommand(query, conn);
                ins.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }

            reftomain.Enabled = true;
            reftomain.innerChild(new ViewProfile(memid, reftomain));
            this.Close();

        }

        private void NewCBUAccount_Load(object sender, EventArgs e)
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


                conn.Close();

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reftomain.Enabled = true;
            this.Close();
        }
    }
}
