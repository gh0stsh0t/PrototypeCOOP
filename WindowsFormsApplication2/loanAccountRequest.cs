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

namespace WindowsFormsApplication2
{
    public partial class loanAccountRequest : Form
    {
        int[] ids;
        Members upper;
        public MySqlConnection conn;
        public loanAccountRequest(Members x)
        {
            InitializeComponent();
            upper = x;
            conn = new MySqlConnection("Server=localhost;Database=test_db;Uid=root;Pwd=root;");
        }

        private void loanAccountRequest_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                //Assuming approval no. is AutoIncrementing
                MySqlCommand comm = new MySqlCommand("INSERT INTO LoanRequest (memberid,type,status,date_requested,amount,purpose) VALUES('"+ ids[comboBox1.SelectedIndex] +"','" + comboBox3.SelectedIndex + "', 0, CURDATE(), '" + textBox5.Text + "','" + textBox8.Text + "')", conn);
                comm.ExecuteNonQuery();

                MySqlDataReader myReader = new MySqlCommand("SELECT MAX(approval_no) as loanR FROM LoanRequest", conn).ExecuteReader();
                myReader.Read();
                int loanReq = int.Parse(myReader.GetString("loanR"));

                comm = new MySqlCommand("INSERT INTO CoMakers (loan_request_id,name,address,company,position) VALUES('" + loanReq + "', '" + textBox1.Text + "','" + textBox2.Text + "', '" + textBox3.Text + "','"+  textBox4.Text + "')", conn);
                comm.ExecuteNonQuery();

                comm = new MySqlCommand("INSERT INTO CoMakers (loan_request_id,name,address,company,position) VALUES('" + loanReq + "', '" + textBox12.Text + "','" + textBox11.Text + "', '" + textBox10.Text + "','" + textBox9.Text + "')", conn);
                comm.ExecuteNonQuery();

                comm = new MySqlCommand("INSERT INTO Loans (request_id,type,term,orig_amount,interest_rate,outstanding_balance) VALUES('" + loanReq + "', '" + comboBox2.SelectedIndex+ "','" + textBox7.Text + "', '" + textBox5.Text + "','" + textBox6.Text + "','"+textBox5.Text+")", conn);
                comm.ExecuteNonQuery();


                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message+"\n also, just let it run. data validation is TODO");
                conn.Close();
            }
        }

        private void loanAccountRequest_Load(object sender, EventArgs e)
        {
            try
            {
                int rowCount = 0;
                MySqlDataReader myReader;
                MySqlCommand myCommand = new MySqlCommand("SELECT * FROM Members where memberstatus=1", conn);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                    rowCount++;
                ids = new int[rowCount];
                rowCount = 0;
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    ids[rowCount++] = int.Parse(myReader.GetString("id"));
                    comboBox1.Items.Add(myReader.GetString("family_name") + ", " + myReader.GetString("first_name"));
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message + " also, just let it run. data validation is TODO");
                conn.Close();
            }
        }
    }
}
