using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace WealthBuilder
{
    public partial class YearEndTaxReportForm : Form
    {
        public YearEndTaxReportForm()
        {
            InitializeComponent();
        }

        private void viewReportButton_Click(object sender, System.EventArgs e)
        {
            if(!int.TryParse(yearTextBox.Text, out int year))
            {
                MessageBox.Show("Invalid year");
                return;
            }

            var startDate = new DateTime(year, 1, 1);
            var endDate = new DateTime(year, 12, 31);

            if (Process.GetProcessesByName("winword").Any())
            {
                MessageBox.Show("Please close Microsoft Word before running this report.", Text);
                return;
            }

            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string workingFile = Constants.DataFolder + "WorkingFile.docx";
            System.IO.File.Copy(appPath + "Tax Summary Report.docx", workingFile, true);
            var app = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document doc = app.Documents.Open(workingFile);
            doc.Tables[1].Cell(1, 1).Range.Text = "Entity: McMillan Family";
            doc.Tables[1].Cell(1, 2).Range.Text = "Start Date: " + startDate.ToShortDateString();
            doc.Tables[1].Cell(2, 2).Range.Text = "End Date: " + endDate.ToShortDateString();
            Microsoft.Office.Interop.Word.Table table = doc.Tables[2];

            using (var db = new WealthBuilderEntities1())
            {
                //todo: 
                var transactions = db.Transactions.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();

                var result = from t in transactions
                             join tc in db.TaxCategories on t.TaxCategoryId equals tc.Id
                             group t by t.TaxCategoryId into a
                             select new { TaxCategoryId = a.Key, TaxCategoryTotal = a.Sum(p => p.Deposit + p.Withdrawal) };

                var tcs = result.ToList();

                var rs = from b in tcs
                         join c in db.TaxCategories on b.TaxCategoryId equals c.Id
                         orderby c.TaxCategoryName
                         select new { c.TaxCategoryName, b.TaxCategoryTotal };

                int row = 2;
                int col = 0;

                foreach (var r in rs)
                {
                    col++;

                    if (col > 4)
                    {
                        table.Rows.Add();
                        row++;
                        col = 1;
                    }

                    table.Cell(row, col).Range.Text = r.TaxCategoryName;
                    col++;
                    table.Cell(row, col).Range.Text = r.TaxCategoryTotal.ToString("N0");
                }
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PDF|*.pdf";
            saveFileDialog1.Title = "Save File";
            saveFileDialog1.ShowDialog();

            if (string.IsNullOrWhiteSpace(saveFileDialog1.FileName))
            {
                return;
            }

            doc.SaveAs2(saveFileDialog1.FileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
            doc.Close();
            app.Quit();
            app = null;
            Process.Start(saveFileDialog1.FileName);
        }
    }
}
