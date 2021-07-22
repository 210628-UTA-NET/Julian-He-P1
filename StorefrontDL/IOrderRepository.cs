using StorefrontModels;
using System.Collections.Generic;
using System;
namespace StorefrontDL{
    public interface IOrderRepository{

        ///returns all customers in repo
        List<Order> GetAllOrders();
        Order AddOrder(Order order);
        List<Order> GetStoreOrder(int ints);
        List<Order> GetCustomerOrder(int ints);
        Order GetOrder(int id);
        void PlaceOrder(Order order, List<LineItem> listItems);
    }
}