using System;
using System.Collections.Generic;

namespace StorefrontModels
{
    public class Customer
    {
        private string _name;
        private string _address;
        private List<Order> _orders = new List<Order>();
        private Dictionary<string, string> _emailPhone = new Dictionary<string, string>();
        private int _customerID;
        
        public Customer(){
            _emailPhone["Email"] = null;
            _emailPhone["Phone"] = null;
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
        public string EmailPhoneGet(string param){
            if (param == "Email"){
                
                return _emailPhone["Email"];
            }
            else if (param == "Phone"){
                return _emailPhone["Phone"];
            }
            else{
                throw new ArgumentException("Invalid Parameter");
            }
        }

        public void EmailPhoneSet(string param, string value){
            if (param == "Email"){
                _emailPhone["Email"] = value;
            }
            if (param == "Phone"){
                _emailPhone["Phone"] = value;
            }
        }
        public int ID{
            get{return _customerID;}
            set{_customerID = value;}
        }

        public string Email { get; set; }
        public string Phone { get; set; }
    }
}

