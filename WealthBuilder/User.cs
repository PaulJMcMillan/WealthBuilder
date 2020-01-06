using System;
using System.Reflection;
using System.Windows.Forms;

namespace WealthBuilder
{
    internal static class User
    {
        internal static string GetFolder()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.Description = "Where do you want to save the file?";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK) return folderBrowserDialog.SelectedPath;
            return string.Empty;
        }
    }
}