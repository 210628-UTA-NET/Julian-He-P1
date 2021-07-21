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
            return _context.Orders.Select(orders => orders).ToList();
        }

        public List<Order> GetStoreOrder(int storeID, string AscOrDesc, string PriceOrDate)
        {
            if (PriceOrDate == "Price"){
                if (AscOrDesc == "Asc"){
                    return _context.Orders.Where(store => store.StorefrontID == storeID).OrderBy(store=> store.TotalPrice).ToList();
                }
                else{
                    return _context.Orders.Where(store => store.StorefrontID == storeID).OrderByDescending(store=> store.TotalPrice).ToList();
                }
            }
            else{
                if(AscOrDesc == "Asc"){
                    return _context.Orders.Where(store=>store.StorefrontID == storeID).OrderBy(store => store.Date).ToList();
                }
                else{
                    return _context.Orders.Where(store=>store.StorefrontID == storeID).OrderByDescending(store => store.Date).ToList();
                }
                
            }
        }
        public List<Order> GetCustomerOrder(int customerID){
            return this.GetAllOrders().Where(order => order.CustomerID == customerID).ToList();
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