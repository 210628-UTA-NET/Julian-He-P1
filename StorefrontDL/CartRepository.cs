using System;
using System.Collections.Generic;
using System.IO;
using StorefrontModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StorefrontDL{
    public class CartRepository : ICartRepository{
        private StorefrontDBContext  _context;
        public CartRepository(StorefrontDBContext p_context){
            _context= p_context;
        }

        public LineItem AddCartItem(LineItem lineitem, int customerID, int storeID)
        {   
            Cart cart = new Cart();
            cart.CustomerID= customerID;
            cart.StorefrontID = storeID;
            _context.Carts.Add(cart);
            lineitem.CartID = 1;
            _context.LineItems.Add(lineitem);
            _context.SaveChanges();
            return lineitem;
        }

        public List<LineItem> GetAllCartItems()
        {
            return _context.LineItems.Where(cart => cart.CartID == 1).ToList();
        }

        public LineItem GetCartItem(int i)
        {
            throw new NotImplementedException();
        }

        public LineItem UpdateLineItem(LineItem lineitem, int amt)
        {
            throw new NotImplementedException();
        }
    }
}
