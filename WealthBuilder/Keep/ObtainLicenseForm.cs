//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace WealthBuilder
{
    [ComVisibleAttribute(true)]
    public partial class ObtainLicenseForm : Form
    {
        public ObtainLicenseForm()
        {
            InitializeComponent();
        }

        private void obtainProductKeyButton_Click(object sender, EventArgs e)
        {
            if (!wealthBuilderRadioButton.Checked && !pennyPincherRadioButton.Checked)
            {
                MessageBox.Show("Please select an edition.", "Obtain a Product License");
                return;
            }

            string url = string.Empty;

            if (wealthBuilderRadioButton.Checked) url = "https://app.moonclerk.com/pay/789sibvttj4x";
            if (pennyPincherRadioButton.Checked) url = "https://app.moonclerk.com/pay/6rrj2fl2xrv4";
            Process.Start(url);
            string input = Interaction.InputBox("Enter product key:", "Activate Product");

            if (input == string.Empty)
            {
                Application.Exit();
                return;
            }

            AppSettings.License = input;
            AppSettings.Update();

            if (KeyGen.IsLicenseValid())
            {
                AppSettings.BackupReminderDate = DateTime.Today.AddDays(7);
                AppSettings.MinimumCashBalance = 0;
                AppSettings.Update();
                MessageBox.Show("Your software has been activated!", "Actived!");
                Application.Exit();
                return;
            }

            MessageBox.Show("Something went wrong during the product activation process. Please try again or contact Customer Care.", "Product Activation Failed");
            Application.Exit();
        }
    }
}
