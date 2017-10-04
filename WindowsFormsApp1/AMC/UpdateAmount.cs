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
    public partial class UpdateAmount : Form
    {
        MySqlConnection conn;
        MainForm reftomain;
        Dashboard source;
        string a, b, c, d, e, f;

        public UpdateAmount(MySqlConnection con, MainForm main, string b1, string b2, string b3, string b4, string b5, string b6, Dashboard ds)
        {
            InitializeComponent();
            conn = con;
            reftomain = main;
            a = b1; b = b2; c = b3; d = b4; e = b5; f = b6;
            source = ds;
        }

        private void UpdateAmount_Load(object sender, EventArgs e)
        {
            cbxFund.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            reftomain.Enabled = true;
            this.Close();
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            if(cbxFund.Text == "" || tbAmt.Text == "")
            {
                MessageBox.Show("Please fill in all fields.");
            } else if(!isNum(tbAmt.Text))
            {
                MessageBox.Show("Please make sure the amount field is in the valid format.");
                tbAmt.Focus();
            }
            else if (matchvalues())
            {
                MessageBox.Show("You have not made any changes.");
            }
            else if (!validrates())
            {
                MessageBox.Show("Please enter a valid rate value.");
            } 
            else
            {
                string z;
                if (cbxFund.SelectedIndex > 1)
                    z = " Capital Build-Up ";
                else
                    z = " Savings ";
                string y = "Changes will affect all future" + z + "computations starting today. Would you like to continue?";
                DialogResult dialogResult = MessageBox.Show(y, "Warning", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string q = ""; string n;
                    if (cbxFund.SelectedIndex == 0)
                    {
                        n = (Convert.ToDouble(tbAmt.Text) / 100).ToString();
                        q = "INSERT INTO interest_rate_log (interest_rate, date, updated_by) VALUES (" + n + ",'" + DateTime.Today.ToString("yyyy-MM-dd") + "','" + User.Name.id + "')";
                    }
                    else if (cbxFund.SelectedIndex == 1)
                        q = "INSERT INTO avg_daily_balance_log (amount, date, updated_by) VALUES (" + tbAmt.Text + ",'" + DateTime.Today.ToString("yyyy-MM-dd") + "','" + User.Name.id +  "')";
                    else if (cbxFund.SelectedIndex == 2)
                        q = "INSERT INTO capital_general_log (fund_type, amount, date, updated_by) VALUES (0," + tbAmt.Text + ",'" + DateTime.Today.ToString("yyyy-MM-dd") + "','" + User.Name.id  + "')";
                    else if (cbxFund.SelectedIndex == 3)
                    {
                        n = (Convert.ToDouble(tbAmt.Text) / 100).ToString();
                        q = "INSERT INTO capital_general_log (fund_type, amount, date, updated_by) VALUES (1," + n + ",'" + DateTime.Today.ToString("yyyy-MM-dd") + "','" + User.Name.id  + "')";
                    }
                    else if (cbxFund.SelectedIndex == 4)
                        q = "INSERT INTO capital_general_log (fund_type, amount, date, updated_by) VALUES (2," + tbAmt.Text + ",'" + DateTime.Today.ToString("yyyy-MM-dd") + "','" + User.Name.id  + "')";
                    else if (cbxFund.SelectedIndex == 5)
                        q = "INSERT INTO capital_general_log (fund_type, amount, date, updated_by) VALUES (3," + tbAmt.Text + ",'" + DateTime.Today.ToString("yyyy-MM-dd") + "','" + User.Name.id  + "')";
                    try
                    {
                        conn.Open();
                        MySqlCommand comm = new MySqlCommand(q, conn);
                        comm.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ee)
                    {
                        //MessageBox.Show(ee.Message);
                        conn.Close();
                    }
                    reftomain.Enabled = true;
                    source.reloadbals();
                    this.Close();
                }
                else if (dialogResult == DialogResult.No)
                {

                }
            }
        }

        public static Boolean isNum(string strToCheck)
        {
            Regex rg = new Regex(@"^[0-9]+\.?[0-9]{0,2}$");
            return rg.IsMatch(strToCheck);
        }

        private void cbxFund_SelectedIndexChanged(object sender, EventArgs ee)
        {
            if (cbxFund.SelectedIndex == 0 || cbxFund.SelectedIndex == 3)
                lbl1.Text = "Enter New Rate (%)";
            else
                lbl1.Text = "Enter New Amount";
            if (cbxFund.SelectedIndex == 0)
                tbAmt.Text = a.Replace("%", "");
            else if (cbxFund.SelectedIndex == 1)
                tbAmt.Text = b.Replace(",", "");
            else if (cbxFund.SelectedIndex == 2)
                tbAmt.Text = c.Replace(",", "");
            else if (cbxFund.SelectedIndex == 3)
                tbAmt.Text = d.Replace("%", "");
            else if (cbxFund.SelectedIndex == 4)
                tbAmt.Text = e.Replace(",", "");
            else if (cbxFund.SelectedIndex == 5)
                tbAmt.Text = f.Replace(",", "");
        }

        private Boolean matchvalues()
        {
            if (cbxFund.SelectedIndex == 0 &&
                tbAmt.Text == a.Replace("%", ""))
                return true;
            else if (cbxFund.SelectedIndex == 1 &&
                tbAmt.Text == b.Replace(",", ""))
                return true;
            else if (cbxFund.SelectedIndex == 2 &&
                tbAmt.Text == c.Replace(",", ""))
                return true;
            else if (cbxFund.SelectedIndex == 3 &&
                tbAmt.Text == d.Replace("%", ""))
                return true;
            else if (cbxFund.SelectedIndex == 4 &&
                tbAmt.Text == e.Replace(",", ""))
                return true;
            else if (cbxFund.SelectedIndex == 5 &&
                tbAmt.Text == f.Replace(",", ""))
                return true;
            else return false;
        }

        private Boolean validrates()
        {
            if ((cbxFund.SelectedIndex == 0 || cbxFund.SelectedIndex == 3) &&
                Convert.ToDouble(tbAmt.Text) > 100) 
                return false;
            else return true;
        }
    }

    
}
