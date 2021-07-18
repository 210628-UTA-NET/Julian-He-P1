using System;
using System.Collections.Generic;

namespace StorefrontModels
{
    public class Customer
    {
        private string _name;
        private string _address;
        private List<Order> _orders = new List<Order>();
        private string _email;
        private string _phone;
        private int _customerID;

        public Customer(){
        }

        public string Name { 
            get
            {
                return _name;
            } 
            set
            {
                _name= value;
            } 
        }
        public string Address { 
            get{
                return _address;
            } 
            set{
                _address=value;
            }
             }
        public List<Order> Orders { 
            get{
                return _orders;
            }
             set{
                _orders = value;
            } 
        }
        public int ID{
            get{return _customerID;}
            set{_customerID = value;}
        }

        public string Email { 
            get{
            return _email;
            }
            set{
            _email=value;
         } }
        public string Phone { 
            get{
                return _phone;

        } 
        set{
                _phone = value;
        } }
    }
}

