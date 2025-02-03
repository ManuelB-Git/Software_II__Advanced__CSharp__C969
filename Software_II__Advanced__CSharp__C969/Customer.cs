namespace Software_II__Advanced__CSharp__C969
{
    public class Customer
    {
        public int CustomerId { get; set; }     // 0 for new customers.
        public string CustomerName { get; set; }
        public string Address { get; set; }       // Street address
        public int CityId { get; set; }           // Foreign key from the city table
        public string PostalCode { get; set; }    // Postal Code
        public string Phone { get; set; }
        // Optional: for display only
        public string CityName { get; set; }
    }
}
