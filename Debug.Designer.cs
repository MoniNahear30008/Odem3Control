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
            label3 = new Label();
            I2Cdest = new Label();
            I2CsList = new ComboBox();
            RegsNames = new ComboBox();
            I2Cval = new TextBox();
            regVal = new TextBox();
            regAdd = new TextBox();
            label2 = new Label();
            WriteI2C = new Button();
            WriteReg = new Button();
            resetDSP = new Button();
            wrOTDelay = new Button();
            OTDelay = new NumericUpDown();
            pwBox = new GroupBox();
            pw = new TextBox();
            clr = new Button();
            showVer = new RadioButton();
            showCom = new RadioButton();
            AutoScroll = new CheckBox();
            MonitorView = new RichTextBox();
            splitContainer1 = new SplitContainer();
            groupBox1 = new GroupBox();
            startCustom = new Button();
            VecFln = new Label();
            vecReg = new Label();
            vecList = new ComboBox();
            WrVec = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)OTDelay).BeginInit();
            pwBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(486, 149);
            label3.Name = "label3";
            label3.Size = new Size(35, 15);
            label3.TabIndex = 11;
            label3.Text = "Value";
            // 
            // I2Cdest
            // 
            I2Cdest.AutoSize = true;
            I2Cdest.Location = new Point(322, 149);
            I2Cdest.Name = "I2Cdest";
            I2Cdest.Size = new Size(119, 15);
            I2Cdest.TabIndex = 10;
            I2Cdest.Text = "Ch: ...; Dev: ...; Reg: ...";
            // 
            // I2CsList
            // 
            I2CsList.FormattingEnabled = true;
            I2CsList.Location = new Point(176, 145);
            I2CsList.Name = "I2CsList";
            I2CsList.Size = new Size(135, 23);
            I2CsList.TabIndex = 9;
            I2CsList.SelectedIndexChanged += I2CsList_SelectedIndexChanged;
            // 
            // RegsNames
            // 
            RegsNames.FormattingEnabled = true;
            RegsNames.Location = new Point(176, 63);
            RegsNames.Name = "RegsNames";
            RegsNames.Size = new Size(135, 23);
            RegsNames.TabIndex = 9;
            RegsNames.SelectedIndexChanged += RegsNames_SelectedIndexChanged;
            // 
            // I2Cval
            // 
            I2Cval.Location = new Point(530, 145);
            I2Cval.Name = "I2Cval";
            I2Cval.Size = new Size(82, 23);
            I2Cval.TabIndex = 8;
            // 
            // regVal
            // 
            regVal.Location = new Point(510, 63);
            regVal.Name = "regVal";
            regVal.Size = new Size(82, 23);
            regVal.TabIndex = 8;
            // 
            // regAdd
            // 
            regAdd.Location = new Point(375, 63);
            regAdd.Name = "regAdd";
            regAdd.Size = new Size(73, 23);
            regAdd.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(321, 67);
            label2.Name = "label2";
            label2.Size = new Size(183, 15);
            label2.TabIndex = 7;
            label2.Text = "Addr: 0x                                   Value";
            // 
            // WriteI2C
            // 
            WriteI2C.Location = new Point(23, 145);
            WriteI2C.Name = "WriteI2C";
            WriteI2C.Size = new Size(127, 23);
            WriteI2C.TabIndex = 6;
            WriteI2C.Text = "Write via I2C";
            WriteI2C.UseVisualStyleBackColor = true;
            WriteI2C.Click += WriteI2C_Click;
            // 
            // WriteReg
            // 
            WriteReg.Location = new Point(23, 63);
            WriteReg.Name = "WriteReg";
            WriteReg.Size = new Size(127, 23);
            WriteReg.TabIndex = 6;
            WriteReg.Text = "Write Register";
            WriteReg.UseVisualStyleBackColor = true;
            WriteReg.Click += WriteReg_Click;
            // 
            // resetDSP
            // 
            resetDSP.Location = new Point(611, 20);
            resetDSP.Name = "resetDSP";
            resetDSP.Size = new Size(127, 23);
            resetDSP.TabIndex = 6;
            resetDSP.Text = "Reset DSP";
            resetDSP.UseVisualStyleBackColor = true;
            resetDSP.Click += resetDSP_Click;
            // 
            // wrOTDelay
            // 
            wrOTDelay.Location = new Point(23, 22);
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
            OTDelay.Location = new Point(176, 22);
            OTDelay.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            OTDelay.Minimum = new decimal(new int[] { 10000, 0, 0, int.MinValue });
            OTDelay.Name = "OTDelay";
            OTDelay.Size = new Size(101, 23);
            OTDelay.TabIndex = 5;
            OTDelay.TextAlign = HorizontalAlignment.Right;
            // 
            // pwBox
            // 
            pwBox.Controls.Add(pw);
            pwBox.Location = new Point(304, 76);
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
            MonitorView.Size = new Size(834, 414);
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
            splitContainer1.Panel1.Controls.Add(pwBox);
            splitContainer1.Panel1.Controls.Add(groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(MonitorView);
            splitContainer1.Panel2.Controls.Add(clr);
            splitContainer1.Panel2.Controls.Add(showVer);
            splitContainer1.Panel2.Controls.Add(AutoScroll);
            splitContainer1.Panel2.Controls.Add(showCom);
            splitContainer1.Size = new Size(850, 654);
            splitContainer1.SplitterDistance = 201;
            splitContainer1.TabIndex = 6;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(startCustom);
            groupBox1.Controls.Add(WriteI2C);
            groupBox1.Controls.Add(resetDSP);
            groupBox1.Controls.Add(wrOTDelay);
            groupBox1.Controls.Add(OTDelay);
            groupBox1.Controls.Add(VecFln);
            groupBox1.Controls.Add(I2Cval);
            groupBox1.Controls.Add(vecReg);
            groupBox1.Controls.Add(WriteReg);
            groupBox1.Controls.Add(RegsNames);
            groupBox1.Controls.Add(I2CsList);
            groupBox1.Controls.Add(regAdd);
            groupBox1.Controls.Add(vecList);
            groupBox1.Controls.Add(regVal);
            groupBox1.Controls.Add(I2Cdest);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(WrVec);
            groupBox1.Controls.Add(label3);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(850, 201);
            groupBox1.TabIndex = 18;
            groupBox1.TabStop = false;
            groupBox1.Visible = false;
            // 
            // startCustom
            // 
            startCustom.Location = new Point(736, 133);
            startCustom.Name = "startCustom";
            startCustom.Size = new Size(88, 45);
            startCustom.TabIndex = 18;
            startCustom.Text = "Start Custom Device";
            startCustom.UseVisualStyleBackColor = true;
            startCustom.Click += startCustom_Click;
            // 
            // VecFln
            // 
            VecFln.AutoSize = true;
            VecFln.Location = new Point(434, 108);
            VecFln.Name = "VecFln";
            VecFln.Size = new Size(138, 15);
            VecFln.TabIndex = 17;
            VecFln.Text = "Double click to select file";
            VecFln.MouseDoubleClick += VecFln_MouseDoubleClick;
            // 
            // vecReg
            // 
            vecReg.AutoSize = true;
            vecReg.Location = new Point(342, 108);
            vecReg.Name = "vecReg";
            vecReg.Size = new Size(16, 15);
            vecReg.TabIndex = 16;
            vecReg.Text = "...";
            // 
            // vecList
            // 
            vecList.FormattingEnabled = true;
            vecList.Location = new Point(176, 104);
            vecList.Name = "vecList";
            vecList.Size = new Size(148, 23);
            vecList.TabIndex = 15;
            // 
            // WrVec
            // 
            WrVec.Location = new Point(23, 104);
            WrVec.Name = "WrVec";
            WrVec.Size = new Size(127, 23);
            WrVec.TabIndex = 14;
            WrVec.Text = "Write Vactor";
            WrVec.UseVisualStyleBackColor = true;
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
            ((System.ComponentModel.ISupportInitialize)OTDelay).EndInit();
            pwBox.ResumeLayout(false);
            pwBox.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private GroupBox pwBox;
        private TextBox pw;
        private Button wrOTDelay;
        private NumericUpDown OTDelay;
        private RichTextBox MonitorView;
        private CheckBox AutoScroll;
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
        private Label VecFln;
        private Label vecReg;
        private ComboBox vecList;
        private Button WrVec;
        private GroupBox groupBox1;
        private Button startCustom;
    }
}