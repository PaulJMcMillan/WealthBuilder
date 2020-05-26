using System;
using System.Diagnostics;
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
            FilterAndSortDgv();
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
            FilterAndSortDgv();
            UpdateCurrentBalance();
        }

        public void FilterAndSortDgv()
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
                var rs = db.Transactions.Where(x => x.EntityId == CurrentEntity.Id && x.AccountId == accountId &&
                    x.Date >= displayTransactionsBackTo && x.Reconciled== includeReconciledTransactionsCheckBox.Checked && 
                    x.Cleared==includeClearedTransactions.Checked).OrderByDescending(x=>x.Date).ToList();
                Debug.Print(db.Database.Connection.ConnectionString);
                dgv.DataSource = rs;
            }

        }

        private void Save(int transId)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                if (!ValidateData()) 
                {
                    MessageBox.Show("Invalid data");
                    return;
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
                }

                return;
            }
            catch (Exception ex)
            {
                Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(WBResource.GenericErrorMessage, WBResource.GenericErrorTitle);
                return ;
            }
        }

        private void AddNew()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                if (!ValidateData())
                {
                    MessageBox.Show("Invalid data");
                    return;
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
                   
                }

                return;
            }
            catch (Exception ex)
            {
                Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(WBResource.GenericErrorMessage, WBResource.GenericErrorTitle);
                return ;
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
            FilterAndSortDgv();
            UpdateCurrentBalance();
            availableBankBalanceTextBox.Text = string.Empty;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddNew();
            FilterAndSortDgv();
            UpdateCurrentBalance();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var row = dgv.CurrentRow;
            var transId = (int)row.Cells["Id"].Value;
            Save(transId);
            FilterAndSortDgv();
            UpdateCurrentBalance();
            return;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var row = dgv.CurrentRow;
            int transId = (int)row.Cells["Id"].Value;

            using (var db = new WBEntities())
            {
                var trans = db.Transactions.Where(x => x.Id == transId).FirstOrDefault();
                db.Transactions.Remove(trans);
                db.SaveChanges();
            }

            ClearFormFields();
            FilterAndSortDgv();
            UpdateCurrentBalance();
            return;
        }

        private void includeClearedTransactions_CheckedChanged(object sender, EventArgs e)
        {
            FilterAndSortDgv();
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
                    FilterAndSortDgv();
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
            FilterAndSortDgv();
        }

        private void Dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearFormFields();
            var currentRow = dgv.CurrentRow;
            dateTimePicker.Value = (DateTime)currentRow.Cells["Date"].Value;
            DescriptionTextBox.Text = (string)currentRow.Cells["Description"].Value;
            decimal deposit = (decimal)currentRow.Cells["Deposit"].Value;
            DepositTextBox.Text = deposit==0 ? string.Empty : deposit.ToString("C");
            decimal wd = (decimal)currentRow.Cells["Withdrawal"].Value;
            WithdrawalTextBox.Text = wd == 0 ? string.Empty : wd.ToString("C");
            ClearedCheckBox.Checked = (bool)currentRow.Cells["Cleared"].Value;
            ReconciledCheckBox.Checked = (bool)currentRow.Cells["Reconciled"].Value;
            NotesRichTextBox.Text = (string)currentRow.Cells["Notes"].Value;
            CheckNumberTextBox.Text = (string)currentRow.Cells["CheckNumber"].Value;
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
    }
}
