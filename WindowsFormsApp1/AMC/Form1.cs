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
    public partial class MainForm : Form
    {
        public Form childform;
        public MainForm()
        {
            InitializeComponent();
            MessageBox.Show(User.Name.person);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            innerChild(new ViewLoanSched(this));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            sidebarPanelCBU.Visible = false;
            sidebarPanelLoans.Visible = false;
            sidebarPanelSavings.Visible = false;
            sidebarPanelMembers.Visible = false;
            innerChild(new Dashboard(this));
        }

        private void sidebarBtnMembers_Click(object sender, EventArgs e)
        {
            if (sidebarPanelMembers.Visible == false)
                sidebarPanelMembers.Visible = true;
            else
                sidebarPanelMembers.Visible = false;

            sidebarPanelCBU.Visible = false;
            sidebarPanelLoans.Visible = false;
            sidebarPanelSavings.Visible = false;
        }

        private void sidebarBtnLoans_Click(object sender, EventArgs e)
        {
            if (sidebarPanelLoans.Visible == false)
                sidebarPanelLoans.Visible = true;
            else
                sidebarPanelLoans.Visible = false;

            sidebarPanelCBU.Visible = false;
            sidebarPanelMembers.Visible = false;
            sidebarPanelSavings.Visible = false;
        }

        private void sidebarBtnSavings_Click(object sender, EventArgs e)
        {
            if (sidebarPanelSavings.Visible == false)
                sidebarPanelSavings.Visible = true;
            else
                sidebarPanelSavings.Visible = false;

            sidebarPanelCBU.Visible = false;
            sidebarPanelMembers.Visible = false;
            sidebarPanelLoans.Visible = false;
        }

        private void sidebarBtnCBU_Click(object sender, EventArgs e)
        {
            if (sidebarPanelCBU.Visible == false)
                sidebarPanelCBU.Visible = true;
            else
                sidebarPanelCBU.Visible = false;

            sidebarPanelSavings.Visible = false;
            sidebarPanelMembers.Visible = false;
            sidebarPanelLoans.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
	        innerChild(new ViewMember(this));
        }
        public void innerChild(Form child)
        {
            breaker();
            childform = child;
            childform.TopLevel = false;
            panel1.Controls.Add(childform);
            childform.Show();
        }
        private void breaker()
        {
            try
            {
                childform.Close();
                childform.Dispose();
            }
            catch { }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            breaker();
            childform = new AddMember();
            childform.TopLevel = false;
            panel1.Controls.Add(childform);
            childform.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            innerChild(new ViewSavings(this));
        }

        private void button12_Click(object sender, EventArgs e)
        {
            innerChild(new AddTransaction(this, "savings"));
        }

        private void button17_Click(object sender, EventArgs e)
        {
            innerChild(new AddTransaction(this, "capitals"));
        }

        private void button14_Click(object sender, EventArgs e)
        {
            innerChild(new ViewCapitals(this));
        }

        private void button16_Click(object sender, EventArgs e)
        {
            innerChild(new ViewTransactions(this, 'c'));
        }

        private void button11_Click(object sender, EventArgs e)
        {
            innerChild(new ViewTransactions(this, 's'));
        }

        private void sidebarBtnDashboard_Click(object sender, EventArgs e)
        {
            innerChild(new Dashboard(this));
        }

        private void sidebarPanelLoans_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            innerChild(new AddLoan(-1));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            innerChild(new AddRepayment());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            innerChild(new ViewLoans(this));
        }
    }
}
