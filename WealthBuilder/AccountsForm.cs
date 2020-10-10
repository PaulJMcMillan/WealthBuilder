using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WealthBuilder
{
    public partial class AccountsForm : Form
    {
        public AccountsForm()
        {
            InitializeComponent();
        }

        private void AccountsForm_Load(object sender, EventArgs e)
        {
           

            Text = Common.GetFormText(Text);
            dgv.AutoGenerateColumns = true;
            dataSet.Accounts.Columns["EntityId"].DefaultValue = CurrentEntity.Id;
            string filter = "EntityId = " + CurrentEntity.Id;
            accountsTableAdapter.Fill(dataSet.Accounts);
            DataView dv = dataSet.Tables["Accounts"].DefaultView;
            dv.RowFilter = filter;
            dgv.DataSource = dv;
        }

        private void AccountsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!DefaultAccountCountIsCorrect())
            {

                MessageBox.Show("This form cannot close due to invalid data.  Please fix it and try again.");
                e.Cancel = true;
                return;
            }

            Save();
        }

        private bool DefaultAccountCountIsCorrect()
        {
            if (dgv.Rows.Count == 0) return true;
            int defaultAccountCount = 0;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                object defaultAccount = row.Cells["DefaultAccount"].Value;
                if(defaultAccount != DBNull.Value && DefaultAccount != null && Convert.ToBoolean(defaultAccount)) defaultAccountCount++;
            }

            if (defaultAccountCount == 1) return true;

            if (defaultAccountCount == 0)
            {
                MessageBox.Show("You must specify a Default Account.", "Invalid Default Account");
                return false;
            }

            MessageBox.Show("You can only specify one Default Account.", "Invalid Default Account");
            return false;
        }

        private bool Save()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                Validate();
                accountsBindingSource.EndEdit();
                accountsTableAdapter.Update(dataSet.Accounts);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            dataSet.Accounts.Rows.Add();
            accountsBindingSource.ResetBindings(false);
            int rowCount = dgv.RowCount;
            if (rowCount > 0) dgv.CurrentCell = dgv.Rows[rowCount - 1].Cells[1];
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!DefaultAccountCountIsCorrect()) return;
            Save();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (AccountsHaveTransactions())
            {
                MessageBox.Show("One or more Accounts have Transactions associated with them.  You must delete the Transactions first.", Text);
                return;
            }

            var row = dgv.CurrentRow;
            int id = (int)row.Cells["Id"].Value;

            using (var db = new WBEntities())
            {
                var table = db.Accounts;
                var entity = table.Where(x => x.Id == id).FirstOrDefault();
                table.Remove(entity);
                db.SaveChanges();
            }
        }

        private bool AccountsHaveTransactions()
        {
            //Check Account in CurrentRow
            int currentRowIndex = dgv.CurrentCell.RowIndex;
            DataGridViewRow currentRow = dgv.Rows[currentRowIndex];

            if (currentRow.Cells[0].Value != null && currentRow.Cells["entityIdDataGridViewTextBoxColumn"].Value != DBNull.Value && currentRow.Cells["entityIdDataGridViewTextBoxColumn"].Value != null)
            {
                int currentRowAccountId = (int)currentRow.Cells[0].Value;
                int currentRowEntityId = (int)currentRow.Cells["entityIdDataGridViewTextBoxColumn"].Value;
                if (TransactionsExistForThisAccount(currentRowAccountId)) return true;
            }

            //Check Entities in any selected rows.
            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                int accountId = (int)row.Cells[0].Value;
                int entityId = (int)row.Cells["entityIdDataGridViewTextBoxColumn"].Value;

                if (TransactionsExistForThisAccount(accountId)) return true;
            }

            return false;
        }

        private bool TransactionsExistForThisAccount(int accountId)
        {
            using (var db = new WBEntities())
            {
                var rs = db.Transactions.Where(x => x.AccountId == accountId);
                return rs.Count() > 0;
            }
        }
    }
}
