//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Windows.Forms;

namespace WealthBuilder
{
    public partial class DailyBalancesForm : Form
    {
        public DailyBalancesForm()
        {
            InitializeComponent();
        }

        private void ProjectedCashBalancesForm_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            Text = Common.GetFormText(Text);
            PopulateDGV();
            Width = dgv.Width + 40;
        }

        private void PopulateDGV()
        {
            dgv.Rows.Clear();
            DateTime endDate = DateTime.Today.AddYears(1).AddDays(-1);
            int interval = 6;
            var cashFlowForecast = new CashFlowForecast(endDate, interval);
            cashFlowForecast.CalculateForOneEntity((long)CurrentEntity.Id);

            foreach (var r in cashFlowForecast.DailyBalances)
            {
                string date = r.Key.ToShortDateString();
                string balance = r.Value.ToString("C");
                dgv.Rows.Add(date, balance);
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            PopulateDGV();
        }
    }
}
