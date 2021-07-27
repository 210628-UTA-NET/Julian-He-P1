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
        
        /// <summary>
        /// Views all customer orders
        /// </summary>
        /// <param name="p_id"></param>
        /// <param name="sortby"></param>
        /// <returns>View with customer orders</returns>
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
            ViewBag.Customer = _customerBL.GetCustomer(p_id);
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
        /// <summary>
        /// View all store orders and give capability to sort
        /// </summary>
        /// <param name="p_id"></param>
        /// <param name="sortby"></param>
        /// <returns>A view with all the orders the store has</returns>
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
                    orders = orders.OrderByDescending(x => DateTime.Parse(x.Date));
                    break;
                case "Date":
                    orders = orders.OrderBy(x => DateTime.Parse(x.Date));
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
            if (p_id !=0){
            CookieOptions option = new CookieOptions(); 
            option.Expires = DateTime.Now.AddDays(1); 
            Response.Cookies.Append("CustomerID", Convert.ToString(p_id), option);
            }
            Log.Information("{0}", p_id);
            TempData["Customer"] = Request.Cookies["CustomerID"];
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
            try{
                string Customercookie = Request.Cookies["CustomerID"];
                    string Storecookie = Request.Cookies["StoreID"];
                if (store_id == 0 && cust_id == 0){
                    store_id = Convert.ToInt32(Storecookie);
                    cust_id = Convert.ToInt32(Customercookie);
                }
            TempData["Customer"] = cust_id;
            List<Cart> carts = _cartBL.GetAllCarts();
            Log.Information("Getting cart");
            List<Cart> cart = carts.Where(cart => cart.CustomerID == cust_id && cart.StorefrontID == store_id).Select(cart=> cart).ToList();
            Log.Information("Finding Only cart");
            if(cart.Count() == 0){
                Log.Information("Customer does not have a cart already in this store");
                Cart newCart = new Cart();
                newCart.CustomerID = cust_id;
                newCart.StorefrontID = store_id;
                _cartBL.AddCart(newCart);
            }
            
            if(Storecookie == null){
                CookieOptions option = new CookieOptions(); 
                option.Expires = DateTime.Now.AddDays(1); 
                Response.Cookies.Append("StoreID", Convert.ToString(store_id), option);
                Log.Information("Cookie Made for StoreID order");
                Log.Information("{0} storeid, {1} customerid", store_id, cust_id);
            }
            else if(Convert.ToInt32(Storecookie) != store_id){
                Log.Information("Making new cookie");
                CookieOptions option = new CookieOptions(); 
                option.Expires = DateTime.Now.AddDays(1); 
                Response.Cookies.Append("StoreID", Convert.ToString(store_id), option);
                Log.Information("Cookie Made for StoreID order");
                Log.Information("{0} storeid, {1} customerid", store_id, cust_id);
            }

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
            catch(Exception e){
                Log.Debug(e.ToString());
                return View();
            }
        }

        public IActionResult AddItem(int Quantity, int itemID){
            try{
                Log.Information("Quantity {0}, itemID {1}", Quantity, itemID);
                Log.Information("Getting cart");
            List<Cart> carts = _cartBL.GetAllCarts();
            Log.Information("Finding Only cart, removed excessive cart in make order know only 1 cart available");
            
            Cart cart = _cartBL.GetCart(Convert.ToInt32(Request.Cookies["CustomerID"]));
            LineItem lineitem = new LineItem();
            lineitem.CartID=cart.ID;
            
            Log.Information(Convert.ToString(cart.ID));

            CookieOptions option = new CookieOptions(); 
            option.Expires = DateTime.Now.AddDays(1); 
            Response.Cookies.Append("CartID", Convert.ToString(cart.ID), option);  
            LineItem item = _lineItemBL.GetLineItem(itemID);
            Product product = item.ProductName;
            foreach (LineItem line in cart.CartItems){
                if (line.ProductName.ID == product.ID){
                    line.Quantity+= Quantity;
                    _lineItemBL.UpdateLineItem(line);
                    item.Quantity-= Quantity;
                    _lineItemBL.UpdateLineItem(item);
                    Log.Information("Added item to cart");
                    return RedirectToAction(nameof(MakeOrder));
                }
            }
            Log.Information(product.Name);
            lineitem.ProductName = product;
            lineitem.Quantity = Quantity;

            _lineItemBL.AddLineItem(lineitem);
            item.Quantity-= Quantity;
            _lineItemBL.UpdateLineItem(item);
            Log.Information("Added item to cart");
            return RedirectToAction(nameof(MakeOrder));
            }
            catch (Exception e){
                Log.Information(e.ToString());
                return RedirectToAction(nameof(MakeOrder));
            }
        }

        /// <summary>
        /// returns the lineitems that are in the current user's cart
        /// </summary>
        /// <returns>The standard checkout page</returns>
        public IActionResult Checkout(){
            Log.Information("Default View");
            string Customercookie = Request.Cookies["CustomerID"];
            string Storecookie = Request.Cookies["StoreID"];
            List<Cart> cartlist = _cartBL.GetAllCarts().Where(cart => cart.CustomerID == Convert.ToInt32(Customercookie) && cart.StorefrontID == Convert.ToInt32(Storecookie)).ToList();
            List<LineItem> orderitems = _lineItemBL.GetAllLineItem().Where(cart => cart.CartID == Convert.ToInt32(Request.Cookies["CartID"])).ToList();
            return View(orderitems.Select(line => new LineItemVM(line)).ToList());
        }


        /// <summary>
        /// goes back to the make order page and moves everything in the cart to the order. 
        /// </summary>
        /// <param name="lineItems"></param>
        /// <returns>A view back to the make orders page</returns>
        public IActionResult BuyItems(){
            try{
            Log.Information("entered the buy phase");
            Order generateorder = new Order();
            Log.Information("make a new order");
            generateorder.Date = DateTime.Now.ToString();
            Log.Information("Convert date and time to a string");
            
            Cart TargetCart = _cartBL.GetCartByID(Convert.ToInt32(Request.Cookies["CartID"]));
            
            generateorder.StorefrontID= TargetCart.StorefrontID;
            generateorder.CustomerID=TargetCart.CustomerID;
            
            foreach(LineItem line in TargetCart.CartItems){
                generateorder.TotalPrice += line.Quantity*line.ProductName.Price;
                Log.Information("{0} Cart ID, {1} Order ID", line.CartID, line.OrderID);
                LineItem newLine = new LineItem();
                newLine.ProductName = line.ProductName;
                newLine.Quantity = line.Quantity;
                generateorder.Items.Add(newLine);
            }
            _cartBL.RemoveCart(TargetCart);
            Log.Information("Removed Cart items");
            _orderBL.AddOrder(generateorder);
            Log.Information("Adding Order to list");
            
            return RedirectToAction(nameof(MakeOrder));
            }
            catch (Exception e){
                Log.Debug(e.ToString());
                return RedirectToAction(nameof(Checkout));
            }
        }
    }        
}