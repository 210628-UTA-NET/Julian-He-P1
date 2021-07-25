using StorefrontModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StorefrontUI2.Models
{
    /*
     * Holds the data structure of the models that you will want to use in your views
     */
    public class CartVM
    {
        public CartVM()
        { }

        public CartVM(Cart p_cart)
        {
            ID = p_cart.ID;
            CustomerID = p_cart.CustomerID;
            StorefrontID = p_cart.StorefrontID;
            Cost = p_cart.Cost;
        }

        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int StorefrontID{get; set;}
        public double Cost { get; set; }

    }
}