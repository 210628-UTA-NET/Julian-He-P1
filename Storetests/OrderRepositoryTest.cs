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
    public class OrderRepositoryTest
    {
        private readonly DbContextOptions<StorefrontDBContext> _options;

        //Constructors in unit test will always run before a test case
        public OrderRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<StorefrontDBContext>().UseSqlite("Filename = OrderTest.db").Options;
            this.Seed();
        }
        [Fact]
        public void GetAllOrderShouldGetAllOrder()
        {
            using (var context = new StorefrontDBContext(_options))
            {
                //Arrange
                IOrderRepository repo = new OrderRepository(context);
                List<Order> orders;

                //Act
                orders = repo.GetAllOrders();

                //Assert
                Assert.NotNull(orders);
                Assert.Equal(2,orders.Count);
            }
        }
        
         private void Seed()
        {
            using (var context = new StorefrontDBContext(_options))
            {
                //We want to make sure our inmemory database gets deleted everytime before another test case runs it
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Storefronts.AddRange(new Storefront
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
                    });

                context.Customers.AddRange(new Customer
                    {
                        ID=1,
                        Name = "Macintosh",
                        Address = "Houston",
                        Email = "HttpStyleUriParser@gmail.com",
                        Phone = "0123456789",
                    },
                    new Customer
                    {
                        ID=2,
                        Name = "Nvidia",
                        Address = "Venice",
                        Email = "there@gmail.com",
                        Phone = "1234567890",
                    }
                );

                context.Orders.AddRange(
                    new Order
                    {
                        OrderID=1,
                        StorefrontID= 1,
                        CustomerID = 1,
                        TotalPrice = 350.50,
                        Date = "01/01/2020",
                    },
                    new Order
                    {
                        OrderID=2,
                        StorefrontID= 1,
                        CustomerID = 1,
                        TotalPrice = 250,
                        Date = "12/31/1999",
                    });
                
                context.SaveChanges();
            }
    }   }
}
