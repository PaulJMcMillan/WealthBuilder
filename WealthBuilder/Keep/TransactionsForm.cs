using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WealthBuilder.Helpers;

namespace WealthBuilder
{
    public partial class TransactionsForm : Form
    {
        private int accountId;

        public TransactionsForm()
        {
            InitializeComponent();
        }

        private void TransactionsForm_Load(object sender, EventArgs e)
        {
            displayTransBackTo.Text = "1 Month";
            Text = Common.GetFormText(Text);
            dataSet.Transactions.Columns["EntityId"].DefaultValue = CurrentEntity.Id;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            LoadAccountsComboBox();
            SelectDefaultAccount();
            FilterDgv();
            SortDgv();
            PopulateForm();
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
            currentBalanceTextBox.Text = Cash.GetBalanceForOneAccount(accountId, CurrentEntity.Id).ToString("C");
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
            FilterDgv();
            UpdateCurrentBalance();
        }

        public void SortDgv()
        {
            //Gets a fresh data source.
            if (accountComboBox.SelectedIndex == -1) return;
            if (!int.TryParse(accountComboBox.SelectedValue.ToString(), out accountId)) return;
            dataSet.Transactions.Columns["AccountId"].DefaultValue = accountId;
            DateTime displayTransactionsBackTo = DateTime.Now;

            switch (displayTransBackTo.Text)
            {
                case "1 Month":
                    displayTransactionsBackTo = DateTime.Now.AddMonths(-1).Date;
                    break;
                case "1 Quarter":
                    displayTransactionsBackTo = DateTime.Now.AddMonths(-3).Date;
                    break;
                case "6 Months":
                    displayTransactionsBackTo = DateTime.Now.AddMonths(-6).Date;
                    break;
                case "1 Year":
                    displayTransactionsBackTo = DateTime.Now.AddYears(-1).Date;
                    break;
                default:
                    displayTransactionsBackTo = DateTime.Now.AddMonths(-1).Date;
                    break;
            }

            using (var db = new WBEntities())
            {
                var transactions = db.Transactions.Where(x => x.EntityId == CurrentEntity.Id && x.AccountId == accountId &&
                    x.Date >= displayTransactionsBackTo);

                if (excludeReconciledTransactionsCheckBox.Checked)
                    transactions = transactions.Where(x => x.Reconciled == false);

                if (excludeClearedTransactionsCheckBox.Checked)
                    transactions = transactions.Where(x => x.Cleared == false);

                transactions = transactions.OrderByDescending(x => x.Date);
                dgv.DataSource = transactions.ToList();
            }
        }

        public void FilterDgv()
        {
            if (accountComboBox.SelectedIndex == -1) return;
            if (!int.TryParse(accountComboBox.SelectedValue.ToString(), out accountId)) return;
            dataSet.Transactions.Columns["AccountId"].DefaultValue = accountId;
            DateTime displayTransactionsBackTo = DateTime.Now;

            switch (displayTransBackTo.Text)
            {
                case "1 Month":
                    displayTransactionsBackTo = DateTime.Now.AddMonths(-1).Date;
                    break;
                case "1 Quarter":
                    displayTransactionsBackTo = DateTime.Now.AddMonths(-3).Date;
                    break;
                case "6 Months":
                    displayTransactionsBackTo = DateTime.Now.AddMonths(-6).Date;
                    break;
                case "1 Year":
                    displayTransactionsBackTo = DateTime.Now.AddYears(-1).Date;
                    break;
                default:
                    displayTransactionsBackTo = DateTime.Now.AddMonths(-1).Date;
                    break;
            }

            using (var db = new WBEntities())
            {
                var transactions = db.Transactions.Where(x => x.EntityId == CurrentEntity.Id && x.AccountId == accountId &&
                    x.Date >= displayTransactionsBackTo);

                if (excludeReconciledTransactionsCheckBox.Checked)
                    transactions = transactions.Where(x => x.Reconciled == false);

                if (excludeClearedTransactionsCheckBox.Checked)
                    transactions = transactions.Where(x => x.Cleared == false);

                //transactions = transactions.OrderByDescending(x => x.Date);
                dgv.DataSource = transactions.ToList();
            }

        }

