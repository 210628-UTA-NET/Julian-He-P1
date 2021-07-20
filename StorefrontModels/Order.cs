using System;
using System.Collections.Generic;

namespace StorefrontModels
{
    public class Order{
        private List<LineItem> _items = new List<LineItem>();
        private int _orderID;
        private int _location;
        private double _totalPrice;
        private int _customerID;
        private string _date;
        public Order(){}
        public double TotalPrice { get{
            return  _totalPrice;
        } set{
            _totalPrice = value;
        } }
        public int Location { get{
            return _location;
        } set{
            _location = value;
        } }
        public List<LineItem> Items {get{
            return _items;
        } set{
            _items=value;
        }}
        public int CustomerID {
            get{
                return _customerID;
            }
            set{
                _customerID = value;
            }
        }
        public int OrderID { get{return _orderID;} set{_orderID = value;} }

        public string Date {
            get{return _date;}
            set{_date= value;}
        }
    }
        

}