//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace WealthBuilder
{
    public class AppExecution
    {
        public static void Trace(string className, string methodName)
        {
            try
            {
                using (Stream myFile = File.Open(Constants.DataFolder + @"Trace.log", FileMode.Append, FileAccess.Write))
                {
                    using (TextWriterTraceListener myTextListener = new TextWriterTraceListener(myFile))
                    {
                        System.Diagnostics.Trace.Listeners.Add(myTextListener);
                        System.Diagnostics.Trace.WriteLine(DateTime.Now.ToString() + ": Entering " + className + "." + methodName);
                        System.Diagnostics.Trace.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                Error.Log("AppExecution", "Trace", ex);
                MessageBox.Show("An error occured in AppExecution.Trace.");
            }
        }

        public static void TraceValues(string className, string methodName, string values)
        {
            try
            {
                using (Stream myFile = File.Open(Constants.DataFolder + @"Trace.log", FileMode.Append, FileAccess.Write))
                {
                    using (TextWriterTraceListener myTextListener = new TextWriterTraceListener(myFile))
                    {
                        System.Diagnostics.Trace.Listeners.Add(myTextListener);
                        System.Diagnostics.Trace.WriteLine(DateTime.Now.ToString() + ": " + className + "." + methodName + ": Values: " + values);
                        System.Diagnostics.Trace.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                Error.Log("AppExecution", "Trace", ex);
                MessageBox.Show("An error occured in AppExecution.Trace.");
            }
        }
    }
}