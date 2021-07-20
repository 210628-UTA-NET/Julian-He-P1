using System;
using System.Collections.Generic;
using System.IO;
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
            return _context.Customers.Select(cust => cust).ToList();
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.Find(id);
        }

        public List<Customer> GetCustomer(string p_name)
        {
            List<Customer> allCust = this.GetAllCustomers();
            var queryRes = (from var in allCust where var.Name == p_name select var );
            List<Customer> returnList = new List<Customer>();
            foreach(var res in queryRes){
                returnList.Add(res);
            }
            return returnList;
        }

        public Customer UpdateCustomer(Customer customer){
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return customer;
        }
    }
}