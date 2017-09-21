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
                MySqlCommand comm = new MySqlCommand("SELECT member_id, concat_ws(', ', family_name, first_name) as Name, loan_type, request_type, term, orig_amount, interest_rate FROM loans NATURAL JOIN members where loan_status = 0 AND loan_account_id = " + x, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                var dt = new DataTable();
                adp.Fill(dt);
                name.Text = dt.Rows[0]["Name"].ToString();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void ApprovalForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
