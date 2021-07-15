using Models = StorefrontModels;
using System.Collections.Generic;
using System.Linq;
using Entity = StorefrontDL.Entities;
namespace StorefrontDL{
    public class ProductRepository : IProductRepository
        {
        private Entities.P0DBContext _context;
        private string _jsonString;
        public ProductRepository(Entity.P0DBContext p_context){
            _context = p_context;
        }
        public Models.Product AddProduct(Models.Product product)
        {
            _context.Products.Add(new Entity.Product{
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
                Description = product.Desc
            });
            _context.SaveChanges();
            return product;

        }

        public List<Models.Product> GetAllProducts()
        {
            return _context.Products.Select(prod => new Models.Product(){
                                                Name = prod.Name,
                                                Price = (double) prod.Price,
                                                ID = prod.Id,
                                                Category = prod.Category,
                                                Desc = prod.Description
            }).ToList();
        }

        public Models.Product GetProduct(Models.Product product)
        {
            List<Models.Product> prods = this.GetAllProducts();
            var queryRes = (from res in prods
                                    where res.ID == product.ID
                                    select res);
            List<Models.Product> found = new List<Models.Product>();
            return found[0];
        }
    }
}