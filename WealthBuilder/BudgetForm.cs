//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WealthBuilder.Helpers;
using WealthBuilder.Repositories;

namespace WealthBuilder
{
    public partial class BudgetForm : Form
    {
        private List<Frequency> frequencies;
        public BudgetForm()
        {
            InitializeComponent();
            Text = Common.GetFormText(Text);
        }

        private void BudgetForm_Load(object sender, EventArgs e)
        {
            Text = Common.GetFormText(Text);
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Add("Id", "");
            dgv.Columns["Id"].Visible = false;
            dgv.Columns.Add("Name", "Name");
            dgv.Columns["Name"].Width = 200;
            dgv.Columns.Add("DueDate", "Due Date");
            dgv.Columns["DueDate"].ValueType = typeof(DateTime);
            dgv.Columns.Add("PayDate", "Pay Date");
            dgv.Columns["PayDate"].ValueType = typeof(DateTime);
            dgv.Columns.Add("Amount", "Amount");
            dgv.Columns["Amount"].ValueType = typeof(decimal);
            dgv.Columns.Add("Frequency", "Frequency");
            dgv.Columns.Add("Notes", "Notes");
            dgv.Columns["Notes"].Width = 200;

            using (var db = new WealthBuilderEntities1())
            {
                frequencies = db.Frequencies.OrderBy(x => x.Name).ToList();
                FrequencyComboBox.DisplayMember = "Name";
                FrequencyComboBox.ValueMember = "Id";
                FrequencyComboBox.DataSource = frequencies;
            }

            SortDgv();
            
            
        }

        private void PopulateForm()
        {
            if (dgv.CurrentRow == null) return;
            var currentRow = dgv.CurrentRow;
            nameTextBox.Text = (string)currentRow.Cells["Name"].Value;
            dueDateTimePicker.Value = (DateTime)currentRow.Cells["DueDate"].Value;
            payDateTimePicker.Value = (DateTime)currentRow.Cells["PayDate"].Value;
            amountTextBox.Text = ((decimal)currentRow.Cells["Amount"].Value).ToString("C");
            FrequencyComboBox.Text = (string)currentRow.Cells["Frequency"].Value;
            NotesRichTextBox.Text = (string)currentRow.Cells["Notes"].Value;
        }

        private void SortDgv()
        {
            dgv.Rows.Clear();

            using (var db = new WealthBuilderEntities1())
            {
                var budgets = db.Budgets.Where(x => x.EntityId == CurrentEntity.Id).OrderBy(x => x.PayDate);

                foreach (var budget in budgets)
                {
                    string frequency = new FrequencyRepository().GetName(budget.FrequencyId);
                    dgv.Rows.Add(budget.Id, budget.Name, budget.DueDate, budget.PayDate, budget.Amount, frequency, budget.Notes);
                }
            }
        }

        private Budget Save(int id)
        {
            if (!ValidateData()) return null;

            using (var db = new WealthBuilderEntities1())
            {
                var budget = db.Budgets.Where(x => x.Id == id).FirstOrDefault();
                budget.Name = nameTextBox.Text;
                budget.DueDate = dueDateTimePicker.Value;
                budget.PayDate = payDateTimePicker.Value;
                budget.Amount = StringHelper.ConvertToDecimalWithEmptyString(amountTextBox.Text);
                budget.FrequencyId = (int)FrequencyComboBox.SelectedValue;
                budget.Notes = NotesRichTextBox.Text;
                db.SaveChanges();
                return budget;
            }
        }


        private void addButton_Click(object sender, EventArgs e)
        {
            int id = AddNewRecord();
            if (id == -1) return;
            AddNewRowToDgv(id);
            ClearFormFields();
            Common.ClearSelection(dgv);
            MessageBox.Show("Added");
        }

