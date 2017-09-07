using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMC
{
    public partial class AddParticular : Form
    {
        AddTransaction sourceForm;

        public AddParticular(AddTransaction source)
        {
            InitializeComponent();
            sourceForm = source;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sourceForm.Enabled = true;
            this.Close();
        }
    }
}
