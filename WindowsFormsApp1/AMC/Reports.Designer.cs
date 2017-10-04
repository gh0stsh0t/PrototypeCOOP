namespace AMC
{
    partial class Reports
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblReports = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.monCalFrom = new System.Windows.Forms.MonthCalendar();
            this.monCalTo = new System.Windows.Forms.MonthCalendar();
            this.lblRep = new System.Windows.Forms.Label();
            this.comboReports = new System.Windows.Forms.ComboBox();
            this.pnlReports = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.datagridTableChild = new System.Windows.Forms.DataGridView();
            this.txtDateTo = new System.Windows.Forms.TextBox();
            this.txtDateFrom = new System.Windows.Forms.TextBox();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.datagridTableParent = new System.Windows.Forms.DataGridView();
            this.btnPrintRep = new System.Windows.Forms.Button();
            this.capitalDTP = new System.Windows.Forms.DateTimePicker();
            this.capitalBtn = new System.Windows.Forms.Button();
            this.savingsMDTP = new System.Windows.Forms.DateTimePicker();
            this.pnlReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridTableChild)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datagridTableParent)).BeginInit();
            this.SuspendLayout();
            // 
            // lblReports
            // 
            this.lblReports.AutoSize = true;
            this.lblReports.Font = new System.Drawing.Font("Tw Cen MT", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReports.ForeColor = System.Drawing.Color.White;
            this.lblReports.Location = new System.Drawing.Point(35, 13);
            this.lblReports.Name = "lblReports";
            this.lblReports.Size = new System.Drawing.Size(96, 31);
            this.lblReports.TabIndex = 101;
            this.lblReports.Text = "Reports";
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            // 
            // monCalFrom
            // 
            this.monCalFrom.Location = new System.Drawing.Point(164, 435);
            this.monCalFrom.Name = "monCalFrom";
            this.monCalFrom.TabIndex = 184;
            this.monCalFrom.Visible = false;
            this.monCalFrom.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monCalFrom_DateSelected);
            // 
            // monCalTo
            // 
            this.monCalTo.Location = new System.Drawing.Point(393, 435);
            this.monCalTo.Name = "monCalTo";
            this.monCalTo.TabIndex = 185;
            this.monCalTo.Visible = false;
            this.monCalTo.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monCalTo_DateSelected);
            // 
            // lblRep
            // 
            this.lblRep.AutoSize = true;
            this.lblRep.ForeColor = System.Drawing.SystemColors.Control;
            this.lblRep.Location = new System.Drawing.Point(17, 132);
            this.lblRep.Name = "lblRep";
            this.lblRep.Size = new System.Drawing.Size(42, 13);
            this.lblRep.TabIndex = 192;
            this.lblRep.Text = "Report:";
            // 
            // comboReports
            // 
            this.comboReports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboReports.FormattingEnabled = true;
            this.comboReports.Location = new System.Drawing.Point(78, 129);
            this.comboReports.Name = "comboReports";
            this.comboReports.Size = new System.Drawing.Size(355, 21);
            this.comboReports.TabIndex = 191;
            this.comboReports.SelectedIndexChanged += new System.EventHandler(this.comboReports_SelectedIndexChanged);
            this.comboReports.Leave += new System.EventHandler(this.comboReports_Leave);
            this.comboReports.MouseClick += new System.Windows.Forms.MouseEventHandler(this.comboReports_MouseClick);
            // 
            // pnlReports
            // 
            this.pnlReports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(152)))));
            this.pnlReports.Controls.Add(this.btnBack);
            this.pnlReports.Controls.Add(this.lblReports);
            this.pnlReports.Location = new System.Drawing.Point(0, -4);
            this.pnlReports.Name = "pnlReports";
            this.pnlReports.Size = new System.Drawing.Size(433, 58);
            this.pnlReports.TabIndex = 189;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(152)))));
            this.btnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(181)))), ((int)(((byte)(116)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Tw Cen MT", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(356, 17);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(63, 27);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "BACK";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(181)))), ((int)(((byte)(116)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(152)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tw Cen MT", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1194, -20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(29, 27);
            this.btnClose.TabIndex = 188;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(459, -4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 187;
            this.label2.Text = "Details :";
            // 
            // datagridTableChild
            // 
            this.datagridTableChild.AllowUserToAddRows = false;
            this.datagridTableChild.AllowUserToDeleteRows = false;
            this.datagridTableChild.AllowUserToResizeColumns = false;
            this.datagridTableChild.AllowUserToResizeRows = false;
            this.datagridTableChild.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tw Cen MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridTableChild.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.datagridTableChild.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tw Cen MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridTableChild.DefaultCellStyle = dataGridViewCellStyle2;
            this.datagridTableChild.Location = new System.Drawing.Point(463, 32);
            this.datagridTableChild.Name = "datagridTableChild";
            this.datagridTableChild.ReadOnly = true;
            this.datagridTableChild.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.datagridTableChild.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.datagridTableChild.RowTemplate.Height = 60;
            this.datagridTableChild.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridTableChild.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datagridTableChild.Size = new System.Drawing.Size(459, 403);
            this.datagridTableChild.TabIndex = 186;
            // 
            // txtDateTo
            // 
            this.txtDateTo.Location = new System.Drawing.Point(264, 85);
            this.txtDateTo.Name = "txtDateTo";
            this.txtDateTo.Size = new System.Drawing.Size(169, 20);
            this.txtDateTo.TabIndex = 183;
            this.txtDateTo.Enter += new System.EventHandler(this.txtDateTo_Enter);
            this.txtDateTo.Leave += new System.EventHandler(this.txtDateTo_Leave);
            // 
            // txtDateFrom
            // 
            this.txtDateFrom.Location = new System.Drawing.Point(21, 85);
            this.txtDateFrom.Name = "txtDateFrom";
            this.txtDateFrom.Size = new System.Drawing.Size(169, 20);
            this.txtDateFrom.TabIndex = 182;
            this.txtDateFrom.Enter += new System.EventHandler(this.txtDateFrom_Enter);
            this.txtDateFrom.Leave += new System.EventHandler(this.txtDateFrom_Leave);
            // 
            // lblDateTo
            // 
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.ForeColor = System.Drawing.SystemColors.Control;
            this.lblDateTo.Location = new System.Drawing.Point(260, 60);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(49, 13);
            this.lblDateTo.TabIndex = 181;
            this.lblDateTo.Text = "Date To:";
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.ForeColor = System.Drawing.SystemColors.Control;
            this.lblDateFrom.Location = new System.Drawing.Point(17, 63);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(59, 13);
            this.lblDateFrom.TabIndex = 180;
            this.lblDateFrom.Text = "Date From:";
            // 
            // datagridTableParent
            // 
            this.datagridTableParent.AllowUserToAddRows = false;
            this.datagridTableParent.AllowUserToDeleteRows = false;
            this.datagridTableParent.AllowUserToResizeColumns = false;
            this.datagridTableParent.AllowUserToResizeRows = false;
            this.datagridTableParent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.datagridTableParent.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.datagridTableParent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridTableParent.Location = new System.Drawing.Point(21, 176);
            this.datagridTableParent.Name = "datagridTableParent";
            this.datagridTableParent.ReadOnly = true;
            this.datagridTableParent.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.datagridTableParent.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.datagridTableParent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datagridTableParent.Size = new System.Drawing.Size(289, 259);
            this.datagridTableParent.TabIndex = 179;
            // 
            // btnPrintRep
            // 
            this.btnPrintRep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(152)))));
            this.btnPrintRep.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(152)))));
            this.btnPrintRep.FlatAppearance.BorderSize = 0;
            this.btnPrintRep.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(220)))), ((int)(((byte)(209)))));
            this.btnPrintRep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintRep.Font = new System.Drawing.Font("Tw Cen MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintRep.ForeColor = System.Drawing.Color.White;
            this.btnPrintRep.Location = new System.Drawing.Point(21, 441);
            this.btnPrintRep.Name = "btnPrintRep";
            this.btnPrintRep.Size = new System.Drawing.Size(104, 53);
            this.btnPrintRep.TabIndex = 190;
            this.btnPrintRep.Text = "Print [F5]";
            this.btnPrintRep.UseVisualStyleBackColor = false;
            this.btnPrintRep.Click += new System.EventHandler(this.btnPrintRep_Click);
            // 
            // capitalDTP
            // 
            this.capitalDTP.Location = new System.Drawing.Point(664, 456);
            this.capitalDTP.Name = "capitalDTP";
            this.capitalDTP.Size = new System.Drawing.Size(200, 20);
            this.capitalDTP.TabIndex = 193;
            // 
            // capitalBtn
            // 
            this.capitalBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(152)))));
            this.capitalBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(152)))));
            this.capitalBtn.FlatAppearance.BorderSize = 0;
            this.capitalBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(220)))), ((int)(((byte)(209)))));
            this.capitalBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.capitalBtn.Font = new System.Drawing.Font("Tw Cen MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.capitalBtn.ForeColor = System.Drawing.Color.White;
            this.capitalBtn.Location = new System.Drawing.Point(664, 482);
            this.capitalBtn.Name = "capitalBtn";
            this.capitalBtn.Size = new System.Drawing.Size(60, 31);
            this.capitalBtn.TabIndex = 194;
            this.capitalBtn.Text = "Show";
            this.capitalBtn.UseVisualStyleBackColor = false;
            this.capitalBtn.Click += new System.EventHandler(this.capitalBtn_Click);
            // 
            // savingsMDTP
            // 
            this.savingsMDTP.Location = new System.Drawing.Point(664, 519);
            this.savingsMDTP.Name = "savingsMDTP";
            this.savingsMDTP.Size = new System.Drawing.Size(200, 20);
            this.savingsMDTP.TabIndex = 195;
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 621);
            this.Controls.Add(this.savingsMDTP);
            this.Controls.Add(this.capitalBtn);
            this.Controls.Add(this.capitalDTP);
            this.Controls.Add(this.monCalFrom);
            this.Controls.Add(this.monCalTo);
            this.Controls.Add(this.lblRep);
            this.Controls.Add(this.comboReports);
            this.Controls.Add(this.pnlReports);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.datagridTableChild);
            this.Controls.Add(this.txtDateTo);
            this.Controls.Add(this.txtDateFrom);
            this.Controls.Add(this.lblDateTo);
            this.Controls.Add(this.lblDateFrom);
            this.Controls.Add(this.datagridTableParent);
            this.Controls.Add(this.btnPrintRep);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Reports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Reports_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.formReports_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.formReports_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.formReports_MouseUp);
            this.pnlReports.ResumeLayout(false);
            this.pnlReports.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridTableChild)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datagridTableParent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblReports;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MonthCalendar monCalFrom;
        private System.Windows.Forms.MonthCalendar monCalTo;
        private System.Windows.Forms.Label lblRep;
        private System.Windows.Forms.ComboBox comboReports;
        private System.Windows.Forms.Panel pnlReports;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView datagridTableChild;
        private System.Windows.Forms.TextBox txtDateTo;
        private System.Windows.Forms.TextBox txtDateFrom;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.DataGridView datagridTableParent;
        private System.Windows.Forms.Button btnPrintRep;
        private System.Windows.Forms.DateTimePicker capitalDTP;
        private System.Windows.Forms.Button capitalBtn;
        private System.Windows.Forms.DateTimePicker savingsMDTP;
    }
}