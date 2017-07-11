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

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                if(validation())
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
                    MessageBox.Show("Please insert last name.");
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                databasecon.Close();
            }

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
            if (relFlag)
            {
                txtReligion.AppendText("Ex. Roman Catholic");
                txtReligion.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }

        private void contactno_Leave(object sender, EventArgs e)
        {
            contFlag = (txtContNo.Text == "");
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
            if (lnameFlag)
                return false;
            else if(txtLname.Text.Length > 0 && txtLname.Text.Trim().Length == 0)
                return false;
            return true;
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
            if (posFlag)
            {
                txtPos.AppendText("Ex. CEO");
                txtPos.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }
    }
}
