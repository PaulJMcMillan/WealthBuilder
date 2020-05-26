using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace WealthBuilder
{
    public partial class TaxReportForm : Form
    {
        public TaxReportForm()
        {
            InitializeComponent();
        }

        private void TaxReportForm_Load(object sender, EventArgs e)
        {
            taxFormsTableAdapter.Fill(dataSet.TaxForms);
            entitiesTableAdapter.Fill(dataSet.Entities);
            int year = DateTime.Today.Year;
            startDateDateTimePicker.Value = new DateTime(year, 1, 1);
            endDateDateTimePicker.Value = new DateTime(year, 12, 31);
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            bool pass = true;
            if (entityComboBox.SelectedIndex == -1) pass = false;
            if (taxFormComboBox.SelectedIndex == -1) pass = false;

            if (!pass)
            {
                MessageBox.Show("Please populate all of the criteria.", Text);
                return;
            }

            if (Process.GetProcessesByName("winword").Any())
            {
               
                MessageBox.Show("Please close Microsoft Word before running this report.", Text);
                return;
            }

            string entity = entityComboBox.Text;
            string taxForm = taxFormComboBox.Text;
            DateTime startDate = startDateDateTimePicker.Value.Date;
            DateTime endDate = endDateDateTimePicker.Value.Date;
            int entityId = (int)entityComboBox.SelectedValue;
            int taxFormId = (int)taxFormComboBox.SelectedValue;
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string workingFile = Constants.DataFolder + "WorkingFile.docx";
            System.IO.File.Copy(appPath + "Tax Summary Report.docx", workingFile, true);
            var app = new Word.Application();
            Word.Document doc = app.Documents.Open(workingFile);
            doc.Tables[1].Cell(1, 1).Range.Text = "Entity: " + entity;
            doc.Tables[1].Cell(1, 2).Range.Text = "Start Date: " + startDate.ToShortDateString();
            doc.Tables[1].Cell(2, 1).Range.Text = "Tax Form: " + taxForm;
            doc.Tables[1].Cell(2, 2).Range.Text = "End Date: " + endDate.ToShortDateString();
            Word.Table table = doc.Tables[2];

            using (var db = new WBEntities())
            {
                var transactions = db.Transactions.Where(x => x.Date >= startDate && x.Date <= endDate && x.EntityId == entityId && x.TaxFormId == taxFormId).ToList();
                var result =     from t in transactions
                                  join tc in db.TaxCategories on t.TaxCategoryId equals tc.Id
                                  group t by t.TaxCategoryId into a
                                  select new {TaxCategoryId = a.Key, TaxCategoryTotal = a.Sum(p => p.Deposit + p.Withdrawal)};
                var tcs = result.ToList();

                var rs = from b in tcs
                         join c in db.TaxCategories on b.TaxCategoryId equals c.Id
                         orderby c.TaxCategoryName
                         select new { c.TaxCategoryName, b.TaxCategoryTotal};
                int row = 2;
                int col = 0;

                foreach(var r in rs)
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

            //Finished
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PDF|*.pdf";
            saveFileDialog1.Title = "Save File";
            saveFileDialog1.ShowDialog();
            if (string.IsNullOrWhiteSpace(saveFileDialog1.FileName)) return;
            doc.SaveAs2(saveFileDialog1.FileName, Word.WdSaveFormat.wdFormatPDF);
            doc.Close();
            app.Quit();
            app = null;
            Process.Start(saveFileDialog1.FileName);
            //MessageBox.Show(new Form { TopMost = true }, "Your file have been saved.", "File Saved", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
