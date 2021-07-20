using StorefrontModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StorefrontUI2.Models{
    public class LineItemVM{
        public LineItemVM(){
        }
        public LineItemVM(LineItem lineItem){
            ID = lineItem.ID;
            StoreID = (int)lineItem.StoreID;
            ProductName = lineItem.ProductName;
            OrderID = (int)lineItem.OrderID;
            Quantity = lineItem.Quantity;
        }

        public int ID  { get; set; }
        public int StoreID { get; set; }
        public int OrderID { get; set; }
        public int Quantity { get; set; }
        public Product ProductName{get; set;}
    }
}