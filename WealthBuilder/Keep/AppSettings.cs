//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace WealthBuilder
{
    public static class AppSettings
    {
        public static DateTime BackupReminderDate { get; set; }
        public static string License { get; set; }
        public static string LicenseType { get; set; }
        public static double MinimumCashBalance { get; set; }
        private static readonly string settingsFile = "AppSettings.txt";
        public static DateTime DemoExpirationDate { get; set; }

        public static void Update()
        {
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
            if (isoStore.FileExists(settingsFile)) isoStore.DeleteFile(settingsFile);

            using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(settingsFile, FileMode.Create, isoStore))
            {
                using (StreamWriter writer = new StreamWriter(isoStream))
                {
                    writer.WriteLine("BackupReminderDate={0}", BackupReminderDate.ToShortDateString());
                    writer.WriteLine("License={0}", License);
                    writer.WriteLine("LicenseType={0}", LicenseType);
                    writer.WriteLine("MinimumCashBalance={0}", MinimumCashBalance);
                    writer.WriteLine("DemoExpirationDate={0}", DemoExpirationDate.ToShortDateString());
                }
            }
        }

        public static void Retrieve()
        {
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            if (isoStore.FileExists(settingsFile))
            {
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(settingsFile, FileMode.Open, isoStore))
                {
                    using (StreamReader reader = new StreamReader(isoStream))
                    {
                        while (!reader.EndOfStream) RetrieveSetting(reader.ReadLine());
                    }
                }

                return;
            }

            //Initialize the file.
            using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(settingsFile, FileMode.Create, isoStore))
            {
                BackupReminderDate = DateTime.Today.AddDays(7);
                License = string.Empty;
                LicenseType = string.Empty;
                MinimumCashBalance = 0;
                DemoExpirationDate = DateTime.Today.AddDays(30);

                using (StreamWriter writer = new StreamWriter(isoStream))
                {
                    writer.WriteLine("BackupReminderDate={0}", BackupReminderDate.ToShortDateString());
                    writer.WriteLine("License={0}", License);
                    writer.WriteLine("LicenseType={0}", LicenseType);
                    writer.WriteLine("MinimumCashBalance={0}", MinimumCashBalance);
                    writer.WriteLine("DemoExpirationDate={0}", DemoExpirationDate.ToShortDateString());
                }
            }

            return;
            }

        private static void RetrieveSetting(string line)
        {
            line = line.Replace(" ", string.Empty);
            int i = line.IndexOf("=");
            if (i < 1) return;
            string setting = line.Substring(0, i).Trim();
            int len = line.Length;
            if (len < 1) return;
            int p = i + 1;
            string value = line.Substring(p, len - p).Trim();

            switch (setting)
            {
                case "BackupReminderDate":
                    DateTime dt = DateTime.Today.AddDays(7);
                    DateTime.TryParse(value, out dt);
                    BackupReminderDate = dt;
                    break;
                case "License":
                    License = value;
                    break;
                case "LicenseType":
                    LicenseType = value;
                    break;
                case "MinimumCashBalance":
                    double d = Double.MinValue;
                    Double.TryParse(value, out d);
                    MinimumCashBalance = d;
                    break;
                case "DemoExpirationDate":
                    DateTime.TryParse(value, out dt);
                    DemoExpirationDate = dt;
                    break;
                default:
                    break;
            }
        }
    }
}
