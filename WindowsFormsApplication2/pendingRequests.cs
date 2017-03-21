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
    public partial class pendingRequests : Form
    {
        public int reqID;
        Members upper;
        MySqlConnection conn;
        public pendingRequests(Members x)
        {
            InitializeComponent();
            upper = x;
        }

        private void pendingRequests_Load(object sender, EventArgs e)
        {
            repaintTable();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                reqID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["approval_no"].Value.ToString());
                button1.Enabled = true;
                button2.Enabled = true;
                
            }
            catch (Exception eee)
            {
                conn.Close();
            }
        }
        private void repaintTable()
        {
            try
            {
                MySqlCommand comm = new MySqlCommand("SELECT members.family_name,member.first_name,loanrequest.amount,loanrequest.purpose,loanrequest.approval_no FROM members,loanRequest where RequestStatus=0", conn);
                MySqlDataAdapter listener = new MySqlDataAdapter(comm);
                DataTable table = new DataTable();
                listener.Fill(table);
                dataGridView1.DataSource = table;
                dataGridView1.Columns["family_name"].HeaderText = "Family Name";
                dataGridView1.Columns["first_name"].HeaderText = "Fir.st Name";
                dataGridView1.Columns["amount"].HeaderText = "Amount";
                dataGridView1.Columns["purpose"].HeaderText = "Purpose";
                dataGridView1.Columns["approval_no"].Visible = false;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message + " also, just let it run. data validation is TODO");
                conn.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            approveOr(1);
            repaintTable();
        }
        private void approveOr(int x)
        {
            conn.Open();
            MySqlCommand comm = new MySqlCommand("UPDATE loanrequest SET memberstatus="+x+" where id=" + reqID, conn);
            comm.ExecuteNonQuery();
            if (x == 1)
                comm = new MySqlCommand("UPDATE loans SET date_granted=CURDATE(),status=0 WHERE requestid=" + reqID, conn);
            else
                comm = new MySqlCommand("DELETE from loans WHERE requestid=" + reqID, conn);
            comm.ExecuteNonQuery();
            conn.Close();
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            approveOr(2);
            repaintTable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pendingRequests_FormClosing(object sender, FormClosingEventArgs e)
        {
            upper.Show();
        }
    }
}
