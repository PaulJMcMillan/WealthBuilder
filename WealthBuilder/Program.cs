//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
//TODOs for the the entire app:
//TODO: Add ref integrity to tables.  (one at a time)
//todo TESTING NOTES:
//todo =============
//todo
//todo PRIORITY A DEFECTS
//todo ==================

//todo PRIORITY B DEFECTS
//todo ==================
//todo When you click the Trans btn on the main form, if the transactions form is minimized, it doesn't come to the top.

//todo ENHANCEMENTS:
//todo ============
//todo Priority C: Add error handling.
//todo add try catch to risky stuff.
//todo trace only methods that you're concerned about blowing up.
//todo Delete trace file when it gets so big
//todo support agreement
//todo ask beta users what reports they want to start off with.
//todo add a refer us to a friend form to the order form.
//todo Create mouse hoover comments
//todo Import transactions.

//TODO MARKETING IDEAS:
//TODO ===============
//TODO QUICKBOOKS ADDON
//TODO BANKING COMPANIES
//TODO ADDONS

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WealthBuilder
{
    static class Program
    {
        static readonly string _className = MethodBase.GetCurrentMethod().DeclaringType.Name;
        static readonly string _methodName = MethodBase.GetCurrentMethod().Name;

        [STAThread]
        static void Main(string[] args)
        {
            //Data folder points to User/AppData\Roaming\McMillanFinancialSolutions\WealthBuilder
            if (!Directory.Exists(Constants.DataFolder)) Directory.CreateDirectory(Constants.DataFolder);
            AppExecution.Trace(_className, _methodName);
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

            try
            {
                AppSettings.Retrieve();
                Application.ApplicationExit += CleanupBeforeExit;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                App.DetermineMode();
                if (App.Mode == "Exit") return;
                var app = new SplashScreenApplication();
                app.Run(args);
            }
            catch (Exception ex)
            {
                Error.Log("Program", "Main", ex);
                Application.Exit();
            }
        }

        private static void CleanupBeforeExit(object sender, EventArgs e)
        {
            try
            {
                AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                if (App.Mode == "Exit") return;
                AutomaticBackup();
                CleanUpOldBackupFiles();
                CleanupMsWord();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during final cleanup.", _className + ' ' + _methodName);
                Application.Exit();
            }
        }

        private static void CleanupMsWord()
        {
            //todo

        }

        private static void AutomaticBackup()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            string now = DateTime.Now.ToString();
            now = now.Replace("/", "-");
            now = now.Replace(":", "-");
            if (!Directory.Exists(Constants.DataFolder + @"Backups")) Directory.CreateDirectory(Constants.DataFolder + @"Backups");
            DB.Backup(Constants.DataFolder + @"Backups\WealthBuilder(" + now + ").bak");
        }

        private static void CleanUpOldBackupFiles()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            string backupFolder = Constants.DataFolder + @"Backups";
            if (!Directory.Exists(backupFolder)) return;
            DirectoryInfo info = new DirectoryInfo(backupFolder);
            FileInfo[] files = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();
            DateTime cutoffDate = DateTime.Today.AddDays(-90);

            foreach (FileInfo file in files)
            {
                if (file.CreationTime > cutoffDate) break;
                file.Delete();
            }
        }
    }
}
