using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WealthBuilder
{
    public partial class CashFlowChartCriteriaForm : Form
    {
        public CashFlowChartCriteriaForm()
        {
            InitializeComponent();
        }

        private void CashFlowChartCriteriaForm_Load(object sender, EventArgs e)
        {
            DataRow myNewRow;
            DataTable myTable = MakeDataTable();

            using (var db = new WealthBuilderEntities1())
            {
                var rs = db.Entities.Where(x => x.Active == true).OrderBy(x=>x.Name);
                myNewRow = myTable.NewRow();
                myNewRow["StringCol"] = "All Entities";
                myNewRow["Int32Col"] = -1;
                myNewRow["Sort"] = 1;
                myTable.Rows.Add(myNewRow);
                int sortPosition = 2;

                foreach (var r in rs)
                {
                    // Populate one row with values.
                    myNewRow = myTable.NewRow();
                    myNewRow["StringCol"] = r.Name;
                    myNewRow["Int32Col"] = r.Id;
                    myNewRow["Sort"] = sortPosition;
                    myTable.Rows.Add(myNewRow);
                    sortPosition++;
                }
            }

            myTable.DefaultView.Sort = "Sort" ;
            myTable = myTable.DefaultView.ToTable();
            startDateTimePicker.Value = DateTime.Today;
            endDateTimePicker.Value = DateTime.Today.AddYears(1);
            intervalTextBox.Text = "14";
        }

        public DataTable MakeDataTable()
        {
            DataTable myTable;

            // Create a new DataTable.
            myTable = new DataTable("My Table");
            DataColumn colInt32 = new DataColumn("Int32Col");
            colInt32.DataType = System.Type.GetType("System.Int32");
            myTable.Columns.Add(colInt32);

            // Create DataColumn objects of data types.
            DataColumn colString = new DataColumn("StringCol");
            colString.DataType = System.Type.GetType("System.String");
            myTable.Columns.Add(colString);
            DataColumn sort = new DataColumn("Sort");
            sort.DataType = Type.GetType("System.Int32");
            myTable.Columns.Add(sort);

            return myTable;
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            if (startDateTimePicker.Value > endDateTimePicker.Value)
            {
                MessageBox.Show("The Start Date cannot be after the End Date.", Text);
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            DateTime endDate = endDateTimePicker.Value;

            if (!int.TryParse(intervalTextBox.Text, out int interval))
            {
                MessageBox.Show("Interval is invalid.", Text);
                return;
            }

            decimal beginningBalance = Cash.GetStartingBalanceForCashFlow();
            var cashFlowForecast = new CashFlowForecast(endDate, interval);
            cashFlowForecast.Compute(beginningBalance);
            var f = new CashFlowChartForm(interval, "Cash Flow Chart");
            Cursor.Current = Cursors.Default;
            f.Show();
        }
    }
}
