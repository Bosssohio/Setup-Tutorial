namespace TestingInstaller
{
    partial class Form3
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
            this.chkCreateDesktopShortcut = new System.Windows.Forms.CheckBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblShortcutName = new System.Windows.Forms.Label();
            this.txtShortcutName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtTargetPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkCreateDesktopShortcut
            // 
            this.chkCreateDesktopShortcut.AutoSize = true;
            this.chkCreateDesktopShortcut.Location = new System.Drawing.Point(12, 12);
            this.chkCreateDesktopShortcut.Name = "chkCreateDesktopShortcut";
            this.chkCreateDesktopShortcut.Size = new System.Drawing.Size(181, 20);
            this.chkCreateDesktopShortcut.TabIndex = 0;
            this.chkCreateDesktopShortcut.Text = "Create a desktop shortcut";
            this.chkCreateDesktopShortcut.UseVisualStyleBackColor = true;
            this.chkCreateDesktopShortcut.CheckedChanged += new System.EventHandler(this.chkCreateDesktopShortcut_CheckedChanged);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 98);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(325, 98);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblShortcutName
            // 
            this.lblShortcutName.AutoSize = true;
            this.lblShortcutName.Location = new System.Drawing.Point(12, 44);
            this.lblShortcutName.Name = "lblShortcutName";
            this.lblShortcutName.Size = new System.Drawing.Size(157, 16);
            this.lblShortcutName.TabIndex = 4;
            this.lblShortcutName.Text = "Shortcut Name (optional):";
            // 
            // txtShortcutName
            // 
            this.txtShortcutName.Location = new System.Drawing.Point(175, 41);
            this.txtShortcutName.Name = "txtShortcutName";
            this.txtShortcutName.Size = new System.Drawing.Size(127, 22);
            this.txtShortcutName.TabIndex = 1;
            this.txtShortcutName.TextChanged += new System.EventHandler(this.txtShortcutName_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(325, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Browse...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtTargetPath
            // 
            this.txtTargetPath.Location = new System.Drawing.Point(175, 66);
            this.txtTargetPath.Name = "txtTargetPath";
            this.txtTargetPath.Size = new System.Drawing.Size(127, 22);
            this.txtTargetPath.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Programs Location";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 133);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtShortcutName);
            this.Controls.Add(this.lblShortcutName);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.chkCreateDesktopShortcut);
            this.Controls.Add(this.txtTargetPath);
            this.MaximizeBox = false;
            this.Name = "Form3";
            this.Text = "Create Shortcut";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkCreateDesktopShortcut;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblShortcutName;
        private System.Windows.Forms.TextBox txtShortcutName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}