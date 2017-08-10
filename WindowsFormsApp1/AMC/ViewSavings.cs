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
    public partial class ViewSavings : Form
    {
        public MySqlConnection conn;
        public MainForm reftomain;

        public ViewSavings(MainForm main)
        {
            InitializeComponent();
            reftomain = main;
        }
    }
}
