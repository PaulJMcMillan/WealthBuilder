namespace WealthBuilder
{
    partial class ReportsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsForm));
            this.taxReportButton = new System.Windows.Forms.Button();
            this.ten99MiscReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // taxReportButton
            // 
            this.taxReportButton.BackColor = System.Drawing.SystemColors.HotTrack;
            this.taxReportButton.FlatAppearance.BorderSize = 0;
            this.taxReportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.taxReportButton.ForeColor = System.Drawing.SystemColors.Control;
            this.taxReportButton.Location = new System.Drawing.Point(12, 11);
            this.taxReportButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.taxReportButton.Name = "taxReportButton";
            this.taxReportButton.Size = new System.Drawing.Size(90, 45);
            this.taxReportButton.TabIndex = 30;
            this.taxReportButton.Text = "Tax Report";
            this.taxReportButton.UseVisualStyleBackColor = false;
            this.taxReportButton.Click += new System.EventHandler(this.taxReportButton_Click);
            // 
            // ten99MiscReport
            // 
            this.ten99MiscReport.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ten99MiscReport.FlatAppearance.BorderSize = 0;
            this.ten99MiscReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ten99MiscReport.ForeColor = System.Drawing.SystemColors.Control;
            this.ten99MiscReport.Location = new System.Drawing.Point(108, 11);
            this.ten99MiscReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ten99MiscReport.Name = "ten99MiscReport";
            this.ten99MiscReport.Size = new System.Drawing.Size(99, 45);
            this.ten99MiscReport.TabIndex = 31;
            this.ten99MiscReport.Text = "1099-Misc Report";
            this.ten99MiscReport.UseVisualStyleBackColor = false;
            this.ten99MiscReport.Click += new System.EventHandler(this.ten99MiscReport_Click);
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(217, 61);
            this.Controls.Add(this.ten99MiscReport);
            this.Controls.Add(this.taxReportButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ReportsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reports";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button taxReportButton;
        private System.Windows.Forms.Button ten99MiscReport;
    }
}