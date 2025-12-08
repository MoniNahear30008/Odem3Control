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
            regVal = new TextBox();
            regAdd = new TextBox();
            label2 = new Label();
            pwBox = new GroupBox();
            pw = new TextBox();
            WriteReg = new Button();
            wrOTDelay = new Button();
            OTDelay = new NumericUpDown();
            tabPage1 = new TabPage();
            label1 = new Label();
            clr = new Button();
            showVer = new RadioButton();
            showCom = new RadioButton();
            AutoScroll = new CheckBox();
            MonitorView = new RichTextBox();
            splitContainer1 = new SplitContainer();
            dbgControl.SuspendLayout();
            Control.SuspendLayout();
            pwBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)OTDelay).BeginInit();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // dbgControl
            // 
            dbgControl.Controls.Add(Control);
            dbgControl.Controls.Add(tabPage1);
            dbgControl.Dock = DockStyle.Fill;
            dbgControl.Location = new Point(0, 0);
            dbgControl.Name = "dbgControl";
            dbgControl.SelectedIndex = 0;
            dbgControl.Size = new Size(850, 193);
            dbgControl.TabIndex = 5;
            dbgControl.Visible = false;
            // 
            // Control
            // 
            Control.Controls.Add(regVal);
            Control.Controls.Add(pwBox);
            Control.Controls.Add(regAdd);
            Control.Controls.Add(label2);
            Control.Controls.Add(WriteReg);
            Control.Controls.Add(wrOTDelay);
            Control.Controls.Add(OTDelay);
            Control.Location = new Point(4, 24);
            Control.Name = "Control";
            Control.Padding = new Padding(3);
            Control.Size = new Size(842, 165);
            Control.TabIndex = 0;
            Control.Text = "Control";
            Control.UseVisualStyleBackColor = true;
            // 
            // regVal
            // 
            regVal.Location = new Point(341, 69);
            regVal.Name = "regVal";
            regVal.Size = new Size(82, 23);
            regVal.TabIndex = 8;
            // 
            // regAdd
            // 
            regAdd.Location = new Point(206, 69);
            regAdd.Name = "regAdd";
            regAdd.Size = new Size(73, 23);
            regAdd.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(152, 73);
            label2.Name = "label2";
            label2.Size = new Size(183, 15);
            label2.TabIndex = 7;
            label2.Text = "Addr: 0x                                   Value";
            // 
            // pwBox
            // 
            pwBox.Controls.Add(pw);
            pwBox.Location = new Point(443, 40);
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
            // WriteReg
            // 
            WriteReg.Location = new Point(19, 69);
            WriteReg.Name = "WriteReg";
            WriteReg.Size = new Size(127, 23);
            WriteReg.TabIndex = 6;
            WriteReg.Text = "Write Register";
            WriteReg.UseVisualStyleBackColor = true;
            WriteReg.Click += WriteReg_Click;
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
            // tabPage1
            // 
            tabPage1.Controls.Add(label1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(842, 183);
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
            // clr
            // 
            clr.Location = new Point(329, 3);
            clr.Name = "clr";
            clr.Size = new Size(75, 23);
            clr.TabIndex = 3;
            clr.Text = "Clear";
            clr.UseVisualStyleBackColor = true;
            clr.Click += clr_Click;
            // 
            // showVer
            // 
            showVer.AutoSize = true;
            showVer.Location = new Point(200, 5);
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
            showCom.Location = new Point(119, 5);
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
            AutoScroll.Location = new Point(12, 5);
            AutoScroll.Name = "AutoScroll";
            AutoScroll.Size = new Size(83, 19);
            AutoScroll.TabIndex = 1;
            AutoScroll.Text = "Atuo scroll";
            AutoScroll.UseVisualStyleBackColor = true;
            // 
            // MonitorView
            // 
            MonitorView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MonitorView.Location = new Point(9, 32);
            MonitorView.Name = "MonitorView";
            MonitorView.ReadOnly = true;
            MonitorView.Size = new Size(834, 242);
            MonitorView.TabIndex = 0;
            MonitorView.Text = "";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dbgControl);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(MonitorView);
            splitContainer1.Panel2.Controls.Add(clr);
            splitContainer1.Panel2.Controls.Add(showVer);
            splitContainer1.Panel2.Controls.Add(AutoScroll);
            splitContainer1.Panel2.Controls.Add(showCom);
            splitContainer1.Size = new Size(850, 474);
            splitContainer1.SplitterDistance = 193;
            splitContainer1.TabIndex = 6;
            // 
            // Debug
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(850, 474);
            Controls.Add(splitContainer1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Debug";
            Text = "Debug";
            FormClosing += Debug_FormClosing;
            dbgControl.ResumeLayout(false);
            Control.ResumeLayout(false);
            Control.PerformLayout();
            pwBox.ResumeLayout(false);
            pwBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)OTDelay).EndInit();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TabControl dbgControl;
        private TabPage Control;
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
        private Button WriteReg;
        private TextBox regVal;
        private TextBox regAdd;
        private Label label2;
        private SplitContainer splitContainer1;
    }
}