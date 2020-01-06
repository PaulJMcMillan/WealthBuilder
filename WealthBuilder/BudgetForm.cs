//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Reflection;
using System.Windows.Forms;

namespace WealthBuilder
{
    public partial class BudgetForm : Form
    {
        public BudgetForm()
        {
            InitializeComponent();
            Text = Common.GetFormText(Text);
        }

        private void BudgetForm_Load(object sender, EventArgs e)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            frequenciesTableAdapter.Fill(dataSet.Frequencies);
            dataSet.Budget.Columns["EntityId"].DefaultValue = CurrentEntity.Id;
            budgetBindingSource.Filter = "EntityId = " + CurrentEntity.Id;
            budgetTableAdapter.Fill(dataSet.Budget);
            InsertDateColumns();
            //    row.Cells[10].Value = DateTime.Now;
        }

        private void InsertDateColumns()
        {
            string headerText = "Due Date";
            string dataPropertyName = "DueDate";
            string columnName = "dueDateDataGridViewCalendarColumn";
            int columnPosition = 4;
            DataGridViewHelper.InsertCalendarColumn(dgv, headerText, dataPropertyName, columnName, columnPosition);

            headerText = "Pay Date";
            dataPropertyName = "PayDate";
            columnName = "payDateDataGridViewCalendarColumn";
            columnPosition = 5;
            DataGridViewHelper.InsertCalendarColumn(dgv, headerText, dataPropertyName, columnName, columnPosition);

            headerText = "Start Date";
            dataPropertyName = "StartDate";
            columnName = "startDateDataGridViewCalendarColumn";
            columnPosition = 6;
            DataGridViewHelper.InsertCalendarColumn(dgv, headerText, dataPropertyName, columnName, columnPosition);

            headerText = "End Date";
            dataPropertyName = "EndDate";
            columnName = "endDateDataGridViewCalendarColumn";
            columnPosition = 7;
            DataGridViewHelper.InsertCalendarColumn(dgv, headerText, dataPropertyName, columnName, columnPosition);
        }

        private bool Save()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                Validate();
                budgetBindingSource.EndEdit();
                budgetTableAdapter.Update(dataSet.Budget);
                return true;
            }
            catch (Exception ex)
            {
                Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(WBResource.GenericErrorMessage, WBResource.GenericErrorTitle);
                return false;
            }
        }

        private void BudgetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            if (!Save())
            {
                string msg = "Do you still want to close this form?";
                DialogResult dialogResult = MessageBox.Show(msg, "Invalid Data", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No) e.Cancel = true;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                budgetBindingSource.Sort = "";
                dataSet.Budget.Rows.Add();
                budgetBindingSource.ResetBindings(false);
                int rowCount = dgv.RowCount;
                if (rowCount > 0) dgv.CurrentCell = dgv.Rows[rowCount - 1].Cells[1];
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (!DataGridViewHelper.DeleteRows(dgv)) MessageBox.Show(WBResource.GenericErrorMessage);
        }
    }
}
