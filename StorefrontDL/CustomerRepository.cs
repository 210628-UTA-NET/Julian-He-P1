using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using StorefrontModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StorefrontDL{
    public class CustomerRepository : ICustomerRepository{
        private StorefrontDBContext  _context;
        public CustomerRepository(StorefrontDBContext p_context){
            _context= p_context;
        }

        public Customer AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public List<Customer> GetAllCustomers()
            {
            return _context.Customers.Select(rest => rest).ToList();
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.Find(id);
        }
        public Customer UpdateCustomer(Customer customer){
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return customer;
        }
    }
}