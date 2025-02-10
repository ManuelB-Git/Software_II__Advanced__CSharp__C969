using System;
using System.Text.RegularExpressions;

namespace Software_II__Advanced__CSharp__C969
{
    public static class CustomerManager
    {
        public static void AddCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));
            if (string.IsNullOrWhiteSpace(customer.CustomerName))
                throw new ArgumentException("Customer name is required.", nameof(customer.CustomerName));
            if (string.IsNullOrWhiteSpace(customer.Address))
                throw new ArgumentException("Address is required.", nameof(customer.Address));
            if (customer.CityId <= 0)
                throw new ArgumentException("A valid city must be selected.", nameof(customer.CityId));
            if (string.IsNullOrWhiteSpace(customer.PostalCode))
                throw new ArgumentException("Postal Code is required.", nameof(customer.PostalCode));
            if (string.IsNullOrWhiteSpace(customer.Phone))
                throw new ArgumentException("Phone number is required.", nameof(customer.Phone));

            // Validate phone number format
            if (!Regex.IsMatch(customer.Phone, @"^[0-9-]+$"))
                throw new ArgumentException("Phone number can only contain numbers and dashes ('-').", nameof(customer.Phone));

            CustomerDAO.AddCustomer(customer);
        }

        public static void UpdateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));
            if (customer.CustomerId <= 0)
                throw new ArgumentException("Invalid CustomerId.", nameof(customer.CustomerId));
            if (string.IsNullOrWhiteSpace(customer.CustomerName))
                throw new ArgumentException("Customer name is required.", nameof(customer.CustomerName));
            if (string.IsNullOrWhiteSpace(customer.Address))
                throw new ArgumentException("Address is required.", nameof(customer.Address));
            if (customer.CityId <= 0)
                throw new ArgumentException("A valid city must be selected.", nameof(customer.CityId));
            if (string.IsNullOrWhiteSpace(customer.PostalCode))
                throw new ArgumentException("Postal Code is required.", nameof(customer.PostalCode));
            if (string.IsNullOrWhiteSpace(customer.Phone))
                throw new ArgumentException("Phone number is required.", nameof(customer.Phone));

            // Validate phone number format
            if (!Regex.IsMatch(customer.Phone, @"^[0-9-]+$"))
                throw new ArgumentException("Phone number can only contain numbers and dashes ('-').", nameof(customer.Phone));

            CustomerDAO.UpdateCustomer(customer);
        }

        public static void DeleteCustomer(int customerId)
        {
            if (customerId <= 0)
                throw new ArgumentException("Invalid CustomerId.", nameof(customerId));

            CustomerDAO.DeleteCustomer(customerId);
        }
    }
}
