using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Software_II__Advanced__CSharp__C969
{
    public static class DatabaseHelper
    {
        private static readonly string connectionString = "server=localhost;user id=sqlUser;password=Passw0rd!;database=client_schedule;";

  
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

     
        public static void TestConnection()
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MessageBox.Show("Connection successful!", "Database Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
