using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthBuilder
{
    public static class TextFile
    {
        internal static void AppendInfo(string fileName, string info)
        {
            using (Stream myFile = File.Open(Constants.DataFolder + fileName, FileMode.Append, FileAccess.Write))
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
            AppendInfo("Troubleshooting.txt", header);
            File.AppendAllText(targetPath, File.ReadAllText(sourcePath));
        }
    }
}
