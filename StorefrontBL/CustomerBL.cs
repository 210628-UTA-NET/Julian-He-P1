using System.Collections.Generic;
using StorefrontModels;
using StorefrontDL;

namespace StorefrontBL{

    public class CustomerBL : ICustomerBL
    {
        ICustomerRepository _repo;
        public CustomerBL(ICustomerRepository p_repo){
            _repo = p_repo;
        }
        
        public Customer AddCustomer(Customer customer)
        {
           return _repo.AddCustomer(customer);
        }

        public List<Customer> GetAllCustomers()
        {
            return _repo.GetAllCustomers();
        }

    }


}