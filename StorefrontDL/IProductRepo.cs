using StorefrontModels;
using System.Collections.Generic;
using System;
namespace StorefrontDL{
    public interface IProductRepository{

        ///returns all customers in repo
        List<Product> GetAllProducts();
        Product GetProduct(int id);
        Product AddProduct(Product product);
    }
}