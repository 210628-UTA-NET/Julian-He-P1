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
        public Cart GetCart(int id){
            return _context.Carts.Where(cart => cart.CustomerID == id).Include(c=> c.CartItems).ThenInclude(p=>p.ProductName).FirstOrDefault();
        }

        public void RemoveCart(Cart cart)
        {
            LineItemRepository repoLI = new LineItemRepository(_context);
            repoLI.RemoveCartItems(cart.ID);
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }

        public Cart GetCartByID(int CartID){
            return _context.Carts.Where(c => c.ID == CartID).Include(c=>c.CartItems).ThenInclude(p=>p.ProductName).FirstOrDefault();
        }
    }
}
