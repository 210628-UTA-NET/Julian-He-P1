using System.Collections.Generic;
using StorefrontModels;
using System.Linq;

using Microsoft.EntityFrameworkCore;


namespace StorefrontDL{
    public class OrderRepository : IOrderRepository
    {
        private StorefrontDBContext _context;
        public OrderRepository(StorefrontDBContext p_context){
            _context = p_context;
        }
        public Order AddOrder(Order order)
        {
            _context.Orders.Add(order);
            LineItemRepository linerepo = new LineItemRepository(_context);
            foreach (LineItem line in order.Items){
                linerepo.AddLineItem(line);
            }
            _context.SaveChanges();
            return order;
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.Select(orders => orders).ToList();
        }

        public Order GetOrder(int id){
            Order order = _context.Orders.Find(id);
            LineItemRepository line = new LineItemRepository(_context);
            order.Items = line.GetOrderItems(id);
            return order;
        }  
        public List<Order> GetStoreOrder(int storeID)
        {
            return _context.Orders.Select(orders => orders).Where(store => store.StorefrontID == storeID).ToList();
        }
        public List<Order> GetCustomerOrder(int customerID){
            return this.GetAllOrders().Where(order => order.CustomerID == customerID).ToList();
        }

        public void PlaceOrder(Order order, List<LineItem> listItems)
        {
            throw new System.NotImplementedException();
        }
        /*         public void PlaceOrder(Order order, List<LineItem> listItems){
            this.AddOrder(order);
           LineItemRepository linerepo = new LineItemRepository(_context);
           foreach(LineItem line in listItems){
               foreach(LineItem item in order.Items){
                   if(item.ProductName.ID == line.ProductName.ID){
                   linerepo.UpdateLineItem(line , -item.Quantity);}}

           }
           _context.SaveChanges();
       } */
    }
}