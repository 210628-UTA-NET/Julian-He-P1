using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorefrontBL;
using StorefrontUI2.Models;
using StorefrontModels;
using StorefrontDL;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace StorefrontUI2.Controllers{
    public class OrderController : Controller{
            private ICustomerBL _customerBL;
            private IOrderBL _orderBL;
            private IStoreBL _storeBL;
            private ILineItemBL _lineItemBL;

        public OrderController(ICustomerBL customerBL, IOrderBL orderBL, IStoreBL storeBL, ILineItemBL lineitemBL){
            _customerBL = customerBL;
            _orderBL = orderBL;
            _storeBL = storeBL;
           _lineItemBL = lineitemBL;
        }
        
        public IActionResult CustomerOrders(int p_id, string sortby){
            
            //Viewbag and ViewData
            // Viewbag, dynamically infers a type....
            // Viewdata stores everything as an object
            // These two share the same memory, they're both dictionary types that store things
            // in a key value manner
            // These two last 1 req/res lc
            ViewBag.SortPriceParameter = string.IsNullOrEmpty(sortby) ? "Price Desc":"";
            ViewBag.SortDateParameter = sortby == "Date" ? "Date Desc":"Date";
            List<Order> result = _orderBL.GetCustomerOrder(p_id);
            List<Storefront> stores = _storeBL.GetAllStore();
            foreach(Storefront store in stores){
                ViewData.Add(Convert.ToString(store.ID), store.Name);
            }
            var orders = _orderBL.GetCustomerOrder(p_id).AsQueryable();
            switch (sortby){
                case "Price Desc":
                    orders = orders.OrderByDescending(x => x.TotalPrice);
                    break;
                case "Date Desc":
                    orders = orders.OrderByDescending(x => x.Date);
                    break;
                case "Date":
                    orders = orders.OrderBy(x => x.Date);
                    break;
                default:
                    orders = orders.OrderBy(x=> x.TotalPrice);
                    break;
            }
            return View(orders.Select(order => new OrderVM(order)).ToList());
        }

        public IActionResult StoreOrders(int p_id, string sortby){
            ViewBag.Storefront = _storeBL.GetStorefront(p_id);
            ViewBag.SortPriceParameter = string.IsNullOrEmpty(sortby) ? "Price Desc":"";
            ViewBag.SortDateParameter = sortby == "Date" ? "Date Desc":"Date";
            List<Order> result = _orderBL.GetStoreOrder(p_id);
            List<Customer> customers = _customerBL.GetAllCustomers();
            foreach(Customer customer in customers){
                ViewData.Add(Convert.ToString(customer.ID), customer.Name);
            }
            var orders = _orderBL.GetStoreOrder(p_id).AsQueryable();
            switch (sortby){
                case "Price Desc":
                    orders = orders.OrderByDescending(x => x.TotalPrice);
                    break;
                case "Date Desc":
                    orders = orders.OrderByDescending(x => x.Date);
                    break;
                case "Date":
                    orders = orders.OrderBy(x => x.Date);
                    break;
                default:
                    orders = orders.OrderBy(x=> x.TotalPrice);
                    break;
            }
            return View(result.Select( order => new OrderVM(order)).ToList());
        }

        public IActionResult ViewCustomerOrder(int p_id){
            ViewBag.Order= _orderBL.GetOrder(p_id);
            return View( new OrderVM(_orderBL.GetOrder(p_id)));
        }

        public IActionResult ViewStoreOrder(int p_id){
            Order order = _orderBL.GetOrder(p_id);
            return View(new OrderVM(order));
        }

        public IActionResult PlaceOrder(int p_id, string search)
        {
            TempData["customer"]=_customerBL.GetCustomer(p_id);
            if (search == null){
                return View(_storeBL.GetAllStore().Select(cust => new StorefrontVM(cust)).ToList());
            }
            else{
                return View(_storeBL.GetAllStore().Where(cust => cust.Name.Contains(search)).Select(cust => new StorefrontVM(cust)).ToList());
            }
        }
        public IActionResult MakeOrder(int storeID, int customerID){
            TempData["customerID"] = customerID;
            TempData["storeID"] = storeID;
            return View(_lineItemBL.GetInventory(storeID).Select(inv => new LineItemVM(inv)).ToList());
        }
    }        
}