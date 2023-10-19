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
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_continue = new System.Windows.Forms.Button();
            this.lbl_warn_text = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(237, 141);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(100, 40);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_continue
            // 
            this.btn_continue.Location = new System.Drawing.Point(65, 141);
            this.btn_continue.Name = "btn_continue";
            this.btn_continue.Size = new System.Drawing.Size(100, 40);
            this.btn_continue.TabIndex = 3;
            this.btn_continue.Text = "Continue";
            this.btn_continue.UseVisualStyleBackColor = true;
            this.btn_continue.Click += new System.EventHandler(this.btn_continue_Click);
            // 
            // lbl_warn_text
            // 
            this.lbl_warn_text.Location = new System.Drawing.Point(27, 25);
            this.lbl_warn_text.Name = "lbl_warn_text";
            this.lbl_warn_text.Size = new System.Drawing.Size(337, 86);
            this.lbl_warn_text.TabIndex = 4;
            this.lbl_warn_text.Text = "Before continuing, highly suggest to create a new copy before performing the upgr" +
    "ade. Click cotinue to proceed or cancel and create a copy of your file";
            this.lbl_warn_text.Click += new System.EventHandler(this.lbl_warn_text_Click);
            // 
            // Warn_Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 205);
            this.Controls.Add(this.lbl_warn_text);
            this.Controls.Add(this.btn_continue);
            this.Controls.Add(this.btn_cancel);
            this.Name = "Warn_Window";
            this.Text = "Warn_Window";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_continue;
        private System.Windows.Forms.Label lbl_warn_text;
    }
}