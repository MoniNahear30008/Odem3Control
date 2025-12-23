namespace OdemControl
{
    partial class upgrade
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
            DoUG = new Button();
            label1 = new Label();
            selFile = new Button();
            ugFln = new Label();
            pwBox = new GroupBox();
            pw = new TextBox();
            pwBox.SuspendLayout();
            SuspendLayout();
            // 
            // DoUG
            // 
            DoUG.Enabled = false;
            DoUG.Location = new Point(27, 91);
            DoUG.Name = "DoUG";
            DoUG.Size = new Size(133, 39);
            DoUG.TabIndex = 0;
            DoUG.Text = "Upgrade";
            DoUG.UseVisualStyleBackColor = true;
            DoUG.Visible = false;
            DoUG.Click += DoUG_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.Red;
            label1.Location = new Point(23, 9);
            label1.Name = "label1";
            label1.Size = new Size(294, 15);
            label1.TabIndex = 1;
            label1.Text = "Make sure power is stable before upgrading the ODEM";
            // 
            // selFile
            // 
            selFile.Location = new Point(27, 33);
            selFile.Name = "selFile";
            selFile.Size = new Size(133, 39);
            selFile.TabIndex = 2;
            selFile.Text = "Select upgrade file";
            selFile.UseVisualStyleBackColor = true;
            selFile.Visible = false;
            selFile.Click += selFile_Click;
            // 
            // ugFln
            // 
            ugFln.AutoSize = true;
            ugFln.Location = new Point(180, 45);
            ugFln.Name = "ugFln";
            ugFln.Size = new Size(16, 15);
            ugFln.TabIndex = 3;
            ugFln.Text = "...";
            ugFln.Visible = false;
            // 
            // pwBox
            // 
            pwBox.Controls.Add(pw);
            pwBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pwBox.Location = new Point(192, 63);
            pwBox.Name = "pwBox";
            pwBox.Size = new Size(200, 68);
            pwBox.TabIndex = 16;
            pwBox.TabStop = false;
            pwBox.Text = "Password";
            // 
            // pw
            // 
            pw.Location = new Point(20, 24);
            pw.Name = "pw";
            pw.PasswordChar = '*';
            pw.Size = new Size(159, 25);
            pw.TabIndex = 0;
            pw.KeyDown += pw_KeyDown;
            // 
            // upgrade
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(488, 169);
            Controls.Add(pwBox);
            Controls.Add(ugFln);
            Controls.Add(selFile);
            Controls.Add(label1);
            Controls.Add(DoUG);
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "upgrade";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "upgrade";
            pwBox.ResumeLayout(false);
            pwBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button DoUG;
        private Label label1;
        private Button selFile;
        private Label ugFln;
        private GroupBox pwBox;
        private TextBox pw;
    }
}