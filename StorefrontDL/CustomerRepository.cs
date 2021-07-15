using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Models = StorefrontModels;
using Entity = StorefrontDL.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StorefrontDL{
    public class CustomerRepository : ICustomerRepository{
        private Entity.P0DBContext _context;
        public CustomerRepository(Entity.P0DBContext p_context){
            _context= p_context;
        }
        public Models.Customer AddCustomer(Models.Customer customer)
        {
            _context.Customers.Add(new Entity.Customer{
                Name = customer.Name,
                Address = customer.Address,
                Phone = customer.EmailPhoneGet("Phone"),
                Email = customer.EmailPhoneGet("Email"),
            });
            _context.SaveChanges();
            return customer;
        }

        public List<Models.Customer> GetAllCustomers()
            {
                var result = _context.Customers.Include(o => o.Orders).ToList();
            List<Models.Customer> list = new List<Models.Customer>();

            foreach(var res in result){
                Models.Customer customer = new Models.Customer();
                customer.Address = res.Address;
                customer.ID = res.Id;
                customer.Name = res.Name;
                OrderRepository order = new OrderRepository(_context);
                customer.Orders = order.GetCustomerOrder(customer.ID);
                list.Add(customer);
            }
          return list;
        }

        public Models.Customer GetCustomer(int id)
        {
            Models.Customer returnCustomer = new Models.Customer();
            List<Models.Customer> customers = this.GetAllCustomers();
            foreach(Models.Customer customer in customers){
                if (customer.ID == id){
                    returnCustomer = customer;
                    return returnCustomer;
                }
            }
            return returnCustomer;
        }
    }
}