        private void AddNewRowToDgv(int id)
        {
            string frequencyName = new FrequencyRepository().GetName((int)FrequencyComboBox.SelectedValue);
            decimal amount = StringHelper.ConvertToDecimalWithEmptyString(amountTextBox.Text);
            dgv.Rows.Add(id, nameTextBox.Text, dueDateTimePicker.Value, payDateTimePicker.Value, amount, frequencyName, NotesRichTextBox.Text);

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
        private int AddNewRecord()
        {
            if (!ValidateData()) return -1;

            using (var db = new WealthBuilderEntities1())
            {
                decimal amount = StringHelper.ConvertToDecimalWithEmptyString(amountTextBox.Text);

                var budget = new Budget()
                {
                    Name=nameTextBox.Text,
                    DueDate = dueDateTimePicker.Value,
                    PayDate = payDateTimePicker.Value,
                    Amount  = amount,
                    FrequencyId = (int)FrequencyComboBox.SelectedValue,
                    Notes = NotesRichTextBox.Text,
                    EntityId = CurrentEntity.Id,
                    StartDate = new DateTime(1900, 01, 01),
                    EndDate = new DateTime(1900, 1, 1)
                };

                db.Budgets.Add(budget);
                db.SaveChanges();
                return budget.Id;

            }
        }

        private bool ValidateData()
        {
            decimal amount = 0;
            bool validData = true;

            if (string.IsNullOrWhiteSpace(nameTextBox.Text))  validData = false;

            if (!string.IsNullOrWhiteSpace(amountTextBox.Text))
            {
                if (!decimal.TryParse(StringHelper.StripDollarSignAndCommas(amountTextBox.Text)
                    , out amount)) validData= false;
            }

            if (FrequencyComboBox.SelectedIndex == -1) validData = false;
            if (amount==0) validData= false;

            if (!validData)
            {
                MessageBox.Show("Invalid data");
            }

            return validData;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var row = dgv.CurrentRow;
            if (row == null) return;
            var id = (int)row.Cells["Id"].Value;
            Budget budget = Save(id);
            if (budget == null) return;
            row.Cells["Name"].Value = budget.Name;
            row.Cells["DueDate"].Value = budget.DueDate;
            row.Cells["PayDate"].Value = budget.PayDate;
            row.Cells["Amount"].Value = budget.Amount;
            row.Cells["Frequency"].Value = new FrequencyRepository().GetName(budget.FrequencyId);
            row.Cells["Notes"].Value = budget.Notes;
            ClearFormFields();
            
            Common.ClearSelection(dgv);
            MessageBox.Show("Updated");
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var row = dgv.CurrentRow;
            if (row == null) return;
            int rowIndex = row.Index;
            int id = (int)row.Cells["Id"].Value;

            using (var db = new WealthBuilderEntities1())
            {
                var entity = db.Budgets.Where(x => x.Id == id).FirstOrDefault();
                if (entity == null) return;
                db.Budgets.Remove(entity);
                db.SaveChanges();
            }

            dgv.Rows.Remove(row);

            for(int i=0;i<dgv.Rows.Count;i++)
            {
                if (i == rowIndex)
                {
                    dgv.Rows[rowIndex].Selected = true;
                    dgv.FirstDisplayedScrollingRowIndex = rowIndex;
                }
                else
                {
                    dgv.Rows[i].Selected = false;
                }
            }

            ClearFormFields();
            
            Common.ClearSelection(dgv);
            MessageBox.Show("Deleted");
        }

       

        private void ClearFormFields()
        {
            dueDateTimePicker.Value = DateTime.Today;
            payDateTimePicker.Value = DateTime.Today;
            nameTextBox.Text = string.Empty;
            amountTextBox.Text = string.Empty;
            NotesRichTextBox.Text = string.Empty;
            FrequencyComboBox.SelectedIndex = -1;
        }

       
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PopulateForm();
        }

        private void BudgetForm_Shown(object sender, EventArgs e)
        {
            Common.ClearSelection(dgv);
        }
    }
}
