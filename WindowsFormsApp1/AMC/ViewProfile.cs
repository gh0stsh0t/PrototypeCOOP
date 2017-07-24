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
    public partial class ViewProfile : Form
    {
        public MySqlConnection conn;

        public ViewProfile()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd='';");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ViewProfile_Load(object sender, EventArgs e)
        {
            load_member();
        }

        private void load_member()
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT * FROM members WHERE member_id = 1", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    lblName.Text = dt.Rows[0]["family_name"].ToString() + ", " + dt.Rows[0]["first_name"].ToString() + " " + dt.Rows[0]["middle_name"].ToString();
                    DateTime accept_date = new DateTime();
                    accept_date = Convert.ToDateTime(dt.Rows[0]["acceptance_date"].ToString());
                    lblAcceptDate.Text = accept_date.Date.ToString("MMMM dd, yyyy");
                    lblAcceptNo.Text = dt.Rows[0]["acceptance_no"].ToString();
                    lblAddress.Text = dt.Rows[0]["address"].ToString();
                    lblBenef.Text = dt.Rows[0]["beneficiary_name"].ToString();
                    DateTime bdate = new DateTime();
                    bdate = Convert.ToDateTime(dt.Rows[0]["birthdate"].ToString());
                    lblBirthday.Text = bdate.Date.ToString("MMMM dd, yyyy");
                    string civstat = dt.Rows[0]["civil_status"].ToString();
                    if (civstat == "0")
                        lblCivStat.Text = "SINGLE";
                    else if (civstat == "1")
                        lblCivStat.Text = "MARRIED";
                    else if (civstat == "2")
                        lblCivStat.Text = "DIVORCED";
                    else if (civstat == "3")
                        lblCivStat.Text = "WIDOWED";
                    lblCompany.Text = dt.Rows[0]["company_name"].ToString();
                    lblContact.Text = dt.Rows[0]["contact_no"].ToString();
                    lblDepend.Text = dt.Rows[0]["no_of_dependents"].ToString();
                    lblEduc.Text = dt.Rows[0]["educ_attainment"].ToString();
                    lblGender.Text = dt.Rows[0]["gender"].ToString();
                    lblIncome.Text = dt.Rows[0]["annual_income"].ToString();
                    lblOccup.Text = dt.Rows[0]["occupation"].ToString();
                    lblPosition.Text = dt.Rows[0]["position"].ToString();
                    lblReligion.Text = dt.Rows[0]["religion"].ToString();
                    string stat = dt.Rows[0]["status"].ToString();
                    if (stat == "0")
                        lblStatus.Text = "INACTIVE";
                    else if (stat == "1")
                        lblStatus.Text = "ACTIVE";
                    lblTin.Text = dt.Rows[0]["tin"].ToString();
                    string type = dt.Rows[0]["type"].ToString();
                    if (type == "0")
                        lblType.Text = "REGULAR";
                    else if (type == "1")
                        lblType.Text = "ASSOCIATE";
                    // ANOTHER TYPE????
                   

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

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
