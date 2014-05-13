using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Windows;

namespace CryptographyProject
{
    public class SqlManager
    {
        public static void InsertData()
        {
            string connectionString = Properties.Settings.Default.UserDatabaseConnectionString;

            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {
                connection.Open();

                string query = "Insert into Users (User, Password) Values (@User, @Password)";
                SqlCeCommand command = new SqlCeCommand (query, connection);
                command.Parameters.AddWithValue("@User", "Alex");
                command.Parameters.AddWithValue(@"Password", "VYZF3faFhBg=");

                try
                {
                    command.ExecuteNonQuery();
                }
                catch(SqlCeException)
                {
                    Console.WriteLine("Exception raised in inserting data in db!");   
                }
                connection.Close();
            }
        }

        public static string GetPassword()
        {
            string connectionString = Properties.Settings.Default.UserDatabaseConnectionString;
            string toReturn = null;

            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Password from Users";

                SqlCeCommand command = new SqlCeCommand(query, connection);

                SqlCeDataReader reader = null;

                try
                {
                    reader = command.ExecuteReader();
                }
                catch (SqlCeException)
                {

                }
                finally
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            toReturn = reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }
            return toReturn;
        }
    }
}
