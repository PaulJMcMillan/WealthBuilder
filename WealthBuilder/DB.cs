//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WealthBuilder
{
    public class DB
    {
        public static void Backup(string targetFile)
        {
            string connectionString;

            using (var db = new WBEntities())
            {
                connectionString = db.Database.Connection.ConnectionString;
            }

            string dbName = ParseDbNameFromConnStr(connectionString);

            if (dbName == string.Empty)
            {
                MessageBox.Show("Error: Cannot determine the database file name.");
                return;
            }

            using (var connection = new SqlConnection { ConnectionString = connectionString })
            {
                connection.Open();
                var sql = $@"Backup Database " + dbName + " To Disk='" + targetFile + "'";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery(); 
                }

                connection.Close(); 
            }
        }

        private static string ParseDbNameFromConnStr(string connectionString)
        {
            int attachDbFileNamePos = connectionString.ToLower().IndexOf("attachdbfilename");
            if (attachDbFileNamePos == -1) return string.Empty;
            int equalSignPos = connectionString.ToLower().IndexOf("=", attachDbFileNamePos);
            if (equalSignPos == -1) return string.Empty;
            int startPos = equalSignPos + 1;
            int semicolonPos = connectionString.ToLower().IndexOf(";", equalSignPos);
            if (semicolonPos == -1) return string.Empty;
            int endPos = semicolonPos - 1;
            string dbName = connectionString.Substring(startPos, endPos - startPos + 1);
            return dbName;
        }
    }
}