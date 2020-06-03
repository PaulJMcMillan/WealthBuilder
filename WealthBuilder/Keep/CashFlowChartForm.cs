//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WealthBuilder
{
    public class CashFlowChartForm : Form
    {
        private int interval;
        private Chart cashFlowChart;

        public CashFlowChartForm(int interval, string formText)
        {
            InitializeComponent();
            Text = formText;
            this.interval = interval;
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CashFlowChartForm));
            this.cashFlowChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.cashFlowChart)).BeginInit();
            this.SuspendLayout();
            // 
            // cashFlowChart
            // 
            this.cashFlowChart.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cashFlowChart.BorderlineColor = System.Drawing.Color.Black;
            chartArea1.BackColor = System.Drawing.SystemColors.ButtonFace;
            chartArea1.Name = "ChartArea1";
            cashFlowChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.cashFlowChart.Legends.Add(legend1);
            this.cashFlowChart.Location = new System.Drawing.Point(12, 12);
            this.cashFlowChart.Name = "cashFlowChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.Legend = "Legend1";
            series1.Name = "Cash Flow";
            series1.XValueMember = "DateString";
            series1.YValueMembers = "Balance";
            this.cashFlowChart.Series.Add(series1);
            this.cashFlowChart.Size = new System.Drawing.Size(1239, 527);
            this.cashFlowChart.TabIndex = 1;
            this.cashFlowChart.Text = "Cash Flow Forecast";
            this.cashFlowChart.Click += new System.EventHandler(this.cashFlowChart_Click);
            // 
            // CashFlowChartForm
            // 
            this.ClientSize = new System.Drawing.Size(1268, 552);
            this.Controls.Add(this.cashFlowChart);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CashFlowChartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cash Flow Chart";
            this.Load += new System.EventHandler(this.CashFlowChartForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cashFlowChart)).EndInit();
            this.ResumeLayout(false);

        }

        public static bool ChartRenderable()
        {
            return true;
            //todo: Need to figure out how to prevent the chart from crashing under certain conditions.  For example:
            // if there's no data, it crashes; or
            // if you enter $100 per week income with no expenses.

            //using (var db = new WBEntities())
            //{
            //    var rs = db.CashFlowForecastDatas;
            //    long? maxBalance = (long?)(from b in rs select b.Balance).Max();
            //    long? minBalance = (long?)(from b in rs select b.Balance).Min();
            //    double yAxisInterval = DetermineYAxisInterval(maxBalance);
            //    if (yAxisInterval != 0) return true;
            //    return false;
            //}
        }

        private void CashFlowChartForm_Load(object sender, EventArgs e)
        {
            CreateChart();
        }

        private void CreateChart()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            var c = cashFlowChart;
            c.Titles.Add(new Title("Cash Flow Forecast", Docking.Top, new Font("Verdana", 14f, FontStyle.Regular), Color.Black));
            c.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Trebuchet MS", 8F, FontStyle.Regular);
            c.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Trebuchet MS", 8F, FontStyle.Regular);
            c.ChartAreas[0].AxisX.TitleFont = new Font("Trebuchet MS", 16, FontStyle.Regular);
            c.ChartAreas[0].AxisY.TitleFont = new Font("Trebuchet MS", 16, FontStyle.Regular);
            c.Series[0].XValueType = ChartValueType.DateTime;
            c.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            c.ChartAreas[0].AxisX.IsLabelAutoFit = false;
            c.ChartAreas[0].AxisX.IsMarginVisible = false;
            c.ChartAreas[0].AxisX.Title = "Dates";
            c.ChartAreas[0].AxisY.Title = "Cash Balance";
            c.Series[0].BorderWidth = 4;
            c.ChartAreas[0].AxisY.LabelStyle.Format = "C0";

            using (var db = new WBEntities())
            {
                var rs = db.CashFlowForecastDatas;
                long? maxBalance = (long?)(from b in rs select b.Balance).Max();
                long? minBalance = (long?)(from b in rs select b.Balance).Min();
                c.ChartAreas[0].AxisX.Interval = interval;
                double yAxisInterval;
                yAxisInterval = DetermineYAxisInterval(maxBalance);

                if (yAxisInterval == 0)
                {
                    MessageBox.Show("The chart cannot be displayed because an interval could not be determined for the cash balance axis.  Please check Inflows, Buget, and Transactions data for accuracy.", "Chart Cannot Be Rendered");
                    return;
                }
                c.ChartAreas[0].AxisY.Interval = yAxisInterval;
                c.ChartAreas[0].AxisY.Minimum = (double)minBalance;  // - 100;
                c.ChartAreas[0].AxisY.Maximum = (double)maxBalance;
                foreach (var r in rs) c.Series[0].Points.AddXY(r.Date, r.Balance);
            }
        }

        private static double DetermineYAxisInterval(long? maxBalance)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            if (maxBalance == null)
            {
                MessageBox.Show("The maximum balance is not specified.");
                return 100;
            }

            long mb = (long)maxBalance;
            return mb * .1;
        }

        private void cashFlowChart_Click(object sender, EventArgs e)
        {

        }
    }
}