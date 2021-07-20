using StorefrontModels;
using System.Collections.Generic;
using System;
namespace StorefrontDL{
    public interface IOrderRepository{

        ///returns all customers in repo
        List<Order> GetAllOrders();
        Order AddOrder(Order order);
        List<Order> GetStoreOrder(int ints, string AscOrDesc, string PriceOrDate);
        List<Order> GetCustomerOrder(int ints);

        void PlaceOrder(Order order, List<LineItem> listItems);
    }
}