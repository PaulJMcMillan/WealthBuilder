using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WealthBuilder
{
    public partial class AssetsForm : Form
    {
        public AssetsForm()
        {
            InitializeComponent();
        }

        private void AssetsForm_Load(object sender, EventArgs e)
        {
            assetsTableAdapter.Fill(dataSet.Assets);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                dataSet.Assets.Rows.Add();
                assetsBindingSource.ResetBindings(false);
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
                assetsBindingSource.EndEdit();
                assetsTableAdapter.Update(dataSet.Assets);
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
                MessageBox.Show("This Asset has one or more Transactions depending on it.  You must delete them first.", Text);
                return;
            }

            var row = dgv.CurrentRow;
            int id = (int)row.Cells["Id"].Value;

            using (var db = new WBEntities())
            {
                var table = db.Assets;
                var entity = table.Where(x => x.id == id).FirstOrDefault();
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
                    var rs = db.Transactions.Where(x => x.AssetId == id);
                    return rs.Count() > 0;
                }
            }

            return false;
        }

        private void AssetsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
        }
    }
}
