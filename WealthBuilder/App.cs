using System;
using System.Windows.Forms;

namespace WealthBuilder
{
    public static class App
    {
        public static string Mode { get; set; }
        public static string Title { get; set; }

        public static void DetermineMode()
        {
            if ((string.IsNullOrWhiteSpace(AppSettings.License)))
            {
                AppSettings.License = "PennyPincher";
                AppSettings.BackupReminderDate = DateTime.Today.AddDays(7);
                AppSettings.MinimumCashBalance = 0;
                AppSettings.LicenseType = "PennyPincher";
                Title = "Penny Pincher";
                AppSettings.Update();
                Mode = AppSettings.LicenseType;
                return;
            }

            Title = "Penny Pincher";
            Mode = "PennyPincher";

            //if (KeyGen.IsLicenseValid())
            //{
            //    Mode = AppSettings.LicenseType;

            //    switch (Mode)
            //    {
            //        case "WealthBuilder":
            //            Title = "Wealth Builder";
            //            break;
            //        case "PennyPincher":
            //            Title = "Penny Pincher";
            //            break;
            //        default:
            //            break;
            //    }

            //    return;
            //}

            //MessageBox.Show("You do not have a valid license.  Please obtain one.", "No License");
            //ExitApp();
        }

        private static void ExitApp()
        {
            Mode = "Exit";
            Application.Exit();
        }
    }
}
