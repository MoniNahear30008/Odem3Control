namespace OdemControl
{
    partial class custom_dev
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
            customParams = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            label1 = new Label();
            customFiles = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            getFromFolder = new Button();
            folderName = new Label();
            saveSetting = new Button();
            loadSetting = new Button();
            customConfig = new Button();
            ((System.ComponentModel.ISupportInitialize)customParams).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customFiles).BeginInit();
            SuspendLayout();
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
            customParams.Location = new Point(12, 12);
            customParams.Name = "customParams";
            customParams.RowHeadersVisible = false;
            customParams.Size = new Size(240, 466);
            customParams.TabIndex = 3;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(290, 417);
            label1.Name = "label1";
            label1.Size = new Size(235, 29);
            label1.TabIndex = 4;
            label1.Text = "Under construction";
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
            customFiles.Location = new Point(277, 96);
            customFiles.Name = "customFiles";
            customFiles.RowHeadersVisible = false;
            customFiles.Size = new Size(490, 183);
            customFiles.TabIndex = 6;
            customFiles.CellMouseDoubleClick += customFiles_CellMouseDoubleClick;
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
            // getFromFolder
            // 
            getFromFolder.Location = new Point(280, 59);
            getFromFolder.Name = "getFromFolder";
            getFromFolder.Size = new Size(148, 23);
            getFromFolder.TabIndex = 7;
            getFromFolder.Text = "Get files from folder";
            getFromFolder.UseVisualStyleBackColor = true;
            getFromFolder.Click += getFromFolder_Click;
            // 
            // folderName
            // 
            folderName.AutoSize = true;
            folderName.Location = new Point(444, 63);
            folderName.Name = "folderName";
            folderName.Size = new Size(16, 15);
            folderName.TabIndex = 8;
            folderName.Text = "...";
            // 
            // saveSetting
            // 
            saveSetting.Location = new Point(280, 12);
            saveSetting.Name = "saveSetting";
            saveSetting.Size = new Size(148, 30);
            saveSetting.TabIndex = 9;
            saveSetting.Text = "Save setting to file";
            saveSetting.UseVisualStyleBackColor = true;
            saveSetting.Click += saveSetting_Click;
            // 
            // loadSetting
            // 
            loadSetting.Location = new Point(457, 12);
            loadSetting.Name = "loadSetting";
            loadSetting.Size = new Size(148, 30);
            loadSetting.TabIndex = 9;
            loadSetting.Text = "Load setting from file";
            loadSetting.UseVisualStyleBackColor = true;
            loadSetting.Click += loadSetting_Click;
            // 
            // customConfig
            // 
            customConfig.Location = new Point(280, 297);
            customConfig.Name = "customConfig";
            customConfig.Size = new Size(122, 41);
            customConfig.TabIndex = 10;
            customConfig.Text = "Configure custom device";
            customConfig.UseVisualStyleBackColor = true;
            customConfig.Click += customConfig_Click;
            // 
            // custom_dev
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 500);
            Controls.Add(customConfig);
            Controls.Add(loadSetting);
            Controls.Add(saveSetting);
            Controls.Add(folderName);
            Controls.Add(getFromFolder);
            Controls.Add(customFiles);
            Controls.Add(label1);
            Controls.Add(customParams);
            Name = "custom_dev";
            Text = "custom device";
            ((System.ComponentModel.ISupportInitialize)customParams).EndInit();
            ((System.ComponentModel.ISupportInitialize)customFiles).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView customParams;
        private Label label1;
        private DataGridView customFiles;
        private Button getFromFolder;
        private Label folderName;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private Button saveSetting;
        private Button loadSetting;
        private Button customConfig;
    }
}