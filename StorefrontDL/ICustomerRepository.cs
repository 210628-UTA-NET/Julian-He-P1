using StorefrontModels;
using System.Collections.Generic;
using System;
namespace StorefrontDL{
    public interface ICustomerRepository{

        ///returns all customers in repo
        List<Customer> GetAllCustomers();
        Customer GetCustomer(int customer);
        Customer AddCustomer(Customer customer);
    }
}