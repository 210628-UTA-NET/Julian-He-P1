using StorefrontModels;
using System.Collections.Generic;
using System.Linq;
namespace StorefrontDL{
    public class ProductRepository : IProductRepository
        {
        private StorefrontDBContext _context;
        public ProductRepository(StorefrontDBContext p_context){
            _context = p_context;
        }
        public Product AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;

        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.Select(prod=> prod).ToList();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Find(id);
        }
    }
}