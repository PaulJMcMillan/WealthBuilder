//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace WealthBuilder
{
    public static class FirstTimeRun
    {
        private static string className = MethodBase.GetCurrentMethod().DeclaringType.Name;

        public static void Do()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name,  MethodBase.GetCurrentMethod().Name);

            try
            {
                if (!Directory.Exists(Constants.DataFolder))
                {
                    CreateDataDirectory();
                    string workingFile = Constants.DataFolder + "WorkingFile.docx";
                    File.Copy("WorkingFile.docx", workingFile);
                    File.Copy(Constants.DataFolder + "Tax Summary Report.docx", workingFile, true);
                }
                    
                string methodName = MethodBase.GetCurrentMethod().Name;
                string file = "WealthBuilder.mdf";

                if (!File.Exists(Constants.DataFolder + file))
                {
                    CopyFile(file);
                    CopyFile("WealthBuilder.ldf");
                    ClearTables();
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.  Please contact technical support.");
                Error.Log("FirstTimeRun", "Do", ex);
                return;
            }


        }

        private static void ClearTables()
        {
            var tables = new List<string>();
            tables.Add("Accounts");
            tables.Add("Budget");
            tables.Add("Entities");
            tables.Add("Inflows");
            tables.Add("Reminders");
            tables.Add("Transactions");

            using (var db = new WBEntities())
            {
                foreach (string table in tables)
                {
                    db.Database.ExecuteSqlCommand("Delete From " + table);
                    db.SaveChanges();
                }
            }
        }

        private static void CopyFile(string file)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            string methodName = MethodBase.GetCurrentMethod().Name;
            string sourceFile =  Environment.CurrentDirectory + @"\" + file;
            file = Constants.DataFolder + file;
            string errMsg = "An error occurred during initial setup.  Please contact technical support.";
            string errTitle = "Initial Configuration Failed";

            try
            {
                File.Copy(sourceFile, file);
            }
            catch (UnauthorizedAccessException)
            {
                FileAttributes attr = (new FileInfo(file)).Attributes;

                if ((attr & FileAttributes.ReadOnly) > 0)
                {
                    var di = new DirectoryInfo(Constants.DataFolder);
                    di.Attributes &= ~FileAttributes.ReadOnly;
                }

                try
                {
                    File.Copy(sourceFile, file);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(errMsg, errTitle);
                    Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(errMsg, errTitle);
                Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                return;
            }

            methodName = MethodBase.GetCurrentMethod().Name;
        }

        private static void CreateDataDirectory()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                Directory.CreateDirectory(Constants.DataFolder);
            }
            catch (UnauthorizedAccessException)
            {
                FileAttributes attr = (new FileInfo(Constants.DataFolder)).Attributes;

                if ((attr & FileAttributes.ReadOnly) > 0)
                {
                    var di = new DirectoryInfo(Constants.DataFolder);
                    di.Attributes &= ~FileAttributes.ReadOnly;
                }

                try
                {
                    Directory.CreateDirectory(Constants.DataFolder);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred.  Please contact technical support.");
                    Error.Log("FirstTimeRun", "CreateDataDirectory", ex);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.  Please contact technical support.");
                Error.Log("FirstTimeRun", "CreateDataDirectory", ex);
                return;
            }
        }
    }
}