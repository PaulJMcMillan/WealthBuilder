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
    public partial class _1099ContractorsForm : Form
    {
        public _1099ContractorsForm()
        {
            InitializeComponent();
        }

        private void _1099ContractorsForm_Load(object sender, EventArgs e)
        {
            _1099ContractorsTableAdapter.Fill(dataSet._1099Contractors);

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                dataSet._1099Contractors.Rows.Add();
                contractorsBindingSource.ResetBindings(false);
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
                contractorsBindingSource.EndEdit();
                _1099ContractorsTableAdapter.Update(dataSet._1099Contractors);
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
                MessageBox.Show("This 1099 Contractor has one or more Transactions depending on it.  You must delete them first.", Text);
                return;
            }

            if (!DataGridViewHelper.DeleteRows(dgv)) MessageBox.Show(WBResource.GenericErrorMessage);
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
                    var rs = db.Transactions.Where(x => x.ContractorId == id);
                    return rs.Count() > 0;
                }
            }

            return false;
        }

        private void _1099ContractorsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
        }
    }
}
