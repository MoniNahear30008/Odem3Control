namespace OdemControl
{
    partial class Debug
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
            label1 = new Label();
            button1 = new Button();
            textBox1 = new TextBox();
            button2 = new Button();
            button3 = new Button();
            dbgControl = new TabControl();
            Control = new TabPage();
            wrOTDelay = new Button();
            OTDelay = new NumericUpDown();
            Monitor = new TabPage();
            AutoScroll = new CheckBox();
            MonitorView = new RichTextBox();
            pwBox = new GroupBox();
            pw = new TextBox();
            dbgControl.SuspendLayout();
            Control.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)OTDelay).BeginInit();
            Monitor.SuspendLayout();
            pwBox.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(311, 210);
            label1.Name = "label1";
            label1.Size = new Size(227, 32);
            label1.TabIndex = 0;
            label1.Text = "Under construction";
            // 
            // button1
            // 
            button1.Location = new Point(20, 18);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(124, 38);
            button1.TabIndex = 2;
            button1.Text = "CFAR multiplication";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(160, 27);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(110, 23);
            textBox1.TabIndex = 3;
            // 
            // button2
            // 
            button2.Location = new Point(22, 71);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(122, 34);
            button2.TabIndex = 4;
            button2.Text = "Optotune";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(34, 119);
            button3.Margin = new Padding(3, 2, 3, 2);
            button3.Name = "button3";
            button3.Size = new Size(122, 34);
            button3.TabIndex = 4;
            button3.Text = "Vectors";
            button3.UseVisualStyleBackColor = true;
            // 
            // dbgControl
            // 
            dbgControl.Controls.Add(Control);
            dbgControl.Controls.Add(Monitor);
            dbgControl.Dock = DockStyle.Fill;
            dbgControl.Location = new Point(0, 0);
            dbgControl.Name = "dbgControl";
            dbgControl.SelectedIndex = 0;
            dbgControl.Size = new Size(850, 415);
            dbgControl.TabIndex = 5;
            dbgControl.Visible = false;
            // 
            // Control
            // 
            Control.Controls.Add(wrOTDelay);
            Control.Controls.Add(OTDelay);
            Control.Controls.Add(button1);
            Control.Controls.Add(label1);
            Control.Controls.Add(button3);
            Control.Controls.Add(textBox1);
            Control.Controls.Add(button2);
            Control.Location = new Point(4, 24);
            Control.Name = "Control";
            Control.Padding = new Padding(3);
            Control.Size = new Size(842, 387);
            Control.TabIndex = 0;
            Control.Text = "Control";
            Control.UseVisualStyleBackColor = true;
            // 
            // wrOTDelay
            // 
            wrOTDelay.Location = new Point(34, 287);
            wrOTDelay.Name = "wrOTDelay";
            wrOTDelay.Size = new Size(127, 23);
            wrOTDelay.TabIndex = 6;
            wrOTDelay.Text = "Update OT Delay";
            wrOTDelay.UseVisualStyleBackColor = true;
            // 
            // OTDelay
            // 
            OTDelay.Location = new Point(167, 287);
            OTDelay.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            OTDelay.Minimum = new decimal(new int[] { 10000, 0, 0, int.MinValue });
            OTDelay.Name = "OTDelay";
            OTDelay.Size = new Size(101, 23);
            OTDelay.TabIndex = 5;
            OTDelay.TextAlign = HorizontalAlignment.Right;
            // 
            // Monitor
            // 
            Monitor.Controls.Add(AutoScroll);
            Monitor.Controls.Add(MonitorView);
            Monitor.Location = new Point(4, 24);
            Monitor.Name = "Monitor";
            Monitor.Padding = new Padding(3);
            Monitor.Size = new Size(842, 387);
            Monitor.TabIndex = 1;
            Monitor.Text = "Monitor";
            Monitor.UseVisualStyleBackColor = true;
            // 
            // AutoScroll
            // 
            AutoScroll.AutoSize = true;
            AutoScroll.Checked = true;
            AutoScroll.CheckState = CheckState.Checked;
            AutoScroll.Location = new Point(10, 6);
            AutoScroll.Name = "AutoScroll";
            AutoScroll.Size = new Size(83, 19);
            AutoScroll.TabIndex = 1;
            AutoScroll.Text = "Atuo scroll";
            AutoScroll.UseVisualStyleBackColor = true;
            // 
            // MonitorView
            // 
            MonitorView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MonitorView.Location = new Point(3, 29);
            MonitorView.Name = "MonitorView";
            MonitorView.ReadOnly = true;
            MonitorView.Size = new Size(836, 355);
            MonitorView.TabIndex = 0;
            MonitorView.Text = "";
            // 
            // pwBox
            // 
            pwBox.Controls.Add(pw);
            pwBox.Location = new Point(315, 110);
            pwBox.Name = "pwBox";
            pwBox.Size = new Size(200, 75);
            pwBox.TabIndex = 6;
            pwBox.TabStop = false;
            pwBox.Text = "Password";
            // 
            // pw
            // 
            pw.Location = new Point(23, 33);
            pw.Name = "pw";
            pw.PasswordChar = '*';
            pw.Size = new Size(159, 23);
            pw.TabIndex = 0;
            pw.KeyDown += pw_KeyDown;
            // 
            // Debug
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(850, 415);
            Controls.Add(pwBox);
            Controls.Add(dbgControl);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Debug";
            Text = "Debug";
            FormClosing += Debug_FormClosing;
            dbgControl.ResumeLayout(false);
            Control.ResumeLayout(false);
            Control.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)OTDelay).EndInit();
            Monitor.ResumeLayout(false);
            Monitor.PerformLayout();
            pwBox.ResumeLayout(false);
            pwBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Button button1;
        private TextBox textBox1;
        private Button button2;
        private Button button3;
        private TabControl dbgControl;
        private TabPage Control;
        private TabPage Monitor;
        private GroupBox pwBox;
        private TextBox pw;
        private Button wrOTDelay;
        private NumericUpDown OTDelay;
        private RichTextBox MonitorView;
        private CheckBox AutoScroll;
    }
}