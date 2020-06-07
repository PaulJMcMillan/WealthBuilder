using System;
using System.Reflection;
using Microsoft.VisualBasic.Devices;
using System.IO;
using System.Windows.Forms;

namespace WealthBuilder
{
    internal static class SupportFile
    {
        private static string targetFile;

        internal static bool Create()
        {
            try
            {
                if (!CreateTroubleShootingFile())
                {
                    MessageBox.Show("An error occurred while trying to create the troubleshooting file. " +
                                    "Please try again or contact Customer Care.");
                    return false;
                }

                AppendSystemInfo();
                AppendAppInfo();
                string tf = Constants.DataFolder + "Troubleshooting.txt";
                //TextFile.AppendFile("Error.log", tf);
                //TextFile.AppendFile("Trace.log", tf);
                if (!GetTargetPath()) return false;
                if (!CopyFile()) return false;
                return true;
            }
            catch (Exception ex)
            {
                Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        private static bool GetTargetPath()
        {
            string targetFolder = User.GetFolder();
            if (targetFolder == string.Empty) return false;
            targetFile = targetFolder + @"\Troubleshooting.txt";
            return true;
        }

        private static bool CopyFile()
        {
            string source = Constants.DataFolder + "Troubleshooting.txt";

            try
            {
                File.Copy(source, targetFile, true);
                return true;
            }
            catch (Exception ex)
            {
                Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        private static void AppendAppInfo()
        {
            string exePath = "EXE Path: " + Environment.CurrentDirectory;
            string version = "App Version: " + Application.ProductVersion;
            string info = exePath = version;
            //TextFile.AppendInfo(Constants.DataFolder + "Troubleshooting.txt", info);
        }

        private static bool CreateTroubleShootingFile()
        {
            try
            {
                using (Stream troubleShootingFile = File.Open(Constants.DataFolder + "TroubleShooting.txt", 
                       FileMode.Create, FileAccess.Write)) {}

                return true;
            }
            catch (Exception ex)
            {
                Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        private static void AppendSystemInfo()
        {
           
            var computerInfo = new ComputerInfo();
            var osVersion = "OS Version: " + computerInfo.OSVersion;//6.1.7601.65536
            var osName = "OS Name: " + computerInfo.OSFullName;//Microsoft Windows 7 Ultimate
            var osPlatform = "OS Platform: " + computerInfo.OSPlatform;//WinNT
            var availablePhysicalMemory = "Available Physical Memory: " + computerInfo.AvailablePhysicalMemory.ToString("N0");
            var availableVirtualMemory = "Available Virtual Memory: " + computerInfo.AvailableVirtualMemory.ToString("N0");
            var totalPhysicalMemory = "Total Physical Memory: " + computerInfo.TotalPhysicalMemory.ToString("N0");
            var totalVirtualMemory = "Total Virtual Memory:" + computerInfo.TotalVirtualMemory.ToString("N0");

            string currentDirectory = Environment.CurrentDirectory;
            string driveLetter = Path.GetPathRoot(currentDirectory);
            string diskSpace = "Free Disk Space: ";
             
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveLetter)
                {
                    diskSpace += drive.TotalFreeSpace.ToString("N0");
                }
            }

            //I didn't record the processor type because it was taking too much time.

            string systemInfo = osVersion + Environment.NewLine + osName + Environment.NewLine + osPlatform +
                  totalPhysicalMemory + Environment.NewLine + availablePhysicalMemory + Environment.NewLine +
                  totalVirtualMemory + Environment.NewLine + availableVirtualMemory + Environment.NewLine + diskSpace;

            //TextFile.AppendInfo(Constants.DataFolder + "TroubleShooting.txt", systemInfo);
        }
    }
}