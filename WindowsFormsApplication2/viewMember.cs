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
    public partial class viewMember : Form
    {
        Members upper;
        MySqlConnection conn;
        public viewMember(Members x)
        {
            InitializeComponent();
            upper = x;
            conn = new MySqlConnection("Server=localhost;Database=test_db;Uid=root;Pwd=root;");
        }

        private void viewMember_FormClosing(object sender, FormClosingEventArgs e)
        {
            upper.Show();
        }

        private void viewMember_Load(object sender, EventArgs e)
        {
            try
            {

                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT * FROM members where id=" + upper.memberid, conn);
                MySqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                    label2.Text= reader.GetString(1)+", "+reader.GetString(2)+" "+ reader.GetString(3);
                comm = new MySqlCommand("SELECT loans.loanType, loans.date_granted FROM members,loanrequest,loans ON (members.id=loans.member_id AND loanrequest.approval_no=loans.requestid) WHERE members.id=" + upper.memberid, conn);
                MySqlDataAdapter listener = new MySqlDataAdapter(comm);
                DataTable table = new DataTable();
                listener.Fill(table);
                dataGridView1.DataSource = table;
                dataGridView1.Columns["loanType"].HeaderText = "Loan Type";
                dataGridView1.Columns["date_granted"].HeaderText = "Date Granted";
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                conn.Close();
            }
            catch (Exception ee)
            {
            }
        }
    }
}
