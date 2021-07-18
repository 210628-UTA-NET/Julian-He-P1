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
            _context.SaveChanges();
            return order;
        }

        public List<Order> GetAllOrders()
        {
            var result = _context.Orders.Include(o => o.Items).ToList();
            List<Order> list = new List<Order>();
            foreach (var res in result){
                    Order order = new Order();
                    order.OrderID = res.OrderID;
                    order.CustomerID = (int)res.CustomerID;
                    order.Location = (int)res.Location;
                    order.TotalPrice = (double) res.TotalPrice;
                    LineItemRepository lineItem = new LineItemRepository(_context);
                    order.Items = lineItem.GetLineItem("order", order.OrderID);
                    list.Add(order);
            }
            return list;
        }

        public List<Order> GetStoreOrder(int storeID)
        {
            List<Order> orders = this.GetAllOrders();
            var queryRes = (from res in orders
                                where res.Location == storeID
                                select res);
            List<Order> ordered = new List<Order>();
            foreach(var result in queryRes){
                ordered.Add(result);
            }
            return ordered;
        }
        public List<Order> GetCustomerOrder(int customerID){
            List<Order> orders = this.GetAllOrders();
            var queryRes = (from res in orders
                                where res.CustomerID == customerID
                                select res);
            List<Order> ordered = new List<Order>();
            foreach(var result in queryRes){
                ordered.Add(result);
            }
            return ordered;
        }
        public void PlaceOrder(Order order, List<LineItem> listItems){
             this.AddOrder(order);
            LineItemRepository linerepo = new LineItemRepository(_context);
            foreach(LineItem line in listItems){
                foreach(LineItem item in order.Items){
                    if(item.ProductName.ID == line.ProductName.ID){
                    linerepo.UpdateLineItem(line , -item.Quantity);}}

            }
            _context.SaveChanges();
        }
    }
}