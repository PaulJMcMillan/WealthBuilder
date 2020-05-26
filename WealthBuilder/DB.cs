//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Data.SqlClient;
using System.Reflection;

namespace WealthBuilder
{
    public class DB
    {
        public static void Backup(string targetFile)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                string connectionString = string.Empty;

               using (var db = new WBEntities())
                {
                    connectionString = db.Database.Connection.ConnectionString;
                }

                var connection = new SqlConnection { ConnectionString = connectionString};
                connection.Open();
                var s = $@"BACKUP DATABASE WealthBuilder TO DISK='{targetFile}'";
                var command = new SqlCommand(s, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Error.Log("DB", "Backup", ex);
                throw ex;
            }
        }
    }
}