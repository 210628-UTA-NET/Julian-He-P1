
using System;
using System.Collections.Generic;
using System.IO;
using Models = StorefrontModels;
using Entity = StorefrontDL.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StorefrontDL
{
    public class StoreRepository : IStoreRepository
    {
        private Entities.P0DBContext _context;
        public StoreRepository(Entity.P0DBContext p_context){
            _context = p_context;
        }

        public Models.Storefront AddStore(Models.Storefront store)
        {
            _context.Storefronts.Add(new Entity.Storefront{
                Name = store.Name,
                Address = store.Address,
                
            });
            _context.SaveChanges();
            return store; 
        }

        public List<Models.Storefront> GetAllStores()
        {
            var result = _context.Storefronts.Include(l =>l.LineItems).Include(o => o.Orders).ToList();
            List<Models.Storefront> list = new List<Models.Storefront>();

            foreach(var res in result){
                Models.Storefront store = new Models.Storefront();
                store.Address = res.Address;
                store.ID = res.Id;
                store.Name = res.Name;
                List<Models.LineItem> list1 = new List<Models.LineItem>();
                LineItemRepository lineItem = new LineItemRepository(_context);
                OrderRepository order = new OrderRepository(_context);
                store.Inventory = lineItem.GetLineItem("store", store.ID);
                store.Orders = order.GetStoreOrder(store.ID);
                list.Add(store);
            }
          return list;
        }

        public Models.Storefront GetStorefront(int id)
        {
            Models.Storefront chosen = new Models.Storefront();
            foreach(Models.Storefront store in this.GetAllStores()){
                if (store.ID == id){
                    chosen = store;
                }
            }
            return chosen;
        }

        public Models.LineItem Replenish(Models.LineItem item, int amt){
            LineItemRepository lines = new LineItemRepository(_context);
                lines.UpdateLineItem(item, amt);
            return item;
            }
        }
    }