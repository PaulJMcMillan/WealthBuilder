//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System.Windows.Forms;

namespace WealthBuilder
{
    public partial class SplashScreenForm : Form
    {
        public SplashScreenForm()
        {
            InitializeComponent();
        }

        private void SplashScreenForm_Load(object sender, System.EventArgs e)
        {
            wealthBuilderLabel.Text = App.Title;
            copyrightLabel.Text = Constants.CopyrightMessage;

        }
    }
}
