using System.Collections.Generic;
using StorefrontModels;

using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;


namespace StorefrontDL{

    public class LineItemRepository : ILineItemRepository{
        private StorefrontDBContext _context;
        public LineItemRepository(StorefrontDBContext p_context){
            _context = p_context;
        }

        public LineItem AddLineItem(LineItem lineitem)
        {
            _context.Add(lineitem);
            _context.SaveChanges();
            return lineitem;
        }

        public List<LineItem> GetAllLineItems()
        {
        
            var result =  _context.LineItems.Include(p=> p.ProductName).ToList();
            List<LineItem> list = new List<LineItem>();
            foreach (var res in result){
                    LineItem lineItem = new LineItem();
                    Product product = new Product();
                    lineItem.ID = res.ID;
                    lineItem.OrderID = res.OrderID;
                    lineItem.StoreID = res.StoreID;
                    lineItem.Quantity = (int) res.Quantity;
                    product.ID = res.ProductName.ID;
                    product.Name = res.ProductName.Name;
                    product.Price = (double)res.ProductName.Price;
                    product.Category = res.ProductName.Category;
                    product.Desc = res.ProductName.Desc;
                    lineItem.ProductName = product;
                    list.Add(lineItem);
            }
            return list;
        }

        public List<LineItem> GetLineItem(string type, int id)
        {
            List<LineItem> returnList = new List<LineItem>();
            List<LineItem> allitems = this.GetAllLineItems();
            foreach(LineItem lineitem in allitems){
                if (type == "store"){
                    if (lineitem.StoreID == id){
                        returnList.Add(lineitem);
                    }
                }
                else if(type == "order"){
                    if (lineitem.OrderID == id){
                        returnList.Add(lineitem);
                    }
                }
            }
            return returnList;
        }

        public List<LineItem> GetInventory( int id)
        {
            var result = _context.LineItems.Where(b => b.StoreID == id).Include(p => p.ProductName).ToList();
            List<LineItem> list = new List<LineItem>();
            foreach(var item in result){
                LineItem lineItem = new LineItem();
                Product product = new Product();
                lineItem.ID = item.ID;
                lineItem.Quantity = (int)item.Quantity;
                lineItem.StoreID = item.StoreID;
                product.Name = item.ProductName.Name;
                product.Price = (double)item.ProductName.Price;
                product.Category = item.ProductName.Category;
                product.Desc = item.ProductName.Desc;
                lineItem.ProductName = product;
                list.Add(lineItem);
            }
            return list;
        }


        public object GetOrderItems(int id){
            var result = _context.LineItems.Where(b => b.StoreID == id).Include(p => p.ProductName.ID).ToList();
            List<LineItem> list = new List<LineItem>();
            foreach(var item in result){
                LineItem lineItem = new LineItem();
                Product product = new Product();
                lineItem.ID = item.ID;
                lineItem.Quantity = (int)item.Quantity;
                lineItem.StoreID = item.StoreID;
                product.Name = item.ProductName.Name;
                product.Price = (double)item.ProductName.Price;
                product.Category = item.ProductName.Category;
                product.Desc = item.ProductName.Desc;
                lineItem.ProductName = product;
                list.Add(lineItem);
            }
            return list;
        }
        public LineItem UpdateLineItem(LineItem item, int amt){
            var result = _context.LineItems.Where(p => p.ID == item.ID);
            foreach(var res in result){
                res.Quantity += amt;
            }
            _context.SaveChanges();
            return item;
        }
    }
}