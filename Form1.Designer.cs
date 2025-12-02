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
            connectState = new ToolStripStatusLabel();
            runstatus = new ToolStripStatusLabel();
            optoStat = new ToolStripProgressBar();
            connect = new Button();
            toolTip1 = new ToolTip(components);
            label2 = new Label();
            IPAddredd = new TextBox();
            IPPort = new TextBox();
            devices = new ComboBox();
            mainBox = new GroupBox();
            groupBox4 = new GroupBox();
            tLaser = new RadioButton();
            tPIC = new RadioButton();
            cTemp = new Label();
            checkT = new Button();
            groupBox3 = new GroupBox();
            ModeParams = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            scanMode = new ComboBox();
            debugMode = new Button();
            streamBox = new GroupBox();
            sStop = new Button();
            sStart = new Button();
            confDev = new Button();
            groupBox2 = new GroupBox();
            rangeMax = new RadioButton();
            RangeExt = new RadioButton();
            RangeNormal = new RadioButton();
            groupBox1 = new GroupBox();
            sensitivityHigh = new RadioButton();
            SensitivityNormal = new RadioButton();
            pictureBox1 = new PictureBox();
            label3 = new Label();
            KeepAlive = new CheckBox();
            timer1 = new System.Windows.Forms.Timer(components);
            statusStrip1.SuspendLayout();
            mainBox.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ModeParams).BeginInit();
            streamBox.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { connectState, runstatus, optoStat });
            statusStrip1.Location = new Point(0, 414);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 12, 0);
            statusStrip1.Size = new Size(592, 22);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // connectState
            // 
            connectState.ForeColor = Color.Red;
            connectState.Name = "connectState";
            connectState.Size = new Size(79, 17);
            connectState.Text = "Disconnected";
            // 
            // runstatus
            // 
            runstatus.Name = "runstatus";
            runstatus.Size = new Size(0, 17);
            // 
            // optoStat
            // 
            optoStat.Maximum = 36;
            optoStat.Name = "optoStat";
            optoStat.Size = new Size(100, 16);
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
            label2.Location = new Point(16, 67);
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
            devices.Location = new Point(79, 64);
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
            mainBox.Controls.Add(debugMode);
            mainBox.Controls.Add(streamBox);
            mainBox.Controls.Add(confDev);
            mainBox.Controls.Add(groupBox2);
            mainBox.Controls.Add(groupBox1);
            mainBox.Enabled = false;
            mainBox.Location = new Point(10, 99);
            mainBox.Margin = new Padding(3, 2, 3, 2);
            mainBox.Name = "mainBox";
            mainBox.Padding = new Padding(3, 2, 3, 2);
            mainBox.Size = new Size(574, 313);
            mainBox.TabIndex = 2;
            mainBox.TabStop = false;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(tLaser);
            groupBox4.Controls.Add(tPIC);
            groupBox4.Controls.Add(cTemp);
            groupBox4.Controls.Add(checkT);
            groupBox4.Location = new Point(257, 180);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(297, 52);
            groupBox4.TabIndex = 10;
            groupBox4.TabStop = false;
            groupBox4.Text = "Temperature of";
            // 
            // tLaser
            // 
            tLaser.AutoSize = true;
            tLaser.Location = new Point(65, 24);
            tLaser.Name = "tLaser";
            tLaser.Size = new Size(52, 19);
            tLaser.TabIndex = 10;
            tLaser.Text = "Laser";
            tLaser.UseVisualStyleBackColor = true;
            // 
            // tPIC
            // 
            tPIC.AutoSize = true;
            tPIC.Checked = true;
            tPIC.Location = new Point(12, 24);
            tPIC.Name = "tPIC";
            tPIC.Size = new Size(43, 19);
            tPIC.TabIndex = 10;
            tPIC.TabStop = true;
            tPIC.Text = "PIC";
            tPIC.UseVisualStyleBackColor = true;
            // 
            // cTemp
            // 
            cTemp.AutoSize = true;
            cTemp.BorderStyle = BorderStyle.FixedSingle;
            cTemp.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cTemp.Location = new Point(204, 24);
            cTemp.Name = "cTemp";
            cTemp.Size = new Size(33, 19);
            cTemp.TabIndex = 9;
            cTemp.Text = ".. °c";
            // 
            // checkT
            // 
            checkT.Location = new Point(127, 19);
            checkT.Name = "checkT";
            checkT.Size = new Size(67, 29);
            checkT.TabIndex = 8;
            checkT.Text = "Read";
            checkT.UseVisualStyleBackColor = true;
            checkT.Click += checkT_Click;
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
            ModeParams.Size = new Size(209, 165);
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
            // debugMode
            // 
            debugMode.Location = new Point(413, 20);
            debugMode.Margin = new Padding(3, 2, 3, 2);
            debugMode.Name = "debugMode";
            debugMode.Size = new Size(132, 34);
            debugMode.TabIndex = 6;
            debugMode.Text = "Debug";
            debugMode.UseVisualStyleBackColor = true;
            debugMode.Visible = false;
            debugMode.Click += debugMode_Click;
            // 
            // streamBox
            // 
            streamBox.Controls.Add(sStop);
            streamBox.Controls.Add(sStart);
            streamBox.Enabled = false;
            streamBox.Location = new Point(10, 237);
            streamBox.Margin = new Padding(3, 2, 3, 2);
            streamBox.Name = "streamBox";
            streamBox.Padding = new Padding(3, 2, 3, 2);
            streamBox.Size = new Size(544, 63);
            streamBox.TabIndex = 4;
            streamBox.TabStop = false;
            // 
            // sStop
            // 
            sStop.BackColor = Color.Red;
            sStop.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            sStop.ForeColor = Color.White;
            sStop.Location = new Point(381, 18);
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
            sStart.BackColor = Color.Lime;
            sStart.Location = new Point(30, 18);
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
            confDev.Location = new Point(257, 129);
            confDev.Margin = new Padding(3, 2, 3, 2);
            confDev.Name = "confDev";
            confDev.Size = new Size(127, 34);
            confDev.TabIndex = 3;
            confDev.Text = "Configure device";
            confDev.UseVisualStyleBackColor = true;
            confDev.Click += confDev_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(rangeMax);
            groupBox2.Controls.Add(RangeExt);
            groupBox2.Controls.Add(RangeNormal);
            groupBox2.Location = new Point(413, 65);
            groupBox2.Margin = new Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 2, 3, 2);
            groupBox2.Size = new Size(117, 101);
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
            // groupBox1
            // 
            groupBox1.Controls.Add(sensitivityHigh);
            groupBox1.Controls.Add(SensitivityNormal);
            groupBox1.Location = new Point(257, 20);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(127, 94);
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
            // KeepAlive
            // 
            KeepAlive.AutoSize = true;
            KeepAlive.Location = new Point(16, 36);
            KeepAlive.Name = "KeepAlive";
            KeepAlive.Size = new Size(78, 19);
            KeepAlive.TabIndex = 12;
            KeepAlive.Text = "KeepAlive";
            KeepAlive.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(592, 436);
            Controls.Add(pictureBox1);
            Controls.Add(KeepAlive);
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
            Text = "ODEM Control by Lidwave";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            mainBox.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ModeParams).EndInit();
            streamBox.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel connectState;
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
        private Button debugMode;
        private GroupBox groupBox3;
        private PictureBox pictureBox1;
        private DataGridView ModeParams;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private CheckBox KeepAlive;
        private Button checkT;
        private Label cTemp;
        private GroupBox groupBox4;
        private RadioButton tLaser;
        private RadioButton tPIC;
        private ToolStripStatusLabel runstatus;
        private ToolStripProgressBar optoStat;
        private System.Windows.Forms.Timer timer1;
    }
}
