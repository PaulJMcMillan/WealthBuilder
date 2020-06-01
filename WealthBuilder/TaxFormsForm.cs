using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WealthBuilder
{
    public partial class TaxFormsForm : Form
    {
        public TaxFormsForm()
        {
            InitializeComponent();
        }

        private void TaxFormsForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet.TaxForms' table. You can move, or remove it, as needed.
            this.taxFormsTableAdapter.Fill(this.dataSet.TaxForms);

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                dataSet.TaxForms.Rows.Add();
                taxFormsBindingSource.ResetBindings(false);
                int rowCount = dgv.RowCount;
                if (rowCount > 0) dgv.CurrentCell = dgv.Rows[rowCount - 1].Cells[1];
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private bool Save()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                Validate();
                taxFormsBindingSource.EndEdit();
                taxFormsTableAdapter.Update(dataSet.TaxForms);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (DependanciesExist())
            {
                MessageBox.Show("This Tax Form has one or more Transactions depending on it.  You must delete them first.", Text);
                return;
            }

            var row = dgv.CurrentRow;
            int id = (int)row.Cells["Id"].Value;

            using (var db = new WBEntities())
            {
                var table = db.TaxForms;
                var entity = table.Where(x => x.Id == id).FirstOrDefault();
                table.Remove(entity);
                db.SaveChanges();
            }
        }

        private bool DependanciesExist()
        {
            int currentRowIndex = dgv.CurrentCell.RowIndex;
            DataGridViewRow currentRow = dgv.Rows[currentRowIndex];

            if (currentRow.Cells[0].Value != null && currentRow.Cells["idDataGridViewTextBoxColumn"].Value != DBNull.Value && currentRow.Cells["idDataGridViewTextBoxColumn"].Value != null)
            {
                int id = (int)currentRow.Cells[0].Value;

                using (var db = new WBEntities())
                {
                    var rs = db.Transactions.Where(x => x.TaxFormId == id);
                    return rs.Count() > 0;
                }
            }

            return false;
        }

        private void TaxFormsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
        }
    }
}
