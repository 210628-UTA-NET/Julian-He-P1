using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using StorefrontModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StorefrontDL
{
    public class StoreRepository : IStoreRepository
    {
        private StorefrontDBContext _context;
        public StoreRepository(StorefrontDBContext p_context)
        {
            _context = p_context;
        }
        public Storefront AddStore(Storefront store)
        {
            _context.Storefronts.Add(store);
            _context.SaveChanges();
            return store; 
        }

        public List<Storefront> GetAllStores()
        {
            return _context.Storefronts.Select(store => store).ToList();
        }

        public Storefront GetStorefront(int id)
        {
            Storefront chosen = new Storefront();
            foreach(Storefront store in this.GetAllStores()){
                if (store.ID == id){
                    chosen = store;
                }
            }
            return chosen;
        }
        public LineItem Replenish(LineItem item, int amt){
            LineItemRepository lines = new LineItemRepository(_context);
                lines.UpdateLineItem(item, amt);
            return item;
            }
    }
}
