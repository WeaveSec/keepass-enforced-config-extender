namespace KeePassEnforcedConfigExtender
{
    partial class Warn_Window
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Warn_Window));
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_upgrade = new System.Windows.Forms.Button();
            this.lbl_warn_text1 = new System.Windows.Forms.Label();
            this.lbl_test2 = new System.Windows.Forms.Label();
            this.lbl_text3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(237, 153);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(100, 40);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_upgrade
            // 
            this.btn_upgrade.Location = new System.Drawing.Point(66, 153);
            this.btn_upgrade.Name = "btn_upgrade";
            this.btn_upgrade.Size = new System.Drawing.Size(100, 40);
            this.btn_upgrade.TabIndex = 3;
            this.btn_upgrade.Text = "Upgrade";
            this.btn_upgrade.UseVisualStyleBackColor = true;
            this.btn_upgrade.Click += new System.EventHandler(this.btn_continue_Click);
            // 
            // lbl_warn_text1
            // 
            this.lbl_warn_text1.Location = new System.Drawing.Point(27, 25);
            this.lbl_warn_text1.Name = "lbl_warn_text1";
            this.lbl_warn_text1.Size = new System.Drawing.Size(337, 32);
            this.lbl_warn_text1.TabIndex = 4;
            this.lbl_warn_text1.Text = "The database does not meet the minimum encryption standards. Keepass will attempt" +
    " to upgrade it.";
            this.lbl_warn_text1.Click += new System.EventHandler(this.lbl_warn_text_Click);
            // 
            // lbl_test2
            // 
            this.lbl_test2.Location = new System.Drawing.Point(27, 70);
            this.lbl_test2.Name = "lbl_test2";
            this.lbl_test2.Size = new System.Drawing.Size(337, 32);
            this.lbl_test2.TabIndex = 5;
            this.lbl_test2.Text = "Before upgrading, backup your database by creating a copy of your Keepass (.kdbx)" +
    " file.";
            this.lbl_test2.Click += new System.EventHandler(this.lbl_test2_Click);
            // 
            // lbl_text3
            // 
            this.lbl_text3.Location = new System.Drawing.Point(27, 118);
            this.lbl_text3.Name = "lbl_text3";
            this.lbl_text3.Size = new System.Drawing.Size(337, 20);
            this.lbl_text3.TabIndex = 6;
            this.lbl_text3.Text = "Do you wish to upgrade the database?";
            // 
            // Warn_Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(395, 205);
            this.Controls.Add(this.lbl_text3);
            this.Controls.Add(this.lbl_test2);
            this.Controls.Add(this.lbl_warn_text1);
            this.Controls.Add(this.btn_upgrade);
            this.Controls.Add(this.btn_cancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Warn_Window";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Warning";
            this.Load += new System.EventHandler(this.Warn_Window_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_upgrade;
        private System.Windows.Forms.Label lbl_warn_text1;
        private System.Windows.Forms.Label lbl_test2;
        private System.Windows.Forms.Label lbl_text3;
    }
}