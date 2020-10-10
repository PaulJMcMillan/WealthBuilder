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

            using (var connection = new SqlConnection { ConnectionString = connectionString })
            {
                connection.Open();
                var sql = $@"Backup Database WealthBuilder To Disk='" + targetFile + "'";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery(); 
                }

                connection.Close(); 
            }
        }

        
    }
}