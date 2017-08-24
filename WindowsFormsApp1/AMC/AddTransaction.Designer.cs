namespace AMC
{
    partial class AddTransaction
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.cbxParticular = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lname = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbxTransaction = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxAccount = new System.Windows.Forms.ComboBox();
            this.cbxMember = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.label2 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTop = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.cbxParticular);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.lname);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.cbxTransaction);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.dtpTime);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.dtpDate);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cbxAccount);
            this.panel1.Controls.Add(this.cbxMember);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.splitter2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Location = new System.Drawing.Point(23, 109);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(906, 280);
            this.panel1.TabIndex = 4;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(242, 191);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 15);
            this.label11.TabIndex = 18;
            this.label11.Text = "₱";
            // 
            // cbxParticular
            // 
            this.cbxParticular.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxParticular.FormattingEnabled = true;
            this.cbxParticular.Items.AddRange(new object[] {
            "Savings",
            "Capital Build-Up"});
            this.cbxParticular.Location = new System.Drawing.Point(241, 228);
            this.cbxParticular.Name = "cbxParticular";
            this.cbxParticular.Size = new System.Drawing.Size(217, 21);
            this.cbxParticular.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(94, 230);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 15);
            this.label10.TabIndex = 16;
            this.label10.Text = "Particular";
            // 
            // lname
            // 
            this.lname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lname.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lname.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lname.Location = new System.Drawing.Point(262, 186);
            this.lname.Name = "lname";
            this.lname.Size = new System.Drawing.Size(143, 23);
            this.lname.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(102, 191);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 15);
            this.label9.TabIndex = 14;
            this.label9.Text = "Amount";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(427, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 15);
            this.label8.TabIndex = 13;
            this.label8.Text = "Current Balance: xxxxx";
            // 
            // cbxTransaction
            // 
            this.cbxTransaction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTransaction.FormattingEnabled = true;
            this.cbxTransaction.Items.AddRange(new object[] {
            "Deposit",
            "Withdrawal"});
            this.cbxTransaction.Location = new System.Drawing.Point(241, 148);
            this.cbxTransaction.Name = "cbxTransaction";
            this.cbxTransaction.Size = new System.Drawing.Size(164, 21);
            this.cbxTransaction.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(83, 151);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "Transaction";
            // 
            // dtpTime
            // 
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.Location = new System.Drawing.Point(241, 111);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.Size = new System.Drawing.Size(164, 20);
            this.dtpTime.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(115, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "Time";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(241, 76);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(164, 20);
            this.dtpDate.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(115, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "Date";
            // 
            // cbxAccount
            // 
            this.cbxAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAccount.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAccount.FormattingEnabled = true;
            this.cbxAccount.Items.AddRange(new object[] {
            "Savings",
            "Capital Build-Up"});
            this.cbxAccount.Location = new System.Drawing.Point(241, 41);
            this.cbxAccount.Name = "cbxAccount";
            this.cbxAccount.Size = new System.Drawing.Size(164, 21);
            this.cbxAccount.TabIndex = 6;
            // 
            // cbxMember
            // 
            this.cbxMember.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxMember.FormattingEnabled = true;
            this.cbxMember.Location = new System.Drawing.Point(241, 8);
            this.cbxMember.Name = "cbxMember";
            this.cbxMember.Size = new System.Drawing.Size(355, 21);
            this.cbxMember.TabIndex = 5;
            this.cbxMember.Text = "[Select Member]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(98, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "Account";
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(164, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(69, 280);
            this.splitter2.TabIndex = 2;
            this.splitter2.TabStop = false;
            this.splitter2.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(69, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Member Name";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(164, 280);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            this.splitter1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Transaction Details";
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTop.Location = new System.Drawing.Point(20, 20);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(167, 30);
            this.lblTop.TabIndex = 3;
            this.lblTop.Text = "Add Transaction";
            this.lblTop.Click += new System.EventHandler(this.label3_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(239)))));
            this.button1.Enabled = false;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(7, 87);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(936, 5);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(92)))), ((int)(((byte)(239)))));
            this.btnSubmit.FlatAppearance.BorderSize = 0;
            this.btnSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(44)))));
            this.btnSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(44)))));
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSubmit.Location = new System.Drawing.Point(586, 422);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(139, 42);
            this.btnSubmit.TabIndex = 7;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            // 
            // AddTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(934, 621);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTop);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddTransaction";
            this.Text = "Add Transaction";
            this.Load += new System.EventHandler(this.AddTransaction_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbxTransaction;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxAccount;
        private System.Windows.Forms.ComboBox cbxMember;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbxParticular;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox lname;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label11;
    }
}