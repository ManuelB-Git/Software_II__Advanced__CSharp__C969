using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Software_II__Advanced__CSharp__C969
{
    public static class CustomerDAO
    {
        public static DataTable GetCustomers()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT customerId, customerName, addressId, active, createDate, createdBy, lastUpdateBy FROM customer";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }

        public static void AddCustomer(string customerName, int addressId, bool active, DateTime createDate, string createdBy)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdateBy) " +
                               "VALUES (@customerName, @addressId, @active, @createDate, @createdBy, @createdBy)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customerName", customerName);
                cmd.Parameters.AddWithValue("@addressId", addressId);
                cmd.Parameters.AddWithValue("@active", active ? 1 : 0);
                cmd.Parameters.AddWithValue("@createDate", createDate);
                cmd.Parameters.AddWithValue("@createdBy", createdBy);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateCustomer(int customerId, string customerName, int addressId, bool active, DateTime updateDate, string updatedBy)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "UPDATE customer SET customerName=@customerName, addressId=@addressId, active=@active, " +
                               "lastUpdate=@updateDate, lastUpdateBy=@updatedBy WHERE customerId=@customerId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customerName", customerName);
                cmd.Parameters.AddWithValue("@addressId", addressId);
                cmd.Parameters.AddWithValue("@active", active ? 1 : 0);
                cmd.Parameters.AddWithValue("@updateDate", updateDate);
                cmd.Parameters.AddWithValue("@updatedBy", updatedBy);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteCustomer(int customerId)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "DELETE FROM customer WHERE customerId=@customerId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
