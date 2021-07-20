using System;
using System.Collections.Generic;
using StorefrontModels;
using StorefrontDL;

namespace StorefrontBL{

    public interface ICustomerBL{
        
        
        List<Customer> GetAllCustomers();

        Customer AddCustomer(Customer customer);

        Customer GetCustomer(int id);

        List<Customer> GetCustomer(string p_name);
    }
}