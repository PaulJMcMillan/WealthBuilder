//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Reflection;
using System.Windows.Forms;

namespace WealthBuilder
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            label1.Text = Constants.CopyrightMessage;
            copyrightLabel.Text = Constants.CopyrightMessage;
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            versionLabel.Text = version.ToString();
        }
    }
}
