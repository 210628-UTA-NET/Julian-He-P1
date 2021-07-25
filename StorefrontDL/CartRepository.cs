using System;
using System.Collections.Generic;
using System.IO;
using StorefrontModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StorefrontDL{
    public class CartRepository : ICartRepository{
        private StorefrontDBContext  _context;
        //Constructor
        public CartRepository(StorefrontDBContext p_context){
            _context= p_context;
        }

        //Add an item to the carts table
        public Cart AddCart(Cart cart)
        {   
            _context.Carts.Add(cart);
            _context.SaveChanges();
            return cart;
        }

        //gets all carts
        public List<Cart> GetAllCarts()
        {
            return _context.Carts.Select(cart=> cart).ToList();
        }
        public List<Cart> GetCart(int id){
            return _context.Carts.Where(cart => cart.CustomerID == id).Select(cart=> cart).ToList();
        }

        public void RemoveCart(Cart cart)
        {
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }
    }
}
