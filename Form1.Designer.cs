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
            paramTable = new DataGridView();
            checkT = new Button();
            tempTable = new DataGridView();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            label3 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            splitContainer3 = new SplitContainer();
            groupBox1 = new GroupBox();
            loadAwg = new Button();
            awglen = new Label();
            sendAWG = new Button();
            runAWG = new Button();
            progAWG = new Button();
            comports = new ComboBox();
            setAll = new Button();
            MonitorView = new RichTextBox();
            clr = new Button();
            AutoScroll = new CheckBox();
            Column1 = new DataGridViewButtonColumn();
            Column2 = new DataGridViewTextBoxColumn();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)paramTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tempTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { deviceState, optoStat });
            statusStrip1.Location = new Point(0, 587);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 15, 0);
            statusStrip1.Size = new Size(833, 24);
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
            connect.Location = new Point(12, 14);
            connect.Margin = new Padding(4, 3, 4, 3);
            connect.Name = "connect";
            connect.Size = new Size(105, 31);
            connect.TabIndex = 1;
            connect.Text = "Connect";
            connect.UseVisualStyleBackColor = true;
            connect.Click += connect_Click;
            // 
            // toolTip1
            // 
            toolTip1.AutoPopDelay = 2000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 100;
            // 
            // paramTable
            // 
            paramTable.AllowUserToAddRows = false;
            paramTable.AllowUserToDeleteRows = false;
            paramTable.AllowUserToResizeRows = false;
            paramTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            paramTable.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2 });
            paramTable.Location = new Point(16, 101);
            paramTable.Name = "paramTable";
            paramTable.RowHeadersVisible = false;
            paramTable.ShowCellToolTips = false;
            paramTable.Size = new Size(258, 290);
            paramTable.TabIndex = 26;
            toolTip1.SetToolTip(paramTable, "Start with 0x for hex value");
            paramTable.CellClick += paramTable_CellClick;
            // 
            // checkT
            // 
            checkT.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkT.Location = new Point(632, 21);
            checkT.Margin = new Padding(4);
            checkT.Name = "checkT";
            checkT.Size = new Size(177, 34);
            checkT.TabIndex = 8;
            checkT.Text = "Read temperature";
            checkT.UseVisualStyleBackColor = true;
            checkT.Click += checkT_Click;
            // 
            // tempTable
            // 
            tempTable.AllowUserToAddRows = false;
            tempTable.AllowUserToDeleteRows = false;
            tempTable.AllowUserToResizeRows = false;
            tempTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            tempTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tempTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            tempTable.ColumnHeadersVisible = false;
            tempTable.Columns.AddRange(new DataGridViewColumn[] { Column3, Column4 });
            tempTable.Enabled = false;
            tempTable.Location = new Point(632, 63);
            tempTable.Margin = new Padding(4);
            tempTable.Name = "tempTable";
            tempTable.ReadOnly = true;
            tempTable.RowHeadersVisible = false;
            tempTable.Size = new Size(177, 119);
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
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(125, 19);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(66, 17);
            label3.TabIndex = 11;
            label3.Text = "COM Port";
            // 
            // timer1
            // 
            timer1.Interval = 10000;
            timer1.Tick += timer1_Tick;
            // 
            // splitContainer3
            // 
            splitContainer3.BackColor = Color.Transparent;
            splitContainer3.BorderStyle = BorderStyle.FixedSingle;
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.Location = new Point(0, 0);
            splitContainer3.Name = "splitContainer3";
            splitContainer3.Orientation = Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.BackColor = Color.Transparent;
            splitContainer3.Panel1.Controls.Add(groupBox1);
            splitContainer3.Panel1.Controls.Add(paramTable);
            splitContainer3.Panel1.Controls.Add(tempTable);
            splitContainer3.Panel1.Controls.Add(comports);
            splitContainer3.Panel1.Controls.Add(checkT);
            splitContainer3.Panel1.Controls.Add(label3);
            splitContainer3.Panel1.Controls.Add(setAll);
            splitContainer3.Panel1.Controls.Add(connect);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(MonitorView);
            splitContainer3.Panel2.Controls.Add(clr);
            splitContainer3.Panel2.Controls.Add(AutoScroll);
            splitContainer3.Size = new Size(833, 587);
            splitContainer3.SplitterDistance = 396;
            splitContainer3.TabIndex = 17;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(loadAwg);
            groupBox1.Controls.Add(awglen);
            groupBox1.Controls.Add(sendAWG);
            groupBox1.Controls.Add(runAWG);
            groupBox1.Controls.Add(progAWG);
            groupBox1.Location = new Point(308, 73);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(294, 171);
            groupBox1.TabIndex = 27;
            groupBox1.TabStop = false;
            groupBox1.Text = "AWG";
            // 
            // loadAwg
            // 
            loadAwg.Location = new Point(10, 28);
            loadAwg.Name = "loadAwg";
            loadAwg.Size = new Size(94, 33);
            loadAwg.TabIndex = 21;
            loadAwg.Text = "Get AWG";
            loadAwg.UseVisualStyleBackColor = true;
            loadAwg.Click += loadAwg_Click;
            // 
            // awglen
            // 
            awglen.AutoSize = true;
            awglen.Location = new Point(120, 34);
            awglen.Name = "awglen";
            awglen.Size = new Size(17, 17);
            awglen.TabIndex = 23;
            awglen.Text = "...";
            // 
            // sendAWG
            // 
            sendAWG.Location = new Point(10, 77);
            sendAWG.Name = "sendAWG";
            sendAWG.Size = new Size(136, 33);
            sendAWG.TabIndex = 22;
            sendAWG.Text = "Download AWG";
            sendAWG.UseVisualStyleBackColor = true;
            sendAWG.Click += sendAWG_Click;
            // 
            // runAWG
            // 
            runAWG.Location = new Point(85, 125);
            runAWG.Name = "runAWG";
            runAWG.Size = new Size(114, 33);
            runAWG.TabIndex = 22;
            runAWG.Text = "Run AWG";
            runAWG.UseVisualStyleBackColor = true;
            runAWG.Click += runAWG_Click;
            // 
            // progAWG
            // 
            progAWG.Location = new Point(174, 76);
            progAWG.Name = "progAWG";
            progAWG.Size = new Size(114, 33);
            progAWG.TabIndex = 22;
            progAWG.Text = "Config AWG";
            progAWG.UseVisualStyleBackColor = true;
            progAWG.Click += progAWG_Click;
            // 
            // comports
            // 
            comports.FormattingEnabled = true;
            comports.Location = new Point(210, 16);
            comports.Name = "comports";
            comports.Size = new Size(103, 25);
            comports.TabIndex = 9;
            // 
            // setAll
            // 
            setAll.Location = new Point(16, 63);
            setAll.Margin = new Padding(4, 3, 4, 3);
            setAll.Name = "setAll";
            setAll.Size = new Size(105, 31);
            setAll.TabIndex = 1;
            setAll.Text = "Set all";
            setAll.UseVisualStyleBackColor = true;
            setAll.Click += setAll_Click;
            // 
            // MonitorView
            // 
            MonitorView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MonitorView.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MonitorView.Location = new Point(3, 33);
            MonitorView.Name = "MonitorView";
            MonitorView.ReadOnly = true;
            MonitorView.Size = new Size(825, 148);
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
            // Column1
            // 
            Column1.FillWeight = 80F;
            Column1.HeaderText = "Param";
            Column1.Name = "Column1";
            Column1.Width = 150;
            // 
            // Column2
            // 
            Column2.HeaderText = "Value";
            Column2.Name = "Column2";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.Control;
            ClientSize = new Size(833, 611);
            Controls.Add(splitContainer3);
            Controls.Add(statusStrip1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(750, 650);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ODEM Control by Lidwave";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)paramTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)tempTable).EndInit();
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel1.PerformLayout();
            splitContainer3.Panel2.ResumeLayout(false);
            splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
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
        private Label label3;
        private Button checkT;
        private ToolStripProgressBar optoStat;
        private System.Windows.Forms.Timer timer1;
        private DataGridView tempTable;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private SplitContainer splitContainer3;
        private Button clr;
        private CheckBox AutoScroll;
        private RichTextBox MonitorView;
        private ComboBox comports;
        private Button loadAwg;
        private Label awglen;
        private Button sendAWG;
        private Button progAWG;
        private DataGridView paramTable;
        private GroupBox groupBox1;
        private Button runAWG;
        private Button setAll;
        private DataGridViewButtonColumn Column1;
        private DataGridViewTextBoxColumn Column2;
    }
}
