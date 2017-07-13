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
        private bool lnameFlag = true;  private string lnamepl = "Last Name";
        private bool fnameFlag = true;  private string fnamepl = "First Name";
        private bool mnameFlag = true;  private string mnamepl = "Middle Name";
        private bool addFlag = true;    private string addpl = "Ex. 123 Strawberry St.";
        private bool relFlag = true;    private string relpl = "Ex. Roman Catholic";
        private bool contFlag = true;   private string contpl = "Ex. 09991234567";
        private bool educFlag = true;   private string educpl = "Ex. College Graduate";
        private bool benFlag = true;    private string benpl = "Ex. Bruce Wayne";
        private bool compFlag = true;   private string comppl = "Ex. Wayne Enterprises";
        private bool posFlag = true;    private string pospl = "Ex. CEO";
        private bool occFlag = true;    private string occpl = "Ex. Batman";
        private bool annincFlag = true; private string annincpl = "(in php)";
        private bool tin1Flag = true; private string tin1pl = "123";
        private bool tin2Flag = true; private string tin2pl = "456";
        private bool tin3Flag = true; private string tin3pl = "789";
        private bool tin4Flag = true; private string tin4pl = "000";
        private bool boraccFlag = true; private string boraccpl = "Ex. 1a2b";

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
            ctrlEnter(txtLname, lnameFlag);
        }

        private void lname_Leave(object sender, EventArgs e)
        {
            ctrlLeave(txtLname, lnameFlag, lnamepl);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            ctrlEnter(txtFname, fnameFlag);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            ctrlLeave(txtFname, fnameFlag, fnamepl);
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
            ctrlEnter(txtMname, mnameFlag);
        }

        private void mname_TextChanged(object sender, EventArgs e)
        {

        }

        private void mname_Leave(object sender, EventArgs e)
        {
            ctrlLeave(txtMname, mnameFlag, mnamepl);
        }

        private void textBox1_Enter_1(object sender, EventArgs e)
        {
            ctrlEnter(txtAddr, addFlag);
        }

        private void address_Leave(object sender, EventArgs e)
        {
            ctrlLeave(txtAddr, addFlag, addpl);
        }

        private void religion_Enter(object sender, EventArgs e)
        {
            ctrlEnter(txtReligion, relFlag);
        }

        private void religion_Leave(object sender, EventArgs e)
        {
            ctrlLeaveNR(txtReligion, relFlag, relpl);
        }

        private void contactno_Leave(object sender, EventArgs e)
        {
            ctrlLeave2(txtContNo, contFlag, contpl);
        }

        private void contactno_Enter(object sender, EventArgs e)
        {
            ctrlEnter(txtContNo, contFlag);
        }

        private void educ_Leave(object sender, EventArgs e)
        {
            ctrlLeaveNR(txtEduc, educFlag, educpl);
        }

        private void educ_Enter(object sender, EventArgs e)
        {
            ctrlEnter(txtEduc, educFlag);
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
            ctrlEnter(txtBenificiary, benFlag);
        }

        private void dependents_Leave(object sender, EventArgs e)
        {

        }

        private void benificiary_Leave(object sender, EventArgs e)
        {
            ctrlLeaveNR(txtBenificiary, benFlag, benpl);
        }

        private void txtCompany_Enter(object sender, EventArgs e)
        {
            ctrlEnter(txtCompany, compFlag);
        }

        private void txtCompany_Leave(object sender, EventArgs e)
        {
            ctrlLeave(txtCompany, compFlag, comppl);
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
                txtPos.ForeColor = Color.LightCoral;
            }
        }
        
        private void ctrlEnter(TextBox txtbox, bool flag)
        {
            if (flag)
            {
                txtbox.Text = "";
                txtbox.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void ctrlLeave(TextBox txtbox, bool flag, string pl)
        {
            flag = (txtbox.Text == "");
            if (!flag)
            {
                if (!isNum(txtbox.Text))
                {
                    MessageBox.Show("Please make sure this field contains the valid format");
                    txtbox.Focus();
                }
            }
            else
            {
                txtbox.AppendText(pl);
                txtbox.ForeColor = Color.LightCoral;
            }
        }

        private void ctrlLeaveNR(TextBox txtbox, bool flag, string pl)
        {
            flag = (txtbox.Text == "");
            if (!flag)
            {
                if (!isNum(txtbox.Text))
                {
                    MessageBox.Show("Please make sure this field contains the valid format");
                    txtbox.Focus();
                }
            }
            else
            {
                txtbox.AppendText(pl);
                txtbox.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }


        private void ctrlLeave2(TextBox txtbox, bool flag, string pl)
        {
            annincFlag = (txtAnnInc.Text == "");
            if (!annincFlag)
            {
                if (!isNum(txtAnnInc.Text))
                {
                    MessageBox.Show("Please make sure this field contains the valid format");
                    txtAnnInc.Focus();
                }
            }
            else
            {
                txtAnnInc.AppendText("(in php)");
                txtAnnInc.ForeColor = Color.LightCoral;
            }
        }

        public static Boolean isAlphaNum(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9]+[\s,\-]?[a-zA-Z0-9]+$");
            return rg.IsMatch(strToCheck);
        }

        public static Boolean isNum(string strToCheck)
        {
            Regex rg = new Regex(@"^[0-9]+\.?[0-9]{0,2}$");
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

        private void txtAnnInc_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtAnnInc_Enter(object sender, EventArgs e)
        {
            
        }
    }
}
