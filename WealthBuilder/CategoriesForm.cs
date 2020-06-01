using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WealthBuilder
{
    public partial class CategoriesForm : Form
    {
        public CategoriesForm()
        {
            InitializeComponent();
        }

        private void CategoriesForm_Load(object sender, EventArgs e)
        {
            categoriesTableAdapter.Fill(dataSet.Categories);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                dataSet.Categories.Rows.Add();
                categoriesBindingSource.ResetBindings(false);
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
                categoriesBindingSource.EndEdit();
                categoriesTableAdapter.Update(dataSet.Categories);
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
                MessageBox.Show("This Category has one or more Transactions depending on it.  You must delete them first.", Text);
                return;
            }

            var row = dgv.CurrentRow;
            int id = (int)row.Cells["Id"].Value;

            using (var db = new WBEntities())
            {
                var table = db.Categories;
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
                    var rs = db.Transactions.Where(x => x.CategoryId == id);
                    return rs.Count() > 0;
                }
            }

            return false;
        }

        private void CategoriesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
        }
    }
}
