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
    public class StoreController : Controller{
        private IStoreBL _storebl;
        private ILineItemBL _lineitemBL;
        public StoreController(IStoreBL storeBL, ILineItemBL lineitemBL){
            _storebl = storeBL;
            _lineitemBL = lineitemBL;
        }
        //show all customers 
        public IActionResult Index(){
            return View(_storebl.GetAllStore().Select(store => new StorefrontVM(store)).ToList());
        }
        public IActionResult Find(string search, string searchby){
            if (search == null){
                return View(_storebl.GetAllStore().Select(store => new StorefrontVM(store)).ToList());
            }
            else if (searchby == "Name"){
                return View(_storebl.GetAllStore().Where(store => store.Name.Contains(search)).Select(store => new StorefrontVM(store)).ToList());
            }
            else{
                return View(_storebl.GetAllStore().Where(store => store.Address.Contains(search)).Select(store => new StorefrontVM(store)).ToList());
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
            ViewBag.Storefront = _storebl.GetStorefront(p_id);
            return View(new StorefrontVM(_storebl.GetStorefront(p_id)));
        }
        public IActionResult ViewInventory(int p_id)
        {
            return View(_lineitemBL.GetInventory(p_id).Select(line => new LineItemVM(line)).ToList());
        }
    }
}