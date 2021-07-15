using System;
using System.Text.RegularExpressions;

namespace StorefrontModels{
    public class Product{
        private int _id;
        private string _name;
        private double _price;
        private string _desc = "No description found";
        private string _category = "No category set";

        public Product(){

        }
        public string Name { 
            get{
            return _name;
            } 
            set{

            if (!Regex.IsMatch(value, @"^[A-Za-z .]+$")){
                throw new Exception("Products cannot have numbers");
            }
            _name = value;
            } 
        }

        public double Price{
            get{
                return _price;
            }
            set{
                if (value < 0){
                    throw new ArithmeticException("Value cannot be negative");
                }
                _price=value;
            }
        }
        public string Desc{
            get{
                return _desc;
            }
            set{
                _desc=value;
            }
        }
        public string Category{
            get{
                return _category;
            }
            set{
                _category = value;
            }
        }
        public int ID { get{return _id;} set{_id = value;} }

    }
}