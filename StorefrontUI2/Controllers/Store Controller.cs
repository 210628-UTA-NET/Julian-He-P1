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
            ViewBag.Storefront = _storebl.GetStorefront(p_id);
            return View(new StorefrontVM(_storebl.GetStorefront(p_id)));
        }
        public IActionResult ViewInventory(int p_id)
        {   
            string id = Request.Cookies["StoreID"];
            if (id == null){
                return RedirectToAction(nameof(Index));
            }
            else{
            ViewBag.Storefront = _storebl.GetStorefront(Convert.ToInt32(id));
            return View(_lineitemBL.GetInventory(Convert.ToInt32(id)).Select(line => new LineItemVM(line)).ToList());
            }
        }

        public IActionResult ReplenishItem(int p_id){
            if(p_id == 0){
                Log.Information("lineitem is null");
            }
            else{
                Log.Information("{0}", p_id);
            }
            LineItem item = _lineitemBL.GetLineItem(p_id);
            if (item==null){
                Log.Information("GetLineItem failed {0}", p_id);
            }
            CookieOptions option = new CookieOptions(); 
            option.Expires = DateTime.Now.AddDays(1); 
            Response.Cookies.Append("LineItemID", Convert.ToString(p_id), option);
            TempData["LineID"] = p_id;
            Log.Information("Made Cookie");
            return View(new LineItemVM(item));
        }

        [HttpPost]
        public IActionResult ReplenishItem(int Quantity, int line)  
            {
                try{
                    Log.Information("Beginning update");
                    LineItem item = _lineitemBL.GetLineItem(line);
                    item.Quantity += Quantity;
                    LineItem Lineitem = _lineitemBL.UpdateLineItem(item);
                    Log.Information("Updated item");
                    return RedirectToAction(nameof(ViewInventory));
                }
                catch(Exception e){
                    Log.Information("An error occured while replenishing inventory");
                    return RedirectToAction(nameof(ViewInventory));
                }
            }
    } 
}