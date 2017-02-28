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
    public partial class Members : Form
    {
        MainMenu upper;
        public MySqlConnection conn;
        public MySqlCommand comm;
        public MySqlDataAdapter listener;
        public DataTable table;
        public int memberid;

        public Members(MainMenu x)
        {
            InitializeComponent();
            upper = x;
            conn = new MySqlConnection("Server=10.4.42.104;Database=test_db;Uid=root;Pwd=root;");
        }

        private void Members_FormClosing(object sender, FormClosingEventArgs e)
        {
            upper.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addMember lower = new addMember(this);
            lower.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //dataGridView1.SelectedRows
            viewMember lower = new viewMember(this);
            lower.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loanAccountRequest lower = new loanAccountRequest(this);
            lower.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("is Deactivated");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Report generated");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Members_Load(object sender, EventArgs e)
        {
            comm = new MySqlCommand("SELECT * FROM members", conn);
            listener = new MySqlDataAdapter(comm);
            table = new DataTable();
            listener.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["family_name"].HeaderText = "Family Name";
            dataGridView1.Columns["first_name"].HeaderText = "First Name";
            dataGridView1.Columns["middle_name"].HeaderText = "Middle Name";
            foreach (DataGridViewColumn column in dataGridView1.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                memberid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());
                MessageBox.Show(memberid.ToString());
            }
            catch (ArgumentOutOfRangeException ee)
            {

            }
        }
    }
}
