﻿using MySql.Data.MySqlClient;
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
                string query = @"
                    SELECT c.customerId, c.customerName, 
                           a.address, a.postalCode, a.phone,
                           a.cityId, ci.city AS CityName
                    FROM customer c
                    JOIN address a ON c.addressId = a.addressId
                    JOIN city ci ON a.cityId = ci.cityId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }

        public static void AddCustomer(Customer customer)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();
                try
                {

                    string addressQuery = @"
                        INSERT INTO address 
                           (address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdateBy)
                        VALUES 
                           (@address, '', @cityId, @postalCode, @phone, NOW(), @createdBy, @createdBy);
                        SELECT LAST_INSERT_ID();";
                    MySqlCommand addressCmd = new MySqlCommand(addressQuery, conn, transaction);
                    addressCmd.Parameters.AddWithValue("@address", customer.Address);
                    addressCmd.Parameters.AddWithValue("@cityId", customer.CityId);
                    addressCmd.Parameters.AddWithValue("@postalCode", customer.PostalCode);
                    addressCmd.Parameters.AddWithValue("@phone", customer.Phone);
                    addressCmd.Parameters.AddWithValue("@createdBy", "test"); 
                    int addressId = Convert.ToInt32(addressCmd.ExecuteScalar());

                    string customerQuery = @"
                        INSERT INTO customer 
                           (customerName, addressId, active, createDate, createdBy, lastUpdateBy)
                        VALUES 
                           (@customerName, @addressId, 1, NOW(), @createdBy, @createdBy);";
                    MySqlCommand customerCmd = new MySqlCommand(customerQuery, conn, transaction);
                    customerCmd.Parameters.AddWithValue("@customerName", customer.CustomerName);
                    customerCmd.Parameters.AddWithValue("@addressId", addressId);
                    customerCmd.Parameters.AddWithValue("@createdBy", "test");
                    customerCmd.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void UpdateCustomer(Customer customer)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    string getAddressIdQuery = "SELECT addressId FROM customer WHERE customerId = @customerId";
                    MySqlCommand getCmd = new MySqlCommand(getAddressIdQuery, conn, transaction);
                    getCmd.Parameters.AddWithValue("@customerId", customer.CustomerId);
                    int addressId = Convert.ToInt32(getCmd.ExecuteScalar());

                    string updateAddressQuery = @"
                        UPDATE address 
                        SET address = @address, cityId = @cityId, postalCode = @postalCode, phone = @phone,
                            lastUpdate = NOW(), lastUpdateBy = @updatedBy 
                        WHERE addressId = @addressId";
                    MySqlCommand updateAddressCmd = new MySqlCommand(updateAddressQuery, conn, transaction);
                    updateAddressCmd.Parameters.AddWithValue("@address", customer.Address);
                    updateAddressCmd.Parameters.AddWithValue("@cityId", customer.CityId);
                    updateAddressCmd.Parameters.AddWithValue("@postalCode", customer.PostalCode);
                    updateAddressCmd.Parameters.AddWithValue("@phone", customer.Phone);
                    updateAddressCmd.Parameters.AddWithValue("@updatedBy", "test");
                    updateAddressCmd.Parameters.AddWithValue("@addressId", addressId);
                    updateAddressCmd.ExecuteNonQuery();

                    string updateCustomerQuery = @"
                        UPDATE customer 
                        SET customerName = @customerName, lastUpdate = NOW(), lastUpdateBy = @updatedBy 
                        WHERE customerId = @customerId";
                    MySqlCommand updateCustomerCmd = new MySqlCommand(updateCustomerQuery, conn, transaction);
                    updateCustomerCmd.Parameters.AddWithValue("@customerName", customer.CustomerName);
                    updateCustomerCmd.Parameters.AddWithValue("@updatedBy", "test");
                    updateCustomerCmd.Parameters.AddWithValue("@customerId", customer.CustomerId);
                    updateCustomerCmd.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void DeleteCustomer(int customerId)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    string getAddressIdQuery = "SELECT addressId FROM customer WHERE customerId = @customerId";
                    MySqlCommand getCmd = new MySqlCommand(getAddressIdQuery, conn, transaction);
                    getCmd.Parameters.AddWithValue("@customerId", customerId);
                    int addressId = Convert.ToInt32(getCmd.ExecuteScalar());

                    string deleteCustomerQuery = "DELETE FROM customer WHERE customerId = @customerId";
                    MySqlCommand deleteCustomerCmd = new MySqlCommand(deleteCustomerQuery, conn, transaction);
                    deleteCustomerCmd.Parameters.AddWithValue("@customerId", customerId);
                    deleteCustomerCmd.ExecuteNonQuery();

                    string deleteAddressQuery = "DELETE FROM address WHERE addressId = @addressId";
                    MySqlCommand deleteAddressCmd = new MySqlCommand(deleteAddressQuery, conn, transaction);
                    deleteAddressCmd.Parameters.AddWithValue("@addressId", addressId);
                    deleteAddressCmd.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
