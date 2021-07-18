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
            return _context.Products.Select(prod => new Product(){
                                                Name = prod.Name,
                                                Price = (double) prod.Price,
                                                ID = prod.ID,
                                                Category = prod.Category,
                                                Desc = prod.Desc
            }).ToList();
        }

        public Product GetProduct(Product product)
        {
            List<Product> prods = this.GetAllProducts();
            var queryRes = (from res in prods
                                    where res.ID == product.ID
                                    select res);
            List<Product> found = new List<Product>();
            return found[0];
        }
    }
}