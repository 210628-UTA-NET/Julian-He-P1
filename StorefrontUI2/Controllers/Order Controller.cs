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
using Serilog;

namespace StorefrontUI2.Controllers{
    public class OrderController : Controller{
            private ICustomerBL _customerBL;
            private IOrderBL _orderBL;
            private IStoreBL _storeBL;
            private ILineItemBL _lineItemBL;
            private ICartBL _cartBL;
            private IProductBL _productBL;

        public OrderController(ICustomerBL customerBL, IOrderBL orderBL, IStoreBL storeBL, ILineItemBL lineitemBL, ICartBL cartBL, IProductBL productBL){
            _customerBL = customerBL;
            _orderBL = orderBL;
            _storeBL = storeBL;
            _lineItemBL = lineitemBL;
            _cartBL = cartBL;
            _productBL = productBL;
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
            CookieOptions option = new CookieOptions(); 
            option.Expires = DateTime.Now.AddDays(1); 
            Response.Cookies.Append("CustomerID", Convert.ToString(p_id), option);
            Log.Information("{0}", p_id);
            TempData["Customer"] = p_id;
            if (search == null){
                Log.Information("Get all Stores");
                return View(_storeBL.GetAllStore().Select(cust => new StorefrontVM(cust)).ToList());
            }
            else{
                Log.Information("Get all Stores");
                return View(_storeBL.GetAllStore().Where(cust => cust.Name.Contains(search)).Select(cust => new StorefrontVM(cust)).ToList());
            }
        }
        public IActionResult MakeOrder(int store_id, int cust_id, string search){
            List<Cart> carts = _cartBL.GetAllCarts();
            List<Cart> cart = carts.Where(cart => cart.CustomerID == Convert.ToInt32(Request.Cookies["CustomerID"])).Select(cart=> cart).ToList();
            if(cart.Count() == 0){
                Cart newCart = new Cart();
                newCart.CustomerID = cust_id;
                newCart.StorefrontID = store_id;
                _cartBL.AddCart(newCart);
            }
            else{
                _cartBL.RemoveCart(cart[0]);
                Cart newCart = new Cart();
                newCart.CustomerID = cust_id;
                newCart.StorefrontID = store_id;
                _cartBL.AddCart(newCart);
            }
            CookieOptions option = new CookieOptions(); 
            option.Expires = DateTime.Now.AddDays(1); 
            Response.Cookies.Append("StoreID", Convert.ToString(store_id), option);
            Log.Information("Cookie Made for StoreID order");
            Log.Information("{0} storeid, {1} customerid", store_id, cust_id);
            if (search == null){
                Log.Information("Get all items in inventory");
                return View(_lineItemBL.GetInventory(store_id).Select(line=> new LineItemVM(line)).ToList());
            }
            else{
                Log.Information("Getting specific item");
                return View(_lineItemBL.GetInventory(store_id).Where(cust => cust.ProductName.Name.Contains(search)).Select(line => new LineItemVM(line)).ToList());
            }
        }

        public IActionResult AddItem(int Quantity, int itemID){
            try{
            List<Cart> carts = _cartBL.GetAllCarts();
            List<Cart> cart = carts.Where(cart => cart.CustomerID == Convert.ToInt32(Request.Cookies["CustomerID"])).Select(cart=> cart).ToList(); 
            LineItem lineitem = new LineItem();
            lineitem.CartID=cart[0].ID;
            Product product = _productBL.GetProduct(itemID);
            lineitem.ProductName = product;
            lineitem.Quantity = Quantity;
            return PartialView();
            }
            catch (Exception e){
                Log.Information(e.ToString());
                return RedirectToAction(nameof(MakeOrder));
            }
        }

        public IActionResult Checkout(){
            return View();
        }
    }        
}