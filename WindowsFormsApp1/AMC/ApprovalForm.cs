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
    public partial class ApprovalForm : Form
    {
        public ViewLoans reftomain;
        public MySqlConnection conn;
        private DatabaseConn _addloanconn;
        public DataTable loanmems;
        public int loanid;

        public ApprovalForm(ViewLoans main, int x)
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root;");
            reftomain = main;
            loanid = x;
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT member_id, concat_ws(', ', family_name, first_name) as Name, loan_type, request_type, term, orig_amount, interest_rate, purpose FROM loans NATURAL JOIN members where loan_status = 0 AND loan_account_id = " + x, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                var dt = new DataTable();
                adp.Fill(dt);
                name.Text = dt.Rows[0]["Name"].ToString();
                if (dt.Rows[0]["loan_type"].ToString() == "0")
                    ltype.Text = "Regular Loan";
                else if (dt.Rows[0]["loan_type"].ToString() == "1")
                    ltype.Text = "Emergency Loan";
                else
                    ltype.Text = "Appliance Loan";
                if (dt.Rows[0]["request_type"].ToString() == "0")
                    rtype.Text = "New";
                else if (dt.Rows[0]["request_type"].ToString() == "1")
                    rtype.Text = "Renewal";
                else
                    rtype.Text = "Restructuring";
                term.Text = dt.Rows[0]["term"].ToString() + " days";
                irate.Text = dt.Rows[0]["interest_rate"].ToString() + " %";
                amount.Text = "Php " + dt.Rows[0]["orig_amount"].ToString();
                purpose.Text = dt.Rows[0]["purpose"].ToString();
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
        }

        private void ApprovalForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("UPDATE loans SET loan_status = 1 WHERE loan_account_id = " + loanid, conn);
                comm.ExecuteNonQuery();
                conn.Close();
                this.Close();
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("UPDATE loans SET loan_status = 4 WHERE loan_account_id = " + loanid, conn);
                comm.ExecuteNonQuery();
                conn.Close();
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
    }
}
