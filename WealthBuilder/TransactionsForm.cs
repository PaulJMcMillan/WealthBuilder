using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace WealthBuilder
{
    public partial class TransactionsForm : Form
    {
        private long accountId;

        public TransactionsForm()
        {
            InitializeComponent();
        }

        private void TransactionsForm_Load(object sender, EventArgs e)
        {
            this._1099ContractorsTableAdapter.Fill(this.dataSet._1099Contractors);
            this.assetsTableAdapter.Fill(this.dataSet.Assets);
            this.taxCategoriesTableAdapter.Fill(this.dataSet.TaxCategories);
            this.taxFormsTableAdapter.Fill(this.dataSet.TaxForms);
            this.categoriesTableAdapter.Fill(this.dataSet.Categories);
            Text = Common.GetFormText(Text);
            string headerText = "Date";
            string dataPropertyName = "Date";
            string columnName = "dateDataGridViewCalendarColumn";
            int columnPosition = 1;
            DataGridViewHelper.InsertCalendarColumn(dgv, headerText, dataPropertyName, columnName, columnPosition);
            dataSet.Transactions.Columns["EntityId"].DefaultValue = CurrentEntity.Id;
            transactionsTableAdapter.Fill(dataSet.Transactions);
            LoadAccountsComboBox();
            SelectDefaultAccount();
            FilterDGV();
            UpdateCurrentBalance();

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

        }

        private void SelectDefaultAccount()
        {
            using (var db = new WBEntities())
            {
                var r = db.Accounts.Where(x => x.Active == true && x.DefaultAccount == true && x.EntityId == CurrentEntity.Id).FirstOrDefault();
                if (r == null) return;
                accountComboBox.Text = r.Name;
            }
        }

        public void UpdateCurrentBalance()
        {
            long accountId;
            if (accountComboBox.SelectedIndex == -1) return;
            if (!long.TryParse(accountComboBox.SelectedValue.ToString(), out accountId)) return;
            currentBalanceTextBox.Text = Cash.GetBalanceForOneAccount(accountId, (long)CurrentEntity.Id).ToString("C");
        }

        private void LoadAccountsComboBox()
        {
            using (var db = new WBEntities())
            {
                var accounts = db.Accounts.Where(x => x.EntityId == CurrentEntity.Id && x.Active == true).OrderBy(x => x.Name).ToList();
                accountComboBox.DisplayMember = "Name";
                accountComboBox.ValueMember = "Id";
                accountComboBox.DataSource = accounts;
            }
        }

        private void includeReconciledTransactionsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Save();
            UpdateCurrentBalance();
            FilterDGV();
        }

        public void FilterDGV()
        {
            if (accountComboBox.SelectedIndex == -1) return;
            if (!long.TryParse(accountComboBox.SelectedValue.ToString(), out accountId)) return;
            dataSet.Transactions.Columns["AccountId"].DefaultValue = accountId;
            transactionsBindingSource.Filter = BuildFilter(true);
        }

        private string BuildFilter(bool includeDateRange)
        {
            string s = includeReconciledTransactionsCheckBox.Checked ? string.Empty : " AND (Reconciled is null or Reconciled = 0)";
            s += includeClearedTransactions.Checked ? string.Empty : " AND (Cleared is null or Cleared = 0)";
            string filter = "EntityId = " + CurrentEntity.Id + " And AccountId = " + accountId + s;
            return filter;
        }

        private bool Save()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                Validate();
                transactionsBindingSource.EndEdit();
                transactionsTableAdapter.Update(dataSet.Transactions);
                FilterDGV();
                return true;
            }
            catch (Exception ex)
            {
                Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(WBResource.GenericErrorMessage, WBResource.GenericErrorTitle);
                return false;
            }
        }

        private void accountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataSet.Transactions.Columns["AccountId"].DefaultValue = accountId;
            Save();
            FilterDGV();
            UpdateCurrentBalance();
            availableBankBalanceTextBox.Text = string.Empty;
        }

        private void TransactionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
            UpdateCurrentBalance();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                //transactionsBindingSource.Filter = BuildFilter(false);
                dataSet.Transactions.Rows.Add();
               // transactionsBindingSource.ResetBindings(false);
               // int rowCount = dgv.RowCount;
               // if (rowCount > 0) dgv.CurrentCell = dgv.Rows[rowCount - 1].Cells[1];
            }
            catch (Exception ex)
            {
                Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(WBResource.GenericErrorMessage, WBResource.GenericErrorTitle);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Save();
            UpdateCurrentBalance();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (!DataGridViewHelper.DeleteRows(dgv)) MessageBox.Show(WBResource.GenericErrorMessage);
        }

        private void includeClearedTransactions_CheckedChanged(object sender, EventArgs e)
        {
            Save();
            UpdateCurrentBalance();
            FilterDGV();
        }

        private void reconcileButton_Click(object sender, EventArgs e)
        {
            if (accountComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Select an account.", Text);
                return;
            }

            if (!decimal.TryParse(availableBankBalanceTextBox.Text, out decimal availableBankBalance))
            {
                MessageBox.Show("Invalid amount", Text);
                return;
            }

            reconciliationReport = new StringBuilder();
            reconciliationReport.Append("Reconciliation Report" + Environment.NewLine);
            reconciliationReport.Append("=====================" + Environment.NewLine+Environment.NewLine);
            Save();
            UpdateCurrentBalance();
            decimal accountBalance;
            decimal unclearedAmount;
            int accountId;
            if (!int.TryParse(accountComboBox.SelectedValue.ToString(), out accountId)) return;
            accountBalance = Cash.GetBalanceForOneAccount(accountId, (long)CurrentEntity.Id);
            reconciliationReport.Append("Current Balance: " + accountBalance.ToString("C") + Environment.NewLine);
            reconciliationReport.Append("Available Bank Balance: " + availableBankBalance.ToString("C") + Environment.NewLine+Environment.NewLine);
            unclearedAmount = Reconciliation.CalculateUnclearedTransactions(accountId, reconciliationReport);
            decimal adjustedBankBalance = availableBankBalance + unclearedAmount;
            reconciliationReport.Append("Adjusted Available Bank Balance: " + adjustedBankBalance.ToString("C")+" (Available Bank Balance minus the sum of Uncleared Amounts)"+ Environment.NewLine + Environment.NewLine);
            decimal difference = adjustedBankBalance - accountBalance;
            reconciliationReport.Append("Difference: " + difference.ToString("C")+" (Adjusted Available Bank Balance minus Current Balance)"+ Environment.NewLine + Environment.NewLine);
            reconciliationReport.Append("* If the Difference is positive, it means the bank thinks you have more money than your checkbook says you do." + Environment.NewLine);
            reconciliationReport.Append("* Vice versa, if the Difference is negative, it means the bank thinks you have less money than your checkbook says you do." + Environment.NewLine);
            difference = Math.Round(difference, 2);
            File.WriteAllText(Constants.ReconciliationReportFileName, reconciliationReport.ToString());

            if (difference == 0)
            {
                BalancedProcessing();
                return;
            }

            string msg = "You did not balance.  You are off by " + difference.ToString("C") + ".  Click OK to display the reconciliation report.";
            MessageBox.Show(msg, Text);
            Code.Form.Open("ReconciliationReportForm");
        }

        private StringBuilder reconciliationReport;

        private void BalancedProcessing()
        {
            try
            {
                using (var db = new WBEntities())
                {
                    string sql = "Update Transactions Set Reconciled = 1 Where (reconciled is null or reconciled = 0) And Cleared = 1 And AccountId = " + accountId.ToString() + " and EntityId = " + CurrentEntity.Id.ToString();
                    db.Database.ExecuteSqlCommand(sql);
                    transactionsTableAdapter.Fill(dataSet.Transactions);
                    MessageBox.Show("You balanced!  Cleared Transactions have been marked Reconciled.", Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to mark reconciled transactions.  Please try again or contact Customer Care.");
                Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        //private void MoveReconciledTransactionsToHistoryTable()
        //{
        //    //Put transaction in a holding table.
        //    string deleteSql = "Delete From TransferTransactions";
        //    string sql = "Insert Into TransferTransactions Select * From Transactions Where Reconciled = 1 And AccountId = " + accountId.ToString() + " and EntityId = " + CurrentEntity.Id.ToString();

        //    using (var db = new WBEntities())
        //    {
        //        db.Database.ExecuteSqlCommand(deleteSql);
        //        int numberOfReconciledTransactions = db.Database.ExecuteSqlCommand("Select count(*) From Transactions where Reconciled = 1 And AccountId = " + accountId.ToString() + " and EntityId = " + CurrentEntity.Id.ToString()); 
        //        int numberOfRowsAffected = db.Database.ExecuteSqlCommand(sql);

        //        if(numberOfReconciledTransactions != numberOfRowsAffected)
        //        {
        //            MessageBox.Show("Something went wrong.  Operation was aborted.");
        //            return;
        //        }

        //        int numberOfRowsDeleted = db.Database.ExecuteSqlCommand("Delete From Transactions Where Reconciled =1 And AccountId = " + accountId.ToString() + " and EntityId = " + CurrentEntity.Id.ToString());

        //        if (numberOfRowsDeleted != numberOfRowsAffected)
        //        {
        //            MessageBox.Show("Something went wrong."); //todo:
        //            return;
        //        }

        //        CreateSummaryEntryForReconciledTransactions();
        //    }
        //}

        //private void CreateSummaryEntryForReconciledTransactions()
        //{
        //    using (var db = new WBEntities())
        //    {
        //        decimal? depositSummary = db.TransferTransactions.Sum(x => x.Deposit);
        //        decimal? withdrawalSummary = db.TransferTransactions.Sum(x => x.Withdrawal);

        //        var transaction = new Transaction()
        //        {
        //            Date = DateTime.Now,
        //            Description = "Reconciliation Summary",
        //            Deposit = depositSummary,
        //            Withdrawal = withdrawalSummary,
        //            Cleared = true,
        //            Reconciled = false,//todo:
        //            Notes = "Result of reconciliation on this date.",
        //            AccountId = (int)accountId,
        //            EntityId = (int)CurrentEntity.Id
        //        };

        //        db.Transactions.Add(transaction);
        //        db.SaveChanges();
        //    }
        //}

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTextBox.Text)) return;

            foreach(DataGridViewRow row in dgv.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex == 0) continue;
                    string content = cell.Value.ToString();
                    if (content.Contains(searchTextBox.Text)) dgv.CurrentCell = dgv[cell.ColumnIndex, cell.RowIndex];
                }
            }
        }

        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //Don't remove this function, it prevents the grid for erroring out when you exit the form.
        }

        private void startDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            FilterDGV();
        }

        private void endDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            FilterDGV();
        }
    }
}
