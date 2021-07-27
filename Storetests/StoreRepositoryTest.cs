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
    public class StoreRepositoryTest
    {
        private readonly DbContextOptions<StorefrontDBContext> _options;

        //Constructors in unit test will always run before a test case
        public StoreRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<StorefrontDBContext>().UseSqlite("Filename = StoreTest.db").Options;
            this.Seed();
        }
        [Fact]
        public void GetAllStoreShouldGetAllStore()
        {
            using (var context = new StorefrontDBContext(_options))
            {
                //Arrange
                IStoreRepository repo = new StoreRepository(context);
                List<Storefront> stores;

                //Act
                stores = repo.GetAllStores();

                //Assert
                Assert.NotNull(stores);
                Assert.Equal(2,stores.Count);
            }
        }

        [Fact]
        public void GetStoreIDShouldGetAStoreByID(){
            using(var context = new StorefrontDBContext(_options)){
                IStoreRepository repo = new StoreRepository(context);
                Storefront store;
                store= repo.GetStorefront(1);

                Assert.NotNull(store);
                Assert.Equal("Macintosh", store.Name);
            }
        }
        [Fact]
        public void ReplenishShouldAddtoQuantity(){
            using(var context = new StorefrontDBContext(_options)){
                IStoreRepository repo = new StoreRepository(context);
                LineItem line;
                LineItem linetoreplace = new LineItem();
                Product prod = new Product{
                        ID= 1,
                        Name = "Milk",
                        Price=4.50
                    };
                linetoreplace.ProductName = prod;
                linetoreplace.ID = 1;
                linetoreplace.Quantity = 200;
                linetoreplace.StorefrontID = 1;
                repo.Replenish(linetoreplace);
                ILineItemRepository Linerepo = new LineItemRepository(context);
                line = Linerepo.GetLineItem(1);
                Assert.NotNull(line);
                Assert.Equal(200, line.Quantity);
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

                context.Storefronts.AddRange(
                    new Storefront
                    {
                        ID = 1,
                        Name = "Macintosh",
                        Address = "Houston",
                    },
                    new Storefront
                    {
                        ID=2,
                        Name = "Nvidia",
                        Address = "Venice",
                    }
                );
                context.LineItems.AddRange(
                    new LineItem{
                        ID = 1,
                        Quantity=5,
                        StorefrontID= 1,
                        ProductName = prod
                    }
                );
                context.Products.AddRange(
                    prod
                );

                context.SaveChanges();
            }
    }   }
}