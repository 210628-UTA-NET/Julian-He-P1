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

namespace StorefrontUI2.Controllers{
    public class StoreController : Controller{
        private IStoreBL _storebl;
        private ILineItemBL _lineitemBL;
        public StoreController(IStoreBL storeBL, ILineItemBL lineitemBL){
            _storebl = storeBL;
            _lineitemBL = lineitemBL;
        }
        //show all customers 
        public IActionResult Index(string search){
            if (search == null){
                return View(_storebl.GetAllStore().Select(store => new StorefrontVM(store)).ToList());
            }
            else{
                return View(_storebl.GetAllStore().Where(store => store.Name.Contains(search)).Select(cust => new StorefrontVM(cust)).ToList());
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        //make a store
        [HttpPost]
        public IActionResult Create(StorefrontVM store)
        {
            try{
                    if (ModelState.IsValid){
                        _storebl.AddStore(new Storefront{
                            Name = store.Name,
                            Address = store.Address,
                            Orders = store.Orders,
                            Inventory = store.Inventory,
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
            
            CookieOptions option = new CookieOptions(); 
            option.Expires = DateTime.Now.AddDays(1); 
            Response.Cookies.Append("StoreID", Convert.ToString(p_id), option); 
            TempData["StoreID"] =Request.Cookies["StoreID"];
            return View(new StorefrontVM(_storebl.GetStorefront(p_id)));
        }
        public IActionResult ViewInventory(int p_id)
        {   
            ViewBag.Data = Request.Cookies["StoreID"];
            ViewBag.Storefront = _storebl.GetStorefront(p_id);
            return View(_lineitemBL.GetInventory(p_id).Select(line => new LineItemVM(line)).ToList());
        }

        [HttpPost]
         public IActionResult ReplenishItem(int amt, LineItem item)  
            {
                
                LineItem Lineitem = _lineitemBL.UpdateLineItem(item, amt);
                int storeid = (int) Lineitem.StorefrontID;
                ViewBag.Storefront = _storebl.GetStorefront(storeid);
                return View();

            }
        public IActionResult ReplenishItem(LineItem item){
            TempData["store"] = item.StorefrontID;
            ViewBag.LineItem = item;
            return View(new LineItemVM(item));
        }
    } 
}