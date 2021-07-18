using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorefrontBL;
using StorefrontUI2.Models;
using StorefrontModels;

namespace StorefrontUI2.Controllers{
    public class CustomerController : Controller{
        private ICustomerBL _customerbl;
        public CustomerController(ICustomerBL customerBL){
            _customerbl = customerBL;
        }
        //show all customers 
        public IActionResult Index(){
            return View(_customerbl.GetAllCustomers().Select(cust => new CustomerVM(cust)).ToList());
        }

        //make a customer
        [HttpPost]
        public IActionResult Create(CustomerVM customer, List<Order> orders)
        {

            try{
                    if (ModelState.IsValid){
                        _customerbl.AddCustomer(new Customer{
                            Name = customer.Name,
                            Address = customer.Address,
                            Orders = customer.Orders,
                            Phone = customer.Phone,
                            Email = customer.Email,
                        });
                        return RedirectToAction(nameof(Index));
                    }
                
            }
            catch (Exception){
                return View();
            }
            return View();
        }

        public IActionResult Edit(int p_id)
        {
            return View(new CustomerVM(_customerbl.GetCustomer(p_id)));
        }
    }
}