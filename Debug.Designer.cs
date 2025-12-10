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
            components = new System.ComponentModel.Container();
            dbgControl = new TabControl();
            Control = new TabPage();
            VecFln = new Label();
            vecReg = new Label();
            label3 = new Label();
            I2Cdest = new Label();
            vecList = new ComboBox();
            I2CsList = new ComboBox();
            RegsNames = new ComboBox();
            I2Cval = new TextBox();
            regVal = new TextBox();
            regAdd = new TextBox();
            label2 = new Label();
            WrVec = new Button();
            WriteI2C = new Button();
            WriteReg = new Button();
            resetDSP = new Button();
            wrOTDelay = new Button();
            OTDelay = new NumericUpDown();
            tabPage1 = new TabPage();
            customParams = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            tableLayoutPanel1 = new TableLayoutPanel();
            label4 = new Label();
            cCaptureDelay = new TextBox();
            label5 = new Label();
            cSensitivity = new TextBox();
            label6 = new Label();
            textBox1 = new TextBox();
            label1 = new Label();
            pwBox = new GroupBox();
            pw = new TextBox();
            clr = new Button();
            showVer = new RadioButton();
            showCom = new RadioButton();
            AutoScroll = new CheckBox();
            MonitorView = new RichTextBox();
            splitContainer1 = new SplitContainer();
            timer1 = new System.Windows.Forms.Timer(components);
            dbgControl.SuspendLayout();
            Control.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)OTDelay).BeginInit();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)customParams).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            pwBox.SuspendLayout();
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
            dbgControl.Size = new Size(850, 266);
            dbgControl.TabIndex = 5;
            dbgControl.Visible = false;
            // 
            // Control
            // 
            Control.Controls.Add(VecFln);
            Control.Controls.Add(vecReg);
            Control.Controls.Add(label3);
            Control.Controls.Add(I2Cdest);
            Control.Controls.Add(vecList);
            Control.Controls.Add(I2CsList);
            Control.Controls.Add(RegsNames);
            Control.Controls.Add(I2Cval);
            Control.Controls.Add(regVal);
            Control.Controls.Add(regAdd);
            Control.Controls.Add(label2);
            Control.Controls.Add(WrVec);
            Control.Controls.Add(WriteI2C);
            Control.Controls.Add(WriteReg);
            Control.Controls.Add(resetDSP);
            Control.Controls.Add(wrOTDelay);
            Control.Controls.Add(OTDelay);
            Control.Location = new Point(4, 24);
            Control.Name = "Control";
            Control.Padding = new Padding(3);
            Control.Size = new Size(842, 238);
            Control.TabIndex = 0;
            Control.Text = "Control";
            Control.UseVisualStyleBackColor = true;
            // 
            // VecFln
            // 
            VecFln.AutoSize = true;
            VecFln.Location = new Point(414, 163);
            VecFln.Name = "VecFln";
            VecFln.Size = new Size(138, 15);
            VecFln.TabIndex = 13;
            VecFln.Text = "Double click to select file";
            VecFln.MouseDoubleClick += VecFln_MouseDoubleClick;
            // 
            // vecReg
            // 
            vecReg.AutoSize = true;
            vecReg.Location = new Point(322, 163);
            vecReg.Name = "vecReg";
            vecReg.Size = new Size(16, 15);
            vecReg.TabIndex = 12;
            vecReg.Text = "...";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(466, 117);
            label3.Name = "label3";
            label3.Size = new Size(35, 15);
            label3.TabIndex = 11;
            label3.Text = "Value";
            // 
            // I2Cdest
            // 
            I2Cdest.AutoSize = true;
            I2Cdest.Location = new Point(302, 116);
            I2Cdest.Name = "I2Cdest";
            I2Cdest.Size = new Size(119, 15);
            I2Cdest.TabIndex = 10;
            I2Cdest.Text = "Ch: ...; Dev: ...; Reg: ...";
            // 
            // vecList
            // 
            vecList.FormattingEnabled = true;
            vecList.Location = new Point(156, 159);
            vecList.Name = "vecList";
            vecList.Size = new Size(148, 23);
            vecList.TabIndex = 9;
            vecList.SelectedIndexChanged += vecList_SelectedIndexChanged;
            // 
            // I2CsList
            // 
            I2CsList.FormattingEnabled = true;
            I2CsList.Location = new Point(156, 112);
            I2CsList.Name = "I2CsList";
            I2CsList.Size = new Size(135, 23);
            I2CsList.TabIndex = 9;
            I2CsList.SelectedIndexChanged += I2CsList_SelectedIndexChanged;
            // 
            // RegsNames
            // 
            RegsNames.FormattingEnabled = true;
            RegsNames.Location = new Point(156, 69);
            RegsNames.Name = "RegsNames";
            RegsNames.Size = new Size(135, 23);
            RegsNames.TabIndex = 9;
            RegsNames.SelectedIndexChanged += RegsNames_SelectedIndexChanged;
            // 
            // I2Cval
            // 
            I2Cval.Location = new Point(510, 112);
            I2Cval.Name = "I2Cval";
            I2Cval.Size = new Size(82, 23);
            I2Cval.TabIndex = 8;
            // 
            // regVal
            // 
            regVal.Location = new Point(490, 69);
            regVal.Name = "regVal";
            regVal.Size = new Size(82, 23);
            regVal.TabIndex = 8;
            // 
            // regAdd
            // 
            regAdd.Location = new Point(355, 69);
            regAdd.Name = "regAdd";
            regAdd.Size = new Size(73, 23);
            regAdd.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(301, 73);
            label2.Name = "label2";
            label2.Size = new Size(183, 15);
            label2.TabIndex = 7;
            label2.Text = "Addr: 0x                                   Value";
            // 
            // WrVec
            // 
            WrVec.Location = new Point(19, 159);
            WrVec.Name = "WrVec";
            WrVec.Size = new Size(127, 23);
            WrVec.TabIndex = 6;
            WrVec.Text = "Write Vactor";
            WrVec.UseVisualStyleBackColor = true;
            WrVec.Click += WrVec_Click;
            // 
            // WriteI2C
            // 
            WriteI2C.Location = new Point(19, 112);
            WriteI2C.Name = "WriteI2C";
            WriteI2C.Size = new Size(127, 23);
            WriteI2C.TabIndex = 6;
            WriteI2C.Text = "Write via I2C";
            WriteI2C.UseVisualStyleBackColor = true;
            WriteI2C.Click += WriteI2C_Click;
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
            // resetDSP
            // 
            resetDSP.Location = new Point(325, 24);
            resetDSP.Name = "resetDSP";
            resetDSP.Size = new Size(127, 23);
            resetDSP.TabIndex = 6;
            resetDSP.Text = "Reset DSP";
            resetDSP.UseVisualStyleBackColor = true;
            resetDSP.Click += resetDSP_Click;
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
            tabPage1.Controls.Add(customParams);
            tabPage1.Controls.Add(tableLayoutPanel1);
            tabPage1.Controls.Add(label1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(842, 238);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "Custom setting";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // customParams
            // 
            customParams.AllowUserToAddRows = false;
            customParams.AllowUserToDeleteRows = false;
            customParams.AllowUserToResizeRows = false;
            customParams.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            customParams.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            customParams.ColumnHeadersVisible = false;
            customParams.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2 });
            customParams.Location = new Point(24, 13);
            customParams.Name = "customParams";
            customParams.RowHeadersVisible = false;
            customParams.Size = new Size(240, 150);
            customParams.TabIndex = 2;
            // 
            // Column1
            // 
            Column1.FillWeight = 50F;
            Column1.HeaderText = "Column1";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // Column2
            // 
            Column2.FillWeight = 50F;
            Column2.HeaderText = "Column2";
            Column2.Name = "Column2";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.46154F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.53846F));
            tableLayoutPanel1.Controls.Add(label4, 0, 0);
            tableLayoutPanel1.Controls.Add(cCaptureDelay, 1, 0);
            tableLayoutPanel1.Controls.Add(label5, 0, 1);
            tableLayoutPanel1.Controls.Add(cSensitivity, 1, 1);
            tableLayoutPanel1.Controls.Add(label6, 0, 2);
            tableLayoutPanel1.Controls.Add(textBox1, 1, 2);
            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel1.Location = new Point(531, 13);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(5);
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 59.01639F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 40.98361F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 73F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(271, 226);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(9, 17);
            label4.Name = "label4";
            label4.Size = new Size(81, 15);
            label4.TabIndex = 0;
            label4.Text = "Cupture delay";
            // 
            // cCaptureDelay
            // 
            cCaptureDelay.Anchor = AnchorStyles.None;
            cCaptureDelay.Location = new Point(148, 13);
            cCaptureDelay.Name = "cCaptureDelay";
            cCaptureDelay.Size = new Size(100, 23);
            cCaptureDelay.TabIndex = 1;
            cCaptureDelay.Text = "3200";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new Point(9, 49);
            label5.Name = "label5";
            label5.Size = new Size(60, 15);
            label5.TabIndex = 0;
            label5.Text = "Sensitivity";
            // 
            // cSensitivity
            // 
            cSensitivity.Anchor = AnchorStyles.None;
            cSensitivity.Location = new Point(154, 47);
            cSensitivity.Name = "cSensitivity";
            cSensitivity.Size = new Size(88, 23);
            cSensitivity.TabIndex = 1;
            cSensitivity.Text = "0x81010E3C";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Location = new Point(9, 79);
            label6.Name = "label6";
            label6.Size = new Size(112, 15);
            label6.TabIndex = 0;
            label6.Text = "CFAR Multiplication";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.None;
            textBox1.Location = new Point(154, 75);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(88, 23);
            textBox1.TabIndex = 1;
            textBox1.Text = "0x00000404";
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
            pwBox.Location = new Point(306, 142);
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
            MonitorView.Size = new Size(834, 349);
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
            splitContainer1.Panel2.Controls.Add(pwBox);
            splitContainer1.Panel2.Controls.Add(MonitorView);
            splitContainer1.Panel2.Controls.Add(clr);
            splitContainer1.Panel2.Controls.Add(showVer);
            splitContainer1.Panel2.Controls.Add(AutoScroll);
            splitContainer1.Panel2.Controls.Add(showCom);
            splitContainer1.Size = new Size(850, 654);
            splitContainer1.SplitterDistance = 266;
            splitContainer1.TabIndex = 6;
            // 
            // timer1
            // 
            timer1.Interval = 2000;
            timer1.Tick += timer1_Tick;
            // 
            // Debug
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(850, 654);
            Controls.Add(splitContainer1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Debug";
            Text = "Debug";
            FormClosing += Debug_FormClosing;
            dbgControl.ResumeLayout(false);
            Control.ResumeLayout(false);
            Control.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)OTDelay).EndInit();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)customParams).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            pwBox.ResumeLayout(false);
            pwBox.PerformLayout();
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
        private System.Windows.Forms.Timer timer1;
        private Button resetDSP;
        private ComboBox RegsNames;
        private Button WriteI2C;
        private ComboBox I2CsList;
        private Label I2Cdest;
        private Label label3;
        private TextBox I2Cval;
        private Button WrVec;
        private ComboBox vecList;
        private Label vecReg;
        private Label VecFln;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label4;
        private TextBox cCaptureDelay;
        private Label label5;
        private TextBox cSensitivity;
        private Label label6;
        private TextBox textBox1;
        private DataGridView customParams;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
    }
}