using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
            if(!Regex.IsMatch(value, @"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$")){
                throw new Exception("Phone Number cannot have Letters!");
                }
            if (value.Length >10 || value.Length<10){
                throw new Exception("Must have 10 numbers in phone number");
            }
            _phone = value; 
            } 
        }
    }
}

