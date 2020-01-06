//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Data;

namespace WealthBuilder
{
    public partial class InflowsForm : Form
    {
        public InflowsForm()
        {
            InitializeComponent();
        }

        private void InflowsForm_Load(object sender, EventArgs e)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            this.frequenciesTableAdapter.Fill(this.dataSet.Frequencies);
            this.inflowsTableAdapter.Fill(this.dataSet.Inflows);
            Text = Common.GetFormText(Text);
            //dgv.AutoGenerateColumns = true;
            dataSet.Inflows.Columns["EntityId"].DefaultValue = CurrentEntity.Id;
            string filter = "EntityId = " + CurrentEntity.Id;
            inflowsTableAdapter.Fill(dataSet.Inflows);
            DataView dv = dataSet.Tables["Inflows"].DefaultView;
            dv.Sort = "InflowDate";
            dv.RowFilter = filter;
            dgv.DataSource = dv;
            InsertDateColumns();
        }

        private void InsertDateColumns()
        {
            string headerText = "Date";
            string dataPropertyName = "InflowDate";
            string columnName = "inflowDateDataGridViewCalendarColumn";
            int columnPosition = 1;
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
                inflowsBindingSource.EndEdit();
                inflowsTableAdapter.Update(dataSet.Inflows);
                return true;
            }
            catch (Exception ex)
            {
                Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(WBResource.GenericErrorMessage, WBResource.GenericErrorTitle);
                return false;
            }
        }

        private void InflowsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            if (!Save())
            {
                string msg = "Do you still want to close this form?";
                DialogResult dialogResult = MessageBox.Show(msg, "Invalid Data", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No) e.Cancel = true;
            }
        }

        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            Save();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            dataSet.Inflows.Rows.Add();
            inflowsBindingSource.ResetBindings(false);
            int rowCount = dgv.RowCount;
            if (rowCount > 0) dgv.CurrentCell = dgv.Rows[rowCount - 1].Cells[1];
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (!DataGridViewHelper.DeleteRows(dgv)) MessageBox.Show(WBResource.GenericErrorMessage);
        }
    }
}
