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
                Assert.Equal(store.Name, "Macintosh");
            }
        }
         private void Seed()
        {
            using (var context = new StorefrontDBContext(_options))
            {
                //We want ot make sure our inmemory database gets deleted everytime before another test case runs it
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Storefronts.AddRange(
                    new Storefront
                    {
                        Name = "Macintosh",
                        Address = "Houston",
                    },
                    new Storefront
                    {
                        Name = "Nvidia",
                        Address = "Venice",
                    }
                );

                context.SaveChanges();
            }
    }   }
}