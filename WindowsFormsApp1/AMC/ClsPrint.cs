using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;


namespace AMC
{
    public class Test
    {
        public MySqlConnection conn = new MySqlConnection("Server=localhost;Database=tetehardware;Uid=root;Pwd=root");

        public bool IsFloat(string input)
        {
            float test;
            return float.TryParse(input, out test);
        }

        public bool IsNumeric(string input)
        {
            int test;
            return int.TryParse(input, out test);
        }

        public string stringToDecimal(string _myString, int _decPlaces)
        {
            string _myReturn = "";
            if (_decPlaces > 6)
            {
                MessageBox.Show("Decimal places up to 6 only!", "", MessageBoxButtons.OK);
            }
            else
            {
                if (_myString.Contains("."))
                {
                    _myReturn = Convert.ToString(decimal.Round(decimal.Parse(_myString + "000000"), _decPlaces));
                }
                else
                {
                    _myReturn = Convert.ToString(decimal.Round(decimal.Parse(_myString + ".000000"), _decPlaces));
                }
            }

            return _myReturn;
        }

    }


    class ClsPrint
    {
        #region Variables

        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        int _pageCounter = 0; //Used to count the pages
        int _thePages = 0;
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height
        StringFormat strFormatRight; //Used to format the grid rows.
        StringFormat strFormatLeft;
        StringFormat strFormatCenter;
        StringFormat mystrFormat;
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        private PrintDocument _printDocument = new PrintDocument();
        private DataGridView gw = new DataGridView();
        private string _ReportHeader;
        Test myTest = new Test();
        #endregion

        public ClsPrint(DataGridView gridview, string ReportHeader)
        {
            _printDocument.PrintPage += new PrintPageEventHandler(_printDocument_PrintPage);
            _printDocument.BeginPrint += new PrintEventHandler(_printDocument_BeginPrint);
            gw = gridview;
            _ReportHeader = ReportHeader;
        }

        public void PrintForm()
        {
            ////Open the print dialog
            //PrintDialog printDialog = new PrintDialog();
            //printDialog.Document = _printDocument;
            //printDialog.UseEXDialog = true;

            ////Get the document
            //if (DialogResult.OK == printDialog.ShowDialog())
            //{
            //    _printDocument.DocumentName = "Test Page Print";
            //    _printDocument.Print();
            //}

            //Open the print preview dialog
            PrintPreviewDialog objPPdialog = new PrintPreviewDialog();
            _printDocument.DefaultPageSettings.Landscape = true;
            _printDocument.DefaultPageSettings.Margins.Top = 175;
            _printDocument.DefaultPageSettings.Margins.Bottom = 75;
            _printDocument.DefaultPageSettings.Margins.Left = 75;
            _printDocument.DefaultPageSettings.Margins.Right = 75;
            objPPdialog.Document = _printDocument;
            objPPdialog.ShowDialog();
        }

        private void _printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //try
            //{
            //Set the left margin
            int iLeftMargin = e.MarginBounds.Left;
            //Set the top margin
            int iTopMargin = e.MarginBounds.Top;
            //Whether more pages have to print or not
            bool bMorePagesToPrint = false;
            int iTmpWidth = 0;

            //For the first page to print set the cell width and header height
            _pageCounter++;

