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
            return _context.LineItems.Select(line => line).Include(l => l.ProductName).ToList();
        }

        public List<LineItem> GetInventory( int id)
        {
           return _context.LineItems.Where(line => line.StorefrontID== id).Include(line=> line.ProductName).ToList();
        }

        public LineItem GetLineItem(int id)
        {
            List<LineItem>lines  = _context.LineItems.Where(line => line.ID== id).Include(line=> line.ProductName).ToList();
            return lines[0];
        }

        public List<LineItem> GetOrderItems(int id){

            return _context.LineItems.Where(line => line.OrderID==id).Include(l => l.ProductName).ToList();
        }
        public LineItem UpdateLineItem(LineItem item){
            _context.LineItems.Update(item);
            _context.SaveChanges();
            return item;
        }

        public void RemoveCartItems(int id){
            List<LineItem> cartitems = _context.LineItems.Where(line=> line.CartID == id).Select(line=> line).ToList();
            foreach(LineItem line in cartitems){
                _context.LineItems.Remove(line);
            }
            _context.SaveChanges();
        }
    }
}