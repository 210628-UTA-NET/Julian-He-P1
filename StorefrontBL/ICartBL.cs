using System;
using System.Collections.Generic;
using StorefrontModels;

namespace StorefrontBL
{
    /// <summary>
    /// Handles all the business logic for the restaurant model
    /// They are in charge of further processing/ sanitizing/ further validation of data
    /// Any logic that is used to access the data is for the DL, anything else will be in BL
    /// </summary>
    public interface ICartBL
    {
        /// <summary>
        /// Gets all the restaurant from the database
        /// </summary>
        /// <returns>Returns a list of restaurants</returns>

        Cart GetCart(int id);

        Cart AddCart(Cart p_cart);

        List<Cart> GetAllCarts();

        void RemoveCart(Cart cart);

    }
}