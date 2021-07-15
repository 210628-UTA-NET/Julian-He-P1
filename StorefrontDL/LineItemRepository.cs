using System.Collections.Generic;
using Models = StorefrontModels;
using Entity= StorefrontDL.Entities;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;


namespace StorefrontDL{

    public class LineItemRepository : ILineItemRepository{
        private Entities.P0DBContext _context;
        public LineItemRepository(Entity.P0DBContext p_context){
            _context = p_context;
        }

        public Models.LineItem AddLineItem(Models.LineItem lineitem)
        {
            _context.Add(new Entity.LineItem{
                                StoreId = lineitem.StoreID, 
                                Quantity = lineitem.Quantity,
                                ProdId = lineitem.ProductName.ID,
                                OrderId = lineitem.OrderID

            });
            _context.SaveChanges();
            return lineitem;
        }

        public List<Models.LineItem> GetAllLineItems()
        {
        
            var result =  _context.LineItems.Include(p=> p.Prod).ToList();
            List<Models.LineItem> list = new List<Models.LineItem>();
            foreach (var res in result){
                    Models.LineItem lineItem = new Models.LineItem();
                    Models.Product product = new Models.Product();
                    lineItem.ID = res.ListId;
                    lineItem.OrderID = res.OrderId;
                    lineItem.StoreID = res.StoreId;
                    lineItem.Quantity = (int) res.Quantity;
                    product.ID = res.Prod.Id;
                    product.Name = res.Prod.Name;
                    product.Price = (double)res.Prod.Price;
                    product.Category = res.Prod.Category;
                    product.Desc = res.Prod.Description;
                    lineItem.ProductName = product;
                    list.Add(lineItem);
            }
            return list;
        }

        public List<Models.LineItem> GetLineItem(string type, int id)
        {
            List<Models.LineItem> returnList = new List<Models.LineItem>();
            List<Models.LineItem> allitems = this.GetAllLineItems();
            foreach(Models.LineItem lineitem in allitems){
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

        public List<Models.LineItem> GetInventory( int id)
        {
            var result = _context.LineItems.Where(b => b.StoreId == id).Include(p => p.Prod).ToList();
            List<Models.LineItem> list = new List<Models.LineItem>();
            foreach(var item in result){
                Models.LineItem lineItem = new Models.LineItem();
                Models.Product product = new Models.Product();
                lineItem.ID = item.ListId;
                lineItem.Quantity = (int)item.Quantity;
                lineItem.StoreID = item.StoreId;
                product.Name = item.Prod.Name;
                product.Price = (double)item.Prod.Price;
                product.Category = item.Prod.Category;
                product.Desc = item.Prod.Description;
                lineItem.ProductName = product;
                list.Add(lineItem);
            }
            return list;
        }


        public object GetOrderItems(int id){
            var result = _context.LineItems.Where(b => b.StoreId == id).Include(p => p.ProdId).ToList();
            List<Models.LineItem> list = new List<Models.LineItem>();
            foreach(var item in result){
                Models.LineItem lineItem = new Models.LineItem();
                Models.Product product = new Models.Product();
                lineItem.ID = item.ListId;
                lineItem.Quantity = (int)item.Quantity;
                lineItem.StoreID = item.StoreId;
                product.Name = item.Prod.Name;
                product.Price = (double)item.Prod.Price;
                product.Category = item.Prod.Category;
                product.Desc = item.Prod.Description;
                lineItem.ProductName = product;
                list.Add(lineItem);
            }
            return list;
        }
        public Models.LineItem UpdateLineItem(Models.LineItem item, int amt){
            var result = _context.LineItems.Where(p => p.ListId == item.ID);
            foreach(var res in result){
                res.Quantity += amt;
            }
            _context.SaveChanges();
            return item;
        }
    }
}