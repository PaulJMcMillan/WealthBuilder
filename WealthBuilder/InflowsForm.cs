//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WealthBuilder.Helpers;
using WealthBuilder.Repositories;

namespace WealthBuilder
{
    public partial class InflowsForm : Form
    {
        //Inflow dgv columns
        //Id
        //Name
        //Amount
        //InflowDate
        //Frequency
        //Notes

        private List<Frequency> frequencies;
        public InflowsForm()
        {
            InitializeComponent();
        }

        private void InflowsForm_Load(object sender, EventArgs e)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            Text = Common.GetFormText(Text);
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Add("Id","");
            dgv.Columns["Id"].Visible = false;
            dgv.Columns.Add("Name", "Name");
            dgv.Columns.Add("Amount", "Amount");
            dgv.Columns.Add("InflowDate", "Date");
            dgv.Columns.Add("Frequency", "Frequency");
            dgv.Columns.Add("Notes", "Notes");
           
            using (var db = new WBEntities())
            {
                frequencies = db.Frequencies.OrderBy(x => x.Name).ToList();
                FrequencyComboBox.DisplayMember = "Name";
                FrequencyComboBox.ValueMember = "Id";
                FrequencyComboBox.DataSource = frequencies;
            }
           
           
            SortDgv();
            PopulateForm();
        }

        private void SortDgv()
        {
            using (var db = new WBEntities())
            {
                var inflows = db.Inflows.Where(x => x.EntityId == CurrentEntity.Id).OrderBy(x => x.InflowDate);

                foreach (var inflow in inflows)
                {
                    string frequency = GetFrequencyName(inflow.FrequencyId);
                    dgv.Rows.Add(inflow.Id, inflow.Name, inflow.Amount.ToString("C"),
                        inflow.InflowDate.ToShortDateString(), frequency, inflow.Notes);

                }
            }
        }


        private void PopulateForm()
        {
            if (dgv.CurrentRow == null) return;
            var currentRow = dgv.CurrentRow;
            NameTextBox.Text = (string)currentRow.Cells["Name"].Value;
            dateTimePicker.Value = DateTime.Parse((string)currentRow.Cells["InflowDate"].Value);
            AmountTextBox.Text = (string)currentRow.Cells["Amount"].Value;
            FrequencyComboBox.Text = (string)currentRow.Cells["Frequency"].Value;
            NotesRichTextBox.Text = (string)currentRow.Cells["Notes"].Value;
        }

        

        private string GetFrequencyName(int frequencyId)
        {
            foreach(var frequency in frequencies)
            {
                if (frequency.Id == frequencyId) return frequency.Name;
            }

            return string.Empty;
        }

        private Inflow Save(int id)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                if (!ValidateData())
                {
                    MessageBox.Show("Invalid data");
                    return null;
                }

                using (var db = new WBEntities())
                {
                    var inflow = db.Inflows.Where(x => x.Id == id).FirstOrDefault();
                    decimal amount = StringHelper.ConvertToDecimalWithEmptyString(AmountTextBox.Text);
                    inflow.InflowDate = dateTimePicker.Value;
                    inflow.Name = NameTextBox.Text;
                    inflow.Amount = amount;
                    inflow.Notes = NotesRichTextBox.Text;
                    inflow.FrequencyId = (int)FrequencyComboBox.SelectedValue;
                    db.SaveChanges();
                    return inflow;
                }
            }
            catch (Exception ex)
            {
                Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(WBResource.GenericErrorMessage, WBResource.GenericErrorTitle);
                return null;
            }
        }

        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            var row = dgv.CurrentRow;
            if (row == null) return;
            var id = (int)row.Cells["Id"].Value;
            Inflow inflow = Save(id);
            if (inflow == null) return;
            
            row.Cells["Name"].Value = inflow.Name;
            row.Cells["Amount"].Value = inflow.Amount;
            row.Cells["InflowDate"].Value = inflow.InflowDate.ToShortDateString();
            row.Cells["Frequency"].Value = new FrequencyRepository().GetName(inflow.FrequencyId);
            row.Cells["Notes"].Value = inflow.Notes;
            MessageBox.Show("Updated");
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            int id = AddNewRecord();
            if (id == -1) return;
            AddNewRowToDgv(id);
            MessageBox.Show("Added");
        }

        private int AddNewRecord()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                if (!ValidateData())
                {
                    MessageBox.Show("Invalid data");
                    return -1;
                }

                using (var db = new WBEntities())
                {
                    decimal amount = StringHelper.ConvertToDecimalWithEmptyString(AmountTextBox.Text);

                    var inflow = new Inflow()
                    {
                        InflowDate = dateTimePicker.Value,
                        Name = NameTextBox.Text,
                        Amount = amount,
                        Notes = NotesRichTextBox.Text,
                        FrequencyId = (int)FrequencyComboBox.SelectedValue,
                        EntityId = CurrentEntity.Id,
                        StartDate = new DateTime(1900, 01, 01),
                        EndDate = new DateTime(1900,1,1)
                    };

                    db.Inflows.Add(inflow);
                    db.SaveChanges();
                    return inflow.Id;

                }

            }
            catch (Exception ex)
            {
                Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(WBResource.GenericErrorMessage, WBResource.GenericErrorTitle);
                return -1;
            }
        }

        private void AddNewRowToDgv(int id)
        {
            string frequencyName = new FrequencyRepository().GetName((int)FrequencyComboBox.SelectedValue);
            dgv.Rows.Add(id, NameTextBox.Text, AmountTextBox.Text, dateTimePicker.Value.ToShortDateString(),frequencyName,NotesRichTextBox.Text);

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if ((int)row.Cells["Id"].Value == id)
                {
                    row.Selected = true;
                    break;
                }

                row.Selected = false;
            }

            dgv.FirstDisplayedScrollingRowIndex = dgv.SelectedRows[0].Index;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var row = dgv.CurrentRow;
            if (row == null) return;
            int rowIndex = row.Index;
            int id = (int)row.Cells["Id"].Value;

            using (var db = new WBEntities())
            {
                var table = db.Inflows;
                var entity = table.Where(x => x.Id == id).FirstOrDefault();
                table.Remove(entity);
                db.SaveChanges();
            }
            
            dgv.Rows.Remove(row);
            ClearFormFields();

            if (rowIndex > -1 && rowIndex < dgv.Rows.Count)
            {
                dgv.Rows[rowIndex].Selected = true;
                dgv.FirstDisplayedScrollingRowIndex = rowIndex;
            }

            MessageBox.Show("Deleted");
        }

        private void ClearFormFields()
        {
            dateTimePicker.Value = DateTime.Today;
            NameTextBox.Text = string.Empty;
            AmountTextBox.Text = string.Empty;
            NotesRichTextBox.Text = string.Empty;
            FrequencyComboBox.SelectedIndex = -1;
        }

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text)) return false;
            if (string.IsNullOrWhiteSpace(AmountTextBox.Text)) return false;
            if (FrequencyComboBox.SelectedIndex == -1) return false;
            return true;
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PopulateForm();
        }
    }
}
