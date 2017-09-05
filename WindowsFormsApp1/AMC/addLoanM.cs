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
    public partial class addLoanM : Form
    {
        public MainForm reftomain;
        public int memid;
        public MySqlConnection conn;
        string accid = DateTime.Today.ToString("yyyyMM") + "001";

        public addLoanM(int id, MySqlConnection c, MainForm main)
        {
           
        }

    }
}
