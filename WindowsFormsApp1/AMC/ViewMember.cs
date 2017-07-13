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
        public partial class ViewMember : Form
        {
            public MySqlConnection conn;
            public ViewMember()
            {
                InitializeComponent();
                conn = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root;");
            }

            private void label4_Click(object sender, EventArgs e)
            {
                panel1.Visible = !panel1.Visible;
            }
            private void Animations()
            {
                opaque.Location = new Point(0, 0);
                opaque.Size = this.Size;
                popupEnabler.Start();
            }
            private void ViewMember_Load(object sender, EventArgs e)
            {
                Rifrish();
            }
            private void Rifrish()
            {
                try
                {
                    conn.Open();
                    var comm = new MySqlCommand("SELECT concat(family_name, ', ',first_name) as Name FROM members ", conn);
                    var adp = new MySqlDataAdapter(comm);
                    var dt = new DataTable();
                    adp.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Height = dataGridView1.GetRowDisplayRectangle(0, true).Bottom * dataGridView1.RowCount + dataGridView1.ColumnHeadersHeight;
                    dataGridView1.Columns["member_id"].Visible = false;
                    conn.Close();
                    var addColumn = new DataGridViewButtonColumn
                    {
                        Name = "loans",
                        Text = "loans "
                    };
                    var editColumn = new DataGridViewButtonColumn
                    {
                        Name = "savings",
                        Text = "savings "
                    };
                    var columnIndex = dataGridView1.ColumnCount;
                    if (dataGridView1.Columns["add_column"] == null)
                    {
                        dataGridView1.Columns.Add(addColumn);
                        dataGridView1.Columns.Add(editColumn);
                    }
                    dataGridView1.CellContentClick += dataGridView1_CellContentClick;
                /*
              buttonArray= new Button[2][dataGridView1.RowCount]
             */
                // MessageBox.Show("" + dataGridView1.RowCount+ dataGridView1.ColumnHeadersHeight);
            }
                catch (Exception ee)
                {
                    //MessageBox.Show(ee.ToString());
                    conn.Close();
                }
                dataGridView1.ClearSelection();
            }

            private void tbSearch_TextChanged(object sender, EventArgs e)
            {
                if (tbSearch.Text != "")
                {
                    try
                    {
                        conn.Open();
                        MySqlCommand comm = new MySqlCommand("SELECT * FROM members WHERE family_name LIKE '" + tbSearch.Text + "%'", conn);
                        MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                        var dt = new DataTable();
                        adp.Fill(dt);
                        dataGridView1.DataSource = dt;
                        dataGridView1.Height = dataGridView1.GetRowDisplayRectangle(0, true).Bottom * dataGridView1.RowCount + dataGridView1.ColumnHeadersHeight;
                        conn.Close();
                    }
                    catch (Exception ee)
                    {
                        //MessageBox.Show(ee.ToString());
                        conn.Close();
                    }
                }
                else
                {
                    Rifrish();
                    Rifrish();
                }
            }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["loans"].Index)
            {
                MessageBox.Show("LEMAOOO");
            }
            else if (e.ColumnIndex == dataGridView1.Columns["savings"].Index)
            {
                MessageBox.Show("LEMFAO");
            }
        }

        private void popupEnabler_Tick(object sender, EventArgs e)
        {
            //opaque.Opactiy
        }
    }
}
