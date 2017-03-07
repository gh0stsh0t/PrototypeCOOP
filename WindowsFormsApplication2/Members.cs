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
        Form lower;
        public MySqlConnection conn;
        public MySqlCommand comm;
        public MySqlDataAdapter listener;
        public DataTable table;
        public int memberid;
        string name;
        public Members(MainMenu x)
        {
            InitializeComponent();
            upper = x;
            conn = new MySqlConnection("Server=localhost;Database=test_db;Uid=root;Pwd=root;");
        }

        private void Members_FormClosing(object sender, FormClosingEventArgs e)
        {
            upper.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lower = new addMember(this,0);
            lower.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lower = new addMember(this, 1);
            lower.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lower = new viewMember(this);
            lower.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lower = new loanAccountRequest(this);
            lower.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(name+" is Deactivated");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Report generated");
        }
  

        private void Members_Load(object sender, EventArgs e)
        {
            refrehasjkda();
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                memberid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());
                name = dataGridView1.Rows[e.RowIndex].Cells["family_name"].Value.ToString();
                button1.Enabled = true;
                button4.Enabled = true;
                button3.Enabled = true;
                button6.Enabled = true;
            }
            catch (ArgumentOutOfRangeException ee)
            {

            }
        }
        public void refrehasjkda()
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
    }
}
