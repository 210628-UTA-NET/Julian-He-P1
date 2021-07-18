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
    public class StorefrontVM
    {
        public StorefrontVM()
        { }

        public StorefrontVM(Storefront p_rest)
        {
            ID = p_rest.ID;
            Address = p_rest.Address;
            Name = p_rest.Name;
            Inventory = p_rest.Inventory;
            Orders = p_rest.Orders;
        }

        public int ID { get; set; }

        //This is a data annotation that helps with validation
        [Required]
        public string Address { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public List<LineItem> Inventory { get; set; }

        [Required]

        public List<Order> Orders { get; set; }

        public string Email{get; set;}
    }
}