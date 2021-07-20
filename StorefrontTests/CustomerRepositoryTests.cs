using System;
using Xunit;
using System.Linq;
using System.Text;
using StorefrontDL;
using StorefrontBL;
using Microsoft.EntityFrameworkCore;
using StorefrontModels;
using System.Collections.Generic;


namespace StorefrontTests
{
    public class CustomerRepositoryTest
    {
        private readonly DbContextOptions<StorefrontDBContext> _options;

        //Constructors in unit test will always run before a test case
        public CustomerRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<StorefrontDBContext>().UseSqlite("Filename = Test.db").Options;
            this.Seed();
        }
        [Fact]
        public void GetAllCustomerShouldGetAllCustomer()
        {
            using (var context = new StorefrontDBContext(_options))
            {
                //Arrange
                ICustomerRepository repo = new CustomerRepository(context);
                List<Customer> customers;

                //Act
                customers = repo.GetAllCustomers();

                //Assert
                Assert.NotNull(customers);
                Assert.Equal(2,customers.Count);
            }
        }
         private void Seed()
        {
            using (var context = new StorefrontDBContext(_options))
            {
                //We want ot make sure our inmemory database gets deleted everytime before another test case runs it
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Customers.AddRange(
                    new Customer
                    {
                        Name = "Macintosh",
                        Address = "Houston",
                        Email = "HttpStyleUriParser@gmail.com",
                        Phone = "0123456789",
                    },
                    new Customer
                    {
                        Name = "Nvidia",
                        Address = "Venice",
                        Email = "there@gmail.com",
                        Phone = "1234567890",
                    }
                );

                context.SaveChanges();
            }
    }   }
}
