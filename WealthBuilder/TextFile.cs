using System;
using System.Diagnostics;
using System.IO;

namespace WealthBuilder
{
    public static class TextFile
    {

        internal static void AppendInfo(string fileName, string info)
        {
            using (Stream myFile = File.Open(fileName, FileMode.Append, FileAccess.Write))
            {
                TextWriterTraceListener myTextListener = new TextWriterTraceListener(myFile);
                Trace.Listeners.Add(myTextListener);
                Trace.WriteLine(info);
                Trace.Flush();
            }
        }

        internal static void AppendFile(string sourceFile, string targetPath)
        {
            string sourcePath = Constants.DataFolder + sourceFile;
            string header = Environment.NewLine + sourceFile + Environment.NewLine;
            //AppendInfo(Constants.DataFolder + "Troubleshooting.txt", header);
            File.AppendAllText(targetPath, File.ReadAllText(sourcePath));
        }
    }
}
