using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WealthBuilder.Helpers;

namespace WealthBuilder
{
    public partial class TransactionsForm : Form
    {
        private int accountId;
        private List<TaxCategory> _taxCategories;

        public enum SortDirection
        {
            Ascending,
            Descending,
            None
        };

        private SortDirection sortDirection;

        public TransactionsForm()
        {
            InitializeComponent();
        }

        private void TransactionsForm_Load(object sender, EventArgs e)
        {
            GetTaxCategories();
            LoadTaxCategoriesComboBox();
            showAllTransactionsCheckBox.Checked = false;
            sortDirection = SortDirection.Descending;
            Text = Common.GetFormText(Text);
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            LoadAccountsComboBox();
            SelectDefaultAccount();
            var transactions = GetTransactions(sortDirection);
            dgv.DataSource = transactions;
            ClearFormFields();
            UpdateCurrentBalance();
            Common.ClearSelection(dgv);
        }

        private void LoadTaxCategoriesComboBox()
        {
            using (var db = new WealthBuilderEntities1())
            {
                var taxCategories = db.TaxCategories.OrderBy(x => x.TaxCategoryName).ToList();
                taxCategoryIdComboBox.DisplayMember = "TaxCategoryName";
                taxCategoryIdComboBox.ValueMember = "Id";
                taxCategoryIdComboBox.DataSource = taxCategories;
            }
        }

        private void GetTaxCategories()
        {
            using (var db = new WealthBuilderEntities1())
            {
                _taxCategories = db.TaxCategories.ToList();
            }
        }

        private void SelectDefaultAccount()
        {
            using (var db = new WealthBuilderEntities1())
            {
                var r = db.Accounts.Where(x => x.Active == true && x.DefaultAccount == true && x.EntityId == CurrentEntity.Id).FirstOrDefault();
                if (r == null) return;
                accountComboBox.Text = r.Name;
            }
        }

        public void UpdateCurrentBalance()
        {
            long accountId;
            if (accountComboBox.SelectedIndex == -1 || !long.TryParse(accountComboBox.SelectedValue.ToString(), out accountId))
            {
                currentBalanceTextBox.Text = "$0.00";
                return;
            }

            currentBalanceTextBox.Text = Cash.GetAccountBalance(accountId).ToString("C");
        }

        private void LoadAccountsComboBox()
        {
            using (var db = new WealthBuilderEntities1())
            {
                var accounts = db.Accounts.Where(x => x.EntityId == CurrentEntity.Id && x.Active == true).OrderBy(x => x.Name).ToList();
                accountComboBox.DisplayMember = "Name";
                accountComboBox.ValueMember = "Id";
                accountComboBox.DataSource = accounts;
            }
        }

        public List<YearEndTaxViewModel> GetTransactions(SortDirection sortDirection)
        {
            var yearEndViewModels = new List<YearEndTaxViewModel>();

            if (accountComboBox.SelectedIndex == -1)
            {
                return yearEndViewModels;
            }

            if (!int.TryParse(accountComboBox.SelectedValue.ToString(), out accountId))
            {
                return yearEndViewModels;
            }

            List<Transaction> transactions;

            using (var db = new WealthBuilderEntities1())
            {
                if (showAllTransactionsCheckBox.Checked)
                {
                    if(sortDirection == SortDirection.Descending)
                    {
                        transactions = db.Transactions.Where(x => x.AccountId == accountId).OrderByDescending(x => x.Date).ToList();
                    }
                    else
                    {
                        transactions = db.Transactions.Where(x => x.AccountId == accountId).OrderBy(x => x.Date).ToList();
                    }

                    yearEndViewModels = WrapTransactionData(transactions);
                    return yearEndViewModels;
                }
          
                if(sortDirection == SortDirection.Descending)
                {
                    transactions = db.Transactions.Where(x => x.AccountId == accountId &&
                        (x.Reconciled == false || x.Cleared == false)).OrderByDescending(x => x.Date).ToList();
                }
                else
                {
                    transactions = db.Transactions.Where(x => x.AccountId == accountId &&
                        (x.Reconciled == false || x.Cleared == false)).OrderBy(x => x.Date).ToList();
                }

                yearEndViewModels = WrapTransactionData(transactions);
                return yearEndViewModels;
            }
        }

