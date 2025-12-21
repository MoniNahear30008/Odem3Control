namespace OdemControl
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            statusStrip1 = new StatusStrip();
            deviceState = new ToolStripStatusLabel();
            optoStat = new ToolStripProgressBar();
            connect = new Button();
            toolTip1 = new ToolTip(components);
            label2 = new Label();
            IPAddredd = new TextBox();
            IPPort = new TextBox();
            devices = new ComboBox();
            ReadIntProg = new ProgressBar();
            ReadIntText = new Label();
            ReadInt = new NumericUpDown();
            checkT = new Button();
            autoTemp = new CheckBox();
            tempTable = new DataGridView();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            ModeParams = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            scanMode = new ComboBox();
            confDev = new Button();
            sensitivityHigh = new RadioButton();
            SensitivityNormal = new RadioButton();
            streaming = new Label();
            coldLaser = new Label();
            sStop = new Button();
            sStart = new Button();
            debugMode = new Button();
            pictureBox1 = new PictureBox();
            label3 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            timer2 = new System.Windows.Forms.Timer(components);
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            groupBox1 = new GroupBox();
            splitContainer3 = new SplitContainer();
            MonitorView = new RichTextBox();
            clr = new Button();
            showVer = new RadioButton();
            AutoScroll = new CheckBox();
            showCom = new RadioButton();
            splitContainer4 = new SplitContainer();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            label8 = new Label();
            groupBox4 = new GroupBox();
            I2CsList = new ComboBox();
            WriteI2C = new Button();
            I2Cdest = new Label();
            label4 = new Label();
            I2Cval = new TextBox();
            groupBox3 = new GroupBox();
            vecList = new ComboBox();
            WrVec = new Button();
            VecFln = new Label();
            vecReg = new Label();
            groupBox2 = new GroupBox();
            groupBox5 = new GroupBox();
            textBox1 = new TextBox();
            WriteReg = new Button();
            RegsNames = new ComboBox();
            regVal = new TextBox();
            regAdd = new TextBox();
            label5 = new Label();
            stopOT = new Button();
            resetDSP = new Button();
            OTDelay = new NumericUpDown();
            wrOTDelay = new Button();
            tabPage2 = new TabPage();
            label6 = new Label();
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
            label7 = new Label();
            customConfig = new Button();
            customFiles = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            loadSetting = new Button();
            saveSetting = new Button();
            folderName = new Label();
            getFromFolder = new Button();
            customParams = new DataGridView();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            pwBox = new GroupBox();
            pw = new TextBox();
            timer3 = new System.Windows.Forms.Timer(components);
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ReadInt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tempTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ModeParams).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer4).BeginInit();
            splitContainer4.Panel1.SuspendLayout();
            splitContainer4.Panel2.SuspendLayout();
            splitContainer4.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)OTDelay).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cWaveForm).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cModeParams).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customFiles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customParams).BeginInit();
            pwBox.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { deviceState, optoStat });
            statusStrip1.Location = new Point(0, 587);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 15, 0);
            statusStrip1.Size = new Size(1484, 24);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // deviceState
            // 
            deviceState.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            deviceState.ForeColor = Color.Red;
            deviceState.Name = "deviceState";
            deviceState.Size = new Size(115, 19);
            deviceState.Text = "Disconnected";
            // 
            // optoStat
            // 
            optoStat.Maximum = 36;
            optoStat.Name = "optoStat";
            optoStat.Size = new Size(129, 28);
            optoStat.Visible = false;
            // 
            // connect
            // 
            connect.Location = new Point(33, 16);
            connect.Margin = new Padding(4, 3, 4, 3);
            connect.Name = "connect";
            connect.Size = new Size(105, 31);
            connect.TabIndex = 1;
            connect.Text = "Connect";
            connect.UseVisualStyleBackColor = true;
            connect.Click += connect_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(39, 73);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(81, 21);
            label2.TabIndex = 7;
            label2.Text = "Device SN";
            toolTip1.SetToolTip(label2, "Need to select according to Lidwave support");
            // 
            // IPAddredd
            // 
            IPAddredd.Location = new Point(161, 17);
            IPAddredd.Margin = new Padding(4, 3, 4, 3);
            IPAddredd.Name = "IPAddredd";
            IPAddredd.ReadOnly = true;
            IPAddredd.Size = new Size(117, 29);
            IPAddredd.TabIndex = 10;
            toolTip1.SetToolTip(IPAddredd, "Fix IP in this version");
            // 
            // IPPort
            // 
            IPPort.Location = new Point(331, 17);
            IPPort.Margin = new Padding(4, 3, 4, 3);
            IPPort.Name = "IPPort";
            IPPort.ReadOnly = true;
            IPPort.Size = new Size(68, 29);
            IPPort.TabIndex = 9;
            IPPort.TextAlign = HorizontalAlignment.Right;
            toolTip1.SetToolTip(IPPort, "Fix Port in this version");
            // 
            // devices
            // 
            devices.DropDownStyle = ComboBoxStyle.DropDownList;
            devices.FormattingEnabled = true;
            devices.Location = new Point(120, 69);
            devices.Margin = new Padding(4, 3, 4, 3);
            devices.Name = "devices";
            devices.Size = new Size(158, 29);
            devices.TabIndex = 8;
            toolTip1.SetToolTip(devices, "Will automatic detected in future versions");
            devices.SelectedIndexChanged += devices_SelectedIndexChanged;
            // 
            // ReadIntProg
            // 
            ReadIntProg.Location = new Point(59, 182);
            ReadIntProg.Margin = new Padding(4);
            ReadIntProg.Name = "ReadIntProg";
            ReadIntProg.Size = new Size(177, 21);
            ReadIntProg.TabIndex = 14;
            // 
            // ReadIntText
            // 
            ReadIntText.AutoSize = true;
            ReadIntText.Location = new Point(137, 154);
            ReadIntText.Margin = new Padding(4, 0, 4, 0);
            ReadIntText.Name = "ReadIntText";
            ReadIntText.Size = new Size(59, 21);
            ReadIntText.TabIndex = 12;
            ReadIntText.Text = "Minute";
            // 
            // ReadInt
            // 
            ReadInt.Location = new Point(59, 150);
            ReadInt.Margin = new Padding(4);
            ReadInt.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            ReadInt.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ReadInt.Name = "ReadInt";
            ReadInt.Size = new Size(62, 29);
            ReadInt.TabIndex = 11;
            ReadInt.TextAlign = HorizontalAlignment.Right;
            ReadInt.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // checkT
            // 
            checkT.Location = new Point(97, 151);
            checkT.Margin = new Padding(4);
            checkT.Name = "checkT";
            checkT.Size = new Size(66, 41);
            checkT.TabIndex = 8;
            checkT.Text = "Read";
            checkT.UseVisualStyleBackColor = true;
            checkT.Visible = false;
            checkT.Click += checkT_Click;
            // 
            // autoTemp
            // 
            autoTemp.AutoSize = true;
            autoTemp.Checked = true;
            autoTemp.CheckState = CheckState.Checked;
            autoTemp.Location = new Point(58, 122);
            autoTemp.Margin = new Padding(4);
            autoTemp.Name = "autoTemp";
            autoTemp.Size = new Size(152, 25);
            autoTemp.TabIndex = 10;
            autoTemp.Text = "Auto temperature\r\n";
            autoTemp.UseVisualStyleBackColor = true;
            autoTemp.CheckedChanged += autoTemp_CheckedChanged;
            // 
            // tempTable
            // 
            tempTable.AllowUserToAddRows = false;
            tempTable.AllowUserToDeleteRows = false;
            tempTable.AllowUserToResizeRows = false;
            tempTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tempTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tempTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            tempTable.ColumnHeadersVisible = false;
            tempTable.Columns.AddRange(new DataGridViewColumn[] { Column3, Column4 });
            tempTable.Enabled = false;
            tempTable.Location = new Point(57, 211);
            tempTable.Margin = new Padding(4);
            tempTable.Name = "tempTable";
            tempTable.ReadOnly = true;
            tempTable.RowHeadersVisible = false;
            tempTable.Size = new Size(182, 150);
            tempTable.TabIndex = 9;
            // 
            // Column3
            // 
            Column3.FillWeight = 60F;
            Column3.HeaderText = "Section";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            // 
            // Column4
            // 
            Column4.FillWeight = 40F;
            Column4.HeaderText = "Temp";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            // 
            // ModeParams
            // 
            ModeParams.AllowUserToAddRows = false;
            ModeParams.AllowUserToDeleteRows = false;
            ModeParams.AllowUserToResizeRows = false;
            ModeParams.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ModeParams.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ModeParams.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ModeParams.ColumnHeadersVisible = false;
            ModeParams.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2 });
            ModeParams.Location = new Point(17, 160);
            ModeParams.Margin = new Padding(4, 3, 4, 3);
            ModeParams.Name = "ModeParams";
            ModeParams.ReadOnly = true;
            ModeParams.RowHeadersVisible = false;
            ModeParams.RowHeadersWidth = 51;
            ModeParams.Size = new Size(293, 286);
            ModeParams.TabIndex = 1;
            // 
            // Column1
            // 
            Column1.FillWeight = 60F;
            Column1.HeaderText = "Param";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            Column2.FillWeight = 40F;
            Column2.HeaderText = "Value";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // scanMode
            // 
            scanMode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            scanMode.DropDownStyle = ComboBoxStyle.DropDownList;
            scanMode.FormattingEnabled = true;
            scanMode.Location = new Point(17, 125);
            scanMode.Margin = new Padding(4, 3, 4, 3);
            scanMode.Name = "scanMode";
            scanMode.Size = new Size(293, 29);
            scanMode.TabIndex = 0;
            scanMode.SelectedIndexChanged += scanMode_SelectedIndexChanged;
            // 
            // confDev
            // 
            confDev.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            confDev.Location = new Point(315, 253);
            confDev.Margin = new Padding(4, 3, 4, 3);
            confDev.Name = "confDev";
            confDev.Size = new Size(102, 63);
            confDev.TabIndex = 3;
            confDev.Text = "Configure device";
            confDev.UseVisualStyleBackColor = true;
            confDev.Click += confDev_Click;
            // 
            // sensitivityHigh
            // 
            sensitivityHigh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            sensitivityHigh.AutoSize = true;
            sensitivityHigh.Location = new Point(336, 202);
            sensitivityHigh.Margin = new Padding(4, 3, 4, 3);
            sensitivityHigh.Name = "sensitivityHigh";
            sensitivityHigh.Size = new Size(61, 25);
            sensitivityHigh.TabIndex = 0;
            sensitivityHigh.Text = "High";
            sensitivityHigh.UseVisualStyleBackColor = true;
            // 
            // SensitivityNormal
            // 
            SensitivityNormal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SensitivityNormal.AutoSize = true;
            SensitivityNormal.Checked = true;
            SensitivityNormal.Location = new Point(336, 167);
            SensitivityNormal.Margin = new Padding(4, 3, 4, 3);
            SensitivityNormal.Name = "SensitivityNormal";
            SensitivityNormal.Size = new Size(81, 25);
            SensitivityNormal.TabIndex = 0;
            SensitivityNormal.TabStop = true;
            SensitivityNormal.Text = "Normal";
            SensitivityNormal.UseVisualStyleBackColor = true;
            SensitivityNormal.CheckedChanged += SensitivityNormal_CheckedChanged;
            // 
            // streaming
            // 
            streaming.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            streaming.AutoSize = true;
            streaming.BackColor = Color.Green;
            streaming.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            streaming.ForeColor = Color.White;
            streaming.Location = new Point(285, 80);
            streaming.Margin = new Padding(4, 0, 4, 0);
            streaming.Name = "streaming";
            streaming.Size = new Size(182, 25);
            streaming.TabIndex = 1;
            streaming.Text = "Device is streaming";
            streaming.Visible = false;
            // 
            // coldLaser
            // 
            coldLaser.AutoSize = true;
            coldLaser.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            coldLaser.ForeColor = Color.Orange;
            coldLaser.Location = new Point(186, 25);
            coldLaser.Margin = new Padding(4, 0, 4, 0);
            coldLaser.Name = "coldLaser";
            coldLaser.Size = new Size(345, 19);
            coldLaser.TabIndex = 13;
            coldLaser.Text = "Laser is warming up - Do not start streaming";
            coldLaser.Visible = false;
            // 
            // sStop
            // 
            sStop.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            sStop.BackColor = Color.Red;
            sStop.Enabled = false;
            sStop.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sStop.ForeColor = Color.White;
            sStop.Location = new Point(515, 52);
            sStop.Margin = new Padding(4, 3, 4, 3);
            sStop.Name = "sStop";
            sStop.Size = new Size(181, 72);
            sStop.TabIndex = 0;
            sStop.Text = "Stop streaming";
            sStop.UseVisualStyleBackColor = false;
            sStop.Click += sStop_Click;
            // 
            // sStart
            // 
            sStart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            sStart.BackColor = Color.Green;
            sStart.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sStart.ForeColor = Color.White;
            sStart.Location = new Point(33, 52);
            sStart.Margin = new Padding(4, 3, 4, 3);
            sStart.Name = "sStart";
            sStart.Size = new Size(181, 72);
            sStart.TabIndex = 0;
            sStart.Text = "Start streaming";
            sStart.UseVisualStyleBackColor = false;
            sStart.Click += sStart_Click;
            // 
            // debugMode
            // 
            debugMode.Location = new Point(15, 78);
            debugMode.Margin = new Padding(4, 3, 4, 3);
            debugMode.Name = "debugMode";
            debugMode.Size = new Size(108, 32);
            debugMode.TabIndex = 6;
            debugMode.Text = "Debug";
            debugMode.UseVisualStyleBackColor = true;
            debugMode.Visible = false;
            debugMode.Click += debugMode_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.InitialImage = (Image)resources.GetObject("pictureBox1.InitialImage");
            pictureBox1.Location = new Point(15, 16);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(244, 31);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(285, 20);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(38, 21);
            label3.TabIndex = 11;
            label3.Text = "Port";
            // 
            // timer1
            // 
            timer1.Interval = 10000;
            timer1.Tick += timer1_Tick;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(336, 132);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(81, 21);
            label1.TabIndex = 15;
            label1.Text = "Sensitivity";
            // 
            // timer2
            // 
            timer2.Tick += timer2_Tick;
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
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox1);
            splitContainer1.Size = new Size(717, 499);
            splitContainer1.SplitterDistance = 365;
            splitContainer1.TabIndex = 16;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(ModeParams);
            splitContainer2.Panel1.Controls.Add(label1);
            splitContainer2.Panel1.Controls.Add(scanMode);
            splitContainer2.Panel1.Controls.Add(sensitivityHigh);
            splitContainer2.Panel1.Controls.Add(confDev);
            splitContainer2.Panel1.Controls.Add(SensitivityNormal);
            splitContainer2.Panel1.Controls.Add(label3);
            splitContainer2.Panel1.Controls.Add(IPPort);
            splitContainer2.Panel1.Controls.Add(IPAddredd);
            splitContainer2.Panel1.Controls.Add(devices);
            splitContainer2.Panel1.Controls.Add(label2);
            splitContainer2.Panel1.Controls.Add(connect);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(tempTable);
            splitContainer2.Panel2.Controls.Add(ReadInt);
            splitContainer2.Panel2.Controls.Add(debugMode);
            splitContainer2.Panel2.Controls.Add(pictureBox1);
            splitContainer2.Panel2.Controls.Add(autoTemp);
            splitContainer2.Panel2.Controls.Add(checkT);
            splitContainer2.Panel2.Controls.Add(ReadIntProg);
            splitContainer2.Panel2.Controls.Add(ReadIntText);
            splitContainer2.Size = new Size(717, 365);
            splitContainer2.SplitterDistance = 436;
            splitContainer2.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(sStart);
            groupBox1.Controls.Add(coldLaser);
            groupBox1.Controls.Add(sStop);
            groupBox1.Controls.Add(streaming);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(717, 130);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.Location = new Point(0, 0);
            splitContainer3.Name = "splitContainer3";
            splitContainer3.Orientation = Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(splitContainer1);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(MonitorView);
            splitContainer3.Panel2.Controls.Add(clr);
            splitContainer3.Panel2.Controls.Add(showVer);
            splitContainer3.Panel2.Controls.Add(AutoScroll);
            splitContainer3.Panel2.Controls.Add(showCom);
            splitContainer3.Size = new Size(717, 587);
            splitContainer3.SplitterDistance = 499;
            splitContainer3.TabIndex = 17;
            // 
            // MonitorView
            // 
            MonitorView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MonitorView.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MonitorView.Location = new Point(3, 33);
            MonitorView.Name = "MonitorView";
            MonitorView.ReadOnly = true;
            MonitorView.Size = new Size(711, 47);
            MonitorView.TabIndex = 8;
            MonitorView.Text = "";
            // 
            // clr
            // 
            clr.Font = new Font("Segoe UI", 9.75F);
            clr.Location = new Point(329, 5);
            clr.Name = "clr";
            clr.Size = new Size(75, 23);
            clr.TabIndex = 7;
            clr.Text = "Clear";
            clr.UseVisualStyleBackColor = true;
            clr.Click += clr_Click;
            // 
            // showVer
            // 
            showVer.AutoSize = true;
            showVer.Font = new Font("Segoe UI", 9.75F);
            showVer.Location = new Point(200, 6);
            showVer.Name = "showVer";
            showVer.Size = new Size(75, 21);
            showVer.TabIndex = 5;
            showVer.Text = "Versions";
            showVer.UseVisualStyleBackColor = true;
            // 
            // AutoScroll
            // 
            AutoScroll.AutoSize = true;
            AutoScroll.Checked = true;
            AutoScroll.CheckState = CheckState.Checked;
            AutoScroll.Font = new Font("Segoe UI", 9.75F);
            AutoScroll.Location = new Point(12, 6);
            AutoScroll.Name = "AutoScroll";
            AutoScroll.Size = new Size(89, 21);
            AutoScroll.TabIndex = 4;
            AutoScroll.Text = "Auto scroll";
            AutoScroll.UseVisualStyleBackColor = true;
            // 
            // showCom
            // 
            showCom.AutoSize = true;
            showCom.Checked = true;
            showCom.Font = new Font("Segoe UI", 9.75F);
            showCom.Location = new Point(119, 6);
            showCom.Name = "showCom";
            showCom.Size = new Size(64, 21);
            showCom.TabIndex = 6;
            showCom.TabStop = true;
            showCom.Text = "Comm";
            showCom.UseVisualStyleBackColor = true;
            showCom.CheckedChanged += showCom_CheckedChanged;
            // 
            // splitContainer4
            // 
            splitContainer4.Dock = DockStyle.Fill;
            splitContainer4.Location = new Point(0, 0);
            splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            splitContainer4.Panel1.Controls.Add(splitContainer3);
            // 
            // splitContainer4.Panel2
            // 
            splitContainer4.Panel2.Controls.Add(tabControl1);
            splitContainer4.Panel2.Controls.Add(pwBox);
            splitContainer4.Size = new Size(1484, 587);
            splitContainer4.SplitterDistance = 717;
            splitContainer4.TabIndex = 18;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(763, 587);
            tabControl1.TabIndex = 20;
            tabControl1.Visible = false;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(label8);
            tabPage1.Controls.Add(groupBox4);
            tabPage1.Controls.Add(groupBox3);
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(stopOT);
            tabPage1.Controls.Add(resetDSP);
            tabPage1.Controls.Add(OTDelay);
            tabPage1.Controls.Add(wrOTDelay);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(755, 557);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Control";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(21, 423);
            label8.Name = "label8";
            label8.Size = new Size(263, 17);
            label8.TabIndex = 21;
            label8.Text = "Use 0x prefix in value fields for Hex number";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(I2CsList);
            groupBox4.Controls.Add(WriteI2C);
            groupBox4.Controls.Add(I2Cdest);
            groupBox4.Controls.Add(label4);
            groupBox4.Controls.Add(I2Cval);
            groupBox4.Font = new Font("Segoe UI", 11.25F);
            groupBox4.Location = new Point(15, 304);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(511, 100);
            groupBox4.TabIndex = 20;
            groupBox4.TabStop = false;
            // 
            // I2CsList
            // 
            I2CsList.FormattingEnabled = true;
            I2CsList.Location = new Point(166, 22);
            I2CsList.Name = "I2CsList";
            I2CsList.Size = new Size(160, 28);
            I2CsList.TabIndex = 9;
            I2CsList.SelectedIndexChanged += I2CsList_SelectedIndexChanged;
            // 
            // WriteI2C
            // 
            WriteI2C.Location = new Point(12, 22);
            WriteI2C.Name = "WriteI2C";
            WriteI2C.Size = new Size(127, 27);
            WriteI2C.TabIndex = 6;
            WriteI2C.Text = "Write via I2C";
            WriteI2C.UseVisualStyleBackColor = true;
            WriteI2C.Click += WriteI2C_Click;
            // 
            // I2Cdest
            // 
            I2Cdest.AutoSize = true;
            I2Cdest.Location = new Point(12, 63);
            I2Cdest.Name = "I2Cdest";
            I2Cdest.Size = new Size(140, 20);
            I2Cdest.TabIndex = 10;
            I2Cdest.Text = "Ch: ...; Dev: ...; Reg: ...";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(285, 63);
            label4.Name = "label4";
            label4.Size = new Size(45, 20);
            label4.TabIndex = 11;
            label4.Text = "Value";
            // 
            // I2Cval
            // 
            I2Cval.Location = new Point(348, 60);
            I2Cval.Name = "I2Cval";
            I2Cval.Size = new Size(114, 27);
            I2Cval.TabIndex = 8;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(vecList);
            groupBox3.Controls.Add(WrVec);
            groupBox3.Controls.Add(VecFln);
            groupBox3.Controls.Add(vecReg);
            groupBox3.Font = new Font("Segoe UI", 11.25F);
            groupBox3.Location = new Point(15, 193);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(511, 100);
            groupBox3.TabIndex = 19;
            groupBox3.TabStop = false;
            // 
            // vecList
            // 
            vecList.FormattingEnabled = true;
            vecList.Location = new Point(155, 22);
            vecList.Name = "vecList";
            vecList.Size = new Size(171, 28);
            vecList.TabIndex = 15;
            // 
            // WrVec
            // 
            WrVec.Location = new Point(11, 22);
            WrVec.Name = "WrVec";
            WrVec.Size = new Size(127, 27);
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
            VecFln.Size = new Size(176, 20);
            VecFln.TabIndex = 17;
            VecFln.Text = "Double click to select file";
            VecFln.MouseDoubleClick += VecFln_MouseDoubleClick;
            // 
            // vecReg
            // 
            vecReg.AutoSize = true;
            vecReg.Location = new Point(31, 63);
            vecReg.Name = "vecReg";
            vecReg.Size = new Size(18, 20);
            vecReg.TabIndex = 16;
            vecReg.Text = "...";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox5);
            groupBox2.Controls.Add(WriteReg);
            groupBox2.Controls.Add(RegsNames);
            groupBox2.Controls.Add(regVal);
            groupBox2.Controls.Add(regAdd);
            groupBox2.Controls.Add(label5);
            groupBox2.Font = new Font("Segoe UI", 11.25F);
            groupBox2.Location = new Point(15, 82);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(511, 100);
            groupBox2.TabIndex = 18;
            groupBox2.TabStop = false;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(textBox1);
            groupBox5.Location = new Point(328, 52);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(200, 75);
            groupBox5.TabIndex = 6;
            groupBox5.TabStop = false;
            groupBox5.Text = "Password";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(23, 33);
            textBox1.Name = "textBox1";
            textBox1.PasswordChar = '*';
            textBox1.Size = new Size(159, 27);
            textBox1.TabIndex = 0;
            // 
            // WriteReg
            // 
            WriteReg.Location = new Point(6, 22);
            WriteReg.Name = "WriteReg";
            WriteReg.Size = new Size(127, 27);
            WriteReg.TabIndex = 6;
            WriteReg.Text = "Write Register";
            WriteReg.UseVisualStyleBackColor = true;
            WriteReg.Click += WriteReg_Click;
            // 
            // RegsNames
            // 
            RegsNames.FormattingEnabled = true;
            RegsNames.Location = new Point(188, 21);
            RegsNames.Name = "RegsNames";
            RegsNames.Size = new Size(176, 28);
            RegsNames.TabIndex = 9;
            RegsNames.SelectedIndexChanged += RegsNames_SelectedIndexChanged;
            // 
            // regVal
            // 
            regVal.Location = new Point(262, 60);
            regVal.Name = "regVal";
            regVal.Size = new Size(102, 27);
            regVal.TabIndex = 8;
            // 
            // regAdd
            // 
            regAdd.Location = new Point(79, 59);
            regAdd.Name = "regAdd";
            regAdd.Size = new Size(90, 27);
            regAdd.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 63);
            label5.Name = "label5";
            label5.Size = new Size(240, 20);
            label5.TabIndex = 7;
            label5.Text = "Addr: 0x                                   Value";
            // 
            // stopOT
            // 
            stopOT.Font = new Font("Segoe UI", 11.25F);
            stopOT.Location = new Point(466, 28);
            stopOT.Name = "stopOT";
            stopOT.Size = new Size(120, 29);
            stopOT.TabIndex = 6;
            stopOT.Text = "Stop OT";
            stopOT.UseVisualStyleBackColor = true;
            stopOT.Click += stopOT_Click;
            // 
            // resetDSP
            // 
            resetDSP.Font = new Font("Segoe UI", 11.25F);
            resetDSP.Location = new Point(308, 28);
            resetDSP.Name = "resetDSP";
            resetDSP.Size = new Size(120, 29);
            resetDSP.TabIndex = 6;
            resetDSP.Text = "Reset DSP";
            resetDSP.UseVisualStyleBackColor = true;
            resetDSP.Click += resetDSP_Click;
            // 
            // OTDelay
            // 
            OTDelay.Font = new Font("Segoe UI", 11.25F);
            OTDelay.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            OTDelay.Location = new Point(190, 29);
            OTDelay.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            OTDelay.Minimum = new decimal(new int[] { 10000, 0, 0, int.MinValue });
            OTDelay.Name = "OTDelay";
            OTDelay.Size = new Size(101, 27);
            OTDelay.TabIndex = 5;
            OTDelay.TextAlign = HorizontalAlignment.Right;
            // 
            // wrOTDelay
            // 
            wrOTDelay.Font = new Font("Segoe UI", 11.25F);
            wrOTDelay.Location = new Point(15, 29);
            wrOTDelay.Name = "wrOTDelay";
            wrOTDelay.Size = new Size(169, 27);
            wrOTDelay.TabIndex = 6;
            wrOTDelay.Text = "Update OT Delay";
            wrOTDelay.UseVisualStyleBackColor = true;
            wrOTDelay.Click += wrOTDelay_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(impSM);
            tabPage2.Controls.Add(genJSON);
            tabPage2.Controls.Add(selWF);
            tabPage2.Controls.Add(cWaveForm);
            tabPage2.Controls.Add(customMode);
            tabPage2.Controls.Add(cModeParams);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(customConfig);
            tabPage2.Controls.Add(customFiles);
            tabPage2.Controls.Add(loadSetting);
            tabPage2.Controls.Add(saveSetting);
            tabPage2.Controls.Add(folderName);
            tabPage2.Controls.Add(getFromFolder);
            tabPage2.Controls.Add(customParams);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(755, 557);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Custom device";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BorderStyle = BorderStyle.FixedSingle;
            label6.Location = new Point(498, 76);
            label6.Name = "label6";
            label6.Size = new Size(241, 36);
            label6.TabIndex = 22;
            label6.Text = "Update the waveform files after setting \r\nscan mode from selected scan mode ";
            // 
            // impSM
            // 
            impSM.Location = new Point(495, 44);
            impSM.Name = "impSM";
            impSM.Size = new Size(219, 28);
            impSM.TabIndex = 21;
            impSM.Text = "Set table from selected scan mode";
            impSM.UseVisualStyleBackColor = true;
            impSM.Click += impSM_Click;
            // 
            // genJSON
            // 
            genJSON.ForeColor = Color.Red;
            genJSON.Location = new Point(498, 489);
            genJSON.Name = "genJSON";
            genJSON.Size = new Size(244, 29);
            genJSON.TabIndex = 20;
            genJSON.Text = "Generate json";
            genJSON.UseVisualStyleBackColor = true;
            genJSON.Click += genJSON_Click;
            // 
            // selWF
            // 
            selWF.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            selWF.Location = new Point(498, 382);
            selWF.Name = "selWF";
            selWF.Size = new Size(244, 29);
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
            cWaveForm.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cWaveForm.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            cWaveForm.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            cWaveForm.ColumnHeadersVisible = false;
            cWaveForm.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6 });
            cWaveForm.Location = new Point(498, 420);
            cWaveForm.Name = "cWaveForm";
            cWaveForm.RowHeadersVisible = false;
            cWaveForm.Size = new Size(241, 61);
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
            customMode.Location = new Point(498, 23);
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
            cModeParams.Size = new Size(244, 246);
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
            // label7
            // 
            label7.BorderStyle = BorderStyle.FixedSingle;
            label7.Location = new Point(265, 84);
            label7.Name = "label7";
            label7.Size = new Size(151, 218);
            label7.TabIndex = 16;
            label7.Text = resources.GetString("label7.Text");
            // 
            // customConfig
            // 
            customConfig.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
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
            customFiles.Size = new Size(450, 171);
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
            loadSetting.Font = new Font("Segoe UI", 9.75F);
            loadSetting.Location = new Point(265, 52);
            loadSetting.Name = "loadSetting";
            loadSetting.Size = new Size(151, 29);
            loadSetting.TabIndex = 12;
            loadSetting.Text = "Load setting from file";
            loadSetting.UseVisualStyleBackColor = true;
            loadSetting.Click += loadSetting_Click;
            // 
            // saveSetting
            // 
            saveSetting.Font = new Font("Segoe UI", 9.75F);
            saveSetting.Location = new Point(265, 15);
            saveSetting.Name = "saveSetting";
            saveSetting.Size = new Size(151, 29);
            saveSetting.TabIndex = 13;
            saveSetting.Text = "Save setting to file";
            saveSetting.UseVisualStyleBackColor = true;
            saveSetting.Click += saveSetting_Click;
            // 
            // folderName
            // 
            folderName.AutoSize = true;
            folderName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            folderName.Location = new Point(214, 354);
            folderName.Name = "folderName";
            folderName.Size = new Size(19, 21);
            folderName.TabIndex = 11;
            folderName.Text = "...";
            // 
            // getFromFolder
            // 
            getFromFolder.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
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
            customParams.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn7, dataGridViewTextBoxColumn8 });
            customParams.Location = new Point(6, 6);
            customParams.Name = "customParams";
            customParams.RowHeadersVisible = false;
            customParams.Size = new Size(240, 329);
            customParams.TabIndex = 4;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.FillWeight = 50F;
            dataGridViewTextBoxColumn7.HeaderText = "Column1";
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            dataGridViewTextBoxColumn7.ReadOnly = true;
            dataGridViewTextBoxColumn7.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewTextBoxColumn8.FillWeight = 50F;
            dataGridViewTextBoxColumn8.HeaderText = "Column2";
            dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            dataGridViewTextBoxColumn8.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // pwBox
            // 
            pwBox.Controls.Add(pw);
            pwBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pwBox.Location = new Point(281, 86);
            pwBox.Name = "pwBox";
            pwBox.Size = new Size(200, 68);
            pwBox.TabIndex = 15;
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
            // timer3
            // 
            timer3.Interval = 2000;
            timer3.Tick += timer3_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.LightGray;
            ClientSize = new Size(1484, 611);
            Controls.Add(splitContainer4);
            Controls.Add(statusStrip1);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ODEM Control by Lidwave";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ReadInt).EndInit();
            ((System.ComponentModel.ISupportInitialize)tempTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)ModeParams).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            splitContainer4.Panel1.ResumeLayout(false);
            splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer4).EndInit();
            splitContainer4.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)OTDelay).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cWaveForm).EndInit();
            ((System.ComponentModel.ISupportInitialize)cModeParams).EndInit();
            ((System.ComponentModel.ISupportInitialize)customFiles).EndInit();
            ((System.ComponentModel.ISupportInitialize)customParams).EndInit();
            pwBox.ResumeLayout(false);
            pwBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel deviceState;
        private Button connect;
        private ToolTip toolTip1;
        private ComboBox scanMode;
        private RadioButton sensitivityHigh;
        private RadioButton SensitivityNormal;
        private Button confDev;
        private Button sStop;
        private Button sStart;
        private ComboBox devices;
        private Label label2;
        private Label label3;
        private TextBox IPPort;
        private TextBox IPAddredd;
        public Button debugMode;
        private PictureBox pictureBox1;
        public DataGridView ModeParams;
        private Button checkT;
        private ToolStripProgressBar optoStat;
        private System.Windows.Forms.Timer timer1;
        private Label streaming;
        private DataGridView tempTable;
        private CheckBox autoTemp;
        private Label ReadIntText;
        private NumericUpDown ReadInt;
        private Label coldLaser;
        private ProgressBar ReadIntProg;
        private Label label1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Timer timer2;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private GroupBox groupBox1;
        private SplitContainer splitContainer3;
        private Button clr;
        private RadioButton showVer;
        private CheckBox AutoScroll;
        private RadioButton showCom;
        private RichTextBox MonitorView;
        private SplitContainer splitContainer4;
        private GroupBox pwBox;
        private TextBox pw;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private GroupBox groupBox4;
        private ComboBox I2CsList;
        private Button WriteI2C;
        private Label I2Cdest;
        private Label label4;
        private TextBox I2Cval;
        private GroupBox groupBox3;
        private ComboBox vecList;
        private Button WrVec;
        private Label VecFln;
        private Label vecReg;
        private GroupBox groupBox2;
        private GroupBox groupBox5;
        private TextBox textBox1;
        private Button WriteReg;
        private ComboBox RegsNames;
        private Label label5;
        private TextBox regVal;
        private TextBox regAdd;
        private Button resetDSP;
        private NumericUpDown OTDelay;
        private Button wrOTDelay;
        private TabPage tabPage2;
        private Label label6;
        private Button impSM;
        private Button genJSON;
        private Button selWF;
        private DataGridView cWaveForm;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private CheckBox customMode;
        private DataGridView cModeParams;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private Label label7;
        private Button customConfig;
        private DataGridView customFiles;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private Button loadSetting;
        private Button saveSetting;
        private Label folderName;
        private Button getFromFolder;
        private DataGridView customParams;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.Timer timer3;
        private Label label8;
        private Button stopOT;
    }
}
