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
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ReadInt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tempTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ModeParams).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { deviceState, optoStat });
            statusStrip1.Location = new Point(0, 439);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 12, 0);
            statusStrip1.Size = new Size(584, 22);
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
            // ReadIntProg
            // 
            ReadIntProg.Location = new Point(399, 156);
            ReadIntProg.Name = "ReadIntProg";
            ReadIntProg.Size = new Size(167, 15);
            ReadIntProg.TabIndex = 14;
            // 
            // ReadIntText
            // 
            ReadIntText.AutoSize = true;
            ReadIntText.Location = new Point(460, 138);
            ReadIntText.Name = "ReadIntText";
            ReadIntText.Size = new Size(45, 15);
            ReadIntText.TabIndex = 12;
            ReadIntText.Text = "Minute";
            // 
            // ReadInt
            // 
            ReadInt.Location = new Point(404, 134);
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
            checkT.Location = new Point(440, 131);
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
            autoTemp.Location = new Point(396, 111);
            autoTemp.Name = "autoTemp";
            autoTemp.Size = new Size(177, 19);
            autoTemp.TabIndex = 10;
            autoTemp.Text = "Auto temperature read every";
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
            tempTable.Location = new Point(396, 177);
            tempTable.Name = "tempTable";
            tempTable.ReadOnly = true;
            tempTable.RowHeadersVisible = false;
            tempTable.Size = new Size(164, 144);
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
            ModeParams.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ModeParams.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ModeParams.ColumnHeadersVisible = false;
            ModeParams.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2 });
            ModeParams.Location = new Point(12, 140);
            ModeParams.Margin = new Padding(3, 2, 3, 2);
            ModeParams.Name = "ModeParams";
            ModeParams.ReadOnly = true;
            ModeParams.RowHeadersVisible = false;
            ModeParams.RowHeadersWidth = 51;
            ModeParams.Size = new Size(209, 195);
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
            scanMode.FormattingEnabled = true;
            scanMode.Location = new Point(11, 107);
            scanMode.Margin = new Padding(3, 2, 3, 2);
            scanMode.Name = "scanMode";
            scanMode.Size = new Size(210, 23);
            scanMode.TabIndex = 0;
            scanMode.SelectedIndexChanged += scanMode_SelectedIndexChanged;
            // 
            // confDev
            // 
            confDev.Location = new Point(242, 248);
            confDev.Margin = new Padding(3, 2, 3, 2);
            confDev.Name = "confDev";
            confDev.Size = new Size(95, 45);
            confDev.TabIndex = 3;
            confDev.Text = "Configure device";
            confDev.UseVisualStyleBackColor = true;
            confDev.Click += confDev_Click;
            // 
            // sensitivityHigh
            // 
            sensitivityHigh.AutoSize = true;
            sensitivityHigh.Location = new Point(257, 187);
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
            SensitivityNormal.Location = new Point(257, 152);
            SensitivityNormal.Margin = new Padding(3, 2, 3, 2);
            SensitivityNormal.Name = "SensitivityNormal";
            SensitivityNormal.Size = new Size(65, 19);
            SensitivityNormal.TabIndex = 0;
            SensitivityNormal.TabStop = true;
            SensitivityNormal.Text = "Normal";
            SensitivityNormal.UseVisualStyleBackColor = true;
            SensitivityNormal.CheckedChanged += SensitivityNormal_CheckedChanged;
            // 
            // streaming
            // 
            streaming.AutoSize = true;
            streaming.BackColor = Color.Green;
            streaming.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            streaming.ForeColor = Color.White;
            streaming.Location = new Point(188, 389);
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
            coldLaser.Location = new Point(142, 363);
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
            sStop.Location = new Point(406, 382);
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
            sStart.Location = new Point(15, 382);
            sStart.Margin = new Padding(3, 2, 3, 2);
            sStart.Name = "sStart";
            sStart.Size = new Size(141, 39);
            sStart.TabIndex = 0;
            sStart.Text = "Start streaming";
            sStart.UseVisualStyleBackColor = false;
            sStart.Click += sStart_Click;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(257, 127);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 15;
            label1.Text = "Sensitivity";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightGray;
            ClientSize = new Size(584, 461);
            Controls.Add(label1);
            Controls.Add(sensitivityHigh);
            Controls.Add(confDev);
            Controls.Add(SensitivityNormal);
            Controls.Add(ReadIntProg);
            Controls.Add(ReadIntText);
            Controls.Add(scanMode);
            Controls.Add(ReadInt);
            Controls.Add(ModeParams);
            Controls.Add(checkT);
            Controls.Add(coldLaser);
            Controls.Add(autoTemp);
            Controls.Add(streaming);
            Controls.Add(tempTable);
            Controls.Add(sStop);
            Controls.Add(pictureBox1);
            Controls.Add(debugMode);
            Controls.Add(label3);
            Controls.Add(sStart);
            Controls.Add(IPPort);
            Controls.Add(IPAddredd);
            Controls.Add(devices);
            Controls.Add(label2);
            Controls.Add(connect);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ODEM Control by Lidwave";
            Resize += Form1_Resize;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ReadInt).EndInit();
            ((System.ComponentModel.ISupportInitialize)tempTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)ModeParams).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
        private DataGridView ModeParams;
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
    }
}
