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
        private int mid;
        private bool lnameFlag = true;  private string lnamepl = "Last Name"; 
        private bool fnameFlag = true;  private string fnamepl = "First Name"; 
        private bool mnameFlag = true;  private string mnamepl = "Middle Name"; 
        private bool addFlag = true;    private string addpl = "Ex. 123 Strawberry St."; 
        private bool relFlag = true;    private string relpl = "Ex. Roman Catholic"; private string rel;
        private bool contFlag = true;   private string contpl = "Ex. 09991234567"; 
        private bool educFlag = true;   private string educpl = "Ex. College Graduate"; private string educ;
        private bool benFlag = true;    private string benpl = "Ex. Bruce Wayne";   private string ben;
        private bool compFlag = true;   private string comppl = "Ex. Wayne Enterprises";    
        private bool posFlag = true;    private string pospl = "Ex. CEO";   private string pos;
        private bool occFlag = true;    private string occpl = "Ex. Batman";    
        private bool annincFlag = true; private string annincpl = "(in php)";   private string anninc;
        private bool tin1Flag = true; private string tin1pl = "123";    
        private bool tin2Flag = true; private string tin2pl = "456";    
        private bool tin3Flag = true; private string tin3pl = "789";    
        private bool tin4Flag = true; private string tin4pl = "000";    
        private bool boraccFlag = true; private string boraccpl = "Ex. 1a2b";   private string boracc;
        private string annincval;

        public DataTable holder;
        public MySqlConnection databasecon;
        public MySqlDataAdapter listener;
        public MySqlCommand query;
        public AddMember()
        {
            InitializeComponent();
            databasecon = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root;");
        }

        public AddMember(int memid) : this()
        {
            mid = memid;
            filldata();
            neweditform();
            label3.Text = "Edit Member";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        
        private void neweditform()
        {
            lnameFlag = fnameFlag = mnameFlag = addFlag = relFlag = contFlag = educFlag = benFlag = compFlag = posFlag = 
            occFlag = annincFlag = tin1Flag = tin2Flag = tin3Flag = tin4Flag = boraccFlag = dtpDate.Enabled = radioButton1.Enabled = 
            radioButton2.Enabled = txtTIN1.Enabled = txtTIN2.Enabled = txtTIN3.Enabled = txtTIN4.Enabled = txtBORAcc.Enabled =
            dateTimePicker1.Enabled = false;
        }


        private void filldata()
        {
            databasecon.Open();

            query = new MySqlCommand("SELECT * FROM members where member_id = " + mid, databasecon);
            listener = new MySqlDataAdapter(query);
            holder = new DataTable();
            listener.Fill(holder);

            lnamepl = txtLname.Text = holder.Rows[0]["family_name"].ToString();
            txtLname.ForeColor = Color.FromArgb(0, 0, 0);
            fnamepl = txtFname.Text = holder.Rows[0]["first_name"].ToString();
            txtFname.ForeColor = Color.FromArgb(0, 0, 0);
            mnamepl =  txtMname.Text = holder.Rows[0]["middle_name"].ToString();
            txtMname.ForeColor = Color.FromArgb(0, 0, 0);
            addpl = txtAddr.Text = holder.Rows[0]["address"].ToString();
            txtAddr.ForeColor = Color.FromArgb(0, 0, 0);
            DateTime dt = Convert.ToDateTime(holder.Rows[0]["birthdate"].ToString());
            dtpDate.Value = dt;
            relpl = txtReligion.Text = holder.Rows[0]["religion"].ToString();
            txtReligion.ForeColor = Color.FromArgb(0, 0, 0);
            string g = holder.Rows[0]["Gender"].ToString();
            radioButton2.Checked = g == "Female";
            civstat.SelectedIndex = int.Parse(holder.Rows[0]["civil_status"].ToString());
            contpl = txtContNo.Text = holder.Rows[0]["contact_no"].ToString();
            txtContNo.ForeColor = Color.FromArgb(0, 0, 0);
            educpl = txtEduc.Text = holder.Rows[0]["educ_attainment"].ToString();
            txtEduc.ForeColor = Color.FromArgb(0, 0, 0);
            benpl = txtBenificiary.Text = holder.Rows[0]["beneficiary_name"].ToString();
            txtBenificiary.ForeColor = Color.FromArgb(0, 0, 0);
            numericUpDown1.Value = int.Parse(holder.Rows[0]["no_of_dependents"].ToString());
            comppl = txtCompany.Text = holder.Rows[0]["company_name"].ToString();
            txtCompany.ForeColor = Color.FromArgb(0, 0, 0);
            pospl = txtPos.Text = holder.Rows[0]["position"].ToString();
            txtPos.ForeColor = Color.FromArgb(0, 0, 0);
            occpl = txtOcc.Text = holder.Rows[0]["occupation"].ToString();
            txtOcc.ForeColor = Color.FromArgb(0, 0, 0);
            annincpl = txtAnnInc.Text = holder.Rows[0]["annual_income"].ToString();
            txtAnnInc.ForeColor = Color.FromArgb(0, 0, 0);
            string TIN = holder.Rows[0]["tin"].ToString();
            tin1pl = txtTIN1.Text = TIN.Substring(0,3);
            txtTIN1.ForeColor = Color.FromArgb(0, 0, 0);
            tin2pl = txtTIN2.Text = TIN.Substring(3,3);
            txtTIN2.ForeColor = Color.FromArgb(0, 0, 0);
            tin3pl = txtTIN3.Text = TIN.Substring(6, 3);
            txtTIN3.ForeColor = Color.FromArgb(0, 0, 0);
            tin4pl = txtTIN4.Text = TIN.Substring(9, 3);
            txtTIN4.ForeColor = Color.FromArgb(0, 0, 0);
            mtype.SelectedIndex = int.Parse(holder.Rows[0]["type"].ToString());





            databasecon.Close();
        }

        private void AddMember_Load(object sender, EventArgs e)
        {
            this.ActiveControl = label3;
            civstat.SelectedIndex = 0;
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
            ctrlLeave(txtLname, ref lnameFlag, lnamepl);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            ctrlEnter(txtFname, fnameFlag);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            ctrlLeave(txtFname, ref fnameFlag, fnamepl);
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
            ctrlLeave(txtMname, ref mnameFlag, mnamepl);
        }

        private void textBox1_Enter_1(object sender, EventArgs e)
        {
            ctrlEnter(txtAddr, addFlag);
        }

        private void address_Leave(object sender, EventArgs e)
        {
            ctrlLeave(txtAddr, ref addFlag, addpl);
        }

        private void religion_Enter(object sender, EventArgs e)
        {
            ctrlEnter(txtReligion, relFlag);
        }

        private void religion_Leave(object sender, EventArgs e)
        {
            ctrlLeaveNR(txtReligion, ref relFlag, relpl);
        }

        private void contactno_Leave(object sender, EventArgs e)
        {
            contFlag = (txtContNo.Text == "");
            if (!contFlag)
            {
                Regex rg = new Regex(@"^(09\d{9}|\+639\d{9}|\+6382\d{7})$");
                bool cond = rg.IsMatch(txtContNo.Text);
                if (!cond)
                {
                    MessageBox.Show("Please make sure this field contains the valid format");
                    txtContNo.Focus();
                }
            }
            else
            {
                txtContNo.AppendText(contpl);
                txtContNo.ForeColor = Color.LightCoral;
            }
        }

        private void contactno_Enter(object sender, EventArgs e)
        {
            ctrlEnter(txtContNo, contFlag);
        }

        private void educ_Leave(object sender, EventArgs e)
        {
            ctrlLeaveNR(txtEduc, ref educFlag, educpl);
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
            else if ((txtAddr.Text.Length > 0 && txtAddr.Text.Trim().Length == 0) || addFlag)
                x = 4;
            else if ((txtContNo.Text.Length > 0 && txtContNo.Text.Trim().Length == 0) || contFlag)
                x = 5;
            else if ((txtCompany.Text.Length > 0 && txtCompany.Text.Trim().Length == 0) || compFlag)
                x = 6;
            else if ((txtOcc.Text.Length > 0 && txtOcc.Text.Trim().Length == 0) || occFlag)
                x = 7;
            else if ((txtTIN1.Text.Length > 0 && txtTIN1.Text.Trim().Length == 0) || tin1Flag || (txtTIN2.Text.Length > 0 && txtTIN2.Text.Trim().Length == 0) || tin2Flag ||
                    (txtTIN3.Text.Length > 0 && txtTIN3.Text.Trim().Length == 0) || tin3Flag || (txtTIN4.Text.Length > 0 && txtTIN4.Text.Trim().Length == 0) || tin4Flag)
                x = 8;
            if (x == 0)
            {
                if (relFlag)
                    rel = "-";
                else
                    rel = txtReligion.Text;
                if (educFlag)
                    educ = "-";
                else
                    educ = txtEduc.Text;
                if (posFlag)
                    pos = "-";
                else
                    pos = txtPos.Text;
                if (annincFlag)
                    anninc = "0.00";
                else
                    anninc = annincval;
                if (boraccFlag)
                    boracc = "0";
                else
                    boracc = txtBORAcc.Text;
                if (benFlag)
                    ben = "-";
                else
                    ben = txtBenificiary.Text;
                return true;
            }
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
                    case 4:
                        MessageBox.Show("Please fill up the field \"Address\"");
                        break;
                    case 5:
                        MessageBox.Show("Please fill up the field \"Contact Number\"");
                        break;
                    case 6:
                        MessageBox.Show("Please fill up the field \"Company\"");
                        break;
                    case 7:
                        MessageBox.Show("Please fill up the field \"Occupation\"");
                        break;
                    case 8:
                        MessageBox.Show("Please fill up the field \"TIN\"");
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
            ctrlLeaveNR(txtBenificiary, ref benFlag, benpl);
        }

        private void txtCompany_Enter(object sender, EventArgs e)
        {
            ctrlEnter(txtCompany, compFlag);
        }

        private void txtCompany_Leave(object sender, EventArgs e)
        {
            ctrlLeave(txtCompany, ref compFlag, comppl);
        }

        private void txtPos_Enter(object sender, EventArgs e)
        {
            ctrlEnter(txtPos, posFlag);
        }

        private void txtPos_Leave(object sender, EventArgs e)
        {
            ctrlLeaveNR(txtPos, ref posFlag, pospl);
        }
        
        private void ctrlEnter(TextBox txtbox, bool flag)
        {
            if (flag)
            {
                txtbox.Text = "";
                txtbox.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void ctrlLeave(TextBox txtbox, ref bool flag, string pl)
        {
            flag = (txtbox.Text == "");
            if (!flag)
            {
                if (!isAlphaNum(txtbox.Text))
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

        private void ctrlLeaveNR(TextBox txtbox, ref bool flag, string pl)
        {
            flag = (txtbox.Text == "");
            if (!flag)
            {
                if (!isAlphaNum(txtbox.Text))
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

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }


        private void ctrlLeave2(TextBox txtbox, ref bool flag, string pl)
        {
            flag = (txtbox.Text == "");
            if (!flag)
            {
                if (!isNum(txtbox.Text))
                {
                    MessageBox.Show("Please make sure this field contains the valid format");
                    txtbox.Focus();
                }
                else
                {
                    annincval = txtbox.Text;
                    string annincdec;
                    bool state;
                    Regex rg = new Regex(@"^[0-9]+\.[0-9]{2}$");
                    state = rg.IsMatch(annincval);
                    if (state)
                        annincdec = annincval.Substring(annincval.Length -3);
                    txtbox.Text = annincval.Substring(0, annincval.Length - 3);
                    txtbox.Text = Reverse(txtbox.Text);
                    StringBuilder sb = new StringBuilder();
                    for (int i = txtbox.Text.Length; i > 0; i--)
                    {
                        if (i % 3 == 0 && i!=txtbox.Text.Length)
                            sb.Append(',');
                        sb.Append(txtbox.Text[i-1]);
                    }
                    if(state)
                        sb.Append(annincdec);
                    string formatted = sb.ToString();
                    txtbox.Text = formatted;
            }
            else
            {
                txtbox.AppendText(pl);
                txtbox.ForeColor = Color.FromArgb(219, 200, 210);
            }
        }

        public static Boolean isAlphaNum(string strToCheck)
        {
            Regex rg = new Regex(@"^([a-zA-Z0-9]+[\s,\-\.]?)+$");
            return rg.IsMatch(strToCheck);
        }

        public static Boolean isNum(string strToCheck)
        {
            Regex rg = new Regex(@"^[0-9]+\.?[0-9]{2}$");
            return rg.IsMatch(strToCheck);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            
            try
            {
                if(label3.Text.Equals("Add Member"))
                {
                    //validate
                    if (validation())
                    {
                        //add member query
                        databasecon.Open();
                        string bday = dtpDate.Value.ToString("yyyy-MM-dd");
                        string gender;
                        if (radioButton1.Checked) gender = "Male";
                        else gender = "Female";
                        string now = DateTime.Now.ToString("yyyy-MM-dd");
                        string tin = txtTIN1.Text + txtTIN2.Text + txtTIN3.Text + txtTIN4.Text;
                        int dependents = (int)numericUpDown1.Value;

                        query = new MySqlCommand("INSERT members (family_name, first_name, middle_name, birthdate, gender, address, contact_no, occupation, company_name, position, annual_income,"
                        + "tin, educ_attainment, civil_status, religion, no_of_dependents, beneficiary_name, type, status, acceptance_date)"
                        + "VALUES ('" + txtLname.Text + "', '" + txtFname.Text + "', '" + txtMname.Text + "', '" + bday + "', '" + gender + "', '" + txtAddr.Text + "', '" + txtContNo.Text + "', '"
                        + txtOcc.Text + "', '" + txtCompany.Text + "', '" + pos + "', '" + anninc + "', '" + tin + "', '"
                        + educ + "', '" + civstat.SelectedIndex + "', '" + rel + "'," + dependents + ", '" + ben + "', " + mtype.SelectedIndex + ", '0', '" + now + "')", databasecon);
                        query.ExecuteNonQuery();

                        databasecon.Close();
                        MessageBox.Show(txtLname.Text + " sucesfully added!");
                    }
                    else
                    {
                        databasecon.Close();
                    }
                }
                else
                {
                    //get variables that are taken through validation method (not accessed by edit/update)
                    rel = txtReligion.Text;
                    educ = txtEduc.Text;
                    pos = txtPos.Text;
                    anninc = annincval;
                    boracc = txtBORAcc.Text;
                    ben = txtBenificiary.Text;

                    //update query
                    databasecon.Open();
                    string bday = dtpDate.Value.ToString("yyyy-MM-dd");
                    string gender;
                    if (radioButton1.Checked) gender = "Male";
                    else gender = "Female";
                    string now = DateTime.Now.ToString("yyyy-MM-dd");
                    string tin = txtTIN1.Text + txtTIN2.Text + txtTIN3.Text + txtTIN4.Text;
                    int dependents = (int)numericUpDown1.Value;

                    query = new MySqlCommand("UPDATE members SET family_name = '" + txtLname.Text + "', first_name = '" + txtFname.Text + "', middle_name = '" + txtMname.Text + "', "
                        + "birthdate = '" + bday + "', gender = '" + gender + "', address = '" + txtAddr.Text + "', contact_no = '" + txtContNo.Text + "', occupation = '" + txtOcc.Text + "', "
                       + "company_name = '" + txtCompany.Text + "', position = '" + txtPos.Text + "', annual_income = '" + anninc + "',"
                    + "tin = '" + tin + "', educ_attainment = '" + educ + "', civil_status = '" + civstat.SelectedIndex + "', religion = '" + rel + "', no_of_dependents = '" + dependents + "',"
                    + "beneficiary_name = '" + ben + "', type = '" + mtype.SelectedIndex + "', status = 0, acceptance_date = '" + now + "' where member_id = " + mid, databasecon);
                    query.ExecuteNonQuery();

                    databasecon.Close();
                    MessageBox.Show(txtLname.Text + " sucesfully edited!");

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
            ctrlLeave2(txtAnnInc, ref annincFlag, annincpl);
        }

        private void txtAnnInc_Enter(object sender, EventArgs e)
        {
            if (annincFlag)
            {
                txtAnnInc.Text = "";
                txtAnnInc.ForeColor = Color.FromArgb(0, 0, 0);
            }
            else
            {
                txtAnnInc.Text = annincval;
            }
        }

        private void txtOcc_Enter(object sender, EventArgs e)
        {
            ctrlEnter(txtOcc, occFlag);
        }

        private void txtOcc_Leave(object sender, EventArgs e)
        {
            ctrlLeave(txtOcc, ref occFlag, occpl);
        }

        private void txtTIN1_Leave(object sender, EventArgs e)
        {
            tin1Flag = (txtTIN1.Text == "");
            if (!tin1Flag)
            {
                Regex rg = new Regex(@"^\d{3}$");
                bool cond = rg.IsMatch(txtTIN1.Text);
                if (!cond)
                {
                    MessageBox.Show("Please make sure this field contains the valid format");
                    txtTIN1.Focus();
                }
            }
            else
            {
                txtTIN1.AppendText(tin1pl);
                txtTIN1.ForeColor = Color.LightCoral;
            }
        }

        private void txtTIN1_Enter(object sender, EventArgs e)
        {
            ctrlEnter(txtTIN1, tin1Flag);
        }

        private void txtTIN2_Leave(object sender, EventArgs e)
        {
            tin2Flag = (txtTIN2.Text == "");
            if (!tin2Flag)
            {
                Regex rg = new Regex(@"^\d{3}$");
                bool cond = rg.IsMatch(txtTIN2.Text);
                if (!cond)
                {
                    MessageBox.Show("Please make sure this field contains the valid format");
                    txtTIN2.Focus();
                }
            }
            else
            {
                txtTIN2.AppendText(tin2pl);
                txtTIN2.ForeColor = Color.LightCoral;
            }
        }

        private void txtTIN2_Enter(object sender, EventArgs e)
        {
            ctrlEnter(txtTIN2, tin2Flag);
        }

        private void txtTIN3_Leave(object sender, EventArgs e)
        {
            tin3Flag = (txtTIN3.Text == "");
            if (!tin3Flag)
            {
                Regex rg = new Regex(@"^\d{3}$");
                bool cond = rg.IsMatch(txtTIN3.Text);
                if (!cond)
                {
                    MessageBox.Show("Please make sure this field contains the valid format");
                    txtTIN3.Focus();
                }
            }
            else
            {
                txtTIN3.AppendText(tin3pl);
                txtTIN3.ForeColor = Color.LightCoral;
            }
        }

        private void txtTIN3_Enter(object sender, EventArgs e)
        {
            ctrlEnter(txtTIN3, tin3Flag);
        }

        private void txtTIN4_Enter(object sender, EventArgs e)
        {
            ctrlEnter(txtTIN4, tin4Flag);
        }

        private void txtTIN4_Leave(object sender, EventArgs e)
        {
            tin4Flag = (txtTIN4.Text == "");
            if (!tin4Flag)
            {
                Regex rg = new Regex(@"^\d{3}$");
                bool cond = rg.IsMatch(txtTIN4.Text);
                if (!cond)
                {
                    MessageBox.Show("Please make sure this field contains the valid format");
                    txtTIN4.Focus();
                }
            }
            else
            {
                txtTIN4.AppendText(tin4pl);
                txtTIN4.ForeColor = Color.LightCoral;
            }
        }

        private void txtBORAcc_Enter(object sender, EventArgs e)
        {
            ctrlEnter(txtBORAcc, boraccFlag);
        }

        private void txtBORAcc_Leave(object sender, EventArgs e)
        {
            ctrlLeaveNR(txtBORAcc, ref boraccFlag, boraccpl);
        }

        private void AddMember_Click(object sender, EventArgs e)
        {
            this.ActiveControl = label3;
        }

        private void txtAddr_FontChanged(object sender, EventArgs e)
        {

        }
    }
}
