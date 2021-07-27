using System;
using Xunit;
using System.Linq;
using System.Text;
using StorefrontDL;
using StorefrontBL;
using Microsoft.EntityFrameworkCore;
using StorefrontModels;
using System.Collections.Generic;


namespace Storetests
{
    public class ProductRepositoryTest
    {
        private readonly DbContextOptions<StorefrontDBContext> _options;

        //Constructors in unit test will always run before a test case
        public ProductRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<StorefrontDBContext>().UseSqlite("Filename = ProductTest.db").Options;
            this.Seed();
        }

        /// <summary>
        /// A test for add lineitem
        /// </summary>
        [Fact]
        public void AddProductToShouldAddProduct(){
            using(var context = new StorefrontDBContext(_options)){
                IProductRepository repo = new ProductRepository(context);
                Product prod;
                Product newprod = new Product();
                newprod.Name = "Pizza";
                newprod.Price = 5;
                prod = repo.AddProduct(newprod);
                Assert.NotNull(prod);
                Assert.Equal("Pizza", prod.Name);
            }
        }

        /// <summary>
        /// Test to get all products
        /// </summary>
        [Fact]
        public void GetAllProductsShouldGetAllProducts(){
            using(var context = new StorefrontDBContext(_options)){
                IProductRepository repo = new ProductRepository(context);
                List<Product> products;
                products = repo.GetAllProducts();
                
                Assert.NotNull(products);
                Assert.Equal(1, products.Count);

            }
        }

        /// <summary>
        ///    Test to get 1 product
        /// </summary>
        [Fact]
        public void GetProductShouldGetOneProduct(){
            using(var context = new StorefrontDBContext(_options)){
                IProductRepository repo = new ProductRepository(context);
                Product product;
                product = repo.GetProduct(1);
                Assert.NotNull(product);
                Assert.Equal("Milk", product.Name);
            }
        }
        private void Seed()
        {
            using (var context = new StorefrontDBContext(_options))
            {
                //We want ot make sure our inmemory database gets deleted everytime before another test case runs it
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                Product prod = new Product();
                    prod.Name = "Milk";
                    prod.Price = 4.50;
                    prod.ID = 1;

                context.Products.AddRange(
                    prod
                );

                context.SaveChanges();
            }
    }   }
}