using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace WealthBuilder
{
    public partial class Ten99MiscReportForm : Form
    {
        public Ten99MiscReportForm()
        {
            InitializeComponent();
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(yearTextBox.Text, out int year))
            {
                MessageBox.Show("Please enter a valid year.", Text);
                return;
            }

            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string workingFile = appPath + "WorkingFile.docx";

            try
            {
                System.IO.File.Copy(appPath + "1099 Summary Report.docx", workingFile, true);
            }
            catch (Exception ex)
            {
                throw;
            }

            var app = new Word.Application();
            Word.Document doc = app.Documents.Open(workingFile);
            app.Selection.GoTo(Word.WdGoToDirection.wdGoToFirst, Word.WdGoToDirection.wdGoToFirst);
            app.Selection.Text = "Year: " + year.ToString(); 
            Word.Table table = doc.Tables[1];

            using (var db = new WBEntities())
            {
                DateTime startDate = new DateTime(year, 1, 1);
                DateTime endDate = new DateTime(year, 12, 31, 23, 59, 59);
                var transactions = db.Transactions.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();
                var result = from t in transactions
                             join tc in db.C1099Contractors on t.ContractorId equals tc.id
                             group t by t.ContractorId into a
                             select new { ContractorId = a.Key, PaidYTD = a.Sum(p => p.Withdrawal ?? 0) };
                var tcs = result.ToList();

                var rs = from b in tcs
                         join c in db.C1099Contractors on b.ContractorId equals c.id
                         orderby c.Name
                         where c.Active == true
                         select new { c.Name, b.PaidYTD };
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

                    table.Cell(row, col).Range.Text = r.Name;
                    col++;
                    table.Cell(row, col).Range.Text = r.PaidYTD.ToString("C");
                }
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PDF|*.pdf";
            saveFileDialog1.Title = "Save File";
            saveFileDialog1.ShowDialog();
            if (string.IsNullOrWhiteSpace(saveFileDialog1.FileName)) return;
            doc.SaveAs2(saveFileDialog1.FileName, Word.WdSaveFormat.wdFormatPDF);
            doc.Close();
            app.Quit();
            app = null;
            System.Diagnostics.Process.Start(saveFileDialog1.FileName);
        }
    }
}
