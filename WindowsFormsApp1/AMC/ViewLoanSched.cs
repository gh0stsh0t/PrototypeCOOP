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
    public partial class ViewLoanSched : Form
    {
        public MySqlConnection conn;
        public MainForm reftomain;
        private Form popup;
        public int counter;
        public List<int> loanids = new List<int>();
        public MySqlDataAdapter holder;

        public ViewLoanSched(MainForm parent)
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root;");
            reftomain = parent;
            this.TopLevel = false;
            Rifrish();
            Rifrish2();

        }

        private void breaker()
        {
            try
            {
                popup.Close();
                popup.Dispose();
            }
            catch
            {
            }

                
        }

        private void Rifrish()
        {
            try
            {
                var tae = new DatabaseConn();
                dataGridView1.DataSource = tae.storedProc("viewloanschedname");
                dataGridView1.Columns["member_id"].Visible = false;
                dataGridView1.Columns["loan_account_id"].Visible = false;
                dataGridView1.Columns["date_granted"].HeaderText = "Date Granted";
                int height = 0;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        height += row.Height;
                    }
                    height += dataGridView1.ColumnHeadersHeight;

                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if(col.Visible == true)
                            width += col.Width;
                }
                    width += dataGridView1.RowHeadersWidth;
                    dataGridView1.ClientSize = new Size(width + 3, height + 2);
                conn.Close();
                var x = new DataGridViewButtonColumn();
                x.DefaultCellStyle.BackColor = button1.BackColor;
                var columnIndex = dataGridView1.ColumnCount;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                    dataGridView1.Columns[col.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
            dataGridView1.ClearSelection();
            loanids.Clear();
            for (int i = 0; i < dataGridView1.RowCount; i++)
                loanids.Add(int.Parse(dataGridView1.Rows[i].Cells["loan_account_id"].Value.ToString()));
        }

        private void Rifrish2()
        {
            if (counter == 0)
                details();
            else
                trans(counter);
        }

        public void details()
        {
            try
            {
                var tae = new DatabaseConn();
                dataGridView2.DataSource = tae.storedProc("viewloansched");
                dataGridView2.Columns["member_id"].Visible = false;
                dataGridView2.Columns["loan_account_id"].Visible = false;
                int height = 0;
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    height += row.Height;
                }
                height += dataGridView2.ColumnHeadersHeight;

                int width = 0;
                width = dataGridView2.Width;
                dataGridView2.ClientSize = new Size(width, height + 2);
                conn.Close();
                var x = new DataGridViewButtonColumn();
                x.DefaultCellStyle.BackColor = button1.BackColor;
                var columnIndex = dataGridView2.ColumnCount;
                dataGridView2.Columns["term"].HeaderText = "Term";
                dataGridView2.Columns["due_date"].HeaderText = "Due Date";
                dataGridView2.Columns["orig_amount"].HeaderText = "Original Amount";
                dataGridView2.Columns["outstanding_balance"].HeaderText = "Beginning Balance as of Jan 02, 2017";
                foreach (DataGridViewColumn col in dataGridView2.Columns)
                    dataGridView2.Columns[col.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
            dataGridView2.ClearSelection();
        }

        public void trans(int mo)
        {
            dataGridView2.DataSource = null;
            if(dataGridView2.Rows.Count != 0)
                dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            try
            {         
                foreach (int n in loanids)
                {
                    var tae = new DatabaseConn();
                    var dt = new DataTable();
                    dt = tae.storedProc("monthview", "loanid", n, "mo", mo);
                    if(dataGridView2.ColumnCount == 0)
                    {
                        foreach (DataColumn column in dt.Columns)
                            dataGridView2.Columns.Add(column.ColumnName, column.ColumnName);
                    }
                    DataRow row = dt.Rows[0];
                    dataGridView2.Rows.Add(row.ItemArray);
                }
                conn.Close();
                var x = new DataGridViewButtonColumn();
                x.DefaultCellStyle.BackColor = button1.BackColor;
                var columnIndex = dataGridView2.ColumnCount;
                foreach (DataGridViewColumn col in dataGridView2.Columns)
                    dataGridView2.Columns[col.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
            dataGridView2.ClearSelection();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView2.ClearSelection();
        }

        private void forward_Click(object sender, EventArgs e)
        {
            if (counter != 12)
            {
                counter++;
                month.Text = counter.ToString();
            }
            Rifrish2();
        }

        private void back_Click(object sender, EventArgs e)
        {
            if (counter != 0)
            {
                counter--;
                month.Text = counter.ToString();
            }
            else
                month.Text = "month";           
            Rifrish2();
        }
    }
}