            e.Graphics.DrawString("Page " + _pageCounter.ToString() + " of " + _thePages.ToString(), new Font(gw.Font, FontStyle.Bold),
                        Brushes.Black, new Point(75, 775));
            if (bFirstPage)
            {
                foreach (DataGridViewColumn GridCol in gw.Columns)
                {
                    iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                        (double)iTotalWidth * (double)iTotalWidth *
                        ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                    iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                        GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                    // Save width and height of headers
                    arrColumnLefts.Add(iLeftMargin);
                    arrColumnWidths.Add(iTmpWidth);
                    iLeftMargin += iTmpWidth;
                }
            }
            //Loop till all the grid rows not get printed
            while (iRow <= gw.Rows.Count - 1)
            {
                DataGridViewRow GridRow = gw.Rows[iRow];
                //Set the cell height
                iCellHeight = GridRow.Height + 5;
                int iCount = 0;
                //Check whether the current page settings allows more rows to print
                if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                {
                    bNewPage = true;
                    bFirstPage = false;
                    bMorePagesToPrint = true;
                    break;
                }
                else
                {

                    if (bNewPage)
                    {
                        //Draw Header
                        e.Graphics.DrawString(_ReportHeader,
                            new Font(gw.Font, FontStyle.Bold),
                            Brushes.Black, e.MarginBounds.Left,
                            e.MarginBounds.Top - e.Graphics.MeasureString(_ReportHeader,
                            new Font(gw.Font, FontStyle.Bold),
                            e.MarginBounds.Width).Height - 13);

                        /*
                        String strDate = "";
                        //Draw Date
                        e.Graphics.DrawString(strDate,
                            new Font(gw.Font, FontStyle.Bold), Brushes.Black,
                            e.MarginBounds.Left +
                            (e.MarginBounds.Width - e.Graphics.MeasureString(strDate,
                            new Font(gw.Font, FontStyle.Bold),
                            e.MarginBounds.Width).Width),
                            e.MarginBounds.Top - e.Graphics.MeasureString(_ReportHeader,
                            new Font(new Font(gw.Font, FontStyle.Bold),
                            FontStyle.Bold), e.MarginBounds.Width).Height - 13);
                            */

                        //Draw Columns                 
                        iTopMargin = e.MarginBounds.Top;
                        DataGridViewColumn[] _GridCol = new DataGridViewColumn[gw.Columns.Count];
                        int colcount = 0;
                        //Convert ltr to rtl
                        foreach (DataGridViewColumn GridCol in gw.Columns)
                        {
                            _GridCol[colcount++] = GridCol;
                        }
                        for (int i = 0; i < _GridCol.Count(); i++)
                        {
                            //Format Alignment based on type of value
                            if (_GridCol[i].HeaderText.Contains("Q"))
                                mystrFormat = strFormatCenter;
                            else if (_GridCol[i].HeaderText.Contains("Sales") || _GridCol[i].HeaderText.Contains("Discount"))
                                mystrFormat = strFormatRight;
                            else
                                mystrFormat = strFormatLeft;

                            e.Graphics.FillRectangle(new SolidBrush(Color.Black),
                            new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                            (int)arrColumnWidths[iCount], iHeaderHeight));

                            e.Graphics.DrawRectangle(Pens.Black,
                                new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                (int)arrColumnWidths[iCount], iHeaderHeight));

                            e.Graphics.DrawString(_GridCol[i].HeaderText,
                                _GridCol[i].InheritedStyle.Font,
                                new SolidBrush(_GridCol[i].InheritedStyle.ForeColor),
                                //                                new SolidBrush(_GridCol[i].InheritedStyle.ForeColor),
                                new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                (int)arrColumnWidths[iCount], iHeaderHeight), mystrFormat);
                            iCount++;
                        }
                        bNewPage = false;
                        iTopMargin += iHeaderHeight;
                    }
                    iCount = 0;
                    DataGridViewCell[] _GridCell = new DataGridViewCell[GridRow.Cells.Count];
                    int cellcount = 0;
                    //Convert ltr to rtl
                    foreach (DataGridViewCell Cel in GridRow.Cells)
                    {
                        _GridCell[cellcount++] = Cel;
                    }
                    //Draw Columns Contents                
                    for (int i = 0; i < _GridCell.Count(); i++)
                    //                    for (int i = (_GridCell.Count() - 1); i >= 0; i--)
                    {
                        if (_GridCell[i].Value != null)
                        {
                            //Format Alignment based on type of value
                            if (myTest.IsNumeric(_GridCell[i].FormattedValue.ToString()))
                                mystrFormat = strFormatCenter;
                            else if (myTest.IsFloat(_GridCell[i].FormattedValue.ToString()))
                                mystrFormat = strFormatRight;
                            else
                                mystrFormat = strFormatLeft;

                            e.Graphics.DrawString(_GridCell[i].FormattedValue.ToString(),
                                _GridCell[i].InheritedStyle.Font,
                                new SolidBrush(_GridCell[i].InheritedStyle.ForeColor),
                                new RectangleF((int)arrColumnLefts[iCount],
                                (float)iTopMargin,
                                (int)arrColumnWidths[iCount], (float)iCellHeight),
                                mystrFormat);
                        }
                        //Drawing Cells Borders 
                        e.Graphics.DrawRectangle(Pens.Black,
                            new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                            (int)arrColumnWidths[iCount], iCellHeight));
                        iCount++;
                    }
                }
                iRow++;
                iTopMargin += iCellHeight;


            }

            //If more lines exist, print another page.
            if (bMorePagesToPrint)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                _thePages = _pageCounter;
                _pageCounter = 0;
            }
            //}
            //catch (Exception exc)
            //{
            //    MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK,
            //       MessageBoxIcon.Error);
            //}
        }

        private void _printDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                strFormatRight = new StringFormat();
                strFormatRight.Alignment = StringAlignment.Far;
                strFormatRight.LineAlignment = StringAlignment.Near;
                strFormatRight.Trimming = StringTrimming.EllipsisCharacter;

                strFormatLeft = new StringFormat();
                strFormatLeft.Alignment = StringAlignment.Near;
                strFormatLeft.LineAlignment = StringAlignment.Near;

                strFormatCenter = new StringFormat();
                strFormatCenter.Alignment = StringAlignment.Center;
                strFormatCenter.LineAlignment = StringAlignment.Near;


                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in gw.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}