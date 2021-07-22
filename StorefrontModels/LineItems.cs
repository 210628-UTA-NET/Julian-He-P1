using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StorefrontModels
{
    public class LineItem{
        private int _quantity;
        private Product _product;
        private int? _orderID;
        private int? _storefrontID;
        private int _id;
        public LineItem(){
        }
        public int Quantity{
                get{
                    return _quantity;
                }
                set{
                    if (value <0){
                        throw new Exception("Quantity cannot be less than 0");
                    }
                    else{
                    
                    _quantity = value;
                    }
                }
            }
        public Product ProductName{
            get{
                return _product;
            }
            set{
                _product = value;
            }
        }
        public int? OrderID{
            get{return _orderID;}
            set{_orderID = value;}
        }
        public int? StorefrontID{
            get{return _storefrontID;}
            set{_storefrontID = value;}
        }
        public int? CartID{
            get; set;}
        public int ID{
            get{return _id;}
            set{_id = value;}
        }
    }
}