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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            WrVec = new Button();
            VecFln = new Label();
            vecReg = new Label();
            vecList = new ComboBox();
            tabPage2 = new TabPage();
            customConfig = new Button();
            customFiles = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            loadSetting = new Button();
            saveSetting = new Button();
            folderName = new Label();
            getFromFolder = new Button();
            customParams = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            timer1 = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)OTDelay).BeginInit();
            pwBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)customFiles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customParams).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(483, 156);
            label3.Name = "label3";
            label3.Size = new Size(35, 15);
            label3.TabIndex = 11;
            label3.Text = "Value";
            // 
            // I2Cdest
            // 
            I2Cdest.AutoSize = true;
            I2Cdest.Location = new Point(319, 156);
            I2Cdest.Name = "I2Cdest";
            I2Cdest.Size = new Size(119, 15);
            I2Cdest.TabIndex = 10;
            I2Cdest.Text = "Ch: ...; Dev: ...; Reg: ...";
            // 
            // I2CsList
            // 
            I2CsList.FormattingEnabled = true;
            I2CsList.Location = new Point(159, 152);
            I2CsList.Name = "I2CsList";
            I2CsList.Size = new Size(135, 23);
            I2CsList.TabIndex = 9;
            I2CsList.SelectedIndexChanged += I2CsList_SelectedIndexChanged;
            // 
            // RegsNames
            // 
            RegsNames.FormattingEnabled = true;
            RegsNames.Location = new Point(159, 70);
            RegsNames.Name = "RegsNames";
            RegsNames.Size = new Size(135, 23);
            RegsNames.TabIndex = 9;
            RegsNames.SelectedIndexChanged += RegsNames_SelectedIndexChanged;
            // 
            // I2Cval
            // 
            I2Cval.Location = new Point(527, 152);
            I2Cval.Name = "I2Cval";
            I2Cval.Size = new Size(82, 23);
            I2Cval.TabIndex = 8;
            // 
            // regVal
            // 
            regVal.Location = new Point(500, 70);
            regVal.Name = "regVal";
            regVal.Size = new Size(82, 23);
            regVal.TabIndex = 8;
            // 
            // regAdd
            // 
            regAdd.Location = new Point(365, 70);
            regAdd.Name = "regAdd";
            regAdd.Size = new Size(73, 23);
            regAdd.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(311, 74);
            label2.Name = "label2";
            label2.Size = new Size(183, 15);
            label2.TabIndex = 7;
            label2.Text = "Addr: 0x                                   Value";
            // 
            // WriteI2C
            // 
            WriteI2C.Location = new Point(15, 152);
            WriteI2C.Name = "WriteI2C";
            WriteI2C.Size = new Size(127, 23);
            WriteI2C.TabIndex = 6;
            WriteI2C.Text = "Write via I2C";
            WriteI2C.UseVisualStyleBackColor = true;
            WriteI2C.Click += WriteI2C_Click;
            // 
            // WriteReg
            // 
            WriteReg.Location = new Point(15, 70);
            WriteReg.Name = "WriteReg";
            WriteReg.Size = new Size(127, 23);
            WriteReg.TabIndex = 6;
            WriteReg.Text = "Write Register";
            WriteReg.UseVisualStyleBackColor = true;
            WriteReg.Click += WriteReg_Click;
            // 
            // resetDSP
            // 
            resetDSP.Location = new Point(622, 18);
            resetDSP.Name = "resetDSP";
            resetDSP.Size = new Size(127, 23);
            resetDSP.TabIndex = 6;
            resetDSP.Text = "Reset DSP";
            resetDSP.UseVisualStyleBackColor = true;
            resetDSP.Click += resetDSP_Click;
            // 
            // wrOTDelay
            // 
            wrOTDelay.Location = new Point(15, 29);
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
            OTDelay.Location = new Point(159, 29);
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
            pwBox.Location = new Point(301, 92);
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
            MonitorView.Size = new Size(834, 213);
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
            splitContainer1.Panel1.Controls.Add(groupBox1);
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
            splitContainer1.SplitterDistance = 402;
            splitContainer1.TabIndex = 6;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tabControl1);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(850, 402);
            groupBox1.TabIndex = 18;
            groupBox1.TabStop = false;
            groupBox1.Visible = false;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(3, 19);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(844, 380);
            tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(WriteI2C);
            tabPage1.Controls.Add(WrVec);
            tabPage1.Controls.Add(WriteReg);
            tabPage1.Controls.Add(resetDSP);
            tabPage1.Controls.Add(OTDelay);
            tabPage1.Controls.Add(I2Cval);
            tabPage1.Controls.Add(VecFln);
            tabPage1.Controls.Add(I2Cdest);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(wrOTDelay);
            tabPage1.Controls.Add(vecReg);
            tabPage1.Controls.Add(I2CsList);
            tabPage1.Controls.Add(vecList);
            tabPage1.Controls.Add(regAdd);
            tabPage1.Controls.Add(regVal);
            tabPage1.Controls.Add(RegsNames);
            tabPage1.Controls.Add(label2);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(836, 352);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Control";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // WrVec
            // 
            WrVec.Location = new Point(15, 111);
            WrVec.Name = "WrVec";
            WrVec.Size = new Size(127, 23);
            WrVec.TabIndex = 14;
            WrVec.Text = "Write Vactor";
            WrVec.UseVisualStyleBackColor = true;
            WrVec.Click += WrVec_Click;
            // 
            // VecFln
            // 
            VecFln.AutoSize = true;
            VecFln.Location = new Point(411, 115);
            VecFln.Name = "VecFln";
            VecFln.Size = new Size(138, 15);
            VecFln.TabIndex = 17;
            VecFln.Text = "Double click to select file";
            VecFln.MouseDoubleClick += VecFln_MouseDoubleClick;
            // 
            // vecReg
            // 
            vecReg.AutoSize = true;
            vecReg.Location = new Point(319, 115);
            vecReg.Name = "vecReg";
            vecReg.Size = new Size(16, 15);
            vecReg.TabIndex = 16;
            vecReg.Text = "...";
            // 
            // vecList
            // 
            vecList.FormattingEnabled = true;
            vecList.Location = new Point(159, 111);
            vecList.Name = "vecList";
            vecList.Size = new Size(148, 23);
            vecList.TabIndex = 15;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(customConfig);
            tabPage2.Controls.Add(customFiles);
            tabPage2.Controls.Add(loadSetting);
            tabPage2.Controls.Add(saveSetting);
            tabPage2.Controls.Add(folderName);
            tabPage2.Controls.Add(getFromFolder);
            tabPage2.Controls.Add(customParams);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(836, 352);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Custom device";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // customConfig
            // 
            customConfig.Font = new Font("Segoe UI", 12F);
            customConfig.Location = new Point(265, 294);
            customConfig.Name = "customConfig";
            customConfig.Size = new Size(201, 41);
            customConfig.TabIndex = 15;
            customConfig.Text = "Configure custom device";
            customConfig.UseVisualStyleBackColor = true;
            customConfig.Click += customConfig_Click;
            // 
            // customFiles
            // 
            customFiles.AllowUserToAddRows = false;
            customFiles.AllowUserToDeleteRows = false;
            customFiles.AllowUserToResizeRows = false;
            customFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            customFiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            customFiles.ColumnHeadersVisible = false;
            customFiles.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2 });
            customFiles.Location = new Point(265, 101);
            customFiles.Name = "customFiles";
            customFiles.RowHeadersVisible = false;
            customFiles.Size = new Size(490, 183);
            customFiles.TabIndex = 14;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.FillWeight = 25F;
            dataGridViewTextBoxColumn1.HeaderText = "Column1";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.FillWeight = 75F;
            dataGridViewTextBoxColumn2.HeaderText = "Column2";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // loadSetting
            // 
            loadSetting.Font = new Font("Segoe UI", 12F);
            loadSetting.Location = new Point(442, 15);
            loadSetting.Name = "loadSetting";
            loadSetting.Size = new Size(168, 33);
            loadSetting.TabIndex = 12;
            loadSetting.Text = "Load setting from file";
            loadSetting.UseVisualStyleBackColor = true;
            loadSetting.Click += loadSetting_Click;
            // 
            // saveSetting
            // 
            saveSetting.Font = new Font("Segoe UI", 12F);
            saveSetting.Location = new Point(265, 15);
            saveSetting.Name = "saveSetting";
            saveSetting.Size = new Size(168, 33);
            saveSetting.TabIndex = 13;
            saveSetting.Text = "Save setting to file";
            saveSetting.UseVisualStyleBackColor = true;
            saveSetting.Click += saveSetting_Click;
            // 
            // folderName
            // 
            folderName.AutoSize = true;
            folderName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            folderName.Location = new Point(460, 68);
            folderName.Name = "folderName";
            folderName.Size = new Size(19, 21);
            folderName.TabIndex = 11;
            folderName.Text = "...";
            // 
            // getFromFolder
            // 
            getFromFolder.Font = new Font("Segoe UI", 12F);
            getFromFolder.Location = new Point(265, 58);
            getFromFolder.Name = "getFromFolder";
            getFromFolder.Size = new Size(168, 33);
            getFromFolder.TabIndex = 10;
            getFromFolder.Text = "Get files from folder";
            getFromFolder.UseVisualStyleBackColor = true;
            getFromFolder.Click += getFromFolder_Click;
            // 
            // customParams
            // 
            customParams.AllowUserToAddRows = false;
            customParams.AllowUserToDeleteRows = false;
            customParams.AllowUserToResizeRows = false;
            customParams.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            customParams.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            customParams.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            customParams.ColumnHeadersVisible = false;
            customParams.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2 });
            customParams.Location = new Point(6, 6);
            customParams.Name = "customParams";
            customParams.RowHeadersVisible = false;
            customParams.Size = new Size(240, 340);
            customParams.TabIndex = 4;
            // 
            // Column1
            // 
            Column1.FillWeight = 50F;
            Column1.HeaderText = "Column1";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            Column2.FillWeight = 50F;
            Column2.HeaderText = "Column2";
            Column2.Name = "Column2";
            Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // timer1
            // 
            timer1.Interval = 2000;
            timer1.Tick += timer1_Tick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Location = new Point(490, 300);
            label1.Name = "label1";
            label1.Size = new Size(260, 32);
            label1.TabIndex = 16;
            label1.Text = "- Set scan mode and sensitivity in main window\r\n- Use \"Configure custom device\"";
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
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)customFiles).EndInit();
            ((System.ComponentModel.ISupportInitialize)customParams).EndInit();
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
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DataGridView customParams;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private Button loadSetting;
        private Button saveSetting;
        private Label folderName;
        private Button getFromFolder;
        private DataGridView customFiles;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private Button customConfig;
        private Label label1;
    }
}