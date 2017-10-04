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
    public partial class NewCOA : Form
    {
        MySqlConnection conn;
        MainForm reftomain;
        Dashboard source;

        public NewCOA(MainForm main, Dashboard ds, MySqlConnection con)
        {
            InitializeComponent();
            reftomain = main;
            source = ds;
            conn = con;
        }

        private void NewCOA_Load(object sender, EventArgs e)
        {
            cbxType.SelectedIndex = 0;
        }

        public static Boolean isNum(string strToCheck)
        {
            Regex rg = new Regex(@"^[0-9]+\.?[0-9]{0,2}$");
            return rg.IsMatch(strToCheck);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            reftomain.Enabled = true;
            this.Close();
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            if(txtTitle.Text == "" || tbAmt.Text == "")
            {
                MessageBox.Show("Please fill in all fields.");
            }
            else if (!isNum(tbAmt.Text))
            {
                MessageBox.Show("Please make sure the balance field is in the valid format.");
                tbAmt.Focus();
            }
            else
            {
                string q = "INSERT INTO chart_of_accounts (code, title, type, status) VALUES (103,'" + txtTitle.Text + "'," + cbxType.SelectedIndex.ToString() + ",1);"
                    + " SELECT code FROM chart_of_accounts WHERE code = (SELECT MAX(code) FROM chart_of_accounts);";
                Int32 newID;
                try
                {
                    conn.Open();
                    MySqlCommand comm = new MySqlCommand(q, conn);
                    newID = Convert.ToInt32(comm.ExecuteScalar());
                    q = "INSERT INTO chart_of_accounts_log (code, amount, date) VALUES(" + newID.ToString() + "," + tbAmt.Text + ",'" + DateTime.Today.ToString("yyyy-MM-dd") + "')";
                    MySqlCommand ins = new MySqlCommand(q, conn);
                    ins.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Added New Account.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ee)
                {
                    //MessageBox.Show(ee.Message);
                    conn.Close();
                }
                reftomain.Enabled = true;
                source.loadAccs();
                this.Close();
            }
        }
    }
}
