using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly Dictionary<string, Customer> _customerList;
        public CustomerController(Dictionary<string, Customer> customerList)
        {
            _customerList = customerList;
        }

        [HttpGet]
        [Route("{customerId}")]
        public Customer GetCustomer([FromRoute] string customerId)
        {
            var customer = _customerList[customerId];

            if (customer is null)
                throw new NullReferenceException("customer doesn't exist");

            return customer;
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerList.Values.ToList();
        }
        
        [HttpPost]
        public Customer CreateCustomer([FromBody] CustomerCrud customerCrud)
        {
            if (CustomerExists(customerCrud))
                throw new Exception("Customer Already exists");
            var customer = new Customer(customerCrud);
            _customerList.Add(customer.CustomerId, customer);

            return _customerList[customer.CustomerId];
        }

        [HttpPut]
        public Customer UpdateCustomer([FromBody] Customer customer)
        {
            if (!_customerList.Keys.Contains(customer.CustomerId))
                throw new Exception("Customer Not found.");

            _customerList[customer.CustomerId] = customer;

            return _customerList[customer.CustomerId];
        }
        
        [HttpDelete]
        public void DeleteCustomer([FromQuery] string customerId)
        {
            if (!_customerList.Keys.Contains(customerId))
                throw new Exception("Customer Not found.");

            _customerList.Remove(customerId);
        }

        private bool CustomerExists(CustomerCrud customerCrud)
        {
            return _customerList.Values.Any(x => x.Email == customerCrud.Email ||
                                          x.FirstName + x.LastName == customerCrud.FirstName + customerCrud.LastName);
        }
    }
}