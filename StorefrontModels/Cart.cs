using System;
using System.Collections.Generic;

namespace StorefrontModels
{
    public class Cart
    {
        public Cart(){

        }
        public List<LineItem> CartItems { get; set; }
        public int CustomerID { get; set; }
        public int StorefrontID {get; set;}
        public int ID {get; set;}
        public double Cost { get; set; }

    }
}