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
using System.Collections;
using System.Drawing.Printing;
using System.Globalization;


namespace AMC
{
    public partial class Reports : Form
    {
        public MySqlConnection conn; //connection
        public string myType;
        Test func = new Test();
        float totalAmount = 0;
        float totalPrincipal = 0;
        float totalInterest = 0;
        float totalPenalty = 0;
        string mySelectSQLChild = "";
        string myOrderSQL = "";
        string[] myDatesSQL = new string[3];
        string myID = "";
        bool mouseDown; //boolean for mousedown
        Point lastLocation; //variable for the last location of the mouse
        int myCounter;
        MainForm src;

        public Reports(MainForm main)
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root"); //connection
            src = main;
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            int myScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            int myScreenHeight = Screen.PrimaryScreen.Bounds.Height;
            /*
                        //Change size and location of the form
                        this.Size = new Size(530, 300);
                        this.Location = new Point((myScreenWidth - this.Width) / 2, (myScreenHeight - this.Height) / 2);
                        //Put Print button in the lowermiddle portion of the form
                        btnPrintRep.Location = new Point((this.Width - btnPrintRep.Width) / 2, 225);
                        btnPrintRep.Visible = true;
                        datagridTableParent.Visible = false;
                        datagridTableChild.Visible = false;
                        */
            //set Dates
            monCalFrom.Location = txtDateFrom.Location;
            txtDateFrom.Text = DateTime.Now.ToString();
            txtDateTo.Text = DateTime.Now.ToString();
            monCalFrom.MinDate = Convert.ToDateTime("2/2/2017");
            monCalFrom.MaxDate = DateTime.Now;
            monCalTo.MaxDate = DateTime.Now;
            hideall();
            datagridTableChild.RowTemplate.Height = 60;
            populateComboReport();
            comboReports.Focus();
            capitalDTP.Format = DateTimePickerFormat.Custom;
            capitalDTP.CustomFormat = "yyyy";
            capitalDTP.ShowUpDown = true;
            capitalDTP.Location = txtDateFrom.Location;
            capitalBtn.Location = txtDateTo.Location;
            savingsMDTP.Location = txtDateFrom.Location;
            savingsMDTP.Format = DateTimePickerFormat.Custom;
            savingsMDTP.CustomFormat = "MM/yyyy";
            hideall();
        }

        private void showdates()
        {         
            monCalFrom.Visible = true;
            monCalTo.Visible = true;
            txtDateFrom.Visible = true;
            txtDateTo.Visible = true;
        }
        private void hideall()
        {
            savingsMDTP.Visible = false;
            monCalFrom.Visible = false;
            monCalTo.Visible = false;
            txtDateFrom.Visible = false;
            txtDateTo.Visible = false;
            capitalDTP.Visible = false;
            capitalBtn.Visible = false;
        }

        private void capitals()
        {
            hideall();
            capitalDTP.Visible = true;
            capitalBtn.Visible = true;
        }

        private void savingsmonth()
        {
            hideall();
            savingsMDTP.Visible = true;
            capitalBtn.Visible = true;
        }

        private void savingsyear()
        {
            hideall();
            capitalDTP.Visible = true;
            capitalBtn.Visible = true;
        }


