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
    public class LineItemRepositoryTest
    {
        private readonly DbContextOptions<StorefrontDBContext> _options;

        //Constructors in unit test will always run before a test case
        public LineItemRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<StorefrontDBContext>().UseSqlite("Filename = LineItemTest.db").Options;
            this.Seed();
        }

        /// <summary>
        /// A test for add lineitem
        /// </summary>
        [Fact]
        public void AddLineItemToStoreShouldAddLineItem(){
            using(var context = new StorefrontDBContext(_options)){
                ILineItemRepository repo = new LineItemRepository(context);
                LineItem lineItem;
                Product newprod = new Product();
                newprod.Name = "Pizza";
                newprod.Price = 5;
                LineItem line = new LineItem();
                line.ID = 4;
                line.StorefrontID = 1;
                line.Quantity = 5;
                line.ProductName = newprod; 
                LineItem lineend = repo.AddLineItem(line);
                Assert.NotNull(lineend);
                Assert.Equal("Pizza", lineend.ProductName.Name);
            }
        }

        /// <summary>
        /// Test to get all line items
        /// </summary>
        [Fact]
        public void GetAllLineItemShouldGetAllLineItems(){
            using(var context = new StorefrontDBContext(_options)){
                ILineItemRepository repo = new LineItemRepository(context);
                List<LineItem> lines;
                lines = repo.GetAllLineItems();
                
                Assert.NotNull(lines);
                Assert.Equal(3, lines.Count);

            }
        }

        /// <summary>
        /// A test for getting inventory
        /// </summary>
        [Fact]
        public void GetInventoryShouldGetStoreInventory(){
            using(var context = new StorefrontDBContext(_options)){
                ILineItemRepository repo = new LineItemRepository(context);
                List<LineItem> lines;
                lines= repo.GetInventory(1);
                Assert.NotNull(lines);
                Assert.Equal(1, lines.Count);
            }
        }

        /// <summary>
        /// test for one line item
        /// </summary>
        [Fact]
        public void GetLineItemShouldGetOneLineItem(){
            using(var context = new StorefrontDBContext(_options)){
                ILineItemRepository repo = new LineItemRepository(context);
                LineItem line;
                line = repo.GetLineItem(1);
                Assert.NotNull(line);
                Assert.Equal(5, line.Quantity);
            }
        }
        /// <summary>
        /// A test for get order items
        /// </summary>
        [Fact]
        public void GetOrderItemsShouldReturnItems(){
            using(var context = new StorefrontDBContext(_options)){
                ILineItemRepository repo = new LineItemRepository(context);
                List<LineItem> lines;
                lines=repo.GetOrderItems(1);
                Assert.NotNull(lines);
                Assert.Equal(1, lines.Count);
            }
        }
        
        /// <summary>
        /// A test for 
        /// </summary>
        [Fact]
        public void RemoveCartItemsShouldRemoveCartItems(){
            using (var context = new StorefrontDBContext(_options)){
                ILineItemRepository repo = new LineItemRepository(context);
                repo.RemoveCartItems(1);
                List<LineItem> lines = repo.GetAllLineItems();
                Assert.Equal(2, lines.Count);
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
                    }
                );
                context.LineItems.AddRange(
                    new LineItem{
                        ID = 1,
                        Quantity=5,
                        OrderID= 1,
                        ProductName = prod
                    },
                    new LineItem{
                        ID = 2,
                        Quantity=4,
                        StorefrontID=1,
                        ProductName=prod
                    },
                    new LineItem{
                        ID=3,
                        Quantity=3,
                        CartID = 1,
                        ProductName=prod
                    }
                );
                context.Carts.AddRange(
                    new Cart{
                        ID=1,
                        CustomerID=1,
                        StorefrontID=1
                    }
                );
                context.Customers.AddRange(
                    new Customer{
                        ID= 1,
                        Name = "Johnson",
                        Address="SomeAddress",
                        Phone="0123456789",
                        Email = "Fakeremail@gmail.com"
                    }
                );
                context.Orders.AddRange(
                    new Order{
                        OrderID = 1,
                        StorefrontID = 1,
                        CustomerID=1,
                        Date = Convert.ToString(DateTime.Now),
                        TotalPrice=22.50
                    }
                );

                context.Products.AddRange(
                    prod
                );
                context.SaveChanges();
            }
    }   }
}