        private List<YearEndTaxViewModel> WrapTransactionData(List<Transaction> transactions)
        {
            var yearEndViewModels = new List<YearEndTaxViewModel>();

            foreach (var transaction in transactions)
            {
                var yearEndViewModel = new YearEndTaxViewModel()
                {
                    Id = transaction.Id,
                    Date = transaction.Date,
                    Description = transaction.Description,
                    Deposit = transaction.Deposit,
                    Withdrawal = transaction.Withdrawal,
                    Notes = transaction.Notes,
                    AccountId = transaction.AccountId,
                    TaxCategoryId = transaction.TaxCategoryId,
                    TaxCategoryName = GetTaxCategoryName(transaction.TaxCategoryId),
                    Cleared=transaction.Cleared,
                    CheckNumber=transaction.CheckNumber,
                    Reconciled=transaction.Reconciled,
                };

                yearEndViewModels.Add(yearEndViewModel);
            }

            return yearEndViewModels;
        }

        private string GetTaxCategoryName(int taxCategoryId)
        {
            var taxCategory = _taxCategories.Where(x => x.Id == taxCategoryId).FirstOrDefault();

            if (taxCategory == null)
            {
                return string.Empty;
            }

            return taxCategory.TaxCategoryName;
        }

        private Transaction Save(int transId)
        {
            using (var db = new WealthBuilderEntities1())
            {
                var tran = db.Transactions.Where(x => x.Id == transId).FirstOrDefault();
                decimal deposit = StringHelper.ConvertToDecimalWithEmptyString(DepositTextBox.Text);
                decimal wd = StringHelper.ConvertToDecimalWithEmptyString(WithdrawalTextBox.Text);
                tran.Date = dateTimePicker.Value;
                tran.Description = DescriptionTextBox.Text;
                tran.Deposit = deposit;
                tran.Withdrawal = wd;
                tran.Cleared = ClearedCheckBox.Checked;
                tran.Reconciled = ReconciledCheckBox.Checked;
                tran.CheckNumber = CheckNumberTextBox.Text;
                tran.Notes = NotesRichTextBox.Text;
                tran.TaxCategoryId = (int)taxCategoryIdComboBox.SelectedValue;
                db.SaveChanges();
                return tran; 
            }
        }

        private int AddNewRecord()
        {
            using ( var db = new WealthBuilderEntities1())
            {
                decimal deposit = StringHelper.ConvertToDecimalWithEmptyString(DepositTextBox.Text);
                decimal wd = StringHelper.ConvertToDecimalWithEmptyString(WithdrawalTextBox.Text);

                var transaction = new Transaction()
                {
                    Date = dateTimePicker.Value,
                    Description = DescriptionTextBox.Text,
                    Deposit = deposit,
                    Withdrawal = wd,
                    Cleared = ClearedCheckBox.Checked,
                    Reconciled = ReconciledCheckBox.Checked,
                    CheckNumber = CheckNumberTextBox.Text,
                    Notes = NotesRichTextBox.Text,
                    AccountId = (int)accountComboBox.SelectedValue,
                    TaxCategoryId = taxCategoryIdComboBox.SelectedValue != null ? (int)taxCategoryIdComboBox.SelectedValue : -1
                };

                db.Transactions.Add(transaction);
                db.SaveChanges();
                return transaction.Id; 
            }
        }

        private bool ValidateData()
        {
            decimal deposit = 0;
            decimal wd = 0;
            bool displayMsg = false;

            if (!string.IsNullOrWhiteSpace(DepositTextBox.Text))
            {
                if (!decimal.TryParse(StringHelper.StripDollarSignAndCommas(DepositTextBox.Text)
                    , out deposit)) displayMsg = true;
            }

            if (!string.IsNullOrWhiteSpace(WithdrawalTextBox.Text))
            {
                if (!decimal.TryParse(StringHelper.StripDollarSignAndCommas(WithdrawalTextBox.Text)
                    , out wd)) displayMsg = true;
            }

            if (deposit != 0 && wd != 0) displayMsg = true;
            if (deposit == 0 && wd == 0) displayMsg = true;

            if (displayMsg)
            {
                MessageBox.Show("Your amounts are not correct.");
                return false;
            }

            return true;
        }

