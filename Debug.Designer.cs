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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Debug));
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            groupBox4 = new GroupBox();
            groupBox3 = new GroupBox();
            vecList = new ComboBox();
            WrVec = new Button();
            VecFln = new Label();
            vecReg = new Label();
            groupBox2 = new GroupBox();
            tabPage2 = new TabPage();
            label4 = new Label();
            impSM = new Button();
            genJSON = new Button();
            selWF = new Button();
            cWaveForm = new DataGridView();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            customMode = new CheckBox();
            cModeParams = new DataGridView();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            label1 = new Label();
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
            ((System.ComponentModel.ISupportInitialize)OTDelay).BeginInit();
            pwBox.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cWaveForm).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cModeParams).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customFiles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customParams).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(176, 63);
            label3.Name = "label3";
            label3.Size = new Size(39, 17);
            label3.TabIndex = 11;
            label3.Text = "Value";
            // 
            // I2Cdest
            // 
            I2Cdest.AutoSize = true;
            I2Cdest.Location = new Point(12, 63);
            I2Cdest.Name = "I2Cdest";
            I2Cdest.Size = new Size(130, 17);
            I2Cdest.TabIndex = 10;
            I2Cdest.Text = "Ch: ...; Dev: ...; Reg: ...";
            // 
            // I2CsList
            // 
            I2CsList.FormattingEnabled = true;
            I2CsList.Location = new Point(166, 22);
            I2CsList.Name = "I2CsList";
            I2CsList.Size = new Size(135, 25);
            I2CsList.TabIndex = 9;
            I2CsList.SelectedIndexChanged += I2CsList_SelectedIndexChanged;
            // 
            // RegsNames
            // 
            RegsNames.FormattingEnabled = true;
            RegsNames.Location = new Point(150, 22);
            RegsNames.Name = "RegsNames";
            RegsNames.Size = new Size(135, 25);
            RegsNames.TabIndex = 9;
            RegsNames.SelectedIndexChanged += RegsNames_SelectedIndexChanged;
            // 
            // I2Cval
            // 
            I2Cval.Location = new Point(220, 59);
            I2Cval.Name = "I2Cval";
            I2Cval.Size = new Size(82, 25);
            I2Cval.TabIndex = 8;
            // 
            // regVal
            // 
            regVal.Location = new Point(204, 59);
            regVal.Name = "regVal";
            regVal.Size = new Size(82, 25);
            regVal.TabIndex = 8;
            // 
            // regAdd
            // 
            regAdd.Location = new Point(69, 59);
            regAdd.Name = "regAdd";
            regAdd.Size = new Size(73, 25);
            regAdd.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 63);
            label2.Name = "label2";
            label2.Size = new Size(228, 17);
            label2.TabIndex = 7;
            label2.Text = "Addr: 0x                                   Value";
            // 
            // WriteI2C
            // 
            WriteI2C.Location = new Point(12, 22);
            WriteI2C.Name = "WriteI2C";
            WriteI2C.Size = new Size(127, 23);
            WriteI2C.TabIndex = 6;
            WriteI2C.Text = "Write via I2C";
            WriteI2C.UseVisualStyleBackColor = true;
            WriteI2C.Click += WriteI2C_Click;
            // 
            // WriteReg
            // 
            WriteReg.Location = new Point(6, 22);
            WriteReg.Name = "WriteReg";
            WriteReg.Size = new Size(127, 23);
            WriteReg.TabIndex = 6;
            WriteReg.Text = "Write Register";
            WriteReg.UseVisualStyleBackColor = true;
            WriteReg.Click += WriteReg_Click;
            // 
            // resetDSP
            // 
            resetDSP.Location = new Point(277, 27);
            resetDSP.Name = "resetDSP";
            resetDSP.Size = new Size(82, 27);
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
            OTDelay.Size = new Size(101, 25);
            OTDelay.TabIndex = 5;
            OTDelay.TextAlign = HorizontalAlignment.Right;
            // 
            // pwBox
            // 
            pwBox.Controls.Add(pw);
            pwBox.Location = new Point(448, 107);
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
            pw.Size = new Size(159, 25);
            pw.TabIndex = 0;
            pw.KeyDown += pw_KeyDown;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(896, 573);
            tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(groupBox4);
            tabPage1.Controls.Add(groupBox3);
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(resetDSP);
            tabPage1.Controls.Add(OTDelay);
            tabPage1.Controls.Add(pwBox);
            tabPage1.Controls.Add(wrOTDelay);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(888, 543);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Control";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(I2CsList);
            groupBox4.Controls.Add(WriteI2C);
            groupBox4.Controls.Add(I2Cdest);
            groupBox4.Controls.Add(label3);
            groupBox4.Controls.Add(I2Cval);
            groupBox4.Location = new Point(15, 304);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(344, 100);
            groupBox4.TabIndex = 20;
            groupBox4.TabStop = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(vecList);
            groupBox3.Controls.Add(WrVec);
            groupBox3.Controls.Add(VecFln);
            groupBox3.Controls.Add(vecReg);
            groupBox3.Location = new Point(15, 193);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(344, 100);
            groupBox3.TabIndex = 19;
            groupBox3.TabStop = false;
            // 
            // vecList
            // 
            vecList.FormattingEnabled = true;
            vecList.Location = new Point(155, 22);
            vecList.Name = "vecList";
            vecList.Size = new Size(148, 25);
            vecList.TabIndex = 15;
            // 
            // WrVec
            // 
            WrVec.Location = new Point(11, 22);
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
            VecFln.Location = new Point(123, 63);
            VecFln.Name = "VecFln";
            VecFln.Size = new Size(152, 17);
            VecFln.TabIndex = 17;
            VecFln.Text = "Double click to select file";
            VecFln.MouseDoubleClick += VecFln_MouseDoubleClick;
            // 
            // vecReg
            // 
            vecReg.AutoSize = true;
            vecReg.Location = new Point(31, 63);
            vecReg.Name = "vecReg";
            vecReg.Size = new Size(17, 17);
            vecReg.TabIndex = 16;
            vecReg.Text = "...";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(WriteReg);
            groupBox2.Controls.Add(RegsNames);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(regVal);
            groupBox2.Controls.Add(regAdd);
            groupBox2.Location = new Point(15, 82);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(344, 100);
            groupBox2.TabIndex = 18;
            groupBox2.TabStop = false;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(impSM);
            tabPage2.Controls.Add(genJSON);
            tabPage2.Controls.Add(selWF);
            tabPage2.Controls.Add(cWaveForm);
            tabPage2.Controls.Add(customMode);
            tabPage2.Controls.Add(cModeParams);
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
            tabPage2.Size = new Size(888, 545);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Custom device";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BorderStyle = BorderStyle.FixedSingle;
            label4.Location = new Point(495, 73);
            label4.Name = "label4";
            label4.Size = new Size(306, 36);
            label4.TabIndex = 22;
            label4.Text = "When setting scan mode from selected scan mode\r\nremmember to use the right waveform files";
            // 
            // impSM
            // 
            impSM.Location = new Point(495, 44);
            impSM.Name = "impSM";
            impSM.Size = new Size(219, 23);
            impSM.TabIndex = 21;
            impSM.Text = "Set table from selected scan mode";
            impSM.UseVisualStyleBackColor = true;
            impSM.Click += impSM_Click;
            // 
            // genJSON
            // 
            genJSON.Location = new Point(695, 381);
            genJSON.Name = "genJSON";
            genJSON.Size = new Size(177, 23);
            genJSON.TabIndex = 20;
            genJSON.Text = "Generate json";
            genJSON.UseVisualStyleBackColor = true;
            genJSON.Click += genJSON_Click;
            // 
            // selWF
            // 
            selWF.Location = new Point(495, 382);
            selWF.Name = "selWF";
            selWF.Size = new Size(177, 23);
            selWF.TabIndex = 20;
            selWF.Text = "Select waveform files from folder";
            selWF.UseVisualStyleBackColor = true;
            selWF.Click += selWF_Click;
            // 
            // cWaveForm
            // 
            cWaveForm.AllowUserToAddRows = false;
            cWaveForm.AllowUserToDeleteRows = false;
            cWaveForm.AllowUserToResizeRows = false;
            cWaveForm.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            cWaveForm.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            cWaveForm.ColumnHeadersVisible = false;
            cWaveForm.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6 });
            cWaveForm.Enabled = false;
            cWaveForm.Location = new Point(492, 414);
            cWaveForm.Name = "cWaveForm";
            cWaveForm.RowHeadersVisible = false;
            cWaveForm.Size = new Size(379, 61);
            cWaveForm.TabIndex = 19;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.FillWeight = 25F;
            dataGridViewTextBoxColumn5.HeaderText = "Column1";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            dataGridViewTextBoxColumn5.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.FillWeight = 75F;
            dataGridViewTextBoxColumn6.HeaderText = "Column2";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            dataGridViewTextBoxColumn6.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // customMode
            // 
            customMode.AutoSize = true;
            customMode.Location = new Point(495, 20);
            customMode.Name = "customMode";
            customMode.Size = new Size(139, 21);
            customMode.TabIndex = 18;
            customMode.Text = "Custom scan mode";
            customMode.UseVisualStyleBackColor = true;
            customMode.CheckedChanged += customMode_CheckedChanged;
            // 
            // cModeParams
            // 
            cModeParams.AllowUserToAddRows = false;
            cModeParams.AllowUserToDeleteRows = false;
            cModeParams.AllowUserToResizeRows = false;
            cModeParams.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            cModeParams.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            cModeParams.ColumnHeadersVisible = false;
            cModeParams.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4 });
            cModeParams.Enabled = false;
            cModeParams.Location = new Point(495, 117);
            cModeParams.Margin = new Padding(4, 3, 4, 3);
            cModeParams.Name = "cModeParams";
            cModeParams.RowHeadersVisible = false;
            cModeParams.RowHeadersWidth = 51;
            cModeParams.Size = new Size(284, 246);
            cModeParams.TabIndex = 17;
            cModeParams.CellEndEdit += cModeParams_CellEndEdit;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.FillWeight = 60F;
            dataGridViewTextBoxColumn3.HeaderText = "Param";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            dataGridViewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.FillWeight = 40F;
            dataGridViewTextBoxColumn4.HeaderText = "Value";
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // label1
            // 
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Location = new Point(265, 107);
            label1.Name = "label1";
            label1.Size = new Size(201, 203);
            label1.TabIndex = 16;
            label1.Text = resources.GetString("label1.Text");
            // 
            // customConfig
            // 
            customConfig.Font = new Font("Segoe UI", 12F);
            customConfig.Location = new Point(265, 322);
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
            customFiles.Location = new Point(16, 380);
            customFiles.Name = "customFiles";
            customFiles.RowHeadersVisible = false;
            customFiles.Size = new Size(450, 183);
            customFiles.TabIndex = 14;
            customFiles.CellDoubleClick += customFiles_CellDoubleClick;
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
            dataGridViewTextBoxColumn2.ReadOnly = true;
            dataGridViewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // loadSetting
            // 
            loadSetting.Font = new Font("Segoe UI", 12F);
            loadSetting.Location = new Point(265, 63);
            loadSetting.Name = "loadSetting";
            loadSetting.Size = new Size(201, 41);
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
            saveSetting.Size = new Size(201, 41);
            saveSetting.TabIndex = 13;
            saveSetting.Text = "Save setting to file";
            saveSetting.UseVisualStyleBackColor = true;
            saveSetting.Click += saveSetting_Click;
            // 
            // folderName
            // 
            folderName.AutoSize = true;
            folderName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            folderName.Location = new Point(211, 351);
            folderName.Name = "folderName";
            folderName.Size = new Size(19, 21);
            folderName.TabIndex = 11;
            folderName.Text = "...";
            // 
            // getFromFolder
            // 
            getFromFolder.Font = new Font("Segoe UI", 12F);
            getFromFolder.Location = new Point(16, 341);
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
            customParams.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            customParams.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            customParams.ColumnHeadersVisible = false;
            customParams.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2 });
            customParams.Location = new Point(6, 6);
            customParams.Name = "customParams";
            customParams.RowHeadersVisible = false;
            customParams.Size = new Size(240, 329);
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
            // Debug
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(896, 573);
            Controls.Add(tabControl1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Debug";
            Text = "Debug";
            FormClosing += Debug_FormClosing;
            ((System.ComponentModel.ISupportInitialize)OTDelay).EndInit();
            pwBox.ResumeLayout(false);
            pwBox.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cWaveForm).EndInit();
            ((System.ComponentModel.ISupportInitialize)cModeParams).EndInit();
            ((System.ComponentModel.ISupportInitialize)customFiles).EndInit();
            ((System.ComponentModel.ISupportInitialize)customParams).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private GroupBox pwBox;
        private TextBox pw;
        private Button wrOTDelay;
        private NumericUpDown OTDelay;
        private Button WriteReg;
        private TextBox regVal;
        private TextBox regAdd;
        private Label label2;
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
        private Button customConfig;
        private Label label1;
        private DataGridView cModeParams;
        private CheckBox customMode;
        private DataGridView cWaveForm;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private Button selWF;
        private Button genJSON;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private GroupBox groupBox4;
        private Button impSM;
        private Label label4;
    }
}