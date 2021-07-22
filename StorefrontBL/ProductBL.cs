using System.Collections.Generic;
using StorefrontDL;
using StorefrontModels;

namespace StorefrontBL
{
    public class ProductBL : IProductBL
    {
        /// <summary>
        /// We are defining the dependenices this class needs in the constructor
        /// We do it this way (with interfaces) because we can easily switch out the implementation of RRDL when we want to change data source 
        /// (change from file system into database stored in the cloud)
        /// </summary>
        private IProductRepository _repo;
        public ProductBL(IProductRepository p_repo)
        {
            _repo = p_repo;
        }


        public Product AddProduct(Product p_product)
        {
            return _repo.AddProduct(p_product);
        }

        public List<Product> GetAllProduct()
        {
           return _repo.GetAllProducts();
        }

        public Product GetProduct(int id)
        {
            return _repo.GetProduct(id);
        }

    }
}