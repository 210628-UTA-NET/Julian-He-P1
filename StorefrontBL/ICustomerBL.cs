using System;
using System.Collections.Generic;
using StorefrontModels;
using StorefrontDL;

namespace StorefrontBL{

    public interface ICustomerBL{
        
        
        List<Customer> GetAllCustomers();

        Customer AddCustomer(Customer customer);

    }
}