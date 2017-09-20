namespace AMC
{
    partial class Form2
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
            this.mlist = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mlist)).BeginInit();
            this.SuspendLayout();
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
            this.mlist.TabIndex = 5;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(239)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(239)))));
            this.button3.Location = new System.Drawing.Point(-1, 42);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(469, 5);
            this.button3.TabIndex = 4;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.Location = new System.Drawing.Point(12, 9);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(57, 30);
            this.name.TabIndex = 3;
            this.name.Text = "label";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 304);
            this.Controls.Add(this.mlist);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.mlist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView mlist;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label name;
    }
}