using System;
using System.Windows.Forms;


namespace WealthBuilder
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
        }

        private void taxReportButton_Click(object sender, EventArgs e)
        {
            Code.Form.Open("TaxReportForm");
        }

        private void ten99MiscReport_Click(object sender, EventArgs e)
        {
            Code.Form.Open("Ten99MiscReportForm");
        }
    }
}
