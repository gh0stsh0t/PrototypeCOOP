﻿using System;
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
    public partial class ViewLoans : Form
    {
        private bool advanceSearchVisible = false;
        private string filter = " where member.status=1";
        public MySqlConnection conn;
        public MainForm reftomain;
        public ViewLoans(MainForm parent)
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root;");
            reftomain = parent;
            this.TopLevel = false;
        }
        private void ViewLoans_Load(object sender, EventArgs e)
        {
            Rifrish();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //breaker();
            int memid = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["member_id"].Value.ToString());
            reftomain.innerChild(new ViewProfile(memid, reftomain));

        }
        private void Rifrish()
        {
            try
            {
                var tae = new DatabaseConn();
                string[] taes = {"member_id",
                    "loan_type", "request_type", "term", "orig_amount", "interest_rate"};
                dataGridView1.DataSource = tae.Select("loans", taes).GetQueryData();
                dataGridView1.Height = dataGridView1.GetRowDisplayRectangle(0, true).Bottom * dataGridView1.RowCount + dataGridView1.ColumnHeadersHeight;
                dataGridView1.Columns["member_id"].Visible = false;
                conn.Close();
                var x = new DataGridViewButtonColumn();
                x.DefaultCellStyle.BackColor = button1.BackColor;
                var addColumn = new DataGridViewButtonColumn
                {
                    Name = "loans",
                    Text = "loans",
                    FlatStyle = FlatStyle.Flat,
                    DefaultCellStyle = x.DefaultCellStyle
                };
                var editColumn = new DataGridViewButtonColumn
                {
                    Name = "savings",
                    Text = "savings ",
                    FlatStyle = FlatStyle.Flat,
                    DefaultCellStyle = x.DefaultCellStyle
                };
                var columnIndex = dataGridView1.ColumnCount;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
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
                    MySqlCommand comm = new MySqlCommand("SELECT DISTINCT concat_ws(',',family_name ,first_name) as name FROM membersloanv WHERE family_name LIKE '" + tbSearch.Text + "%' AND" + filter, conn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                    var dt = new DataTable();
                    adp.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Height = dataGridView1.GetRowDisplayRectangle(0, true).Bottom * dataGridView1.RowCount + dataGridView1.ColumnHeadersHeight;
                    conn.Close();
                    dataGridView1.Columns["loans"].DisplayIndex = dataGridView1.ColumnCount - 2;
                    dataGridView1.Columns["savings"].DisplayIndex = dataGridView1.ColumnCount - 1;
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString());
                    conn.Close();
                }
            }
            else
            {
                Rifrish();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["loans"].Index)
            {
                MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells["member_id"].Value.ToString() + " loans");
            }
            else if (e.ColumnIndex == dataGridView1.Columns["savings"].Index)
            {
                MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells["member_id"].Value.ToString() + " savings");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("edit dialog of user " + dataGridView1.Rows[e.RowIndex].Cells["member_id"].Value.ToString() + " " + dataGridView1.Rows[e.RowIndex].Cells["name"].Value.ToString());
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            int memid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["member_id"].Value.ToString());
            if (e.Button.Equals(MouseButtons.Left))
            {
                reftomain.innerChild(new ViewProfile(memid, reftomain));
            }
            else
            {
                reftomain.innerChild(new AddMember(memid));
            }
        }
    }
}
