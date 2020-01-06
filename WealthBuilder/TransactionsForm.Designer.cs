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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransactionsForm));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.depositDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.withdrawalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clearedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.checkNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reconciledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TaxFormId = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.taxFormsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet = new WealthBuilder.DataSet();
            this.TaxCategoryId = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.taxCategoriesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CategoryId = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.categoriesBindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.AssetId = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.assetsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ContractorId = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.contractorsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.notesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entityIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transactionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.accountComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.includeReconciledTransactionsCheckBox = new System.Windows.Forms.CheckBox();
            this.currentBalanceTextBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.includeClearedTransactions = new System.Windows.Forms.CheckBox();
            this.availableBankBalanceTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.reconcileButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.categoriesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.categoriesBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.categoriesBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.transactionsTableAdapter = new WealthBuilder.DataSetTableAdapters.TransactionsTableAdapter();
            this.categoriesTableAdapter = new WealthBuilder.DataSetTableAdapters.CategoriesTableAdapter();
            this.taxFormsTableAdapter = new WealthBuilder.DataSetTableAdapters.TaxFormsTableAdapter();
            this.taxCategoriesTableAdapter = new WealthBuilder.DataSetTableAdapters.TaxCategoriesTableAdapter();
            this.assetsTableAdapter = new WealthBuilder.DataSetTableAdapters.AssetsTableAdapter();
            this._1099ContractorsTableAdapter = new WealthBuilder.DataSetTableAdapters._1099ContractorsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.taxFormsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.taxCategoriesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoriesBindingSource3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.assetsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contractorsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoriesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoriesBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoriesBindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AutoGenerateColumns = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.depositDataGridViewTextBoxColumn,
            this.withdrawalDataGridViewTextBoxColumn,
            this.clearedDataGridViewCheckBoxColumn,
            this.checkNumberDataGridViewTextBoxColumn,
            this.reconciledDataGridViewCheckBoxColumn,
            this.TaxFormId,
            this.TaxCategoryId,
            this.CategoryId,
            this.AssetId,
            this.ContractorId,
            this.notesDataGridViewTextBoxColumn,
            this.accountIdDataGridViewTextBoxColumn,
            this.entityIdDataGridViewTextBoxColumn});
            this.dgv.DataSource = this.transactionsBindingSource;
            this.dgv.Location = new System.Drawing.Point(13, 66);
            this.dgv.Margin = new System.Windows.Forms.Padding(4);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(1562, 521);
            this.dgv.TabIndex = 0;
            this.dgv.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_DataError);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.Width = 101;
            // 
            // depositDataGridViewTextBoxColumn
            // 
            this.depositDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.depositDataGridViewTextBoxColumn.DataPropertyName = "Deposit";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "C2";
            this.depositDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.depositDataGridViewTextBoxColumn.HeaderText = "Deposit";
            this.depositDataGridViewTextBoxColumn.Name = "depositDataGridViewTextBoxColumn";
            this.depositDataGridViewTextBoxColumn.Width = 80;
            // 
            // withdrawalDataGridViewTextBoxColumn
            // 
            this.withdrawalDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.withdrawalDataGridViewTextBoxColumn.DataPropertyName = "Withdrawal";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "C2";
            this.withdrawalDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.withdrawalDataGridViewTextBoxColumn.HeaderText = "Withdrawal";
            this.withdrawalDataGridViewTextBoxColumn.Name = "withdrawalDataGridViewTextBoxColumn";
            this.withdrawalDataGridViewTextBoxColumn.Width = 99;
            // 
            // clearedDataGridViewCheckBoxColumn
            // 
            this.clearedDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.clearedDataGridViewCheckBoxColumn.DataPropertyName = "Cleared";
            this.clearedDataGridViewCheckBoxColumn.HeaderText = "Cleared";
            this.clearedDataGridViewCheckBoxColumn.Name = "clearedDataGridViewCheckBoxColumn";
            this.clearedDataGridViewCheckBoxColumn.Width = 62;
            // 
            // checkNumberDataGridViewTextBoxColumn
            // 
            this.checkNumberDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.checkNumberDataGridViewTextBoxColumn.DataPropertyName = "CheckNumber";
            this.checkNumberDataGridViewTextBoxColumn.HeaderText = "Check #";
            this.checkNumberDataGridViewTextBoxColumn.Name = "checkNumberDataGridViewTextBoxColumn";
            this.checkNumberDataGridViewTextBoxColumn.Width = 81;
            // 
            // reconciledDataGridViewCheckBoxColumn
            // 
            this.reconciledDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.reconciledDataGridViewCheckBoxColumn.DataPropertyName = "Reconciled";
            this.reconciledDataGridViewCheckBoxColumn.HeaderText = "Reconciled";
            this.reconciledDataGridViewCheckBoxColumn.Name = "reconciledDataGridViewCheckBoxColumn";
            this.reconciledDataGridViewCheckBoxColumn.Width = 83;
            // 
            // TaxFormId
            // 
            this.TaxFormId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TaxFormId.DataPropertyName = "TaxFormId";
            this.TaxFormId.DataSource = this.taxFormsBindingSource;
            this.TaxFormId.DisplayMember = "TaxFormName";
            this.TaxFormId.HeaderText = "Tax Form";
            this.TaxFormId.Name = "TaxFormId";
            this.TaxFormId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TaxFormId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TaxFormId.ValueMember = "Id";
            this.TaxFormId.Width = 90;
            // 
            // taxFormsBindingSource
            // 
            this.taxFormsBindingSource.DataMember = "TaxForms";
            this.taxFormsBindingSource.DataSource = this.dataSet;
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "DataSet";
            this.dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // TaxCategoryId
            // 
            this.TaxCategoryId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TaxCategoryId.DataPropertyName = "TaxCategoryId";
            this.TaxCategoryId.DataSource = this.taxCategoriesBindingSource;
            this.TaxCategoryId.DisplayMember = "TaxCategoryName";
            this.TaxCategoryId.HeaderText = "Tax Category";
            this.TaxCategoryId.Name = "TaxCategoryId";
            this.TaxCategoryId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TaxCategoryId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TaxCategoryId.ValueMember = "Id";
            this.TaxCategoryId.Width = 114;
            // 
            // taxCategoriesBindingSource
            // 
            this.taxCategoriesBindingSource.DataMember = "TaxCategories";
            this.taxCategoriesBindingSource.DataSource = this.dataSet;
            // 
            // CategoryId
            // 
            this.CategoryId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CategoryId.DataPropertyName = "CategoryId";
            this.CategoryId.DataSource = this.categoriesBindingSource3;
            this.CategoryId.DisplayMember = "Name";
            this.CategoryId.HeaderText = "Category";
            this.CategoryId.Name = "CategoryId";
            this.CategoryId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CategoryId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.CategoryId.ValueMember = "Id";
            this.CategoryId.Width = 88;
            // 
            // categoriesBindingSource3
            // 
            this.categoriesBindingSource3.DataMember = "Categories";
            this.categoriesBindingSource3.DataSource = this.dataSet;
            // 
            // AssetId
            // 
            this.AssetId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.AssetId.DataPropertyName = "AssetId";
            this.AssetId.DataSource = this.assetsBindingSource;
            this.AssetId.DisplayMember = "Name";
            this.AssetId.HeaderText = "Asset";
            this.AssetId.Name = "AssetId";
            this.AssetId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AssetId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.AssetId.ValueMember = "id";
            this.AssetId.Width = 67;
            // 
            // assetsBindingSource
            // 
            this.assetsBindingSource.DataMember = "Assets";
            this.assetsBindingSource.DataSource = this.dataSet;
            this.assetsBindingSource.Sort = "Name";
            // 
            // ContractorId
            // 
            this.ContractorId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ContractorId.DataPropertyName = "ContractorId";
            this.ContractorId.DataSource = this.contractorsBindingSource;
            this.ContractorId.DisplayMember = "Name";
            this.ContractorId.HeaderText = "Contractor";
            this.ContractorId.Name = "ContractorId";
            this.ContractorId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ContractorId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ContractorId.ValueMember = "id";
            this.ContractorId.Width = 94;
            // 
            // contractorsBindingSource
            // 
            this.contractorsBindingSource.DataMember = "1099Contractors";
            this.contractorsBindingSource.DataSource = this.dataSet;
            // 
            // notesDataGridViewTextBoxColumn
            // 
            this.notesDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.notesDataGridViewTextBoxColumn.DataPropertyName = "Notes";
            this.notesDataGridViewTextBoxColumn.HeaderText = "Notes";
            this.notesDataGridViewTextBoxColumn.Name = "notesDataGridViewTextBoxColumn";
            this.notesDataGridViewTextBoxColumn.Width = 69;
            // 
            // accountIdDataGridViewTextBoxColumn
            // 
            this.accountIdDataGridViewTextBoxColumn.DataPropertyName = "AccountId";
            this.accountIdDataGridViewTextBoxColumn.HeaderText = "AccountId";
            this.accountIdDataGridViewTextBoxColumn.Name = "accountIdDataGridViewTextBoxColumn";
            this.accountIdDataGridViewTextBoxColumn.Visible = false;
            // 
            // entityIdDataGridViewTextBoxColumn
            // 
            this.entityIdDataGridViewTextBoxColumn.DataPropertyName = "EntityId";
            this.entityIdDataGridViewTextBoxColumn.HeaderText = "EntityId";
            this.entityIdDataGridViewTextBoxColumn.Name = "entityIdDataGridViewTextBoxColumn";
            this.entityIdDataGridViewTextBoxColumn.Visible = false;
            // 
            // transactionsBindingSource
            // 
            this.transactionsBindingSource.DataMember = "Transactions";
            this.transactionsBindingSource.DataSource = this.dataSet;
            this.transactionsBindingSource.Sort = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 27;
            this.label2.Text = "Account:";
            // 
            // accountComboBox
            // 
            this.accountComboBox.DisplayMember = "Id";
            this.accountComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accountComboBox.FormattingEnabled = true;
            this.accountComboBox.Location = new System.Drawing.Point(83, 11);
            this.accountComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.accountComboBox.Name = "accountComboBox";
            this.accountComboBox.Size = new System.Drawing.Size(391, 24);
            this.accountComboBox.TabIndex = 26;
            this.accountComboBox.ValueMember = "Id";
            this.accountComboBox.SelectedIndexChanged += new System.EventHandler(this.accountComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(488, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 25;
            this.label1.Text = "Balance:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // includeReconciledTransactionsCheckBox
            // 
            this.includeReconciledTransactionsCheckBox.AutoSize = true;
            this.includeReconciledTransactionsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.includeReconciledTransactionsCheckBox.Location = new System.Drawing.Point(722, 40);
            this.includeReconciledTransactionsCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.includeReconciledTransactionsCheckBox.Name = "includeReconciledTransactionsCheckBox";
            this.includeReconciledTransactionsCheckBox.Size = new System.Drawing.Size(254, 20);
            this.includeReconciledTransactionsCheckBox.TabIndex = 24;
            this.includeReconciledTransactionsCheckBox.Text = "Include Reconciled Transactions";
            this.includeReconciledTransactionsCheckBox.UseVisualStyleBackColor = true;
            this.includeReconciledTransactionsCheckBox.CheckedChanged += new System.EventHandler(this.includeReconciledTransactionsCheckBox_CheckedChanged);
            // 
            // currentBalanceTextBox
            // 
            this.currentBalanceTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.currentBalanceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.currentBalanceTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentBalanceTextBox.Location = new System.Drawing.Point(563, 12);
            this.currentBalanceTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.currentBalanceTextBox.Name = "currentBalanceTextBox";
            this.currentBalanceTextBox.Size = new System.Drawing.Size(117, 22);
            this.currentBalanceTextBox.TabIndex = 28;
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.SystemColors.HotTrack;
            this.addButton.FlatAppearance.BorderSize = 0;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.ForeColor = System.Drawing.SystemColors.Control;
            this.addButton.Location = new System.Drawing.Point(12, 594);
            this.addButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(117, 30);
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
            this.saveButton.Location = new System.Drawing.Point(135, 594);
            this.saveButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(117, 30);
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
            this.deleteButton.Location = new System.Drawing.Point(259, 594);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(117, 30);
            this.deleteButton.TabIndex = 31;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // includeClearedTransactions
            // 
            this.includeClearedTransactions.AutoSize = true;
            this.includeClearedTransactions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.includeClearedTransactions.Location = new System.Drawing.Point(427, 41);
            this.includeClearedTransactions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.includeClearedTransactions.Name = "includeClearedTransactions";
            this.includeClearedTransactions.Size = new System.Drawing.Size(230, 20);
            this.includeClearedTransactions.TabIndex = 32;
            this.includeClearedTransactions.Text = "Include Cleared Transactions";
            this.includeClearedTransactions.UseVisualStyleBackColor = true;
            this.includeClearedTransactions.CheckedChanged += new System.EventHandler(this.includeClearedTransactions_CheckedChanged);
            // 
            // availableBankBalanceTextBox
            // 
            this.availableBankBalanceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.availableBankBalanceTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.availableBankBalanceTextBox.Location = new System.Drawing.Point(185, 39);
            this.availableBankBalanceTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.availableBankBalanceTextBox.Name = "availableBankBalanceTextBox";
            this.availableBankBalanceTextBox.Size = new System.Drawing.Size(117, 22);
            this.availableBankBalanceTextBox.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 41);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(178, 16);
            this.label4.TabIndex = 34;
            this.label4.Text = "Available Bank Balance:";
            // 
            // reconcileButton
            // 
            this.reconcileButton.BackColor = System.Drawing.SystemColors.HotTrack;
            this.reconcileButton.FlatAppearance.BorderSize = 0;
            this.reconcileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reconcileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reconcileButton.ForeColor = System.Drawing.SystemColors.Control;
            this.reconcileButton.Location = new System.Drawing.Point(311, 36);
            this.reconcileButton.Margin = new System.Windows.Forms.Padding(5);
            this.reconcileButton.Name = "reconcileButton";
            this.reconcileButton.Size = new System.Drawing.Size(91, 27);
            this.reconcileButton.TabIndex = 35;
            this.reconcileButton.Text = "Reconcile";
            this.reconcileButton.UseVisualStyleBackColor = false;
            this.reconcileButton.Click += new System.EventHandler(this.reconcileButton_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchTextBox.Location = new System.Drawing.Point(1180, 40);
            this.searchTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(223, 22);
            this.searchTextBox.TabIndex = 36;
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.SystemColors.HotTrack;
            this.searchButton.FlatAppearance.BorderSize = 0;
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchButton.ForeColor = System.Drawing.SystemColors.Control;
            this.searchButton.Location = new System.Drawing.Point(1103, 37);
            this.searchButton.Margin = new System.Windows.Forms.Padding(5);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(68, 27);
            this.searchButton.TabIndex = 37;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // categoriesBindingSource
            // 
            this.categoriesBindingSource.DataMember = "Categories";
            this.categoriesBindingSource.DataSource = this.dataSet;
            // 
            // categoriesBindingSource1
            // 
            this.categoriesBindingSource1.DataMember = "Categories";
            this.categoriesBindingSource1.DataSource = this.dataSet;
            // 
            // categoriesBindingSource2
            // 
            this.categoriesBindingSource2.DataMember = "Categories";
            this.categoriesBindingSource2.DataSource = this.dataSet;
            // 
            // transactionsTableAdapter
            // 
            this.transactionsTableAdapter.ClearBeforeFill = true;
            // 
            // categoriesTableAdapter
            // 
            this.categoriesTableAdapter.ClearBeforeFill = true;
            // 
            // taxFormsTableAdapter
            // 
            this.taxFormsTableAdapter.ClearBeforeFill = true;
            // 
            // taxCategoriesTableAdapter
            // 
            this.taxCategoriesTableAdapter.ClearBeforeFill = true;
            // 
            // assetsTableAdapter
            // 
            this.assetsTableAdapter.ClearBeforeFill = true;
            // 
            // _1099ContractorsTableAdapter
            // 
            this._1099ContractorsTableAdapter.ClearBeforeFill = true;
            // 
            // TransactionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1578, 631);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.reconcileButton);
            this.Controls.Add(this.availableBankBalanceTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.includeClearedTransactions);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.currentBalanceTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.accountComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.includeReconciledTransactionsCheckBox);
            this.Controls.Add(this.dgv);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TransactionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transactions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TransactionsForm_FormClosing);
            this.Load += new System.EventHandler(this.TransactionsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.taxFormsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.taxCategoriesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoriesBindingSource3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.assetsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contractorsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoriesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoriesBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoriesBindingSource2)).EndInit();
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
        private System.Windows.Forms.CheckBox includeReconciledTransactionsCheckBox;
        private System.Windows.Forms.TextBox currentBalanceTextBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.CheckBox includeClearedTransactions;
        private System.Windows.Forms.TextBox availableBankBalanceTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button reconcileButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.BindingSource categoriesBindingSource;
        private DataSetTableAdapters.CategoriesTableAdapter categoriesTableAdapter;
        private System.Windows.Forms.BindingSource categoriesBindingSource1;
        private System.Windows.Forms.BindingSource categoriesBindingSource2;
        private System.Windows.Forms.BindingSource taxFormsBindingSource;
        private DataSetTableAdapters.TaxFormsTableAdapter taxFormsTableAdapter;
        private System.Windows.Forms.BindingSource taxCategoriesBindingSource;
        private DataSetTableAdapters.TaxCategoriesTableAdapter taxCategoriesTableAdapter;
        private System.Windows.Forms.BindingSource categoriesBindingSource3;
        private System.Windows.Forms.BindingSource assetsBindingSource;
        private DataSetTableAdapters.AssetsTableAdapter assetsTableAdapter;
        private System.Windows.Forms.BindingSource contractorsBindingSource;
        private DataSetTableAdapters._1099ContractorsTableAdapter _1099ContractorsTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn depositDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn withdrawalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clearedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn checkNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn reconciledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn TaxFormId;
        private System.Windows.Forms.DataGridViewComboBoxColumn TaxCategoryId;
        private System.Windows.Forms.DataGridViewComboBoxColumn CategoryId;
        private System.Windows.Forms.DataGridViewComboBoxColumn AssetId;
        private System.Windows.Forms.DataGridViewComboBoxColumn ContractorId;
        private System.Windows.Forms.DataGridViewTextBoxColumn notesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn entityIdDataGridViewTextBoxColumn;
    }
}