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
    public class OrderController : Controller{
            private ICustomerBL _customerBL;
            private IOrderBL _orderBL;
            private IStoreBL _storeBL;
        public OrderController(ICustomerBL customerBL, IOrderBL orderBL, IStoreBL storeBL){
            _customerBL = customerBL;
            _orderBL = orderBL;
            _storeBL = storeBL;
        }
        
        public IActionResult CustomerOrders(int p_id)        {
            //Viewbag and ViewData
            // Viewbag, dynamically infers a type....
            // Viewdata stores everything as an object
            // These two sharwe the same memory, they're both dictionary types that store things
            // in a key value manner
            // These two last 1 req/res lc
            ViewBag.Customer = _customerBL.GetCustomer(p_id);
            List<Order> result = _orderBL.GetCustomerOrder(p_id);
            List<Storefront> stores = _storeBL.GetAllStore();
            foreach(Storefront store in stores){
                ViewData.Add(Convert.ToString(store.ID), store.Name);
            }
            return View(result.Select(order => new OrderVM(order)).ToList());
        }

    }
}