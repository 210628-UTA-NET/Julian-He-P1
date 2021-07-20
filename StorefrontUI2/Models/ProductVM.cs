using StorefrontModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StorefrontUI2.Models
{
    public class ProductVM{
    public ProductVM(){}

    public ProductVM(Product p_prod){
            ID = p_prod.ID;
            Price = p_prod.Price;
            Name = p_prod.Name;
            Desc = p_prod.Desc;
            Category = p_prod.Category;
    }
            public int ID { get; set; }


        //This is a data annotation that helps with validation
        [Required]
        public double Price { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Desc{get; set;}
        [Required]
        public string Category{get; set;}
    }

}