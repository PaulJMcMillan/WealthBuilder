using System;
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
        WBEntities db = new WBEntities();

        enum SortDirection
        {
            Ascending,
            Descending
        };

        private SortDirection sortDirection;
        private IQueryable<Transaction> transactions;

        public TransactionsForm()
        {
            InitializeComponent();
        }

        private void TransactionsForm_Load(object sender, EventArgs e)
        {
            displayTransBackTo.Text = "1 Month";
            Text = Common.GetFormText(Text);
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            sortDirection = SortDirection.Descending;
            LoadAccountsComboBox();
            SelectDefaultAccount();
            FilterDgv();
            SortDgv();
            ClearFormFields();
            UpdateCurrentBalance();
            Common.ClearSelection(dgv);

        }

        private void SelectDefaultAccount()
        {
            //using (var db = new WBEntities())
            //{
                var r = db.Accounts.Where(x => x.Active == true && x.DefaultAccount == true && x.EntityId == CurrentEntity.Id).FirstOrDefault();
                if (r == null) return;
                accountComboBox.Text = r.Name;
            //}
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
            //using (var db = new WBEntities())
            //{
                var accounts = db.Accounts.Where(x => x.EntityId == CurrentEntity.Id && x.Active == true).OrderBy(x => x.Name).ToList();
                accountComboBox.DisplayMember = "Name";
                accountComboBox.ValueMember = "Id";
                accountComboBox.DataSource = accounts;
            //}
        }

        private void includeReconciledTransactionsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FilterDgv();
            SortDgv();
            UpdateCurrentBalance();
        }

        public void SortDgv()
        {
            if (transactions == null || transactions.Count() == 0) return;

            if (sortDirection == SortDirection.Ascending)
            {
                transactions = transactions.OrderBy(x => x.Date);
            }
            else
            {
                transactions = transactions.OrderByDescending(x => x.Date);
            }

            dgv.DataSource = transactions.ToList();

            ////Gets a fresh data source.
            //if (accountComboBox.SelectedIndex == -1) return;
            //if (!int.TryParse(accountComboBox.SelectedValue.ToString(), out accountId)) return;
            //DateTime displayTransactionsBackTo = DateTime.Now;

            //switch (displayTransBackTo.Text)
            //{
            //    case "1 Month":
            //        displayTransactionsBackTo = DateTime.Now.AddMonths(-1).Date;
            //        break;
            //    case "1 Quarter":
            //        displayTransactionsBackTo = DateTime.Now.AddMonths(-3).Date;
            //        break;
            //    case "6 Months":
            //        displayTransactionsBackTo = DateTime.Now.AddMonths(-6).Date;
            //        break;
            //    case "1 Year":
            //        displayTransactionsBackTo = DateTime.Now.AddYears(-1).Date;
            //        break;
            //    default:
            //        displayTransactionsBackTo = DateTime.Now.AddMonths(-1).Date;
            //        break;
            //}

            //using (var db = new WBEntities())
            //{
            //    var transactions = db.Transactions.Where(x => x.EntityId == CurrentEntity.Id && x.AccountId == accountId &&
            //        x.Date >= displayTransactionsBackTo);

            //    if (excludeReconciledTransactionsCheckBox.Checked)
            //        transactions = transactions.Where(x => x.Reconciled == false);

            //    if (excludeClearedTransactionsCheckBox.Checked)
            //        transactions = transactions.Where(x => x.Cleared == false);

            //if (sortDirection == SortDirection.Ascending)
            //    {
            //        transactions = transactions.OrderBy(x => x.Date);
            //    }
            //    else
            //    {
            //        transactions = transactions.OrderByDescending(x => x.Date);
            //    }
                
            //    dgv.DataSource = transactions.ToList();
            //}
        }

        public void FilterDgv()
        {
            if (accountComboBox.SelectedIndex == -1) return;
            if (!int.TryParse(accountComboBox.SelectedValue.ToString(), out accountId)) return;
            DateTime displayTransactionsBackTo;

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

            //using (var db = new WBEntities())
            //{
                transactions = db.Transactions.Where(x => x.EntityId == CurrentEntity.Id && x.AccountId == accountId &&
                    x.Date >= displayTransactionsBackTo);

                if (excludeReconciledTransactionsCheckBox.Checked)
                    transactions = transactions.Where(x => x.Reconciled == false);

                if (excludeClearedTransactionsCheckBox.Checked)
                    transactions = transactions.Where(x => x.Cleared == false);

                dgv.DataSource = transactions.ToList();
            //}

        }

        private Transaction Save(int transId)
        {
            if (transactions == null || transactions.Count() == 0) return null;

            if (!ValidateData()) 
            {
                MessageBox.Show("Invalid data");
                return null;
            }

            //using (var db = new WBEntities())
            //{
            //var trans = db.Transactions.Where(x => x.Id == transId).FirstOrDefault();
            var trans = transactions.Where(x => x.Id == transId);
            if (trans == null) return null;
            var tran = trans.FirstOrDefault();
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
            db.SaveChanges();
            return tran;
            //}
        }

        private int AddNewRecord()
        {
            if (transactions == null) return -1;

            if (!ValidateData())
            {
                MessageBox.Show("Invalid data");
                return -1;
            }

            //using (var db = new WBEntities())
            //{
            decimal deposit = StringHelper.ConvertToDecimalWithEmptyString(DepositTextBox.Text);
            decimal wd = StringHelper.ConvertToDecimalWithEmptyString(WithdrawalTextBox.Text);

            var tran = new Transaction()
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

            //db.Transactions.Add(tran);
            db.Transactions.Add(tran);
            db.SaveChanges();
            return tran.Id;
                   
            //}

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
                MessageBox.Show("Invalid");
                return false;
            }

            return true;
        }

        private void accountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterDgv();
            SortDgv();
            UpdateCurrentBalance();
            availableBankBalanceTextBox.Text = string.Empty;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (!ValidateData()) return;
            int transId = AddNewRecord();
            if (transId == -1) return;
            AddNewRowToDgv(transId);
            UpdateCurrentBalance();
            ClearFormFields();
            Common.ClearSelection(dgv);
            MessageBox.Show("Added");
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
                else
                {
                    row.Selected = false;
                }
            }

            dgv.FirstDisplayedScrollingRowIndex = dgv.SelectedRows[0].Index;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!ValidateData()) return;
            var row = dgv.CurrentRow;
            if (row == null) return;
            var transId = (int)row.Cells["Id"].Value;
            Transaction transaction = Save(transId);
            if (transaction == null) return;
           
            row.Cells["Date"].Value = transaction.Date;
            row.Cells["Description"].Value = transaction.Description;
            row.Cells["deposit"].Value = transaction.Deposit;
            row.Cells["Withdrawal"].Value = transaction.Withdrawal;
            row.Cells["Cleared"].Value = transaction.Cleared;
            row.Cells["Reconciled"].Value = transaction.Reconciled;
            row.Cells["CheckNumber"].Value = transaction.CheckNumber;
            row.Cells["Notes"].Value = transaction.Notes;

            UpdateCurrentBalance();
            ClearFormFields();
            Common.ClearSelection(dgv);
            MessageBox.Show("Updated");
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var row = dgv.CurrentRow;
            if (row == null) return;
            int transId = (int)row.Cells["Id"].Value;
            int rowIndex = row.Index;

            //using (var db = new WBEntities())
            //{
            //    //var trans = db.Transactions.Where(x => x.Id == transId).FirstOrDefault();
            var trans = transactions.Where(x => x.Id == transId);
            if (trans == null) return;
            var tran = trans.FirstOrDefault();
            db.Transactions.Remove(tran);
            db.SaveChanges();
            //}

            FilterDgv();
            SortDgv();
            
            if(rowIndex > -1 && rowIndex < dgv.Rows.Count)
            {
                dgv.Rows[rowIndex].Selected = true;
                dgv.FirstDisplayedScrollingRowIndex = rowIndex;
            }

            
            UpdateCurrentBalance();
            ClearFormFields();
            Common.ClearSelection(dgv);
            MessageBox.Show("Deleted");
        }

        private void includeClearedTransactions_CheckedChanged(object sender, EventArgs e)
        {
            FilterDgv();
            SortDgv();
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
            accountBalance = Cash.GetBalanceForOneAccount(accountId, CurrentEntity.Id);
            reconciliationReport.Append("Current Balance: " + accountBalance.ToString("C") + Environment.NewLine);
            reconciliationReport.Append("Available Bank Balance: " + availableBankBalance.ToString("C") + Environment.NewLine+Environment.NewLine);
            unclearedAmount = Reconciliation.CalculateUnclearedTransactions(accountId, reconciliationReport);
            decimal adjustedBankBalance = availableBankBalance + unclearedAmount;
            reconciliationReport.Append("Adjusted Available Bank Balance: " + adjustedBankBalance.ToString("C")+" (Available Bank Balance minus the sum of Uncleared Amounts)"+ 
                                 Environment.NewLine + Environment.NewLine);
            decimal difference = adjustedBankBalance - accountBalance;
            reconciliationReport.Append("Difference: " + difference.ToString("C")+" (Adjusted Available Bank Balance minus Current Balance)"+ Environment.NewLine + Environment.NewLine);
            reconciliationReport.Append("* If the Difference is positive, it means the bank thinks you have more money than your checkbook says you do." + Environment.NewLine);
            reconciliationReport.Append("* Vice versa, if the Difference is negative, it means the bank thinks you have less money than your checkbook says you do." + Environment.NewLine);
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
            //using (var db = new WBEntities())
            //{
            string sql = "Update Transactions Set Reconciled = 1 Where reconciled = 0 And Cleared = 1 And AccountId = " + 
                accountId.ToString() + " and EntityId = " + CurrentEntity.Id.ToString();
            db.Database.ExecuteSqlCommand(sql);
            MessageBox.Show("You balanced!  Cleared Transactions have been marked Reconciled.", Text);
            //}
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
            SortDgv();
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

                SortDgv();
            }
        }

        private void TransactionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            db.Dispose();
        }
    }
}
