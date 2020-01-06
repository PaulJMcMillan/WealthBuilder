//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace WealthBuilder
{
    public static class Error
    {
        public static void Log(string className, string methodName, Exception ex = null)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            //Error.log is created in the data folder because the user might not have permission to create the file
            //in the PFs folder.
            using (Stream myFile = File.Open(Constants.DataFolder + "Error.Log", FileMode.Append, FileAccess.Write))
            {
                TextWriterTraceListener myTextListener = new TextWriterTraceListener(myFile);
                Trace.Listeners.Add(myTextListener);
                string msg = string.Empty;
                if (ex != null) msg = ex.Message + "/" + ex.InnerException;
                string message = className + "/" + methodName + "/" + msg;
                Trace.WriteLine(message);
                Trace.Flush(); 
            }
        }

        public static void Log(string className, string methodName, object errors, string msg)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            using (Stream myFile = File.Open(Constants.DataFolder + "Error.Log", FileMode.Append, FileAccess.Write))
            {
                TextWriterTraceListener myTextListener = new TextWriterTraceListener(myFile);
                Trace.Listeners.Add(myTextListener);
                string m = errors.ToString();
                string message = className + "/" + methodName + "/" + m + "/" + msg;
                Trace.WriteLine(message);
                Trace.Flush(); 
            }
        }
    }
}