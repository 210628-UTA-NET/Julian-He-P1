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
    public interface IOrderBL
    {
        /// <summary>
        /// Gets all the restaurant from the database
        /// </summary>
        /// <returns>Returns a list of restaurants</returns>
        List<Order> GetAllOrder();

        Order AddOrder(Order p_order);

        List<Order> GetStoreOrder(int p_order);

        List<Order> GetCustomerOrder(int p_order);

        void PlaceOrder(Order order, List<LineItem> lineItems);
    }
}