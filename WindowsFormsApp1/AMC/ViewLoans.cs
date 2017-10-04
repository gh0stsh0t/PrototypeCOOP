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
    public partial class ViewLoans : Form
    {
        private string filter = "loan_status = 0";
        public MySqlConnection conn;
        public MainForm reftomain;
        private Form popup;
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
        private void Rifrish()
        {
            try
            {
                var tae = new DatabaseConn();
                dataGridView1.DataSource = tae.storedProc("viewloanrequests");
               if(dataGridView1.Rows.Count != 0)
                    dataGridView1.Height = dataGridView1.GetRowDisplayRectangle(0, true).Bottom * dataGridView1.RowCount + dataGridView1.ColumnHeadersHeight;
                dataGridView1.Columns["member_id"].Visible = false;
                dataGridView1.Columns["loan_account_id"].Visible = false;
                dataGridView1.Columns["loan_type"].HeaderText = "Loan Type";
                dataGridView1.Columns["request_type"].HeaderText = "Request Type";
                dataGridView1.Columns["term"].HeaderText = "Term";
                dataGridView1.Columns["orig_amount"].HeaderText = "Amount";
                dataGridView1.Columns["interest_rate"].HeaderText = "Interest Rate";
                conn.Close();
                var x = new DataGridViewButtonColumn();
                x.DefaultCellStyle.BackColor = button1.BackColor;
                var columnIndex = dataGridView1.ColumnCount;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                    dataGridView1.Columns[col.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.ToString());
                conn.Close();
            }
            dataGridView1.ClearSelection();
            convert();
        }

        private void convert()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                //change loan type
                if (dataGridView1.Rows[i].Cells["loan_type"].Value.ToString() == "0")
                {
                    dataGridView1.Rows[i].Cells["loan_type"].Value = "Regular Loan";
                }
                else if (dataGridView1.Rows[i].Cells["loan_type"].Value.ToString() == "1")
                {
                    dataGridView1.Rows[i].Cells["loan_type"].Value = "Emergency Loan";
                }
                else
                {
                    dataGridView1.Rows[i].Cells["loan_type"].Value = "Appliance Loan";
                }

                //change request type
                if (dataGridView1.Rows[i].Cells["request_type"].Value.ToString() == "0")
                {
                    dataGridView1.Rows[i].Cells["request_type"].Value = "New";
                }
                else if (dataGridView1.Rows[i].Cells["request_type"].Value.ToString() == "1")
                {
                    dataGridView1.Rows[i].Cells["request_type"].Value = "Renewal";
                }
                else
                {
                    dataGridView1.Rows[i].Cells["request_type"].Value = "Restructuring";
                }
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                try
                {
                    conn.Open();
                    MySqlCommand comm = new MySqlCommand("SELECT loan_account_id, member_id, concat_ws(', ', family_name, first_name) as Name, cast(loan_type AS char(25)) as loan_type, cast(request_type AS char(25)) as request_type, term, orig_amount, interest_rate FROM loans NATURAL JOIN members where (family_name LIKE '" + tbSearch.Text + "%' OR first_name LIKE '" + tbSearch.Text + "%') AND " + filter, conn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                    var dt = new DataTable();
                    adp.Fill(dt);
                    dataGridView1.DataSource = dt;
                    if (dt.Rows.Count != 0)
                    {
                        dataGridView1.Height = dataGridView1.GetRowDisplayRectangle(0, true).Bottom * dataGridView1.RowCount + dataGridView1.ColumnHeadersHeight;
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                            dataGridView1.Columns[col.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    convert();
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
            }
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                breaker();
                ViewLoans reftomain = this;
                popup = new ApprovalForm(reftomain, int.Parse(dataGridView1.Rows[e.RowIndex].Cells["loan_account_id"].Value.ToString()));
                popup.ShowDialog();
                Rifrish();
            }
            catch(Exception ee)
            {
                //MessageBox.Show(ee.ToString());
            }
        }

    }
}

// CREATE DEFINER=`root`@`localhost` PROCEDURE `viewloanrequests`()
//BEGIN
//	SELECT loan_account_id, member_id, concat_ws(', ', family_name, first_name) as Name, cast(loan_type AS char(25)) as loan_type, cast(request_type AS char(25)) as request_type, term, orig_amount, interest_rate FROM loans NATURAL JOIN members where loan_status = 0;
//END
