namespace WealthBuilder
{
    partial class LowestBalancesNotificationsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LowestBalancesNotificationsForm));
            this.lowestBalancesListBox = new System.Windows.Forms.ListBox();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lowestBalancesListBox
            // 
            this.lowestBalancesListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lowestBalancesListBox.FormattingEnabled = true;
            this.lowestBalancesListBox.ItemHeight = 16;
            this.lowestBalancesListBox.Location = new System.Drawing.Point(1, 0);
            this.lowestBalancesListBox.Name = "lowestBalancesListBox";
            this.lowestBalancesListBox.Size = new System.Drawing.Size(767, 276);
            this.lowestBalancesListBox.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.SystemColors.HotTrack;
            this.okButton.FlatAppearance.BorderSize = 0;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.ForeColor = System.Drawing.SystemColors.Control;
            this.okButton.Location = new System.Drawing.Point(13, 283);
            this.okButton.Margin = new System.Windows.Forms.Padding(4);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(83, 28);
            this.okButton.TabIndex = 17;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // LowestBalancesNotificationsForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 318);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.lowestBalancesListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LowestBalancesNotificationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lowest Balance Notifications";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lowestBalancesListBox;
        private System.Windows.Forms.Button okButton;
    }
}