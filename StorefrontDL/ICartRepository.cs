using StorefrontModels;
using System.Collections.Generic;
using System;
namespace StorefrontDL{
    public interface ICartRepository{

        ///returns all customers in repo
        List<Cart> GetAllCarts();
        Cart GetCart(int i);
        Cart AddCart(Cart cart);
        void RemoveCart(Cart cart);
    }
}