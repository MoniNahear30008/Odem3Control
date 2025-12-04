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
            dbgControl = new TabControl();
            Control = new TabPage();
            wrOTDelay = new Button();
            OTDelay = new NumericUpDown();
            Monitor = new TabPage();
            showVer = new RadioButton();
            showCom = new RadioButton();
            AutoScroll = new CheckBox();
            MonitorView = new RichTextBox();
            tabPage1 = new TabPage();
            label1 = new Label();
            pwBox = new GroupBox();
            pw = new TextBox();
            clr = new Button();
            dbgControl.SuspendLayout();
            Control.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)OTDelay).BeginInit();
            Monitor.SuspendLayout();
            tabPage1.SuspendLayout();
            pwBox.SuspendLayout();
            SuspendLayout();
            // 
            // dbgControl
            // 
            dbgControl.Controls.Add(Control);
            dbgControl.Controls.Add(Monitor);
            dbgControl.Controls.Add(tabPage1);
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
            wrOTDelay.Location = new Point(19, 24);
            wrOTDelay.Name = "wrOTDelay";
            wrOTDelay.Size = new Size(127, 23);
            wrOTDelay.TabIndex = 6;
            wrOTDelay.Text = "Update OT Delay";
            wrOTDelay.UseVisualStyleBackColor = true;
            wrOTDelay.Click += wrOTDelay_Click;
            // 
            // OTDelay
            // 
            OTDelay.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            OTDelay.Location = new Point(152, 24);
            OTDelay.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            OTDelay.Minimum = new decimal(new int[] { 10000, 0, 0, int.MinValue });
            OTDelay.Name = "OTDelay";
            OTDelay.Size = new Size(101, 23);
            OTDelay.TabIndex = 5;
            OTDelay.TextAlign = HorizontalAlignment.Right;
            // 
            // Monitor
            // 
            Monitor.Controls.Add(clr);
            Monitor.Controls.Add(showVer);
            Monitor.Controls.Add(showCom);
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
            // showVer
            // 
            showVer.AutoSize = true;
            showVer.Location = new Point(205, 6);
            showVer.Name = "showVer";
            showVer.Size = new Size(68, 19);
            showVer.TabIndex = 2;
            showVer.Text = "Versions";
            showVer.UseVisualStyleBackColor = true;
            // 
            // showCom
            // 
            showCom.AutoSize = true;
            showCom.Checked = true;
            showCom.Location = new Point(124, 6);
            showCom.Name = "showCom";
            showCom.Size = new Size(62, 19);
            showCom.TabIndex = 2;
            showCom.TabStop = true;
            showCom.Text = "Comm";
            showCom.UseVisualStyleBackColor = true;
            showCom.CheckedChanged += showCom_CheckedChanged;
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
            // tabPage1
            // 
            tabPage1.Controls.Add(label1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(842, 387);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "Custom setting";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(290, 196);
            label1.Name = "label1";
            label1.Size = new Size(235, 29);
            label1.TabIndex = 0;
            label1.Text = "Under construction";
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
            // clr
            // 
            clr.Location = new Point(334, 4);
            clr.Name = "clr";
            clr.Size = new Size(75, 23);
            clr.TabIndex = 3;
            clr.Text = "Clear";
            clr.UseVisualStyleBackColor = true;
            clr.Click += clr_Click;
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
            ((System.ComponentModel.ISupportInitialize)OTDelay).EndInit();
            Monitor.ResumeLayout(false);
            Monitor.PerformLayout();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            pwBox.ResumeLayout(false);
            pwBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TabControl dbgControl;
        private TabPage Control;
        private TabPage Monitor;
        private GroupBox pwBox;
        private TextBox pw;
        private Button wrOTDelay;
        private NumericUpDown OTDelay;
        private RichTextBox MonitorView;
        private CheckBox AutoScroll;
        private TabPage tabPage1;
        private Label label1;
        private RadioButton showVer;
        private RadioButton showCom;
        private Button clr;
    }
}