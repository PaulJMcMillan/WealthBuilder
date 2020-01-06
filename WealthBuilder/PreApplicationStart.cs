//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Linq;
using System.Reflection;

namespace WealthBuilder
{
    public class PreApplicationStart
    {
        public static void Perform()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            AppDomain.CurrentDomain.SetData("DataDirectory", Constants.DataFolder);
            FirstTimeRun.Do();
        }

        public static void EstablishDBConnection()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            using (var db = new WBEntities())
            {
                var rs = db.Budgets.ToList();
            }
        }
    }
}
