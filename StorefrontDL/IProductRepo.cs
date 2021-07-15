using StorefrontModels;
using System.Collections.Generic;
using System;
namespace StorefrontDL{
    public interface IProductRepository{

        ///returns all customers in repo
        List<Product> GetAllProducts();
        Product GetProduct(Product product);
        Product AddProduct(Product product);
    }
}