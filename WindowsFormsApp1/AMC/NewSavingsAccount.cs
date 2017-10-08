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
    public partial class NewSavingsAccount : Form
    {
        public MainForm reftomain;
        public int memid;
        public MySqlConnection conn;
        string accid;
        double interest;

        public NewSavingsAccount(int id, MySqlConnection c, MainForm main)
        {
            InitializeComponent();
            memid = id;
            conn = c;
            reftomain = main;
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

                MySqlCommand intrate = new MySqlCommand("SELECT interest_rate FROM interest_rate_log WHERE date = (SELECT MAX(date) FROM interest_rate_log)", conn);
                MySqlDataAdapter adp2 = new MySqlDataAdapter(intrate);
                DataTable dt2 = new DataTable();
                adp2.Fill(dt2);
                if (dt2.Rows.Count == 1)
                {
                    interest = Convert.ToDouble(dt2.Rows[0]["interest_rate"]);
                    intRate.Text = (interest * 100).ToString();
                }


                conn.Close();

            }
            catch (Exception ee)
            {
                ////MessageBox.Show(ee.ToString());
                conn.Close();
            }
        }

        /* private void getNewId()
        {
            string q = "SELECT savings_account_id FROM savings ORDER BY savings_account_id DESC LIMIT 1";
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand(q, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    accid =(Int32.Parse(dt.Rows[0]["savings_account_id"].ToString()) + 1).ToString();

                }
                else { 
                    accid = "1";
                    }


                conn.Close();


            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.ToString());
                conn.Close();
            }
        } */

        private void button2_Click_1(object sender, EventArgs e)
        {
            reftomain.Enabled = true;
            this.Close();
        }

        private void btnSubmit_Click_1(object sender, EventArgs e)
        {
            int newID;
            try
            {
                conn.Open();
                string query = "INSERT INTO savings (member_id, opening_date, account_status)" +
                                "VALUES('" + memid + "','" + DateTime.Today.ToString("yyyy-MM-dd") + "', '1'); "
                                + " SELECT LAST_INSERT_ID()";

                MySqlCommand ins = new MySqlCommand(query, conn);
                newID = Convert.ToInt32(ins.ExecuteScalar());
                query = "INSERT INTO savings_balance_log (savings_account_id, date, amount)" +
                "VALUES('" + newID.ToString() + "','" + DateTime.Today.ToString("yyyy-MM-dd") + "','" + txtBal.Text + "');";
                MySqlCommand ins2 = new MySqlCommand(query, conn);
                ins2.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.ToString());
                conn.Close();
            }

            reftomain.Enabled = true;
            reftomain.innerChild(new ViewProfile(memid, reftomain));
            this.Close();
        }

        private void txtBal_Leave(object sender, EventArgs e)
        {
            if (!isNum(txtBal.Text))
            {
                MessageBox.Show("Please enter a valid number.");
                txtBal.Text = "500.00";
            }
            else if (Convert.ToDouble(txtBal.Text) < 500)
            {
                MessageBox.Show("Please enter an amount not lower than 500.00.");
                txtBal.Text = "500.00";
            }
        }

        public static Boolean isNum(string strToCheck)
        {
            Regex rg = new Regex(@"^[0-9]+\.?[0-9]{0,2}$");
            return rg.IsMatch(strToCheck);
        }
    }
}
