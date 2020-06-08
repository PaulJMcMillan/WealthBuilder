namespace WealthBuilder
{
    partial class ToolsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolsForm));
            this.createSupportFileButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // createSupportFileButton
            // 
            this.createSupportFileButton.BackColor = System.Drawing.SystemColors.HotTrack;
            this.createSupportFileButton.FlatAppearance.BorderSize = 0;
            this.createSupportFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createSupportFileButton.ForeColor = System.Drawing.SystemColors.Control;
            this.createSupportFileButton.Location = new System.Drawing.Point(135, 12);
            this.createSupportFileButton.Name = "createSupportFileButton";
            this.createSupportFileButton.Size = new System.Drawing.Size(117, 43);
            this.createSupportFileButton.TabIndex = 20;
            this.createSupportFileButton.Text = "Create Support File";
            this.createSupportFileButton.UseVisualStyleBackColor = false;
            this.createSupportFileButton.Click += new System.EventHandler(this.createSupportFileButton_Click);
            // 
            // ToolsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 70);
            this.Controls.Add(this.createSupportFileButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ToolsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tools";
            this.Load += new System.EventHandler(this.ToolsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button createSupportFileButton;
    }
}