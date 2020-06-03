namespace WealthBuilder
{
    partial class ObtainLicenseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObtainLicenseForm));
            this.selectEditionLabel = new System.Windows.Forms.Label();
            this.obtainProductKeyButton = new System.Windows.Forms.Button();
            this.wealthBuilderRadioButton = new System.Windows.Forms.RadioButton();
            this.pennyPincherRadioButton = new System.Windows.Forms.RadioButton();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // selectEditionLabel
            // 
            this.selectEditionLabel.AutoSize = true;
            this.selectEditionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectEditionLabel.Location = new System.Drawing.Point(12, 9);
            this.selectEditionLabel.Name = "selectEditionLabel";
            this.selectEditionLabel.Size = new System.Drawing.Size(206, 16);
            this.selectEditionLabel.TabIndex = 0;
            this.selectEditionLabel.Text = "Select which edition do you want?";
            // 
            // obtainProductKeyButton
            // 
            this.obtainProductKeyButton.BackColor = System.Drawing.SystemColors.HotTrack;
            this.obtainProductKeyButton.FlatAppearance.BorderSize = 0;
            this.obtainProductKeyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.obtainProductKeyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.obtainProductKeyButton.ForeColor = System.Drawing.SystemColors.Control;
            this.obtainProductKeyButton.Location = new System.Drawing.Point(12, 100);
            this.obtainProductKeyButton.Name = "obtainProductKeyButton";
            this.obtainProductKeyButton.Size = new System.Drawing.Size(133, 43);
            this.obtainProductKeyButton.TabIndex = 0;
            this.obtainProductKeyButton.Text = "Obtain Product Key";
            this.obtainProductKeyButton.UseVisualStyleBackColor = false;
            this.obtainProductKeyButton.Click += new System.EventHandler(this.obtainProductKeyButton_Click);
            // 
            // wealthBuilderRadioButton
            // 
            this.wealthBuilderRadioButton.AutoSize = true;
            this.wealthBuilderRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wealthBuilderRadioButton.Location = new System.Drawing.Point(35, 38);
            this.wealthBuilderRadioButton.Name = "wealthBuilderRadioButton";
            this.wealthBuilderRadioButton.Size = new System.Drawing.Size(113, 20);
            this.wealthBuilderRadioButton.TabIndex = 24;
            this.wealthBuilderRadioButton.TabStop = true;
            this.wealthBuilderRadioButton.Text = "Wealth Builder";
            this.wealthBuilderRadioButton.UseVisualStyleBackColor = true;
            // 
            // pennyPincherRadioButton
            // 
            this.pennyPincherRadioButton.AutoSize = true;
            this.pennyPincherRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pennyPincherRadioButton.Location = new System.Drawing.Point(35, 62);
            this.pennyPincherRadioButton.Name = "pennyPincherRadioButton";
            this.pennyPincherRadioButton.Size = new System.Drawing.Size(112, 20);
            this.pennyPincherRadioButton.TabIndex = 25;
            this.pennyPincherRadioButton.TabStop = true;
            this.pennyPincherRadioButton.Text = "Penny Pincher";
            this.pennyPincherRadioButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.SystemColors.HotTrack;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.SystemColors.Control;
            this.cancelButton.Location = new System.Drawing.Point(151, 100);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(101, 43);
            this.cancelButton.TabIndex = 26;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(298, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(578, 192);
            this.label2.TabIndex = 27;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // ObtainLicenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 216);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.pennyPincherRadioButton);
            this.Controls.Add(this.wealthBuilderRadioButton);
            this.Controls.Add(this.obtainProductKeyButton);
            this.Controls.Add(this.selectEditionLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ObtainLicenseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Obtain a License";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label selectEditionLabel;
        private System.Windows.Forms.Button obtainProductKeyButton;
        private System.Windows.Forms.RadioButton wealthBuilderRadioButton;
        private System.Windows.Forms.RadioButton pennyPincherRadioButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label2;
    }
}