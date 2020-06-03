//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WealthBuilder
{
    public partial class RemindersForm : Form
    {
        private WBEntities db = new WBEntities();
        private BindingSource bs = new BindingSource();

        public RemindersForm()
        {
            InitializeComponent();
        }

        private void RemindersForm_Load(object sender, EventArgs e)
        {
            remindersListBox.DisplayMember = "Description";
            remindersListBox.ValueMember = "Id";
            LoadListBox();           
        }

        private void RemindersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                db.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to close the Reminders window.  Please try again or contact Customer Care.");
                Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (remindersListBox.SelectedIndex == -1) return;
            if (remindersListBox.SelectedItem == null) return;  
            string idString = remindersListBox.SelectedValue.ToString();
            if (string.IsNullOrWhiteSpace(idString)) return;
            int id;
            if (!int.TryParse(idString, out id)) return;
            var reminder = db.Reminders.Where(x => x.Id == id).FirstOrDefault();
            if (reminder == null) return;
            db.Reminders.Remove(reminder);

            try
            {
                db.SaveChanges();   
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while trying to delete this row.");
                return;
            }

            LoadListBox();
        }

        private void LoadListBox()
        {
            var reminders = from reminder in db.Reminders select reminder;
            bs.DataSource = reminders.ToList();
            remindersListBox.DataSource = bs;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
