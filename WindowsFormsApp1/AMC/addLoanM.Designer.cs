namespace AMC
{
    partial class addLoanM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.mlist = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.mlist)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(152, 30);
            this.label7.TabIndex = 0;
            this.label7.Text = "Select Member";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(239)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(239)))));
            this.button3.Location = new System.Drawing.Point(-1, 42);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(469, 5);
            this.button3.TabIndex = 1;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // mlist
            // 
            this.mlist.AllowUserToAddRows = false;
            this.mlist.AllowUserToDeleteRows = false;
            this.mlist.AllowUserToResizeColumns = false;
            this.mlist.AllowUserToResizeRows = false;
            this.mlist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.mlist.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.mlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mlist.ColumnHeadersVisible = false;
            this.mlist.EnableHeadersVisualStyles = false;
            this.mlist.Location = new System.Drawing.Point(12, 53);
            this.mlist.MultiSelect = false;
            this.mlist.Name = "mlist";
            this.mlist.ReadOnly = true;
            this.mlist.RowHeadersVisible = false;
            this.mlist.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.mlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mlist.ShowCellErrors = false;
            this.mlist.ShowCellToolTips = false;
            this.mlist.ShowEditingIcon = false;
            this.mlist.ShowRowErrors = false;
            this.mlist.Size = new System.Drawing.Size(443, 239);
            this.mlist.TabIndex = 2;
            this.mlist.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mlist_CellContentDoubleClick);
            this.mlist.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.mlist_CellMouseDoubleClick);
            // 
            // addLoanM
            // 
            this.ClientSize = new System.Drawing.Size(467, 304);
            this.Controls.Add(this.mlist);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "addLoanM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.addLoanM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mlist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox lname;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView mlist;
    }
}