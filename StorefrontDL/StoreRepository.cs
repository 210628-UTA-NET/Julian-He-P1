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
            return _context.Storefronts.Find(id);
        }
        public LineItem Replenish(LineItem item){
            LineItemRepository lines = new LineItemRepository(_context);
                lines.UpdateLineItem(item);
            return item;
            }
    }
}
