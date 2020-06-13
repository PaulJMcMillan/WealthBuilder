//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WealthBuilder
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void inflowsButton_Click(object sender, EventArgs e)
        {
            if (!CurrentEntityIsSet()) return;
            Code.Form.Open("InflowsForm");
            //Code.Form.Open("Form1");
        }

        private void budgetButton_Click(object sender, EventArgs e)
        {
            if (!CurrentEntityIsSet()) return;
            Code.Form.Open("BudgetForm");
        }

        private void cashFlowChartButton_Click(object sender, EventArgs e)
        {
            if (!CurrentEntityIsSet()) return;
            Code.Form.Open("CashFlowChartCriteriaForm");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = App.Title;
            PopulateEntityComboBox();
            Width = 413;
            Height = 450;
            entityComboBox.TabIndex = 0;
            transactionsButton.Location = new Point(Constants.x1, Constants.y1);
            transactionsButton.TabIndex = 1;
            accountsButton.Location = new Point(Constants.x2, Constants.y1);
            accountsButton.TabIndex = 2;
            cashFlowChartButton.Location = new Point(Constants.x3, Constants.y1);
            cashFlowChartButton.TabIndex = 3;
            whenCanIAffordButton.Location = new Point(Constants.x1, Constants.y2);
            whenCanIAffordButton.TabIndex = 4;
            inflowsButton.Location = new Point(Constants.x2, Constants.y2);
            inflowsButton.TabIndex = 5;
            budgetButton.Location = new Point(Constants.x3, Constants.y2);
            budgetButton.TabIndex = 6;
            projectCashBalancesButton.Location = new Point(Constants.x1, Constants.y3);
            projectCashBalancesButton.TabIndex = 7;
            toolsButton.Location = new Point(Constants.x2, Constants.y3);
            toolsButton.TabIndex = 8;
            helpButton.Location = new Point(Constants.x3, Constants.y3);
            helpButton.TabIndex = 9;
            aboutButton.Location = new Point(Constants.x1, Constants.y4);
            aboutButton.TabIndex = 10;
            entitiesButton.Location = new Point(Constants.x2, Constants.y4);
            entitiesButton.TabIndex = 11;
            taxFormsButton.Location = new Point(Constants.x3, Constants.y4);
            taxFormsButton.TabIndex = 12;
            taxCategoriesButton.Location = new Point(Constants.x1, Constants.y5);
            taxCategoriesButton.TabIndex = 13;
            categoriesButton.Location = new Point(Constants.x2, Constants.y5);
            categoriesButton.TabIndex = 14;
            assetsButton.Location = new Point(Constants.x3, Constants.y5);
            assetsButton.TabIndex = 15;
            reportsButton.Location = new Point(Constants.x1, Constants.y6);
            reportsButton.TabIndex = 16;
            //ten99ContractorsButton.Location = new Point(Constants.x2, Constants.y6);
            //ten99ContractorsButton.TabIndex = 17;
            quickCashAppTransButton.Location = new Point(Constants.x2, Constants.y6);
            quickCashAppTransButton.TabIndex = 17;
            backupButton.Location = new Point(Constants.x3, Constants.y6);
            backupButton.TabIndex = 18;
            SelectDefaultEntity();
            SetCurrentEntityData();
            
        }

        private void PopulateEntityComboBox()
        {
            using (var db = new WBEntities())
            {
                var entities = db.Entities.Where(x => x.Active == true).ToList();

                //foreach(var entity in entities)
                //{
                this.entityComboBox.DisplayMember = "Name";
                this.entityComboBox.ValueMember = "Id";
                entityComboBox.DataSource = entities;
                //Setup data binding
              
                //}
            }
        }

        private void SetCurrentEntityData()
        {
            if (entityComboBox.SelectedIndex == -1) return;
            int id = (int)entityComboBox.SelectedValue;

            using (var db = new WBEntities())
            {
                var r = db.Entities.Where(x => x.Id == id).FirstOrDefault();
                CurrentEntity.Id = r.Id;
                CurrentEntity.Name = r.Name;
                CurrentEntity.LowestBalanceThreshold = r.LowestBalance;
            }
        }

        private void transactionsButton_Click(object sender, EventArgs e)
        {
            if (!CurrentEntityIsSet()) return;

            if (!AccountsSetup())
            {
                MessageBox.Show("Please setup an Account.", Text);
                return;
            }

            Code.Form.Open("TransactionsForm");
        }

        private void whenCanIAffordButton_Click(object sender, EventArgs e)
        {
            if (!CurrentEntityIsSet()) return;
            Affordability.Determine();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            DataBackupNotice();
            //DateTime endDate = DateTime.Today.AddYears(1).AddDays(-1);
            //int interval = 6;
            //var cashFlowForecast = new CashFlowForecast(endDate, interval);
            //cashFlowForecast.CalculateForDefaultEntity();
            Reminders.Load();
            if (Reminders.Exist()) Code.Form.Open("RemindersForm");

            
        }

        private void SelectDefaultEntity()
        {
            using (var db = new WBEntities())
            {
                var r = db.Entities.Where(x => x.Active == true && x.DefaultEntity == true).FirstOrDefault();
                if (r == null) return;
                entityComboBox.Text = r.Name;
            }
        }

        private static void DataBackupNotice()
        {
            if (DateTime.Today >= AppSettings.BackupReminderDate)
            {
                string msg = "Would you like to backup your data?";
                string title = "Reminder: Backup Your Data";
                if (MessageBox.Show(msg, title, MessageBoxButtons.YesNo) == DialogResult.Yes) Data.Backup();
            }
        }

        private void projectCashBalancesButton_Click(object sender, EventArgs e)
        {
            if (!CurrentEntityIsSet()) return;
            Code.Form.Open("DailyBalancesForm");
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            Code.Form.Open("HelpForm");
        }

        private void toolsButton_Click(object sender, EventArgs e)
        {
            Code.Form.Open("ToolsForm");
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            Code.Form.Open("AboutForm");
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            Code.Form.Open("SettingsForm");
        }

        private void entitiesButton_Click(object sender, EventArgs e)
        {
            Code.Form.Open("EntitiesForm");
        }

        private void accountsButton_Click(object sender, EventArgs e)
        {
            if (!CurrentEntityIsSet()) return;
            Code.Form.Open("AccountsForm");
        }

        private bool AccountsSetup()
        {
            using (var db = new WBEntities())
            {
                var rs = db.Accounts.Where(x=>x.EntityId == CurrentEntity.Id).ToList();
                return rs.Count > 0;
            }
        }

        private void entityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCurrentEntityData();
        }

        private bool CurrentEntityIsSet()
        {
            if (CurrentEntity.Id == null)
            {
                MessageBox.Show("Current Entity is not set.", Text);
                return false;
            }

            return true;
        }

        private void taxFormsButton_Click(object sender, EventArgs e)
        {
            Code.Form.Open("TaxFormsForm");
        }

        private void categoriesButton_Click(object sender, EventArgs e)
        {
            Code.Form.Open("CategoriesForm");
        }

        private void taxCategoriesButton_Click(object sender, EventArgs e)
        {
            Code.Form.Open("TaxCategoriesForm");
        }

        private void assetsButton_Click(object sender, EventArgs e)
        {
            Code.Form.Open("AssetsForm");
        }

        private void reportsButton_Click(object sender, EventArgs e)
        {
            Code.Form.Open("ReportsForm");
        }

        private void ten99ContractorsButton_Click(object sender, EventArgs e)
        {
            Code.Form.Open("_1099ContractorsForm");
        }

        private void QuickCashAppTransButton_Click(object sender, EventArgs e)
        {
            Code.Form.Open("QuickCashAppTransForm");
        }

        private void backupButton_Click(object sender, EventArgs e)
        {
            Data.Backup();
        }
    }
}
