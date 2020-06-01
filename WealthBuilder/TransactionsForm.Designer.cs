namespace WealthBuilder
{
    partial class TransactionsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransactionsForm));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deposit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.withdrawal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cleared = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.reconciled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxFormId = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.TaxCategoryId = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entityId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transactionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet = new WealthBuilder.DataSet();
            this.label2 = new System.Windows.Forms.Label();
            this.accountComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.excludeReconciledTransactionsCheckBox = new System.Windows.Forms.CheckBox();
            this.currentBalanceTextBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.excludeClearedTransactionsCheckBox = new System.Windows.Forms.CheckBox();
            this.availableBankBalanceTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.reconcileButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.transactionsTableAdapter = new WealthBuilder.DataSetTableAdapters.TransactionsTableAdapter();
            this.label3 = new System.Windows.Forms.Label();
            this.displayTransBackTo = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CheckNumberTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.NotesRichTextBox = new System.Windows.Forms.RichTextBox();
            this.ReconciledCheckBox = new System.Windows.Forms.CheckBox();
            this.ClearedCheckBox = new System.Windows.Forms.CheckBox();
            this.WithdrawalTextBox = new System.Windows.Forms.TextBox();
            this.DepositTextBox = new System.Windows.Forms.TextBox();
            this.DescriptionTextBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToOrderColumns = true;
            this.dgv.AutoGenerateColumns = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.description,
            this.deposit,
            this.withdrawal,
            this.cleared,
            this.reconciled,
            this.notes,
            this.checkNumber,
            this.TaxFormId,
            this.TaxCategoryId,
            this.id,
            this.accountIdDataGridViewTextBoxColumn,
            this.entityId});
            this.dgv.DataSource = this.transactionsBindingSource;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgv.Location = new System.Drawing.Point(296, 54);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(1044, 423);
            this.dgv.TabIndex = 0;
            this.dgv.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.Dgv_DataError);
            this.dgv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseClick);
            // 
            // Date
            // 
            this.Date.DataPropertyName = "Date";
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 55;
            // 
            // description
            // 
            this.description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.description.DataPropertyName = "Description";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.description.DefaultCellStyle = dataGridViewCellStyle2;
            this.description.HeaderText = "Description";
            this.description.Name = "description";
            this.description.ReadOnly = true;
            this.description.Width = 85;
            // 
            // deposit
            // 
            this.deposit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.deposit.DataPropertyName = "Deposit";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "C2";
            this.deposit.DefaultCellStyle = dataGridViewCellStyle3;
            this.deposit.HeaderText = "Deposit";
            this.deposit.Name = "deposit";
            this.deposit.ReadOnly = true;
            this.deposit.Width = 68;
            // 
            // withdrawal
            // 
            this.withdrawal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.withdrawal.DataPropertyName = "Withdrawal";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            this.withdrawal.DefaultCellStyle = dataGridViewCellStyle4;
            this.withdrawal.HeaderText = "Withdrawal";
            this.withdrawal.Name = "withdrawal";
            this.withdrawal.ReadOnly = true;
            this.withdrawal.Width = 85;
            // 
            // cleared
            // 
            this.cleared.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cleared.DataPropertyName = "Cleared";
            this.cleared.HeaderText = "Cleared";
            this.cleared.Name = "cleared";
            this.cleared.ReadOnly = true;
            this.cleared.Width = 49;
            // 
            // reconciled
            // 
            this.reconciled.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.reconciled.DataPropertyName = "Reconciled";
            this.reconciled.HeaderText = "Reconciled";
            this.reconciled.Name = "reconciled";
            this.reconciled.ReadOnly = true;
            this.reconciled.Width = 67;
            // 
            // notes
            // 
            this.notes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.notes.DataPropertyName = "Notes";
            this.notes.HeaderText = "Notes";
            this.notes.Name = "notes";
            this.notes.ReadOnly = true;
            this.notes.Width = 60;
            // 
            // checkNumber
            // 
            this.checkNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.checkNumber.DataPropertyName = "CheckNumber";
            this.checkNumber.HeaderText = "Check #";
            this.checkNumber.Name = "checkNumber";
            this.checkNumber.ReadOnly = true;
            this.checkNumber.Width = 73;
            // 
            // TaxFormId
            // 
            this.TaxFormId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TaxFormId.DataPropertyName = "TaxFormId";
            this.TaxFormId.HeaderText = "Tax Form";
            this.TaxFormId.Name = "TaxFormId";
            this.TaxFormId.ReadOnly = true;
            this.TaxFormId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TaxFormId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TaxFormId.Visible = false;
            // 
            // TaxCategoryId
            // 
            this.TaxCategoryId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TaxCategoryId.DataPropertyName = "TaxCategoryId";
            this.TaxCategoryId.HeaderText = "Tax Category";
            this.TaxCategoryId.Name = "TaxCategoryId";
            this.TaxCategoryId.ReadOnly = true;
            this.TaxCategoryId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TaxCategoryId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TaxCategoryId.Visible = false;
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.id.DataPropertyName = "Id";
            this.id.HeaderText = "Id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // accountIdDataGridViewTextBoxColumn
            // 
            this.accountIdDataGridViewTextBoxColumn.DataPropertyName = "AccountId";
            this.accountIdDataGridViewTextBoxColumn.HeaderText = "AccountId";
            this.accountIdDataGridViewTextBoxColumn.Name = "accountIdDataGridViewTextBoxColumn";
            this.accountIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.accountIdDataGridViewTextBoxColumn.Visible = false;
            this.accountIdDataGridViewTextBoxColumn.Width = 81;
            // 
            // entityId
            // 
            this.entityId.DataPropertyName = "EntityId";
            this.entityId.HeaderText = "EntityId";
            this.entityId.Name = "entityId";
            this.entityId.ReadOnly = true;
            this.entityId.Visible = false;
            this.entityId.Width = 67;
            // 
            // transactionsBindingSource
            // 
            this.transactionsBindingSource.DataMember = "Transactions";
            this.transactionsBindingSource.DataSource = this.dataSet;
            this.transactionsBindingSource.Sort = "";
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "DataSet";
            this.dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Account:";
            // 
            // accountComboBox
            // 
            this.accountComboBox.DisplayMember = "Id";
            this.accountComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accountComboBox.FormattingEnabled = true;
            this.accountComboBox.Location = new System.Drawing.Point(69, 9);
            this.accountComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.accountComboBox.Name = "accountComboBox";
            this.accountComboBox.Size = new System.Drawing.Size(287, 21);
            this.accountComboBox.TabIndex = 26;
            this.accountComboBox.ValueMember = "Id";
            this.accountComboBox.SelectedIndexChanged += new System.EventHandler(this.accountComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(366, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Balance:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // excludeReconciledTransactionsCheckBox
            // 
            this.excludeReconciledTransactionsCheckBox.AutoSize = true;
            this.excludeReconciledTransactionsCheckBox.Checked = true;
            this.excludeReconciledTransactionsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.excludeReconciledTransactionsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.excludeReconciledTransactionsCheckBox.Location = new System.Drawing.Point(542, 32);
            this.excludeReconciledTransactionsCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.excludeReconciledTransactionsCheckBox.Name = "excludeReconciledTransactionsCheckBox";
            this.excludeReconciledTransactionsCheckBox.Size = new System.Drawing.Size(185, 17);
            this.excludeReconciledTransactionsCheckBox.TabIndex = 24;
            this.excludeReconciledTransactionsCheckBox.Text = "Exclude Reconciled Transactions";
            this.excludeReconciledTransactionsCheckBox.UseVisualStyleBackColor = true;
            this.excludeReconciledTransactionsCheckBox.CheckedChanged += new System.EventHandler(this.includeReconciledTransactionsCheckBox_CheckedChanged);
            // 
            // currentBalanceTextBox
            // 
            this.currentBalanceTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.currentBalanceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.currentBalanceTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentBalanceTextBox.Location = new System.Drawing.Point(422, 10);
            this.currentBalanceTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.currentBalanceTextBox.Name = "currentBalanceTextBox";
            this.currentBalanceTextBox.Size = new System.Drawing.Size(88, 20);
            this.currentBalanceTextBox.TabIndex = 28;
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.SystemColors.HotTrack;
            this.addButton.FlatAppearance.BorderSize = 0;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.ForeColor = System.Drawing.SystemColors.Control;
            this.addButton.Location = new System.Drawing.Point(9, 483);
            this.addButton.Margin = new System.Windows.Forms.Padding(2);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(88, 24);
            this.addButton.TabIndex = 29;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.SystemColors.HotTrack;
            this.saveButton.FlatAppearance.BorderSize = 0;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.ForeColor = System.Drawing.SystemColors.Control;
            this.saveButton.Location = new System.Drawing.Point(101, 483);
            this.saveButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(88, 24);
            this.saveButton.TabIndex = 30;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.SystemColors.HotTrack;
            this.deleteButton.FlatAppearance.BorderSize = 0;
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.ForeColor = System.Drawing.SystemColors.Control;
            this.deleteButton.Location = new System.Drawing.Point(194, 483);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(88, 24);
            this.deleteButton.TabIndex = 31;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // excludeClearedTransactionsCheckBox
            // 
            this.excludeClearedTransactionsCheckBox.AutoSize = true;
            this.excludeClearedTransactionsCheckBox.Checked = true;
            this.excludeClearedTransactionsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.excludeClearedTransactionsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.excludeClearedTransactionsCheckBox.Location = new System.Drawing.Point(320, 33);
            this.excludeClearedTransactionsCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.excludeClearedTransactionsCheckBox.Name = "excludeClearedTransactionsCheckBox";
            this.excludeClearedTransactionsCheckBox.Size = new System.Drawing.Size(167, 17);
            this.excludeClearedTransactionsCheckBox.TabIndex = 32;
            this.excludeClearedTransactionsCheckBox.Text = "Exclude Cleared Transactions";
            this.excludeClearedTransactionsCheckBox.UseVisualStyleBackColor = true;
            this.excludeClearedTransactionsCheckBox.CheckedChanged += new System.EventHandler(this.includeClearedTransactions_CheckedChanged);
            // 
            // availableBankBalanceTextBox
            // 
            this.availableBankBalanceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.availableBankBalanceTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.availableBankBalanceTextBox.Location = new System.Drawing.Point(139, 32);
            this.availableBankBalanceTextBox.Name = "availableBankBalanceTextBox";
            this.availableBankBalanceTextBox.Size = new System.Drawing.Size(88, 20);
            this.availableBankBalanceTextBox.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Available Bank Balance:";
            // 
            // reconcileButton
            // 
            this.reconcileButton.BackColor = System.Drawing.SystemColors.HotTrack;
            this.reconcileButton.FlatAppearance.BorderSize = 0;
            this.reconcileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reconcileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reconcileButton.ForeColor = System.Drawing.SystemColors.Control;
            this.reconcileButton.Location = new System.Drawing.Point(233, 29);
            this.reconcileButton.Margin = new System.Windows.Forms.Padding(4);
            this.reconcileButton.Name = "reconcileButton";
            this.reconcileButton.Size = new System.Drawing.Size(68, 22);
            this.reconcileButton.TabIndex = 35;
            this.reconcileButton.Text = "Reconcile";
            this.reconcileButton.UseVisualStyleBackColor = false;
            this.reconcileButton.Click += new System.EventHandler(this.reconcileButton_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchTextBox.Location = new System.Drawing.Point(885, 32);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(168, 20);
            this.searchTextBox.TabIndex = 36;
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.SystemColors.HotTrack;
            this.searchButton.FlatAppearance.BorderSize = 0;
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchButton.ForeColor = System.Drawing.SystemColors.Control;
            this.searchButton.Location = new System.Drawing.Point(827, 30);
            this.searchButton.Margin = new System.Windows.Forms.Padding(4);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(51, 22);
            this.searchButton.TabIndex = 37;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // transactionsTableAdapter
            // 
            this.transactionsTableAdapter.ClearBeforeFill = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(539, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Display Transactions Back to: ";
            // 
            // displayTransBackTo
            // 
            this.displayTransBackTo.FormattingEnabled = true;
            this.displayTransBackTo.Items.AddRange(new object[] {
            "1 Month",
            "1 Quarter",
            "6 Months",
            "1 Year"});
            this.displayTransBackTo.Location = new System.Drawing.Point(686, 9);
            this.displayTransBackTo.Margin = new System.Windows.Forms.Padding(2);
            this.displayTransBackTo.Name = "displayTransBackTo";
            this.displayTransBackTo.Size = new System.Drawing.Size(92, 21);
            this.displayTransBackTo.TabIndex = 40;
            this.displayTransBackTo.SelectedIndexChanged += new System.EventHandler(this.DisplayTransBackTo_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.CheckNumberTextBox);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.NotesRichTextBox);
            this.groupBox1.Controls.Add(this.ReconciledCheckBox);
            this.groupBox1.Controls.Add(this.ClearedCheckBox);
            this.groupBox1.Controls.Add(this.WithdrawalTextBox);
            this.groupBox1.Controls.Add(this.DepositTextBox);
            this.groupBox1.Controls.Add(this.DescriptionTextBox);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dateTimePicker);
            this.groupBox1.Location = new System.Drawing.Point(10, 57);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(280, 420);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            // 
            // CheckNumberTextBox
            // 
            this.CheckNumberTextBox.Location = new System.Drawing.Point(67, 246);
            this.CheckNumberTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.CheckNumberTextBox.Name = "CheckNumberTextBox";
            this.CheckNumberTextBox.Size = new System.Drawing.Size(96, 20);
            this.CheckNumberTextBox.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label10.Location = new System.Drawing.Point(4, 248);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Check #:";
            // 
            // NotesRichTextBox
            // 
            this.NotesRichTextBox.Location = new System.Drawing.Point(65, 153);
            this.NotesRichTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.NotesRichTextBox.Name = "NotesRichTextBox";
            this.NotesRichTextBox.Size = new System.Drawing.Size(205, 80);
            this.NotesRichTextBox.TabIndex = 20;
            this.NotesRichTextBox.Text = "";
            // 
            // ReconciledCheckBox
            // 
            this.ReconciledCheckBox.AutoSize = true;
            this.ReconciledCheckBox.Location = new System.Drawing.Point(66, 132);
            this.ReconciledCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.ReconciledCheckBox.Name = "ReconciledCheckBox";
            this.ReconciledCheckBox.Size = new System.Drawing.Size(15, 14);
            this.ReconciledCheckBox.TabIndex = 17;
            this.ReconciledCheckBox.UseVisualStyleBackColor = true;
            // 
            // ClearedCheckBox
            // 
            this.ClearedCheckBox.AutoSize = true;
            this.ClearedCheckBox.Location = new System.Drawing.Point(67, 109);
            this.ClearedCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.ClearedCheckBox.Name = "ClearedCheckBox";
            this.ClearedCheckBox.Size = new System.Drawing.Size(15, 14);
            this.ClearedCheckBox.TabIndex = 15;
            this.ClearedCheckBox.UseVisualStyleBackColor = true;
            // 
            // WithdrawalTextBox
            // 
            this.WithdrawalTextBox.Location = new System.Drawing.Point(67, 84);
            this.WithdrawalTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.WithdrawalTextBox.Name = "WithdrawalTextBox";
            this.WithdrawalTextBox.Size = new System.Drawing.Size(96, 20);
            this.WithdrawalTextBox.TabIndex = 14;
            // 
            // DepositTextBox
            // 
            this.DepositTextBox.Location = new System.Drawing.Point(67, 58);
            this.DepositTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.DepositTextBox.Name = "DepositTextBox";
            this.DepositTextBox.Size = new System.Drawing.Size(96, 20);
            this.DepositTextBox.TabIndex = 13;
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.Location = new System.Drawing.Point(67, 33);
            this.DescriptionTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.Size = new System.Drawing.Size(205, 20);
            this.DescriptionTextBox.TabIndex = 12;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label15.Location = new System.Drawing.Point(4, 130);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 13);
            this.label15.TabIndex = 11;
            this.label15.Text = "Reconciled";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label11.Location = new System.Drawing.Point(4, 153);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Notes:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Location = new System.Drawing.Point(4, 107);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Cleared:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Location = new System.Drawing.Point(4, 84);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Withdrawal:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(4, 61);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Deposit:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(4, 36);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Description:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(4, 15);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Date:";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker.Location = new System.Drawing.Point(67, 9);
            this.dateTimePicker.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(89, 20);
            this.dateTimePicker.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Date";
            this.dataGridViewTextBoxColumn2.HeaderText = "Date";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn3.HeaderText = "Description";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Deposit";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "C2";
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn4.HeaderText = "Deposit";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Withdrawal";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "C2";
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn5.HeaderText = "Withdrawal";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "CheckNumber";
            this.dataGridViewTextBoxColumn6.HeaderText = "Check #";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Notes";
            this.dataGridViewTextBoxColumn7.HeaderText = "Notes";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "AccountId";
            this.dataGridViewTextBoxColumn8.HeaderText = "AccountId";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Visible = false;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "EntityId";
            this.dataGridViewTextBoxColumn9.HeaderText = "EntityId";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Visible = false;
            // 
            // TransactionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1352, 513);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.displayTransBackTo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.reconcileButton);
            this.Controls.Add(this.availableBankBalanceTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.excludeClearedTransactionsCheckBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.currentBalanceTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.accountComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.excludeReconciledTransactionsCheckBox);
            this.Controls.Add(this.dgv);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TransactionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transactions";
            this.Load += new System.EventHandler(this.TransactionsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private DataSet dataSet;
        private System.Windows.Forms.BindingSource transactionsBindingSource;
        private DataSetTableAdapters.TransactionsTableAdapter transactionsTableAdapter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox accountComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox excludeReconciledTransactionsCheckBox;
        private System.Windows.Forms.TextBox currentBalanceTextBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.CheckBox excludeClearedTransactionsCheckBox;
        private System.Windows.Forms.TextBox availableBankBalanceTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button reconcileButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox displayTransBackTo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox NotesRichTextBox;
        private System.Windows.Forms.CheckBox ReconciledCheckBox;
        private System.Windows.Forms.CheckBox ClearedCheckBox;
        private System.Windows.Forms.TextBox WithdrawalTextBox;
        private System.Windows.Forms.TextBox DepositTextBox;
        private System.Windows.Forms.TextBox DescriptionTextBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.TextBox CheckNumberTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn deposit;
        private System.Windows.Forms.DataGridViewTextBoxColumn withdrawal;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cleared;
        private System.Windows.Forms.DataGridViewCheckBoxColumn reconciled;
        private System.Windows.Forms.DataGridViewTextBoxColumn notes;
        private System.Windows.Forms.DataGridViewTextBoxColumn checkNumber;
        private System.Windows.Forms.DataGridViewComboBoxColumn TaxFormId;
        private System.Windows.Forms.DataGridViewComboBoxColumn TaxCategoryId;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn entityId;
    }
}