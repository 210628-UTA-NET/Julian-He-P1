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
    public class CustomerVM
    {
        public CustomerVM()
        { }

        public CustomerVM(Customer p_customer)
        {
            ID = p_customer.ID;
            Address = p_customer.Address;
            Name = p_customer.Name;
            Orders = p_customer.Orders;
            Email = p_customer.Email;
            Phone = p_customer.Phone;
            Orders = p_customer.Orders;
        }

        public int ID { get; set; }

        //This is a data annotation that helps with validation
        [Required]
        public string Address { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Order> Orders { get; set; }

        [Required]
        public string Email{get; set;}
        [Required]
        public string Phone{get; set;}
    }
}