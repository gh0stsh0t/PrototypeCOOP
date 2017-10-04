using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
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
            Int32 newID;
            try
            {
                conn.Open();
                string query = "INSERT INTO capitals (member_id, opening_date, account_status, ics_no, ics_amount, ipuc_amount)" +
                                "VALUES('" + memid + "','" + DateTime.Today.ToString("yyyy-MM-dd") + "', '1', '" + txticsn.Text + 
                                "','" + txticsa.Text.Replace(",", "") + "','" + txtipuc.Text + "') "
                                + "SELECT LAST_INSERT_ID()" ;
            


            MySqlCommand ins = new MySqlCommand(query, conn);
            newID = Convert.ToInt32(ins.ExecuteScalar());
            query = "INSERT INTO capital_balance_log (capital_account_id, date, amount)" +
            "VALUES('" + newID.ToString() + "','" + DateTime.Today.ToString("yyyy-MM-dd") + "','" + txtipuc.Text + "');";
            MySqlCommand ins2 = new MySqlCommand(query, conn);
            ins2.ExecuteNonQuery();
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

        private void txticsn_Leave(object sender, EventArgs e)
        {
            if(!isNum(txticsn.Text))
            {
                MessageBox.Show("Please enter a valid number.");
                txticsn.Text = "100";
            } else if(Convert.ToInt32(txticsn.Text) < 100)
            {
                MessageBox.Show("Please enter a number not lower than 100.");
                txticsn.Text = "100";
            } else
            {
                txticsa.Text = (Convert.ToDouble(txticsn.Text) * 100).ToString("n2");
            }
        }

        public static Boolean isNum(string strToCheck)
        {
            Regex rg = new Regex(@"^[0-9]+\.?[0-9]{0,2}$");
            return rg.IsMatch(strToCheck);
        }

        private void txtipuc_Leave(object sender, EventArgs e)
        {
            if (!isNum(txtipuc.Text))
            {
                MessageBox.Show("Please enter a valid number.");
                txtipuc.Text = "1500.00";
            }
        }
    }
}
