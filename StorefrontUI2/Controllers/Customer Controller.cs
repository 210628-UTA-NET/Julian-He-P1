using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorefrontBL;
using StorefrontUI2.Models;
using StorefrontModels;
using StorefrontDL;

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
        public IActionResult Find(string search, string searchby){
            if (search == null){
                return View(_customerbl.GetAllCustomers().Select(cust => new CustomerVM(cust)).ToList());
            }
            else if (searchby == "Name"){
                return View(_customerbl.GetAllCustomers().Where(cust => cust.Name.Contains(search)).Select(cust => new CustomerVM(cust)).ToList());
            }
            else{
                return View(_customerbl.GetAllCustomers().Where(cust1 => cust1.Address.Contains(search)).Select(cust => new CustomerVM(cust)).ToList());
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        //make a customer
        [HttpPost]
        public IActionResult Create(CustomerVM customer)
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

        public IActionResult ViewInfo(int p_id)
        {
            return View(new CustomerVM(_customerbl.GetCustomer(p_id)));
        }


    }
}