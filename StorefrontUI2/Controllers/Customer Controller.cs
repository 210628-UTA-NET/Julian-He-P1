using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorefrontBL;
using StorefrontUI2.Models;
using StorefrontModels;
using StorefrontDL;
using Serilog;
using Microsoft.AspNetCore.Http;


namespace StorefrontUI2.Controllers{
    public class CustomerController : Controller{
        private ICustomerBL _customerbl;

        public CustomerController(ICustomerBL customerBL){
            _customerbl = customerBL;

        }
        //show all customers
        public IActionResult Index(string search){
            
        try{
            if (search == null){
                return View(_customerbl.GetAllCustomers().Select(cust => new CustomerVM(cust)).ToList());
            }
            else{
                return View(_customerbl.GetAllCustomers().Where(cust => cust.Name.Contains(search)).Select(cust => new CustomerVM(cust)).ToList());
            }
        }
        catch (Exception e){
            Log.Debug(e.ToString());
            return View(_customerbl.GetAllCustomers().Select(cust => new CustomerVM(cust)).ToList());
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
                    Orders = null,
                    Phone = customer.Phone,
                    Email = customer.Email,
                    });
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e){
                Log.Debug(e.ToString());
                return View();
            }
            return View();
        }

        public IActionResult ViewInfo(int p_id)
        {   
            CookieOptions option = new CookieOptions(); 
            option.Expires = DateTime.Now.AddDays(1); 
            Response.Cookies.Append("CustomerID", Convert.ToString(p_id), option); 
            ViewBag.Customer = _customerbl.GetCustomer(p_id);
            return View(new CustomerVM(_customerbl.GetCustomer(p_id)));
        }


    }
}