        private Transaction Save(int transId)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                if (!ValidateData()) 
                {
                    MessageBox.Show("Invalid data");
                    return null;
                }

                using (var db = new WBEntities())
                {
                    var trans = db.Transactions.Where(x => x.Id == transId).FirstOrDefault();
                    decimal deposit = StringHelper.ConvertToDecimalWithEmptyString(DepositTextBox.Text);
                    decimal wd = StringHelper.ConvertToDecimalWithEmptyString(WithdrawalTextBox.Text);
                    trans.Date = dateTimePicker.Value;
                    trans.Description = DescriptionTextBox.Text;
                    trans.Deposit = deposit;
                    trans.Withdrawal = wd;
                    trans.Cleared = ClearedCheckBox.Checked;
                    trans.Reconciled = ReconciledCheckBox.Checked;
                    trans.CheckNumber = CheckNumberTextBox.Text;
                    trans.Notes = NotesRichTextBox.Text;
                    db.SaveChanges();
                    return trans;
                }
            }
            catch (Exception ex)
            {
                Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(WBResource.GenericErrorMessage, WBResource.GenericErrorTitle);
                return null;
            }
        }

        private int AddNewRecord()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                if (!ValidateData())
                {
                    MessageBox.Show("Invalid data");
                    return -1;
                }

                using (var db = new WBEntities())
                {
                    decimal deposit = StringHelper.ConvertToDecimalWithEmptyString(DepositTextBox.Text);
                    decimal wd = StringHelper.ConvertToDecimalWithEmptyString(WithdrawalTextBox.Text);

                    var trans = new Transaction()
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
                        EntityId = CurrentEntity.Id
                    };

                    db.Transactions.Add(trans);
                    db.SaveChanges();
                    return trans.Id;
                   
                }

            }
            catch (Exception ex)
            {
                Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(WBResource.GenericErrorMessage, WBResource.GenericErrorTitle);
                return -1;
            }
        }

        private bool ValidateData()
        {
            if (DepositTextBox.Text == string.Empty && WithdrawalTextBox.Text == string.Empty) return false;
            decimal deposit = 0;
            decimal wd = 0;

            if (!string.IsNullOrWhiteSpace(DepositTextBox.Text))
            {
                if (!decimal.TryParse(StringHelper.StripDollarSignAndCommas(DepositTextBox.Text)
                    , out deposit)) return false;
            }

            if (!string.IsNullOrWhiteSpace(WithdrawalTextBox.Text))
            {
                if (!decimal.TryParse(StringHelper.StripDollarSignAndCommas(WithdrawalTextBox.Text)
                    , out wd)) return false;
            }

            if (deposit != 0 && wd != 0) return false;
            return true;
        }

        private void accountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterDgv();
            UpdateCurrentBalance();
            availableBankBalanceTextBox.Text = string.Empty;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            int transId = AddNewRecord();
            if (transId == -1) return;
            AddNewRowToDgv(transId);
            //FilterDgv();
            UpdateCurrentBalance();
        }

        private void AddNewRowToDgv(int transId)
        {
            FilterDgv();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if((int)row.Cells["Id"].Value == transId)
                {
                    row.Selected = true;
                    break;
                }
            }

            dgv.FirstDisplayedScrollingRowIndex = dgv.SelectedRows[0].Index;
            //using (var db = new WBEntities())
            //{
            //    var transaction = db.Transactions.Where(x => x.Id == transId).FirstOrDefault();
            //    if (transaction == null) return;

            //}
            //int rowIndex = dgv.Rows.Add();
            //DataGridViewRow row = dgv.Rows[rowIndex];
            //row.Cells["Date"].Value = dateTimePicker.Value;
            //row.Cells["Description"].Value = DescriptionTextBox.Text;
            //row.Cells["deposit"].Value = string.IsNullOrWhiteSpace(DepositTextBox.Text) ? 0 : Convert.ToDecimal(DepositTextBox.Text);
            //row.Cells["Withdrawal"].Value = string.IsNullOrWhiteSpace(WithdrawalTextBox.Text) ? 0 : Convert.ToDecimal(WithdrawalTextBox.Text);
            //row.Cells["Cleared"].Value = ClearedCheckBox.Checked;
            //row.Cells["Reconciled"].Value = ReconciledCheckBox.Checked;
            //row.Cells["CheckNumber"].Value = CheckNumberTextBox.Text;
            //row.Cells["Notes"].Value = NotesRichTextBox.Text;
            //dgv.Rows.Add(row);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var row = dgv.CurrentRow;
            if (row == null) return;
            var transId = (int)row.Cells["Id"].Value;
            Transaction transaction = Save(transId);
            if (transaction == null) return;
            dateTimePicker.Value = transaction.Date;
            DescriptionTextBox.Text = transaction.Description;
            DepositTextBox.Text = transaction.Deposit.ToString("C");
            WithdrawalTextBox.Text = transaction.Withdrawal.ToString("C");
            ClearedCheckBox.Checked = transaction.Cleared;
            ReconciledCheckBox.Checked = transaction.Reconciled;
            CheckNumberTextBox.Text = transaction.CheckNumber;
            NotesRichTextBox.Text = transaction.Notes;

            row.Cells["Date"].Value = transaction.Date;
            row.Cells["Description"].Value = transaction.Description;
            row.Cells["deposit"].Value = transaction.Deposit;
            row.Cells["Withdrawal"].Value = transaction.Withdrawal;
            row.Cells["Cleared"].Value = transaction.Cleared;
            row.Cells["Reconciled"].Value = transaction.Reconciled;
            row.Cells["CheckNumber"].Value = transaction.CheckNumber;
            row.Cells["Notes"].Value = transaction.Notes;

            UpdateCurrentBalance();
            return;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var row = dgv.CurrentRow;
            if (row == null) return;
            int transId = (int)row.Cells["Id"].Value;
            int rowIndex = row.Index;

            using (var db = new WBEntities())
            {
                var trans = db.Transactions.Where(x => x.Id == transId).FirstOrDefault();
                db.Transactions.Remove(trans);
                db.SaveChanges();
            }

            ClearFormFields();
            FilterDgv();
            
            if(rowIndex > -1 && rowIndex < dgv.Rows.Count)
            {
                dgv.Rows[rowIndex].Selected = true;
                dgv.FirstDisplayedScrollingRowIndex = rowIndex;
            }

            
            UpdateCurrentBalance();
            return;
        }

        private void includeClearedTransactions_CheckedChanged(object sender, EventArgs e)
        {
            FilterDgv();
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
                    string sql = "Update Transactions Set Reconciled = 1 "+
                        "Where reconciled = 0 And Cleared = 1 And AccountId = " + 
                        accountId.ToString() + " and EntityId = " + CurrentEntity.Id.ToString();
                    db.Database.ExecuteSqlCommand(sql);
                    //FilterAndSortDgv();
                    MessageBox.Show("You balanced!  Cleared Transactions have been marked Reconciled.", Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to mark reconciled transactions.  Please try again or contact Customer Care.");
                Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
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

        private void Dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //Don't remove this function, it prevents the grid for erroring out when you exit the form.
        }

        private void DisplayTransBackTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (displayTransBackTo.SelectedIndex == -1) return;
            FilterDgv();
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
        }

        private void dgv_MouseClick(object sender, MouseEventArgs e)
        {
            ClearFormFields();
            PopulateForm();
        }

        private void PopulateForm()
        {
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
        }
    }
}
