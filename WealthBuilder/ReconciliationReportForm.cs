using System;
using System.IO;
using System.Windows.Forms;

namespace WealthBuilder
{
    public partial class ReconciliationReportForm : Form
    {
        public ReconciliationReportForm()
        {
            InitializeComponent();
        }

        private void ReconciliationReportForm_Load(object sender, EventArgs e)
        {
            string text = File.ReadAllText(Constants.ReconciliationReportFileName);
            richTextBox1.Text = text;
        }
    }
}
