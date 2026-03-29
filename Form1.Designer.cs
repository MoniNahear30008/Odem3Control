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
            checkT = new Button();
            tempTable = new DataGridView();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            label3 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            splitContainer3 = new SplitContainer();
            WrVec = new Button();
            parList = new ComboBox();
            awglen = new Label();
            progAWG = new Button();
            sendAWG = new Button();
            loadAwg = new Button();
            comports = new ComboBox();
            MonitorView = new RichTextBox();
            clr = new Button();
            AutoScroll = new CheckBox();
            parValue = new TextBox();
            label1 = new Label();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tempTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
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
            // checkT
            // 
            checkT.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkT.Location = new Point(630, 43);
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
            tempTable.Location = new Point(630, 85);
            tempTable.Margin = new Padding(4);
            tempTable.Name = "tempTable";
            tempTable.ReadOnly = true;
            tempTable.RowHeadersVisible = false;
            tempTable.Size = new Size(177, 127);
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
            label3.Size = new Size(78, 21);
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
            splitContainer3.Panel1.Controls.Add(label1);
            splitContainer3.Panel1.Controls.Add(parValue);
            splitContainer3.Panel1.Controls.Add(WrVec);
            splitContainer3.Panel1.Controls.Add(parList);
            splitContainer3.Panel1.Controls.Add(awglen);
            splitContainer3.Panel1.Controls.Add(progAWG);
            splitContainer3.Panel1.Controls.Add(sendAWG);
            splitContainer3.Panel1.Controls.Add(loadAwg);
            splitContainer3.Panel1.Controls.Add(tempTable);
            splitContainer3.Panel1.Controls.Add(comports);
            splitContainer3.Panel1.Controls.Add(checkT);
            splitContainer3.Panel1.Controls.Add(label3);
            splitContainer3.Panel1.Controls.Add(connect);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(MonitorView);
            splitContainer3.Panel2.Controls.Add(clr);
            splitContainer3.Panel2.Controls.Add(AutoScroll);
            splitContainer3.Size = new Size(833, 587);
            splitContainer3.SplitterDistance = 448;
            splitContainer3.TabIndex = 17;
            // 
            // WrVec
            // 
            WrVec.Location = new Point(26, 165);
            WrVec.Name = "WrVec";
            WrVec.Size = new Size(131, 29);
            WrVec.TabIndex = 14;
            WrVec.Text = "Set parameter";
            WrVec.UseVisualStyleBackColor = true;
            WrVec.Click += WrVec_Click;
            // 
            // parList
            // 
            parList.FormattingEnabled = true;
            parList.Location = new Point(167, 165);
            parList.Name = "parList";
            parList.Size = new Size(171, 29);
            parList.TabIndex = 15;
            // 
            // awglen
            // 
            awglen.AutoSize = true;
            awglen.Location = new Point(17, 109);
            awglen.Name = "awglen";
            awglen.Size = new Size(19, 21);
            awglen.TabIndex = 23;
            awglen.Text = "...";
            // 
            // progAWG
            // 
            progAWG.Location = new Point(329, 73);
            progAWG.Name = "progAWG";
            progAWG.Size = new Size(136, 33);
            progAWG.TabIndex = 22;
            progAWG.Text = "Config AWG";
            progAWG.UseVisualStyleBackColor = true;
            progAWG.Click += progAWG_Click;
            // 
            // sendAWG
            // 
            sendAWG.Location = new Point(167, 73);
            sendAWG.Name = "sendAWG";
            sendAWG.Size = new Size(136, 33);
            sendAWG.TabIndex = 22;
            sendAWG.Text = "Download AWG";
            sendAWG.UseVisualStyleBackColor = true;
            sendAWG.Click += sendAWG_Click;
            // 
            // loadAwg
            // 
            loadAwg.Location = new Point(12, 73);
            loadAwg.Name = "loadAwg";
            loadAwg.Size = new Size(94, 33);
            loadAwg.TabIndex = 21;
            loadAwg.Text = "Get AWG";
            loadAwg.UseVisualStyleBackColor = true;
            loadAwg.Click += loadAwg_Click;
            // 
            // comports
            // 
            comports.FormattingEnabled = true;
            comports.Location = new Point(210, 16);
            comports.Name = "comports";
            comports.Size = new Size(103, 29);
            comports.TabIndex = 9;
            // 
            // MonitorView
            // 
            MonitorView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MonitorView.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MonitorView.Location = new Point(3, 33);
            MonitorView.Name = "MonitorView";
            MonitorView.ReadOnly = true;
            MonitorView.Size = new Size(825, 96);
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
            // parValue
            // 
            parValue.Location = new Point(356, 165);
            parValue.Name = "parValue";
            parValue.Size = new Size(109, 29);
            parValue.TabIndex = 24;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(356, 141);
            label1.Name = "label1";
            label1.Size = new Size(189, 21);
            label1.TabIndex = 25;
            label1.Text = "Start with 0x for hex value";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.Control;
            ClientSize = new Size(833, 611);
            Controls.Add(splitContainer3);
            Controls.Add(statusStrip1);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(750, 650);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ODEM Control by Lidwave";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tempTable).EndInit();
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel1.PerformLayout();
            splitContainer3.Panel2.ResumeLayout(false);
            splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
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
        private ComboBox parList;
        private Button WrVec;
        private ComboBox comports;
        private Button loadAwg;
        private Label awglen;
        private Button sendAWG;
        private Button progAWG;
        private TextBox parValue;
        private Label label1;
    }
}
