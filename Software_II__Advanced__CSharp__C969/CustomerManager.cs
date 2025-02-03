using System;

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

            DateTime now = DateTime.Now;
            CustomerDAO.AddCustomer(customer.CustomerName, customer.AddressId, customer.Active, now, "test");
        }

        public static void UpdateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));
            if (customer.CustomerId <= 0)
                throw new ArgumentException("Invalid CustomerId.", nameof(customer.CustomerId));

            DateTime now = DateTime.Now;
            CustomerDAO.UpdateCustomer(customer.CustomerId, customer.CustomerName, customer.AddressId, customer.Active, now, "test");
        }

        public static void DeleteCustomer(int customerId)
        {
            if (customerId <= 0)
                throw new ArgumentException("Invalid CustomerId.", nameof(customerId));

            CustomerDAO.DeleteCustomer(customerId);
        }
    }
}
