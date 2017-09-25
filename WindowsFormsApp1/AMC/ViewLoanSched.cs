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
        public MainForm reftomain;
        private Form popup;
        public int counter;
        public List<int> loanids = new List<int>();
        public MySqlDataAdapter holder;

        public ViewLoanSched(MainForm parent)
        {
            InitializeComponent();
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
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                    dataGridView1.Columns[col.Name].SortMode = DataGridViewColumnSortMode.NotSortable;

                dataGridView3.DataSource = tae.storedProc("viewloanstotal");
                dataGridView3.Columns["member_id"].Visible = false;
                height = 0;
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    height += row.Height;
                }
                height += dataGridView3.ColumnHeadersHeight;

                width = 0;
                foreach (DataGridViewColumn col in dataGridView3.Columns)
                {
                    if (col.Visible == true)
                        width += col.Width;
                }
                width += dataGridView3.RowHeadersWidth;
                dataGridView3.ClientSize = new Size(width + 3, height + 2);
                foreach (DataGridViewColumn col in dataGridView3.Columns)
                    dataGridView3.Columns[col.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
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
            dataGridView2.DataSource = null;
            if (dataGridView2.Rows.Count != 0)
                dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataGridView2.Refresh();
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

                int width = dataGridView2.Width;
                dataGridView2.ClientSize = new Size(width, height);
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
                var x = new DataGridViewButtonColumn();
                x.DefaultCellStyle.BackColor = button1.BackColor;
                var columnIndex = dataGridView2.ColumnCount;
                foreach (DataGridViewColumn col in dataGridView2.Columns)
                    dataGridView2.Columns[col.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
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
            if(counter == 0)
                month.Text = "month";           
            Rifrish2();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            back.Visible = !back.Visible;
            month.Visible = back.Visible;
            forward.Visible = back.Visible;
            dataGridView2.Visible = back.Visible;
            dataGridView3.Visible = !back.Visible;
        }
    }
}

