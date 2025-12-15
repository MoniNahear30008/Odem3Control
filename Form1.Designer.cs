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
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { deviceState, optoStat });
            statusStrip1.Location = new Point(0, 547);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 15, 0);
            statusStrip1.Size = new Size(782, 24);
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
            ReadIntProg.Location = new Point(81, 182);
            ReadIntProg.Margin = new Padding(4);
            ReadIntProg.Name = "ReadIntProg";
            ReadIntProg.Size = new Size(177, 21);
            ReadIntProg.TabIndex = 14;
            // 
            // ReadIntText
            // 
            ReadIntText.AutoSize = true;
            ReadIntText.Location = new Point(159, 154);
            ReadIntText.Margin = new Padding(4, 0, 4, 0);
            ReadIntText.Name = "ReadIntText";
            ReadIntText.Size = new Size(59, 21);
            ReadIntText.TabIndex = 12;
            ReadIntText.Text = "Minute";
            // 
            // ReadInt
            // 
            ReadInt.Location = new Point(81, 150);
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
            checkT.Location = new Point(119, 151);
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
            autoTemp.Location = new Point(80, 122);
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
            tempTable.Location = new Point(79, 211);
            tempTable.Margin = new Padding(4);
            tempTable.Name = "tempTable";
            tempTable.ReadOnly = true;
            tempTable.RowHeadersVisible = false;
            tempTable.Size = new Size(179, 165);
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
            ModeParams.Size = new Size(269, 228);
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
            scanMode.FormattingEnabled = true;
            scanMode.Location = new Point(17, 125);
            scanMode.Margin = new Padding(4, 3, 4, 3);
            scanMode.Name = "scanMode";
            scanMode.Size = new Size(269, 29);
            scanMode.TabIndex = 0;
            scanMode.SelectedIndexChanged += scanMode_SelectedIndexChanged;
            // 
            // confDev
            // 
            confDev.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            confDev.Location = new Point(311, 302);
            confDev.Margin = new Padding(4, 3, 4, 3);
            confDev.Name = "confDev";
            confDev.Size = new Size(122, 63);
            confDev.TabIndex = 3;
            confDev.Text = "Configure device";
            confDev.UseVisualStyleBackColor = true;
            confDev.Click += confDev_Click;
            // 
            // sensitivityHigh
            // 
            sensitivityHigh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            sensitivityHigh.AutoSize = true;
            sensitivityHigh.Location = new Point(333, 202);
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
            SensitivityNormal.Location = new Point(333, 167);
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
            streaming.Location = new Point(311, 82);
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
            sStop.Location = new Point(580, 69);
            sStop.Margin = new Padding(4, 3, 4, 3);
            sStop.Name = "sStop";
            sStop.Size = new Size(181, 55);
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
            sStart.Location = new Point(33, 69);
            sStart.Margin = new Padding(4, 3, 4, 3);
            sStart.Name = "sStart";
            sStart.Size = new Size(181, 55);
            sStart.TabIndex = 0;
            sStart.Text = "Start streaming";
            sStart.UseVisualStyleBackColor = false;
            sStart.Click += sStart_Click;
            // 
            // debugMode
            // 
            debugMode.Location = new Point(110, 79);
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
            pictureBox1.Location = new Point(12, 16);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(286, 50);
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
            label1.Location = new Point(333, 132);
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
            splitContainer1.Size = new Size(782, 547);
            splitContainer1.SplitterDistance = 402;
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
            splitContainer2.Size = new Size(782, 402);
            splitContainer2.SplitterDistance = 466;
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
            groupBox1.Size = new Size(782, 141);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.LightGray;
            ClientSize = new Size(782, 571);
            Controls.Add(splitContainer1);
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
    }
}
