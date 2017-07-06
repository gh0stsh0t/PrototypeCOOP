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
                lname.Text = "";
                lname.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void lname_Leave(object sender, EventArgs e)
        {
            lnameFlag = (lname.Text == "");
            if (lnameFlag)
            {
                lname.AppendText("Last Name");
                lname.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (fnameFlag)
            {
                fname.Text = "";
                fname.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            fnameFlag = (fname.Text == "");
            if (fnameFlag)
            {
                fname.AppendText("First Name");
                fname.ForeColor = Color.FromArgb(219, 200, 210);
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

                databasecon.Open();
                query = new MySqlCommand("INSERT members (family_name)"
                + "VALUES ('"+ lname.Text + "')", databasecon);
                ////////////////////////////
                query.ExecuteNonQuery();

                databasecon.Close();
                MessageBox.Show(lname.Text + " sucesfully added!");

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
                mname.Text = "";
                mname.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void mname_TextChanged(object sender, EventArgs e)
        {

        }

        private void mname_Leave(object sender, EventArgs e)
        {
            mnameFlag = (mname.Text == "");
            if (mnameFlag)
            {
                mname.AppendText("Middle Name");
                mname.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }

        private void textBox1_Enter_1(object sender, EventArgs e)
        {
            if (addFlag)
            {
                address.Text = "";
                address.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void address_Leave(object sender, EventArgs e)
        {
            addFlag = (address.Text == "");
            if (addFlag)
            {
                address.AppendText("Ex. 123 Strawberry St.");
                address.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }

        private void religion_Enter(object sender, EventArgs e)
        {
            if (relFlag)
            {
                religion.Text = "";
                religion.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void religion_Leave(object sender, EventArgs e)
        {
            relFlag = (religion.Text == "");
            if (relFlag)
            {
                religion.AppendText("Ex. Roman Catholic");
                religion.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }

        private void contactno_Leave(object sender, EventArgs e)
        {
            contFlag = (contactno.Text == "");
            if (contFlag)
            {
                contactno.AppendText("Ex. 09991234567");
                contactno.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }

        private void contactno_Enter(object sender, EventArgs e)
        {
            if (contFlag)
            {
                contactno.Text = "";
                contactno.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void educ_Leave(object sender, EventArgs e)
        {
            educFlag = (educ.Text == "");
            if (educFlag)
            {
                educ.AppendText("Ex. College Graduate");
                educ.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }

        private void educ_Enter(object sender, EventArgs e)
        {
            if (educFlag)
            {
                educ.Text = "";
                educ.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }
    }
}
