using System.Collections.Generic;
using StorefrontModels;

using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;


namespace StorefrontDL{

    public class LineItemRepository : ILineItemRepository{
        private StorefrontDBContext _context;
        public LineItemRepository(StorefrontDBContext p_context){
            _context = p_context;
        }

        public LineItem AddLineItem(LineItem lineitem)
        {
            _context.Add(lineitem);
            _context.SaveChanges();
            return lineitem;
        }

        public List<LineItem> GetAllLineItems()
        {
            return _context.LineItems.Select(line => line).ToList();
        }

        public List<LineItem> GetInventory( int id)
        {
           return _context.LineItems.Where(line => line.StorefrontID== id).Include(line=> line.ProductName).ToList();
        }

        public LineItem GetLineItem(int id)
        {
            return _context.LineItems.Find(id);
        }

        public List<LineItem> GetOrderItems(int id){

            return _context.LineItems.Where(line => line.OrderID==id).Include(l => l.ProductName).ToList();
        }
        public LineItem UpdateLineItem(LineItem item, int amt){
            var result = _context.LineItems.Where(p => p.ID == item.ID);
            foreach(var res in result){
                res.Quantity += amt;
            }
            _context.SaveChanges();
            return item;
        }
    }
}