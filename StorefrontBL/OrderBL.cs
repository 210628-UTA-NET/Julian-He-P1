using System.Collections.Generic;
using StorefrontDL;
using StorefrontModels;

namespace StorefrontBL
{
    public class OrderBL : IOrderBL
    {
        /// <summary>
        /// We are defining the dependenices this class needs in the constructor
        /// We do it this way (with interfaces) because we can easily switch out the implementation of RRDL when we want to change data source 
        /// (change from file system into database stored in the cloud)
        /// </summary>
        private IOrderRepository _repo;
        public OrderBL(IOrderRepository p_repo)
        {
            _repo = p_repo;
        }


        public Order AddOrder(Order p_order)
        {
            return _repo.AddOrder(p_order);
        }

        public List<Order> GetAllOrder()
        {
           return _repo.GetAllOrders();
        }

        public List<Order> GetStoreOrder(int p_order, string AscOrDesc, string PriceOrDate)
        {
            return _repo.GetStoreOrder(p_order, AscOrDesc, PriceOrDate);
        }
        public List<Order> GetCustomerOrder(int p_order){
            return _repo.GetCustomerOrder(p_order);
        }
        public void PlaceOrder(Order order, List<LineItem> listItems){
            _repo.PlaceOrder(order, listItems);
        }
    }
}