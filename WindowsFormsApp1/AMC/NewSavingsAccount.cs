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
    public partial class NewSavingsAccount : Form
    {
        public MainForm reftomain;
        public int memid;
        public MySqlConnection conn;
        string accid;

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
            getNewId();
            lblAccount.Text = accid;
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                conn.Open();
                string query = "INSERT INTO savings (savings_account_id, member_id, opening_date, outstanding_balance, account_status)" +
                                "VALUES('" + accid + "', '" + memid + "','" + DateTime.Today.ToString("yyyy-MM-dd") + "','" + txtBal.Text + "', '0')";
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

        private void getNewId()
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
                    accid = (Int32.Parse(dt.Rows[0]["savings_account_id"].ToString()) + 1).ToString();

                }


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