        //mouse handling
        private void formReports_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true; //sets mousedown to true
            lastLocation = e.Location; //gets the location of the form and sets it to lastlocation
        }

        private void formReports_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown) //if mouseDown is true, point to the last location of the mouse
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y); //gets the coordinates of the location of the mouse
                this.Update(); //updates the location of the mouse
            }
        }

        private void formReports_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false; //sets mousedown to false
        }

        private void btnPrintRep_Click(object sender, EventArgs e)
        {
            goPrint();
        }

        //Date Handling - put all dates here
        private void txtDateFrom_Enter(object sender, EventArgs e)
        {
            monCalFrom.Location = txtDateFrom.Location;
            monCalFrom.Visible = true;
            monCalFrom.Focus();
        }
        private void txtDateTo_Enter(object sender, EventArgs e)
        {
            monCalTo.Location = txtDateTo.Location;
            monCalTo.Visible = true;
            monCalTo.Focus();
        }
        private void monCalFrom_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtDateFrom.Text = monCalFrom.SelectionRange.Start.ToShortDateString();
            monCalTo.MinDate = monCalFrom.SelectionStart;
            monCalFrom.Visible = false;
            txtDateTo.Focus();
            maketheDataGrid();
        }

        private void monCalTo_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtDateTo.Text = monCalTo.SelectionRange.Start.ToShortDateString();
            monCalTo.Visible = false;
            maketheDataGrid();
        }

        //Controls Handling
        private void comboReports_MouseClick(object sender, MouseEventArgs e)
        {
            maketheDataGrid();
        }

        private void txtDateFrom_Leave(object sender, EventArgs e)
        {
            getSQLDates();
            maketheDataGrid();
        }

        private void txtDateTo_Leave(object sender, EventArgs e)
        {
            getSQLDates();
            maketheDataGrid();
        }

        private void comboReports_Leave(object sender, EventArgs e)
        {
            getSQLDates();
            maketheDataGrid();
        }

        private void populateComboReport()
        {
            comboReports.Items.Clear();
            comboReports.Items.Add("Active Loan Accounts");
            //comboReports.Items.Add("Loan Transactions");
            comboReports.Items.Add("All Members");
            comboReports.Items.Add("CBU Report");
            comboReports.Items.Add("Savings (Month)");
            comboReports.Items.Add("Savings (Year)");
        }

        private void comboReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            maketheDataGrid();
        }

        private void cleardgv()
        {
            datagridTableChild.DataSource = null;
            datagridTableChild.Rows.Clear();
            datagridTableChild.Columns.Clear();
            datagridTableParent.DataSource = null;
        }

        private void maketheDataGrid()
        {
            int myRowIndex = comboReports.SelectedIndex;
            switch (myRowIndex)
            {
                case 0:     //All Loan Accounts
                    {
                        hideall();
                        populatedatagridParent("viewloanscomplete", 0);
                        datagridTableParent.Columns[0].Visible = false;
                        datagridTableParent.Columns[1].Visible = false;
                        datagridTableParent.Columns[3].Name = "Date Granted";
                        datagridTableParent.Columns[4].Name = "Term";
                        datagridTableParent.Columns[5].Name = "Due Date";
                        datagridTableParent.Columns[6].Name = "Original Amount";
                        datagridTableParent.Columns[7].Name = "Outstanding Balance";
                        //set up datagridchild columns
                        datagridTableChild.Rows.Clear();
                        datagridTableChild.ColumnCount = 6;
                        datagridTableChild.ColumnHeadersVisible = true;
                        datagridTableChild.Columns[0].Name = "Name";
                        datagridTableChild.Columns[1].Name = "Date Granted";
                        datagridTableChild.Columns[2].Name = "Term";
                        datagridTableChild.Columns[3].Name = "Due Date";
                        datagridTableChild.Columns[4].Name = "Original Amount";
                        datagridTableChild.Columns[5].Name = "Outstanding Balance";
                        datagridTableChild.ColumnHeadersDefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
                        datagridTableChild.Columns[0].Width = 200;
                        datagridTableChild.Columns[1].Width = 100;
                        datagridTableChild.Columns[2].Width = 50;
                        datagridTableChild.Columns[3].Width = 100;
                        datagridTableChild.Columns[4].Width = 125;
                        datagridTableChild.Columns[5].Width = 125;
                        float totalOrigAmount = 0;
                        float totalOutstandingBalance = 0;

                        mySelectSQLChild = "SELECT concat_ws(', ', family_name, first_name) as Name, date_granted, term, DATE_ADD(date_granted, INTERVAL term DAY) as due_date, orig_amount, outstanding_balance FROM loans NATURAL JOIN members WHERE date_terminated IS NULL";
                        try
                        {
                            conn.Open();
                            MySqlCommand query = new MySqlCommand(mySelectSQLChild, conn);
                            MySqlDataReader reader = query.ExecuteReader();
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    if (reader[0] != null)
                                    {
                                        datagridTableChild.Rows.Add(reader[0], String.Format("{0:M/d/yyyy}", reader[1]), reader[2].ToString(), String.Format("{0:M/d/yyyy}", reader[3]), func.stringToDecimal(reader[4].ToString(), 2), func.stringToDecimal(reader[5].ToString(), 2));
                                        datagridTableChild.AutoResizeRow(datagridTableChild.RowCount - 1, DataGridViewAutoSizeRowMode.AllCells);
                                        datagridTableChild.AllowUserToResizeRows = false;

                                        totalOrigAmount = totalOrigAmount + float.Parse(reader[4].ToString());
                                        totalOutstandingBalance = totalOutstandingBalance + float.Parse(reader[5].ToString());
                                    }
                                }
                            }

                            conn.Close();

                        }
                        catch (Exception x)

                        {
                            MessageBox.Show("Error in Load:" + x.ToString());
                            conn.Close();
                        }

                        datagridTableChild.Rows.Add("Total", "", "", "", totalOrigAmount.ToString("#,#.00#"), totalOutstandingBalance.ToString("#,#.00#"));
                        datagridTableChild.AutoResizeRow(datagridTableChild.RowCount - 1, DataGridViewAutoSizeRowMode.AllCells);
                        datagridTableChild.AllowUserToResizeRows = false;
                        datagridTableChild.Rows[datagridTableChild.RowCount - 1].DefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
                        break;
                    }

                //case 1:         //Loan Transactions
                //    {
                //        showdates();
                //        populatedatagridParent("SELECT DISTINCT Name, transaction_type, date, total_amount, principal, interest, penalty FROM loan_transaction LEFT JOIN (SELECT concat_ws(', ', family_name, first_name) as Name, loan_account_id FROM loans NATURAL JOIN members) AS T ON loan_transaction.loan_account_id = T.loan_account_id ORDER BY T.loan_account_id, loan_transaction_id;", 1);
                //        //set up datagridchild columns
                //        datagridTableChild.Rows.Clear();
                //        datagridTableChild.ColumnCount = 7;
                //        datagridTableChild.ColumnHeadersVisible = true;
                //        datagridTableChild.Columns[0].Name = "Name";
                //        datagridTableChild.Columns[1].Name = "Transaction Type";
                //        datagridTableChild.Columns[2].Name = "Date";
                //        datagridTableChild.Columns[3].Name = "Total Amount";
                //        datagridTableChild.Columns[4].Name = "Principal";
                //        datagridTableChild.Columns[5].Name = "Interest";
                //        datagridTableChild.Columns[6].Name = "Penalty";
                //        datagridTableChild.ColumnHeadersDefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
                //        datagridTableChild.Columns[0].Width = 200;
                //        datagridTableChild.Columns[1].Width = 200;
                //        datagridTableChild.Columns[2].Width = 150;
                //        datagridTableChild.Columns[3].Width = 150;
                //        datagridTableChild.Columns[4].Width = 100;
                //        datagridTableChild.Columns[5].Width = 100;
                //        datagridTableChild.Columns[6].Width = 100;
                //        totalAmount = 0;
                //        totalPrincipal = 0;
                //        totalInterest = 0;
                //        totalPenalty = 0;
                //        string splitter = "";


                //        float myTotalAmount = 0;
                //        float myPrincipal = 0;
                //        float myInterest = 0;
                //        float myPenalty = 0;
                //        DateTime dt = Convert.ToDateTime(txtDateFrom.Text);
                //        string datefrom = dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                //        dt = Convert.ToDateTime(txtDateTo.Text);
                //        string dateto = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                //        myDatesSQL[2] = "date between '" + datefrom + "' AND '" + dateto + "'";
                //        mySelectSQLChild = "SELECT DISTINCT Name, transaction_type, date, total_amount, principal, interest, penalty FROM loan_transaction LEFT JOIN (SELECT concat_ws(', ', family_name, first_name) as Name, loan_account_id FROM loans NATURAL JOIN members) AS T ON loan_transaction.loan_account_id = T.loan_account_id WHERE " + myDatesSQL[2] + " ORDER BY T.loan_account_id, loan_transaction_id";
                //        try
                //        {
                //            conn.Open();
                //            MySqlCommand query = new MySqlCommand(mySelectSQLChild, conn);
                //            MySqlDataReader reader = query.ExecuteReader();
                //            myCounter = 0;
                //            if (reader.HasRows)
                //            {
                //                while (reader.Read())
                //                {
                //                    if (datagridTableChild.RowCount > 0) splitter = datagridTableChild.Rows[datagridTableChild.RowCount - 1].Cells[0].Value.ToString();
                //                    if (datagridTableChild.RowCount > 0 && reader[0].ToString() != splitter)
                //                    {
                //                        if (myTotalAmount > 0)
                //                        {
                //                            datagridTableChild.Rows.Add(datagridTableParent.Rows[datagridTableChild.RowCount - 1].Cells["Name"].Value.ToString() + ": Sub-Total", "", "", myTotalAmount.ToString("#,#.00#"), myPrincipal.ToString("#,#.00#"), myInterest.ToString("#,#.00#"), myPenalty.ToString("#,#.00#"));
                //                            datagridTableChild.AutoResizeRow(datagridTableChild.RowCount - 1, DataGridViewAutoSizeRowMode.AllCells);
                //                            datagridTableChild.AllowUserToResizeRows = false;
                //                            datagridTableChild.Rows[datagridTableChild.RowCount - 1].DefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
                //                            datagridTableChild.Rows.Add("", "", "", "", "", "", "");
                //                            datagridTableChild.Rows[datagridTableChild.RowCount - 1].Height = 8;
                //                        }
                //                        myTotalAmount = 0;
                //                        myPrincipal = 0;
                //                        myInterest = 0;
                //                        myPenalty = 0;
                //                    }
                //                    if (reader[0].ToString() != "")
                //                    {
                //                        if (reader[0] != null)
                //                        {
                //                            datagridTableChild.Rows.Add(reader[0], reader[1], String.Format("{0:M/d/yyyy}", reader[2]), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());
                //                            datagridTableChild.AutoResizeRow(datagridTableChild.RowCount - 1, DataGridViewAutoSizeRowMode.AllCells);
                //                            datagridTableChild.AllowUserToResizeRows = false;
                //                            totalAmount = totalAmount + float.Parse(reader[3].ToString());
                //                            totalPrincipal = totalPrincipal + float.Parse(reader[4].ToString());
                //                            totalInterest = totalInterest + float.Parse(reader[5].ToString());
                //                            totalPenalty = totalPenalty + float.Parse(reader[6].ToString());
                //                            myTotalAmount = myTotalAmount + float.Parse(reader[3].ToString());
                //                            myPrincipal = myPrincipal + float.Parse(reader[4].ToString());
                //                            myInterest = myInterest + float.Parse(reader[5].ToString());
                //                            myPenalty = myPenalty + float.Parse(reader[6].ToString());
                //                        }
                //                    }
                //                }
                //                if (myTotalAmount > 0)
                //                {
                //                    datagridTableChild.Rows.Add(datagridTableChild.Rows[datagridTableChild.RowCount - 1].Cells["Name"].Value.ToString() + ": Sub-Total", "", "", myTotalAmount.ToString("#,#.00#"), myPrincipal.ToString("#,#.00#"), myInterest.ToString("#,#.00#"), myPenalty.ToString("#,#.00#"));
                //                    datagridTableChild.AutoResizeRow(datagridTableChild.RowCount - 1, DataGridViewAutoSizeRowMode.AllCells);
                //                    datagridTableChild.AllowUserToResizeRows = false;
                //                    datagridTableChild.Rows[datagridTableChild.RowCount - 1].DefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
                //                    datagridTableChild.Rows.Add("", "", "", "", "", "", "");
                //                    datagridTableChild.Rows[datagridTableChild.RowCount - 1].Height = 8;
                //                }
                //            }

                //            conn.Close();

                //        }
                //        catch (Exception x)

                //        {
                //            MessageBox.Show("Error in Load:" + x.ToString());
                //            conn.Close();
                //        }
                //    }
                //    datagridTableChild.Rows.Add("Grand Total", "", "", totalAmount.ToString("#,#.00#"), totalPrincipal.ToString("#,#.00#"), totalInterest.ToString("#,#.00#"), totalPenalty.ToString("#,#.00#"));
                //    datagridTableChild.AutoResizeRow(datagridTableChild.RowCount - 1, DataGridViewAutoSizeRowMode.AllCells);
                //    datagridTableChild.AllowUserToResizeRows = false;
                //    datagridTableChild.Rows[datagridTableChild.RowCount - 1].DefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
                //    break;

                case 1:     //View All Members
                    {
                        hideall();
                        populatedatagridParent("SELECT concat_ws(',', family_name, first_name) as Name, gender, address, contact_no, type FROM members where status = 1", 1);
                        datagridTableParent.Columns[1].Name = "Gender";
                        datagridTableParent.Columns[2].Name = "Address";
                        datagridTableParent.Columns[3].Name = "Contact No";
                        datagridTableParent.Columns[4].Name = "Type";
                        //set up datagridchild columns
                        datagridTableChild.Rows.Clear();
                        datagridTableChild.ColumnCount = 5;
                        datagridTableChild.ColumnHeadersVisible = true;
                        datagridTableChild.Columns[1].Name = "Gender";
                        datagridTableChild.Columns[2].Name = "Address";
                        datagridTableChild.Columns[3].Name = "Contact No";
                        datagridTableChild.Columns[4].Name = "Type";
                        datagridTableChild.ColumnHeadersDefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
                        datagridTableChild.Columns[0].Width = 200;
                        datagridTableChild.Columns[1].Width = 200;
                        datagridTableChild.Columns[2].Width = 200;
                        datagridTableChild.Columns[3].Width = 200;
                        datagridTableChild.Columns[4].Width = 200;


                        mySelectSQLChild = "SELECT concat_ws(',', family_name, first_name) as Name, gender, address, contact_no, type FROM members where status = 1";
                        try
                        {
                            conn.Open();
                            MySqlCommand query = new MySqlCommand(mySelectSQLChild, conn);
                            MySqlDataReader reader = query.ExecuteReader();
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    if (reader[0] != null)
                                    {
                                        datagridTableChild.Rows.Add(reader[0], reader[1], reader[2], reader[3].ToString(), reader[4]);
                                        datagridTableChild.AutoResizeRow(datagridTableChild.RowCount - 1, DataGridViewAutoSizeRowMode.AllCells);
                                        datagridTableChild.AllowUserToResizeRows = false;

                                    }
                                }
                            }

                            conn.Close();

                        }
                        catch (Exception x)

                        {
                            MessageBox.Show("Error in Load:" + x.ToString());
                            conn.Close();
                        }
                        break;
                    }
                case 2:
                    {
                        cleardgv();
                        capitals();
                        break;
                    }
                case 3:
                    {
                        cleardgv();
                        savingsmonth();
                        break;
                    }
                case 4:
                    {
                        cleardgv();
                        capitals();
                        break;
                    }

            }



            noSortColumn();
        }


        private void populatedatagridParent(string Command, int n)
        {
            datagridTableParent.DataSource = null;      //remove datasource link for datagridProduct
            //MessageBox.Show("Parent - " + selectCommand,"",MessageBoxButtons.OK);
            try
            {
                if (n == 0)
                {
                    var tae = new DatabaseConn(); //opens the connection
                    BindingSource bs = new BindingSource();
                    bs.DataSource = tae.storedProc(Command);
                    datagridTableParent.DataSource = bs;
                    conn.Close();
                    datagridTableParent.AutoResizeRows();
                }
                else if(n == 1)
                {
                    conn.Open(); //opens the connection
                    MySqlCommand query = new MySqlCommand(Command, conn); //query to select all entries in tbl_productcatalog
                    MySqlDataAdapter adp = new MySqlDataAdapter(query); //adapter for query
                    DataTable dt = new DataTable(); //datatable for adapter
                    BindingSource bs = new BindingSource();
                    dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    adp.Fill(dt);
                    bs.DataSource = dt;
                    datagridTableParent.DataSource = bs;
                    conn.Close();
                    datagridTableParent.AutoResizeRows();
                }
                else if(n == 2)
                {
                    var tae = new DatabaseConn(); //opens the connection
                    BindingSource bs = new BindingSource();
                    DateTime dt = capitalDTP.Value;
                    bs.DataSource = tae.storedProc(Command, "yr", dt.Year, "accountstatus", 1, "likephrase", "%");
                    datagridTableParent.DataSource = bs;
                    datagridTableChild.DataSource = bs;
                    conn.Close();
                    datagridTableParent.AutoResizeRows();
                }
                else if (n == 3)
                {
                    var tae = new DatabaseConn(); //opens the connection
                    BindingSource bs = new BindingSource();
                    DateTime dt = savingsMDTP.Value;
                    if(dt.Month == 3 || dt.Month == 6 || dt.Month == 9 || dt.Month == 12)
                    {
                        bs.DataSource = tae.storedProc("displayQuarterMonthTable","mn", dt.Month, "yr", dt.Year, "accountstatus", 1, "likephrase", "%");
                    }
                    else
                    {
                        bs.DataSource = tae.storedProc("displayMonthTable", "mn", dt.Month, "yr", dt.Year, "accountstatus", 1, "likephrase", "%");
                    }
                    datagridTableParent.DataSource = bs;
                    datagridTableChild.DataSource = bs;
                    conn.Close();
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error in populating datagridTable : " + x.ToString());
                conn.Close();
            }
        }



        //set datagridChild tbl here

        private void populatedatagridChild(string Command)
        {
            try
            {
                conn.Open();
                MySqlCommand query = new MySqlCommand(Command, conn);
                MySqlDataReader reader = query.ExecuteReader();
                while (reader.Read())
                {
                    datagridTableChild.Rows.Add();
                }
                conn.Close();
                //datagridTableChild.AutoResizeRows();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error in Child table:" + x.ToString());
                conn.Close();
            }
        }

        private void setDatagridChildAlignment(int mycolNum)
        {
            if (datagridTableChild.Columns[mycolNum].Name == "Sales" || datagridTableChild.Columns[mycolNum].Name == "Discount")
            {
                datagridTableChild.Columns[mycolNum].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                datagridTableChild.Columns[mycolNum].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight;
            }
            else if (datagridTableChild.Columns[mycolNum].Name == "Quantity" || datagridTableChild.Columns[mycolNum].Name == "Stock")
            {
                datagridTableChild.Columns[mycolNum].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                datagridTableChild.Columns[mycolNum].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopCenter;
            }
            else
            {
                datagridTableChild.Columns[mycolNum].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                datagridTableChild.Columns[mycolNum].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft;
            }
            //          datagridTableChild.AutoResizeColumns();
        }

        private void goPrint()
        {
            if (!(comboReports.Text == ""))
            {
                string myDate;
                string myStore = "Agriculturist Multipurpose Cooperative" + Environment.NewLine + "113 Pichon Street, Barangay 39-D, Poblacion, Davao City, Davao del Sur" + Environment.NewLine + "(082) 227 5240" + Environment.NewLine;
                if (txtDateFrom.Text == txtDateTo.Text)
                {
                    myDate = "Daily Report: " + txtDateFrom;
                }
                else
                {
                    myDate = "From " + txtDateFrom.Text + " To " + txtDateTo.Text;
                }
                ClsPrint printIT = new ClsPrint(datagridTableChild, myStore + comboReports.Text + Environment.NewLine + myDate);
                printIT.PrintForm();
            }
        }

        private void getSQLDates()
        {
            myDatesSQL[0] = txtDateFrom.Text;
            myDatesSQL[1] = txtDateTo.Text;
        }

        private void noSortColumn()
        {
            //set columns as notsortable
            for (int i = 0; i < datagridTableChild.ColumnCount - 1; i++)
            {
                datagridTableChild.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void capitalBtn_Click(object sender, EventArgs e)
        {
            if(capitalDTP.Visible == true && comboReports.SelectedIndex == 3)
            {
                cleardgv();
                populatedatagridParent("displayCapitalsTable", 2);
                datagridTableParent.Columns[0].Visible = false;
                datagridTableChild.Columns[0].Visible = false;
            }
            else if(savingsMDTP.Visible == true)
            {
                cleardgv();
                populatedatagridParent("display", 3);
                datagridTableParent.Columns[0].Visible = false;
                datagridTableChild.Columns[0].Visible = false;
            }
            else
            {
                cleardgv();
                populatedatagridParent("displayYearTable", 2);
                datagridTableParent.Columns[0].Visible = false;
                datagridTableChild.Columns[0].Visible = false;
            }
        }
    }
}
