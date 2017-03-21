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

        Members upper;
        MySqlConnection conn;
        public pendingRequests(Members x)
        {
            InitializeComponent();
            upper = x;
        }

        private void pendingRequests_Load(object sender, EventArgs e)
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
    }
}
