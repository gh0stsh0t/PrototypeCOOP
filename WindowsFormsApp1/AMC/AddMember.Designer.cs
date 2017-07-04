namespace AMC
{
    partial class AddMember
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lname = new System.Windows.Forms.TextBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.label2 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.fname = new System.Windows.Forms.TextBox();
            this.mname = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Personal Information";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mname);
            this.panel1.Controls.Add(this.fname);
            this.panel1.Controls.Add(this.lname);
            this.panel1.Controls.Add(this.splitter2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Location = new System.Drawing.Point(16, 82);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(906, 169);
            this.panel1.TabIndex = 1;
            // 
            // lname
            // 
            this.lname.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lname.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lname.Location = new System.Drawing.Point(239, 6);
            this.lname.Name = "lname";
            this.lname.Size = new System.Drawing.Size(173, 23);
            this.lname.TabIndex = 3;
            this.lname.Text = "Last Name";
            this.lname.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lname_MouseClick);
            this.lname.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.lname.Enter += new System.EventHandler(this.lname_Enter);
            this.lname.Leave += new System.EventHandler(this.lname_Leave);
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(164, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(69, 169);
            this.splitter2.TabIndex = 2;
            this.splitter2.TabStop = false;
            this.splitter2.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Full name";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(164, 169);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            this.splitter1.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(239)))));
            this.button1.Enabled = false;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(0, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(936, 5);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 30);
            this.label3.TabIndex = 0;
            this.label3.Text = "Add Member";
            // 
            // fname
            // 
            this.fname.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fname.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.fname.Location = new System.Drawing.Point(418, 6);
            this.fname.Name = "fname";
            this.fname.Size = new System.Drawing.Size(253, 23);
            this.fname.TabIndex = 4;
            this.fname.Text = "First Name";
            this.fname.TextChanged += new System.EventHandler(this.fname_TextChanged);
            this.fname.Enter += new System.EventHandler(this.textBox1_Enter);
            this.fname.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // mname
            // 
            this.mname.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mname.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.mname.Location = new System.Drawing.Point(677, 6);
            this.mname.Name = "mname";
            this.mname.Size = new System.Drawing.Size(189, 23);
            this.mname.TabIndex = 4;
            this.mname.Text = "Middle Name";
            // 
            // AddMember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(934, 621);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AddMember";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.AddMember_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox lname;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mname;
        private System.Windows.Forms.TextBox fname;
    }
}