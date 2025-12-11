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
            ((System.ComponentModel.ISupportInitialize)customParams).BeginInit();
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
            customParams.Location = new Point(25, 12);
            customParams.Name = "customParams";
            customParams.RowHeadersVisible = false;
            customParams.Size = new Size(240, 150);
            customParams.TabIndex = 3;
            // 
            // Column1
            // 
            Column1.FillWeight = 50F;
            Column1.HeaderText = "Column1";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // Column2
            // 
            Column2.FillWeight = 50F;
            Column2.HeaderText = "Column2";
            Column2.Name = "Column2";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(283, 211);
            label1.Name = "label1";
            label1.Size = new Size(235, 29);
            label1.TabIndex = 4;
            label1.Text = "Under construction";
            // 
            // custom_dev
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(customParams);
            Name = "custom_dev";
            Text = "custom device";
            ((System.ComponentModel.ISupportInitialize)customParams).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView customParams;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private Label label1;
    }
}