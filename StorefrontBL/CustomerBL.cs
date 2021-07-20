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

        public Customer GetCustomer(int id){

            return _repo.GetCustomer(id);
        }
        public List<Customer> GetCustomer(string p_name){
            return _repo.GetCustomer(p_name);
        }

    }


}