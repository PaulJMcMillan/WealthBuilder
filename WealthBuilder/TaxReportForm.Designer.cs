namespace WealthBuilder
{
    partial class TaxReportForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaxReportForm));
            this.entityComboBox = new System.Windows.Forms.ComboBox();
            this.entitiesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet = new WealthBuilder.DataSet();
            this.label1 = new System.Windows.Forms.Label();
            this.entitiesTableAdapter = new WealthBuilder.DataSetTableAdapters.EntitiesTableAdapter();
            this.label2 = new System.Windows.Forms.Label();
            this.taxFormComboBox = new System.Windows.Forms.ComboBox();
            this.taxFormsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.taxFormsTableAdapter = new WealthBuilder.DataSetTableAdapters.TaxFormsTableAdapter();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.startDateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.endDateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.goButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.entitiesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.taxFormsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // entityComboBox
            // 
            this.entityComboBox.DataSource = this.entitiesBindingSource;
            this.entityComboBox.DisplayMember = "Name";
            this.entityComboBox.FormattingEnabled = true;
            this.entityComboBox.Location = new System.Drawing.Point(87, 10);
            this.entityComboBox.Name = "entityComboBox";
            this.entityComboBox.Size = new System.Drawing.Size(251, 24);
            this.entityComboBox.TabIndex = 0;
            this.entityComboBox.ValueMember = "Id";
            // 
            // entitiesBindingSource
            // 
            this.entitiesBindingSource.DataMember = "Entities";
            this.entitiesBindingSource.DataSource = this.dataSet;
            this.entitiesBindingSource.Filter = "Active = true";
            this.entitiesBindingSource.Sort = "Name";
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "DataSet";
            this.dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Entity:";
            // 
            // entitiesTableAdapter
            // 
            this.entitiesTableAdapter.ClearBeforeFill = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tax Form:";
            // 
            // taxFormComboBox
            // 
            this.taxFormComboBox.DataSource = this.taxFormsBindingSource;
            this.taxFormComboBox.DisplayMember = "TaxFormName";
            this.taxFormComboBox.FormattingEnabled = true;
            this.taxFormComboBox.Location = new System.Drawing.Point(87, 40);
            this.taxFormComboBox.Name = "taxFormComboBox";
            this.taxFormComboBox.Size = new System.Drawing.Size(251, 24);
            this.taxFormComboBox.TabIndex = 2;
            this.taxFormComboBox.ValueMember = "Id";
            // 
            // taxFormsBindingSource
            // 
            this.taxFormsBindingSource.DataMember = "TaxForms";
            this.taxFormsBindingSource.DataSource = this.dataSet;
            this.taxFormsBindingSource.Filter = "Active = true";
            this.taxFormsBindingSource.Sort = "TaxFormName";
            // 
            // taxFormsTableAdapter
            // 
            this.taxFormsTableAdapter.ClearBeforeFill = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Start Date:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "End Date:";
            // 
            // startDateDateTimePicker
            // 
            this.startDateDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startDateDateTimePicker.Location = new System.Drawing.Point(87, 70);
            this.startDateDateTimePicker.Name = "startDateDateTimePicker";
            this.startDateDateTimePicker.Size = new System.Drawing.Size(104, 22);
            this.startDateDateTimePicker.TabIndex = 6;
            // 
            // endDateDateTimePicker
            // 
            this.endDateDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endDateDateTimePicker.Location = new System.Drawing.Point(87, 98);
            this.endDateDateTimePicker.Name = "endDateDateTimePicker";
            this.endDateDateTimePicker.Size = new System.Drawing.Size(104, 22);
            this.endDateDateTimePicker.TabIndex = 7;
            // 
            // goButton
            // 
            this.goButton.BackColor = System.Drawing.SystemColors.HotTrack;
            this.goButton.FlatAppearance.BorderSize = 0;
            this.goButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.goButton.ForeColor = System.Drawing.SystemColors.Control;
            this.goButton.Location = new System.Drawing.Point(209, 75);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(67, 43);
            this.goButton.TabIndex = 33;
            this.goButton.Text = "Go";
            this.goButton.UseVisualStyleBackColor = false;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // TaxReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 141);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.endDateDateTimePicker);
            this.Controls.Add(this.startDateDateTimePicker);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.taxFormComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.entityComboBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TaxReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tax Report";
            this.Load += new System.EventHandler(this.TaxReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.entitiesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.taxFormsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox entityComboBox;
        private System.Windows.Forms.Label label1;
        private DataSet dataSet;
        private System.Windows.Forms.BindingSource entitiesBindingSource;
        private DataSetTableAdapters.EntitiesTableAdapter entitiesTableAdapter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox taxFormComboBox;
        private System.Windows.Forms.BindingSource taxFormsBindingSource;
        private DataSetTableAdapters.TaxFormsTableAdapter taxFormsTableAdapter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker startDateDateTimePicker;
        private System.Windows.Forms.DateTimePicker endDateDateTimePicker;
        private System.Windows.Forms.Button goButton;
    }
}