        private void accountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var transactions = GetTransactions(sortDirection);
            dgv.DataSource = transactions;
            UpdateCurrentBalance();
            availableBankBalanceTextBox.Text = string.Empty;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateData()) return;
                int transactionId = AddNewRecord();
                AddNewRowToDgv(transactionId);
                UpdateCurrentBalance();
                ClearFormFields();
                Common.ClearSelection(dgv);
                MessageBox.Show("Added");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddNewRowToDgv(int transactionId)
        {
            var transactions = GetTransactions(SortDirection.None);
            dgv.DataSource = transactions;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if((int)row.Cells["Id"].Value == transactionId)
                {
                    row.Selected = true;
                    break;
                }
                else
                {
                    row.Selected = false;
                }
            }

            if(dgv.SelectedRows.Count > 0) dgv.FirstDisplayedScrollingRowIndex = dgv.SelectedRows[0].Index;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!ValidateData()) return;
            var row = dgv.CurrentRow;

            if (row == null)
            {
                MessageBox.Show("Please select a row, update the data in the form, and then click the 'Update' button.");
                return;
            }
            var transactionId = (int)row.Cells["Id"].Value;
            var transaction = Save(transactionId);
           
            row.Cells["Date"].Value = transaction.Date;
            row.Cells["Description"].Value = transaction.Description;
            row.Cells["deposit"].Value = transaction.Deposit;
            row.Cells["Withdrawal"].Value = transaction.Withdrawal;
            row.Cells["Cleared"].Value = transaction.Cleared;
            row.Cells["Reconciled"].Value = transaction.Reconciled;
            row.Cells["CheckNumber"].Value = transaction.CheckNumber;
            row.Cells["Notes"].Value = transaction.Notes;
            row.Cells["TaxCategoryName"].Value = GetTaxCategoryName(transaction.TaxCategoryId);

            UpdateCurrentBalance();
            ClearFormFields();
            Common.ClearSelection(dgv);
            MessageBox.Show("Updated");
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            using (var db = new WealthBuilderEntities1())
            {
                var row = dgv.CurrentRow;
                if (row == null) return;
                int transactionId = (int)row.Cells["Id"].Value;
                int rowIndex = row.Index;
                var transaction = db.Transactions.Where(x => x.Id == transactionId).FirstOrDefault();
                db.Transactions.Remove(transaction);
                db.SaveChanges();
                var transactions = GetTransactions(sortDirection);
                dgv.DataSource = transactions;

                if (rowIndex > -1 && rowIndex < dgv.Rows.Count)
                {
                    dgv.Rows[rowIndex].Selected = true;
                    dgv.FirstDisplayedScrollingRowIndex = rowIndex;
                } 
            }
            
            UpdateCurrentBalance();
            ClearFormFields();
            Common.ClearSelection(dgv);
            MessageBox.Show("Deleted");
        }

        private void showAllTransactions_CheckBoxChecked(object sender, EventArgs e)
        {
            var transactions = GetTransactions(sortDirection);
            dgv.DataSource = transactions;
            UpdateCurrentBalance();
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
                MessageBox.Show("Invalid Avail. Bank Balance", Text);
                return;
            }

            reconciliationReport = new StringBuilder();
            reconciliationReport.Append("Reconciliation Report" + Environment.NewLine);
            reconciliationReport.Append("=====================" + Environment.NewLine+Environment.NewLine);
            decimal accountBalance;
            decimal unclearedAmount;
            int accountId;
            if (!int.TryParse(accountComboBox.SelectedValue.ToString(), out accountId)) return;
            accountBalance = Cash.GetAccountBalance(accountId);
            reconciliationReport.Append("Current Balance: " + accountBalance.ToString("C") + Environment.NewLine);
            reconciliationReport.Append("Available Bank Balance: " + availableBankBalance.ToString("C") + 
                Environment.NewLine+Environment.NewLine);
            unclearedAmount = Reconciliation.CalculateUnclearedTransactions(accountId, reconciliationReport);
            decimal adjustedBankBalance = availableBankBalance + unclearedAmount;
            reconciliationReport.Append("Adjusted Available Bank Balance: " + adjustedBankBalance.ToString("C")+
                " (Available Bank Balance minus the sum of Uncleared Amounts)"+ Environment.NewLine + Environment.NewLine);
            decimal difference = adjustedBankBalance - accountBalance;
            reconciliationReport.Append("Difference: " + difference.ToString("C")+
                " (Adjusted Available Bank Balance minus Current Balance)"+ Environment.NewLine + Environment.NewLine);
            reconciliationReport.Append("* If the Difference is positive, it means the bank thinks you have more money than your checkbook says you do." + 
                Environment.NewLine);
            reconciliationReport.Append("* Vice versa, if the Difference is negative, it means the bank thinks you have less money than your checkbook says you do." + 
                Environment.NewLine);
            difference = Math.Round(difference, 2);
            File.WriteAllText(Constants.ReconciliationReportFileName, reconciliationReport.ToString());

            if (difference == 0)
            {
                BalancedProcessing();
                UpdateGrid();
                return;
            }

            string msg = "You did not balance.  You are off by " + difference.ToString("C") + ".  Click OK to display the reconciliation report.";
            MessageBox.Show(msg, Text);
            Code.Form.Open("ReconciliationReportForm");
        }

        private void UpdateGrid()
        {
            foreach(DataGridViewRow row in dgv.Rows)
            {
                if((bool)row.Cells["Reconciled"].Value == false && (bool)row.Cells["Cleared"].Value)
                {
                    row.Cells["Reconciled"].Value = true;
                }
            }
        }

        private StringBuilder reconciliationReport;

        private void BalancedProcessing()
        {
            using (var db = new WealthBuilderEntities1())
            {
                string sql = "Update Transactions Set Reconciled = 1 Where reconciled = 0 And Cleared = 1 And AccountId = " + 
                             accountId.ToString();
                db.Database.ExecuteSqlCommand(sql);
                var displayMessageForm = new DisplayMessageForm("You balanced!");
                displayMessageForm.Show();
            }
        }

        private void Dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //Don't remove this function, it prevents the grid for erroring out when you exit the form.
        }

        private void ClearFormFields()
        {
            dateTimePicker.Value = DateTime.Today;
            DescriptionTextBox.Text = string.Empty;
            DepositTextBox.Text = string.Empty;
            WithdrawalTextBox.Text = string.Empty;
            ClearedCheckBox.Checked = false;
            ReconciledCheckBox.Checked = false;
            NotesRichTextBox.Text = string.Empty;
            CheckNumberTextBox.Text = string.Empty;
            taxCategoryIdComboBox.SelectedIndex = -1;
        }

        private void dgv_MouseClick(object sender, MouseEventArgs e)
        {
            ClearFormFields();
            PopulateForm();
        }

        private void PopulateForm()
        {
            if (dgv.CurrentRow == null) return;
            var currentRow = dgv.CurrentRow;
            dateTimePicker.Value = (DateTime)currentRow.Cells["Date"].Value;
            DescriptionTextBox.Text = (string)currentRow.Cells["Description"].Value;
            decimal deposit = (decimal)currentRow.Cells["Deposit"].Value;
            DepositTextBox.Text = deposit == 0 ? string.Empty : deposit.ToString("C");
            decimal wd = (decimal)currentRow.Cells["Withdrawal"].Value;
            WithdrawalTextBox.Text = wd == 0 ? string.Empty : wd.ToString("C");
            ClearedCheckBox.Checked = (bool)currentRow.Cells["Cleared"].Value;
            ReconciledCheckBox.Checked = (bool)currentRow.Cells["Reconciled"].Value;
            NotesRichTextBox.Text = (string)currentRow.Cells["Notes"].Value;
            CheckNumberTextBox.Text = (string)currentRow.Cells["CheckNumber"].Value;
            taxCategoryIdComboBox.Text = (string)currentRow.Cells["TaxCategoryName"].Value;
        }

        private void dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            
        }

        private void TransactionsForm_Shown(object sender, EventArgs e)
        {
            Common.ClearSelection(dgv);
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                if(sortDirection==SortDirection.Ascending)
                {
                    sortDirection = SortDirection.Descending;
                }
                else
                {
                    sortDirection = SortDirection.Ascending;
                }

                var transactions = GetTransactions(sortDirection);
                dgv.DataSource = transactions;
            }
        }

        private void TransactionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void TransactionsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
