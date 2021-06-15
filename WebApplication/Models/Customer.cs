using System;

namespace WebApplication.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public Customer(CustomerCrud customerCrud)
        {
            CustomerId = Guid.NewGuid().ToString();
            FirstName = customerCrud.FirstName;
            LastName = customerCrud.LastName;
            Email = customerCrud.Email;
        }
    }
}