//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Reflection;
using System.Windows.Forms;
using System.IO;

namespace WealthBuilder
{
    public class Data
    {
        public static void Backup()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.Description = "Where do you want to save your data?";

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string now = DateTime.Now.ToString();
                now = now.Replace("/", "-");
                now = now.Replace(":", "-");
                string targetFile = folderBrowserDialog.SelectedPath + @"\WealthBuilder(" + now + ").bak";
                DB.Backup(targetFile);
                AppSettings.BackupReminderDate = DateTime.Today.AddDays(7);
                AppSettings.Update();
                MessageBox.Show("Your data was backed up successfully.", "Successful Backup");
            }
        }
    }
}
