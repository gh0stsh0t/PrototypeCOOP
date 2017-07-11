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
    public partial class AddMember : Form
    {
        private bool lnameFlag = true;
        private bool fnameFlag = true;
        private bool mnameFlag = true;
        private bool addFlag = true;
        private bool relFlag = true;
        private bool contFlag = true;
        private bool educFlag = true;
        private bool benFlag = true;
        private bool compFlag = true;
        private bool posFlag = true;
        private bool occFlag = true;
        private bool annincFlag = true;
        private bool tinFlag = true;
        private bool boraccFlag = true;
        public MySqlConnection databasecon;
        public MySqlDataAdapter listener;
        public MySqlCommand query;
        public AddMember()
        {
            InitializeComponent();
            databasecon = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root;");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void AddMember_Load(object sender, EventArgs e)
        {
            this.ActiveControl = label3;
            civstat.SelectedIndex = 0;
            dependents.SelectedIndex = 0;
            mtype.SelectedIndex = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void lname_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void lname_Enter(object sender, EventArgs e)
        {
            if(lnameFlag)
            {
                txtLname.Text = "";
                txtLname.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void lname_Leave(object sender, EventArgs e)
        {
            lnameFlag = (txtLname.Text == "");
            if (!isAlphaNum(txtLname.Text))
            {
                MessageBox.Show("Please make sure this field contains only letters or numbers");
                txtLname.Focus();
            }
            if (lnameFlag)
            {
                txtLname.AppendText("Last Name");
                txtLname.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (fnameFlag)
            {
                txtFname.Text = "";
                txtFname.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            fnameFlag = (txtFname.Text == "");
            if (!isAlphaNum(txtFname.Text))
            {
                MessageBox.Show("Please make sure this field contains only letters or numbers");
                txtFname.Focus();
            }
            if (fnameFlag)
            {
                txtFname.AppendText("First Name");
                txtFname.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }

        private void fname_TextChanged(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void mname_Enter(object sender, EventArgs e)
        {
            if (mnameFlag)
            {
                txtMname.Text = "";
                txtMname.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void mname_TextChanged(object sender, EventArgs e)
        {

        }

        private void mname_Leave(object sender, EventArgs e)
        {
            mnameFlag = (txtMname.Text == "");
            if (!isAlphaNum(txtMname.Text))
            {
                MessageBox.Show("Please make sure this field contains only letters or numbers");
                txtMname.Focus();
            }
            if (mnameFlag)
            {
                txtMname.AppendText("Middle Name");
                txtMname.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }

        private void textBox1_Enter_1(object sender, EventArgs e)
        {
            if (addFlag)
            {
                txtAddr.Text = "";
                txtAddr.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void address_Leave(object sender, EventArgs e)
        {
            addFlag = (txtAddr.Text == "");
            if (!isAlphaNum(txtAddr.Text))
            {
                MessageBox.Show("Please make sure this field contains only letters or numbers");
                txtAddr.Focus();
            }
            if (addFlag)
            {
                txtAddr.AppendText("Ex. 123 Strawberry St.");
                txtAddr.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }

        private void religion_Enter(object sender, EventArgs e)
        {
            if (relFlag)
            {
                txtReligion.Text = "";
                txtReligion.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void religion_Leave(object sender, EventArgs e)
        {
            relFlag = (txtReligion.Text == "");
            if (!isAlphaNum(txtReligion.Text))
            {
                MessageBox.Show("Please make sure this field contains only letters or numbers");
                txtReligion.Focus();
            }
            if (relFlag)
            {
                txtReligion.AppendText("Ex. Roman Catholic");
                txtReligion.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }

        private void contactno_Leave(object sender, EventArgs e)
        {
            contFlag = (txtContNo.Text == "");
            if (!isAlphaNum(txtContNo.Text))
            {
                MessageBox.Show("Please make sure this field contains only letters or numbers");
                txtContNo.Focus();
            }
            if (contFlag)
            {
                txtContNo.AppendText("Ex. 09991234567");
                txtContNo.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }

        private void contactno_Enter(object sender, EventArgs e)
        {
            if (contFlag)
            {
                txtContNo.Text = "";
                txtContNo.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void educ_Leave(object sender, EventArgs e)
        {
            educFlag = (txtEduc.Text == "");
            if (!isAlphaNum(txtEduc.Text))
            {
                MessageBox.Show("Please make sure this field contains only letters or numbers");
                txtEduc.Focus();
            }
            if (educFlag)
            {
                txtEduc.AppendText("Ex. College Graduate");
                txtEduc.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }

        private void educ_Enter(object sender, EventArgs e)
        {
            if (educFlag)
            {
                txtEduc.Text = "";
                txtEduc.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private bool validation()
        {
            int x = 0;
            if ((txtLname.Text.Length > 0 && txtLname.Text.Trim().Length == 0) || lnameFlag)
                x = 1;
            else if ((txtMname.Text.Length > 0 && txtMname.Text.Trim().Length == 0) || mnameFlag)
                x = 2;
            else if ((txtFname.Text.Length > 0 && txtFname.Text.Trim().Length == 0) || fnameFlag)
                x = 3;
            if (x == 0) return true;
            else
            {
                switch(x)
                {
                    case 1:
                        MessageBox.Show("Please fill up the field \"Last Name\"");
                        break;
                    case 2:
                        MessageBox.Show("Please fill up the field \"First Name\"");
                        break;
                    case 3:
                        MessageBox.Show("Please fill up the field \"Middle Name\"");
                        break;
                }
                return false;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (benFlag)
            {
                txtBenificiary.Text = "";
                txtBenificiary.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void dependents_Leave(object sender, EventArgs e)
        {

        }

        private void benificiary_Leave(object sender, EventArgs e)
        {
            benFlag = (txtBenificiary.Text == "");
            if (!isAlphaNum(txtBenificiary.Text))
            {
                MessageBox.Show("Please make sure this field contains only letters or numbers");
                txtBenificiary.Focus();
            }
            if (benFlag)
            {
                txtBenificiary.AppendText("Ex. Bruce Wayne");
                txtBenificiary.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }

        private void txtCompany_Enter(object sender, EventArgs e)
        {
            if (compFlag)
            {
                txtCompany.Text = "";
                txtCompany.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void txtCompany_Leave(object sender, EventArgs e)
        {
            compFlag = (txtCompany.Text == "");
            if (!isAlphaNum(txtCompany.Text))
            {
                MessageBox.Show("Please make sure this field contains only letters or numbers");
                txtCompany.Focus();
            }
            if (compFlag)
            {
                txtCompany.AppendText("Ex. Wayne Enterprises");
                txtCompany.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }

        private void txtPos_Enter(object sender, EventArgs e)
        {
            if (posFlag)
            {
                txtPos.Text = "";
                txtPos.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void txtPos_Leave(object sender, EventArgs e)
        {
            posFlag = (txtPos.Text == "");
            if (!isAlphaNum(txtPos.Text))
            {
                MessageBox.Show("Please make sure this field contains only letters or numbers");
                txtPos.Focus();
            }
            if (posFlag)
            {
                txtPos.AppendText("Ex. CEO");
                txtPos.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }
        public static Boolean isAlphaNum(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9\s,]*$");
            return rg.IsMatch(strToCheck);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                if (validation())
                {
                    databasecon.Open();
                    query = new MySqlCommand("INSERT members (family_name)"
                    + "VALUES ('" + txtLname.Text + "')", databasecon);
                    ////////////////////////////
                    query.ExecuteNonQuery();

                    databasecon.Close();
                    MessageBox.Show(txtLname.Text + " sucesfully added!");
                }
                else
                {
                    databasecon.Close();
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                databasecon.Close();
            }

        }
    }
}
