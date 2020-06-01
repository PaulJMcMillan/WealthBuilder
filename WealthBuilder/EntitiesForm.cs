using System;
using System.Windows.Forms;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace WealthBuilder
{
    public partial class EntitiesForm : Form
    {
        private bool firstLoaded = true;

        public EntitiesForm()
        {
            InitializeComponent();
        }

        private void EntitiesForm_Load(object sender, EventArgs e)
        {
            entitiesTableAdapter.Fill(this.dataSet.Entities);
            firstLoaded = false;
        }

        private void EntitiesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!DefaultEntityCountIsCorrect())
            {
                e.Cancel = true;
                return;
            }

            Save();
            if(Code.Form.FormIsOpen("MainForm")) UpdateEntitiesComboBox();
        }

        private bool Save()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            //if (App.Mode=="PennyPincher" && entitiesBindingSource.Count > 1)
            //{
            //    MessageBox.Show("Only 1 entity is allowed in Penny Pincher.  If you need more, please upgrade to Wealth Builder.", "Penny Pincher");
            //    return false;
            //}

            try
            {
                Validate();
                entitiesBindingSource.EndEdit();
                entitiesTableAdapter.Update(dataSet.Entities);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool DefaultEntityCountIsCorrect()
        {
            int defaultEntityCount = 0;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells["DefaultEntity"].Value == DBNull.Value || row.Cells["DefaultEntity"].Value == null || (bool)row.Cells["DefaultEntity"].Value == false) continue;
                defaultEntityCount++;
            }

            if (defaultEntityCount == 1) return true;

            if (defaultEntityCount == 0)
            {
                MessageBox.Show("You must specify a Default Entity.", Text);
                return false;
            }
                      
            MessageBox.Show("You can only specify one Default Entity.", Text);
            return false;
        }

        private void entitiesDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (firstLoaded == true) return;
            Save();
            if (Code.Form.FormIsOpen("MainForm")) UpdateEntitiesComboBox();
        }

        private static void UpdateEntitiesComboBox()
        {
            MainForm mainForm = (MainForm)Application.OpenForms["MainForm"];
            ComboBox comboBox = (ComboBox)mainForm.Controls["EntityComboBox"];
            comboBox.DataSource = null;
            comboBox.Items.Clear();
            List<Entity> entities = Code.Entities.GetActive();
            if (entities.Count == 0) return;

            comboBox.DisplayMember = "Name";
            comboBox.ValueMember = "Id";
            comboBox.DataSource = entities;

            using (var db = new WBEntities())
            {
                var r = db.Entities.Where(x => x.Name == CurrentEntity.Name && x.Active == true).FirstOrDefault();
                if (r == null) return;
                comboBox.Text = r.Name;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            dataSet.Entities.Rows.Add();
            entitiesBindingSource.ResetBindings(false);
            int rowCount = dgv.RowCount;
            if (rowCount > 0) dgv.CurrentCell = dgv.Rows[rowCount - 1].Cells[1];
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!DefaultEntityCountIsCorrect()) return;
            Save();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (UserIsTryingToDeleteTheCurrentEntity()) return;

            if (EntitiesHaveAccounts())
            {
                MessageBox.Show("One or more Entities have Account(s) associated with them.  You must delete the Account(s) first.", Text);
                return;
            }

            var row = dgv.CurrentRow;
            int id = (int)row.Cells["Id"].Value;

            using (var db = new WBEntities())
            {
                var table = db.Entities;
                var entity = table.Where(x => x.Id == id).FirstOrDefault();
                table.Remove(entity);
                db.SaveChanges();
            }
        }

        private bool UserIsTryingToDeleteTheCurrentEntity()
        {
            //Check Entity in CurrentRow
            int currentRowIndex = dgv.CurrentCell.RowIndex;

            if (dgv.Rows[currentRowIndex].Cells[0].Value != null)
            {
                int currentRowEntityId = (int)dgv.Rows[currentRowIndex].Cells[0].Value;
                if (ThisEntityIsTheCurrentEntity(currentRowEntityId)) return true;
            }

            //Check Entities in any selected rows.
            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                int entityId = (int)row.Cells[0].Value;
                if (ThisEntityIsTheCurrentEntity(entityId)) return true;
            }

            return false;
        }

        private bool EntitiesHaveAccounts()
        {
            //Check Entity in CurrentRow
            int currentRowIndex = dgv.CurrentCell.RowIndex;

            if (dgv.Rows[currentRowIndex].Cells[0].Value !=null)
            {
                int currentRowEntityId = (int)dgv.Rows[currentRowIndex].Cells[0].Value;
                if (AccountsExistForThisEntity(currentRowEntityId)) return true;
            }

            //Check Entities in any selected rows.
            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                int entityId = (int)row.Cells[0].Value;
                if (AccountsExistForThisEntity(entityId)) return true;
            }

            return false;
        }

        private bool ThisEntityIsTheCurrentEntity(int currentRowEntityId)
        {
            if (currentRowEntityId == CurrentEntity.Id)
            {
                MessageBox.Show("You can't delete the Current Entity.", Text);
                return true;
            }

            return false;
        }

        private bool AccountsExistForThisEntity(int entityId)
        {
            using (var db = new WBEntities())
            {
                var rs = db.Accounts.Where(x => x.EntityId == entityId);
                return rs.Count() > 0;
            }
        }
    }
}
