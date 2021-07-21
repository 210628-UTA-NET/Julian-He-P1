using StorefrontModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StorefrontUI2.Models{
    public class OrderVM{
        public OrderVM(){}

        public OrderVM(Order order){
            ID = order.OrderID;
            CustomerID = order.CustomerID;
            StorefrontID  = order.StorefrontID;
            TotalPrice = order.TotalPrice;
            Items = order.Items;
            Date = order.Date;

        }

        public int ID { get; set; }

        public int CustomerID { get; set; }
        public int StorefrontID { get; set; }

        public double TotalPrice{get; set;}

        public List<LineItem> Items{get; set;}

        public string Date { get; set; }
    }


}