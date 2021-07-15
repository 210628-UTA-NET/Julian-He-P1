using System.Collections.Generic;
using Models = StorefrontModels;
using System.Linq;
using Entity = StorefrontDL.Entities;
using Microsoft.EntityFrameworkCore;


namespace StorefrontDL{
    public class OrderRepository : IOrderRepository
    {
        private Entities.P0DBContext _context;
        public OrderRepository(Entities.P0DBContext p_context){
            _context = p_context;
        }
        public Models.Order AddOrder(Models.Order order)
        {
            _context.Orders.Add(new Entity.Order(){
                                        Location = order.Location,
                                        Totalprice = order.TotalPrice,
                                        CustomerId = order.CustomerID
            });
            Entities.Product product = new Entity.Product();
            
            foreach(Models.LineItem line in order.Items){
                _context.LineItems.Add(new Entity.LineItem(){
                                            Quantity = line.Quantity,
                                            OrderId = line.OrderID,
                                            StoreId = line.StoreID,
                                            ProdId = line.ProductName.ID
                });
            }
            _context.SaveChanges();
            return order;
        }

        public List<Models.Order> GetAllOrders()
        {
            var result = _context.Orders.Include(o => o.LineItems).ToList();
            List<Models.Order> list = new List<Models.Order>();
            foreach (var res in result){
                    Models.Order order = new Models.Order();
                    order.OrderID = res.Id;
                    order.CustomerID = (int)res.CustomerId;
                    order.Location = (int)res.Location;
                    order.TotalPrice = (double) res.Totalprice;
                    LineItemRepository lineItem = new LineItemRepository(_context);
                    order.Items = lineItem.GetLineItem("order", order.OrderID);
                    list.Add(order);
            }
            return list;
        }

        public List<Models.Order> GetStoreOrder(int storeID)
        {
            List<Models.Order> orders = this.GetAllOrders();
            var queryRes = (from res in orders
                                where res.Location == storeID
                                select res);
            List<Models.Order> ordered = new List<Models.Order>();
            foreach(var result in queryRes){
                ordered.Add(result);
            }
            return ordered;
        }
        public List<Models.Order> GetCustomerOrder(int customerID){
            List<Models.Order> orders = this.GetAllOrders();
            var queryRes = (from res in orders
                                where res.CustomerID == customerID
                                select res);
            List<Models.Order> ordered = new List<Models.Order>();
            foreach(var result in queryRes){
                ordered.Add(result);
            }
            return ordered;
        }
        public void PlaceOrder(Models.Order order, List<Models.LineItem> listItems){
             _context.Orders.Add(new Entity.Order(){
                                        Location = order.Location,
                                        Totalprice = order.TotalPrice,
                                        CustomerId = order.CustomerID
                                        
            });
            LineItemRepository linerepo = new LineItemRepository(_context);
            foreach(Models.LineItem line in listItems){
                foreach(Models.LineItem item in order.Items){
                    if(item.ProductName.ID == line.ProductName.ID){
                    linerepo.UpdateLineItem(line , -item.Quantity);}}

            }
            _context.SaveChanges();
        }
    }
}