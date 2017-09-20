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
    public partial class Form2 : Form
    {
        public ViewLoans reftomain;
        public MySqlConnection conn;
        private DatabaseConn _addloanconn;
        public DataTable loanmems;
        public int loanid;

        public Form2(ViewLoans main, int x)
        {
            InitializeComponent();
            reftomain = main;
            _addloanconn = new DatabaseConn();
            loanid = x;
        }
    }
}
