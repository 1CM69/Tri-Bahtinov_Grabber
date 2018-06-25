namespace Bahtinov_grabber_autofocus
{
    partial class Help
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Help));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.HelpCloseButton = new System.Windows.Forms.Button();
            this.RegularMode = new System.Windows.Forms.CheckBox();
            this.NightMode = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Red;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(1032, 510);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.TabStop = false;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // HelpCloseButton
            // 
            this.HelpCloseButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.HelpCloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.HelpCloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpCloseButton.Location = new System.Drawing.Point(12, 528);
            this.HelpCloseButton.Name = "HelpCloseButton";
            this.HelpCloseButton.Size = new System.Drawing.Size(1032, 23);
            this.HelpCloseButton.TabIndex = 1;
            this.HelpCloseButton.Text = "CLOSE";
            this.HelpCloseButton.UseVisualStyleBackColor = true;
            // 
            // RegularMode
            // 
            this.RegularMode.AutoSize = true;
            this.RegularMode.Location = new System.Drawing.Point(23, 532);
            this.RegularMode.Name = "RegularMode";
            this.RegularMode.Size = new System.Drawing.Size(90, 17);
            this.RegularMode.TabIndex = 2;
            this.RegularMode.TabStop = false;
            this.RegularMode.Text = "RegularMode";
            this.RegularMode.UseVisualStyleBackColor = true;
            this.RegularMode.Visible = false;
            this.RegularMode.CheckedChanged += new System.EventHandler(this.RegularMode_CheckedChanged);
            // 
            // NightMode
            // 
            this.NightMode.AutoSize = true;
            this.NightMode.Location = new System.Drawing.Point(120, 532);
            this.NightMode.Name = "NightMode";
            this.NightMode.Size = new System.Drawing.Size(78, 17);
            this.NightMode.TabIndex = 3;
            this.NightMode.TabStop = false;
            this.NightMode.Text = "NightMode";
            this.NightMode.UseVisualStyleBackColor = true;
            this.NightMode.Visible = false;
            this.NightMode.CheckedChanged += new System.EventHandler(this.NightMode_CheckedChanged);
            // 
            // Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(1056, 561);
            this.Controls.Add(this.NightMode);
            this.Controls.Add(this.RegularMode);
            this.Controls.Add(this.HelpCloseButton);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Help";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Help";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button HelpCloseButton;
        private System.Windows.Forms.CheckBox RegularMode;
        private System.Windows.Forms.CheckBox NightMode;
    }
}