using System.Collections.Generic;
using StorefrontModels;
using StorefrontDL;

namespace StorefrontBL{

    public class CartBL : ICartBL
    {
        ICartRepository _repo;
        public CartBL(ICartRepository p_repo){
            _repo = p_repo;
        }

        public Cart AddCart(Cart cart)
        {
            return _repo.AddCart(cart);
        }

        public Cart GetCart(int Customerid)
        {
            return _repo.GetCart(Customerid);
        }
        public List<Cart> GetAllCarts(){
            return _repo.GetAllCarts();
        }

        public void RemoveCart(Cart cart)
        {
            _repo.RemoveCart(cart);
        }
                public Cart GetCartByID(int CartID){
            return _repo.GetCartByID(CartID);
        }
    }
}