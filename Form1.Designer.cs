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
            mainBox = new GroupBox();
            groupBox4 = new GroupBox();
            ReadIntProg = new ProgressBar();
            ReadIntText = new Label();
            ReadInt = new NumericUpDown();
            checkT = new Button();
            autoTemp = new CheckBox();
            tempTable = new DataGridView();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            groupBox3 = new GroupBox();
            ModeParams = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            scanMode = new ComboBox();
            groupBox2 = new GroupBox();
            rangeMax = new RadioButton();
            RangeExt = new RadioButton();
            RangeNormal = new RadioButton();
            streamBox = new GroupBox();
            streaming = new Label();
            coldLaser = new Label();
            sStop = new Button();
            sStart = new Button();
            confDev = new Button();
            groupBox1 = new GroupBox();
            sensitivityHigh = new RadioButton();
            SensitivityNormal = new RadioButton();
            debugMode = new Button();
            pictureBox1 = new PictureBox();
            label3 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            statusStrip1.SuspendLayout();
            mainBox.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ReadInt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tempTable).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ModeParams).BeginInit();
            groupBox2.SuspendLayout();
            streamBox.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { deviceState, optoStat });
            statusStrip1.Location = new Point(0, 424);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 12, 0);
            statusStrip1.Size = new Size(592, 22);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // deviceState
            // 
            deviceState.Font = new Font("Arial", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            deviceState.ForeColor = Color.Red;
            deviceState.Name = "deviceState";
            deviceState.Size = new Size(92, 17);
            deviceState.Text = "Disconnected";
            // 
            // optoStat
            // 
            optoStat.Maximum = 36;
            optoStat.Name = "optoStat";
            optoStat.Size = new Size(100, 20);
            optoStat.Style = ProgressBarStyle.Marquee;
            optoStat.Visible = false;
            // 
            // connect
            // 
            connect.Location = new Point(10, 9);
            connect.Margin = new Padding(3, 2, 3, 2);
            connect.Name = "connect";
            connect.Size = new Size(82, 22);
            connect.TabIndex = 1;
            connect.Text = "Connect";
            connect.UseVisualStyleBackColor = true;
            connect.Click += connect_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 50);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 7;
            label2.Text = "Devie ID";
            toolTip1.SetToolTip(label2, "Need to select according to Lidwave support");
            // 
            // IPAddredd
            // 
            IPAddredd.Location = new Point(110, 10);
            IPAddredd.Margin = new Padding(3, 2, 3, 2);
            IPAddredd.Name = "IPAddredd";
            IPAddredd.ReadOnly = true;
            IPAddredd.Size = new Size(92, 23);
            IPAddredd.TabIndex = 10;
            toolTip1.SetToolTip(IPAddredd, "Fix IP in this version");
            // 
            // IPPort
            // 
            IPPort.Location = new Point(242, 10);
            IPPort.Margin = new Padding(3, 2, 3, 2);
            IPPort.Name = "IPPort";
            IPPort.ReadOnly = true;
            IPPort.Size = new Size(54, 23);
            IPPort.TabIndex = 9;
            IPPort.TextAlign = HorizontalAlignment.Right;
            toolTip1.SetToolTip(IPPort, "Fix Port in this version");
            // 
            // devices
            // 
            devices.FormattingEnabled = true;
            devices.Location = new Point(78, 47);
            devices.Margin = new Padding(3, 2, 3, 2);
            devices.Name = "devices";
            devices.Size = new Size(124, 23);
            devices.TabIndex = 8;
            toolTip1.SetToolTip(devices, "Will automatic detected in future versions");
            devices.SelectedIndexChanged += devices_SelectedIndexChanged;
            // 
            // mainBox
            // 
            mainBox.Controls.Add(groupBox4);
            mainBox.Controls.Add(groupBox3);
            mainBox.Controls.Add(groupBox2);
            mainBox.Controls.Add(streamBox);
            mainBox.Controls.Add(confDev);
            mainBox.Controls.Add(groupBox1);
            mainBox.Enabled = false;
            mainBox.Location = new Point(10, 82);
            mainBox.Margin = new Padding(3, 2, 3, 2);
            mainBox.Name = "mainBox";
            mainBox.Padding = new Padding(3, 2, 3, 2);
            mainBox.Size = new Size(574, 335);
            mainBox.TabIndex = 2;
            mainBox.TabStop = false;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(ReadIntProg);
            groupBox4.Controls.Add(ReadIntText);
            groupBox4.Controls.Add(ReadInt);
            groupBox4.Controls.Add(checkT);
            groupBox4.Controls.Add(autoTemp);
            groupBox4.Controls.Add(tempTable);
            groupBox4.Location = new Point(378, 20);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(171, 225);
            groupBox4.TabIndex = 10;
            groupBox4.TabStop = false;
            groupBox4.Text = "Temperature";
            // 
            // ReadIntProg
            // 
            ReadIntProg.Location = new Point(14, 70);
            ReadIntProg.Name = "ReadIntProg";
            ReadIntProg.Size = new Size(143, 11);
            ReadIntProg.TabIndex = 14;
            // 
            // ReadIntText
            // 
            ReadIntText.AutoSize = true;
            ReadIntText.Location = new Point(73, 48);
            ReadIntText.Name = "ReadIntText";
            ReadIntText.Size = new Size(45, 15);
            ReadIntText.TabIndex = 12;
            ReadIntText.Text = "Minute";
            // 
            // ReadInt
            // 
            ReadInt.Location = new Point(17, 44);
            ReadInt.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            ReadInt.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ReadInt.Name = "ReadInt";
            ReadInt.Size = new Size(48, 23);
            ReadInt.TabIndex = 11;
            ReadInt.TextAlign = HorizontalAlignment.Right;
            ReadInt.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // checkT
            // 
            checkT.Location = new Point(41, 41);
            checkT.Name = "checkT";
            checkT.Size = new Size(51, 29);
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
            autoTemp.Location = new Point(17, 21);
            autoTemp.Name = "autoTemp";
            autoTemp.Size = new Size(109, 19);
            autoTemp.TabIndex = 10;
            autoTemp.Text = "Auto read every";
            autoTemp.UseVisualStyleBackColor = true;
            autoTemp.CheckedChanged += autoTemp_CheckedChanged;
            // 
            // tempTable
            // 
            tempTable.AllowUserToAddRows = false;
            tempTable.AllowUserToDeleteRows = false;
            tempTable.AllowUserToResizeRows = false;
            tempTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tempTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            tempTable.Columns.AddRange(new DataGridViewColumn[] { Column3, Column4 });
            tempTable.Enabled = false;
            tempTable.Location = new Point(12, 87);
            tempTable.Name = "tempTable";
            tempTable.ReadOnly = true;
            tempTable.RowHeadersVisible = false;
            tempTable.Size = new Size(147, 132);
            tempTable.TabIndex = 9;
            // 
            // Column3
            // 
            Column3.HeaderText = "Section";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 80;
            // 
            // Column4
            // 
            Column4.HeaderText = "Temp";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Width = 50;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(ModeParams);
            groupBox3.Controls.Add(scanMode);
            groupBox3.Location = new Point(10, 20);
            groupBox3.Margin = new Padding(3, 2, 3, 2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(3, 2, 3, 2);
            groupBox3.Size = new Size(230, 213);
            groupBox3.TabIndex = 7;
            groupBox3.TabStop = false;
            groupBox3.Text = "Scan Mode";
            // 
            // ModeParams
            // 
            ModeParams.AllowUserToAddRows = false;
            ModeParams.AllowUserToDeleteRows = false;
            ModeParams.AllowUserToResizeRows = false;
            ModeParams.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ModeParams.ColumnHeadersVisible = false;
            ModeParams.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2 });
            ModeParams.Location = new Point(9, 52);
            ModeParams.Margin = new Padding(3, 2, 3, 2);
            ModeParams.Name = "ModeParams";
            ModeParams.RowHeadersVisible = false;
            ModeParams.RowHeadersWidth = 51;
            ModeParams.Size = new Size(209, 173);
            ModeParams.TabIndex = 1;
            // 
            // Column1
            // 
            Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Column1.HeaderText = "Param";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column1.Width = 125;
            // 
            // Column2
            // 
            Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Column2.HeaderText = "Value";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column2.Width = 125;
            // 
            // scanMode
            // 
            scanMode.FormattingEnabled = true;
            scanMode.Location = new Point(9, 22);
            scanMode.Margin = new Padding(3, 2, 3, 2);
            scanMode.Name = "scanMode";
            scanMode.Size = new Size(210, 23);
            scanMode.TabIndex = 0;
            scanMode.SelectedIndexChanged += scanMode_SelectedIndexChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(rangeMax);
            groupBox2.Controls.Add(RangeExt);
            groupBox2.Controls.Add(RangeNormal);
            groupBox2.Location = new Point(270, 185);
            groupBox2.Margin = new Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 2, 3, 2);
            groupBox2.Size = new Size(78, 48);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Range";
            groupBox2.Visible = false;
            // 
            // rangeMax
            // 
            rangeMax.AutoSize = true;
            rangeMax.Location = new Point(17, 80);
            rangeMax.Margin = new Padding(3, 2, 3, 2);
            rangeMax.Name = "rangeMax";
            rangeMax.Size = new Size(47, 19);
            rangeMax.TabIndex = 0;
            rangeMax.Text = "Max";
            rangeMax.UseVisualStyleBackColor = true;
            rangeMax.CheckedChanged += rangeChanged;
            // 
            // RangeExt
            // 
            RangeExt.AutoSize = true;
            RangeExt.Location = new Point(17, 52);
            RangeExt.Margin = new Padding(3, 2, 3, 2);
            RangeExt.Name = "RangeExt";
            RangeExt.Size = new Size(73, 19);
            RangeExt.TabIndex = 0;
            RangeExt.Text = "Extended";
            RangeExt.UseVisualStyleBackColor = true;
            RangeExt.CheckedChanged += rangeChanged;
            // 
            // RangeNormal
            // 
            RangeNormal.AutoSize = true;
            RangeNormal.Checked = true;
            RangeNormal.Location = new Point(17, 23);
            RangeNormal.Margin = new Padding(3, 2, 3, 2);
            RangeNormal.Name = "RangeNormal";
            RangeNormal.Size = new Size(65, 19);
            RangeNormal.TabIndex = 0;
            RangeNormal.TabStop = true;
            RangeNormal.Text = "Normal";
            RangeNormal.UseVisualStyleBackColor = true;
            RangeNormal.CheckedChanged += rangeChanged;
            // 
            // streamBox
            // 
            streamBox.Controls.Add(streaming);
            streamBox.Controls.Add(coldLaser);
            streamBox.Controls.Add(sStop);
            streamBox.Controls.Add(sStart);
            streamBox.Enabled = false;
            streamBox.Location = new Point(10, 246);
            streamBox.Margin = new Padding(3, 2, 3, 2);
            streamBox.Name = "streamBox";
            streamBox.Padding = new Padding(3, 2, 3, 2);
            streamBox.Size = new Size(544, 81);
            streamBox.TabIndex = 4;
            streamBox.TabStop = false;
            // 
            // streaming
            // 
            streaming.AutoSize = true;
            streaming.BackColor = Color.Green;
            streaming.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            streaming.ForeColor = Color.White;
            streaming.Location = new Point(182, 39);
            streaming.Name = "streaming";
            streaming.Size = new Size(182, 25);
            streaming.TabIndex = 1;
            streaming.Text = "Device is streaming";
            streaming.Visible = false;
            // 
            // coldLaser
            // 
            coldLaser.AutoSize = true;
            coldLaser.Font = new Font("Arial", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            coldLaser.ForeColor = Color.Orange;
            coldLaser.Location = new Point(131, 12);
            coldLaser.Name = "coldLaser";
            coldLaser.Size = new Size(287, 16);
            coldLaser.TabIndex = 13;
            coldLaser.Text = "Laser is warming up - Do not start streaming";
            coldLaser.Visible = false;
            // 
            // sStop
            // 
            sStop.BackColor = Color.Red;
            sStop.Enabled = false;
            sStop.Font = new Font("Arial", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sStop.ForeColor = Color.White;
            sStop.Location = new Point(386, 32);
            sStop.Margin = new Padding(3, 2, 3, 2);
            sStop.Name = "sStop";
            sStop.Size = new Size(141, 39);
            sStop.TabIndex = 0;
            sStop.Text = "Stop streaming";
            sStop.UseVisualStyleBackColor = false;
            sStop.Click += sStop_Click;
            // 
            // sStart
            // 
            sStart.BackColor = Color.Green;
            sStart.Font = new Font("Arial", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sStart.ForeColor = Color.White;
            sStart.Location = new Point(19, 32);
            sStart.Margin = new Padding(3, 2, 3, 2);
            sStart.Name = "sStart";
            sStart.Size = new Size(141, 39);
            sStart.TabIndex = 0;
            sStart.Text = "Start streaming";
            sStart.UseVisualStyleBackColor = false;
            sStart.Click += sStart_Click;
            // 
            // confDev
            // 
            confDev.Location = new Point(257, 124);
            confDev.Margin = new Padding(3, 2, 3, 2);
            confDev.Name = "confDev";
            confDev.Size = new Size(95, 45);
            confDev.TabIndex = 3;
            confDev.Text = "Configure device";
            confDev.UseVisualStyleBackColor = true;
            confDev.Click += confDev_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(sensitivityHigh);
            groupBox1.Controls.Add(SensitivityNormal);
            groupBox1.Location = new Point(257, 20);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(95, 94);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Sensitivity";
            // 
            // sensitivityHigh
            // 
            sensitivityHigh.AutoSize = true;
            sensitivityHigh.Location = new Point(13, 52);
            sensitivityHigh.Margin = new Padding(3, 2, 3, 2);
            sensitivityHigh.Name = "sensitivityHigh";
            sensitivityHigh.Size = new Size(51, 19);
            sensitivityHigh.TabIndex = 0;
            sensitivityHigh.Text = "High";
            sensitivityHigh.UseVisualStyleBackColor = true;
            // 
            // SensitivityNormal
            // 
            SensitivityNormal.AutoSize = true;
            SensitivityNormal.Checked = true;
            SensitivityNormal.Location = new Point(13, 23);
            SensitivityNormal.Margin = new Padding(3, 2, 3, 2);
            SensitivityNormal.Name = "SensitivityNormal";
            SensitivityNormal.Size = new Size(65, 19);
            SensitivityNormal.TabIndex = 0;
            SensitivityNormal.TabStop = true;
            SensitivityNormal.Text = "Normal";
            SensitivityNormal.UseVisualStyleBackColor = true;
            SensitivityNormal.CheckedChanged += SensitivityNormal_CheckedChanged;
            // 
            // debugMode
            // 
            debugMode.Location = new Point(225, 47);
            debugMode.Margin = new Padding(3, 2, 3, 2);
            debugMode.Name = "debugMode";
            debugMode.Size = new Size(84, 23);
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
            pictureBox1.Location = new Point(336, 12);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(248, 44);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(206, 12);
            label3.Name = "label3";
            label3.Size = new Size(29, 15);
            label3.TabIndex = 11;
            label3.Text = "Port";
            // 
            // timer1
            // 
            timer1.Interval = 10000;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(592, 446);
            Controls.Add(pictureBox1);
            Controls.Add(debugMode);
            Controls.Add(label3);
            Controls.Add(IPPort);
            Controls.Add(IPAddredd);
            Controls.Add(devices);
            Controls.Add(label2);
            Controls.Add(mainBox);
            Controls.Add(connect);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ODEM Control by Lidwave";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            mainBox.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ReadInt).EndInit();
            ((System.ComponentModel.ISupportInitialize)tempTable).EndInit();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ModeParams).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            streamBox.ResumeLayout(false);
            streamBox.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel deviceState;
        private Button connect;
        private ToolTip toolTip1;
        private GroupBox mainBox;
        private ComboBox scanMode;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private RadioButton sensitivityHigh;
        private RadioButton SensitivityNormal;
        private RadioButton rangeMax;
        private RadioButton RangeExt;
        private RadioButton RangeNormal;
        private Button confDev;
        private GroupBox streamBox;
        private Button sStop;
        private Button sStart;
        private ComboBox devices;
        private Label label2;
        private Label label3;
        private TextBox IPPort;
        private TextBox IPAddredd;
        public Button debugMode;
        private GroupBox groupBox3;
        private PictureBox pictureBox1;
        private DataGridView ModeParams;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private Button checkT;
        private GroupBox groupBox4;
        private ToolStripProgressBar optoStat;
        private System.Windows.Forms.Timer timer1;
        private Label streaming;
        private DataGridView tempTable;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private CheckBox autoTemp;
        private Label ReadIntText;
        private NumericUpDown ReadInt;
        private Label coldLaser;
        private ProgressBar ReadIntProg;
    }
}
