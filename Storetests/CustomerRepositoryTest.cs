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
    public class CustomerRepositoryTest
    {
        private readonly DbContextOptions<StorefrontDBContext> _options;

        //Constructors in unit test will always run before a test case
        public CustomerRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<StorefrontDBContext>().UseSqlite("Filename = CustomerTest.db").Options;
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

        [Fact]
        public void GetCustomerIDShouldGetACustomerByID(){
            using(var context = new StorefrontDBContext(_options)){
                ICustomerRepository repo = new CustomerRepository(context);
                Customer customers;
                customers= repo.GetCustomer(1);

                Assert.NotNull(customers);
                Assert.Equal(customers.Name, "Macintosh");
            }
        }
        [Fact]
        public void AddCustomerShouldAddCustomer(){
            using(var context = new StorefrontDBContext(_options)){
                ICustomerRepository repo = new CustomerRepository(context);
                Customer customers;
                Customer customer = new Customer();
                customer.Name = "P Sherman";
                customer.Address = "32 Wallaby Way";
                customer.Email = "Nemo@gmail.com";
                customer.Phone = "3456789012";
                customers = repo.AddCustomer(customer);
                Customer customer1 = repo.GetCustomer(3);
                Assert.NotNull(customer1);
                Assert.Equal(customer1.Name, "P Sherman");
            }
        }
        
        [Fact]
        public void UpdateCustomerShouldUpdateCustomer(){
            using(var context = new StorefrontDBContext(_options)){
                ICustomerRepository repo = new CustomerRepository(context);
                Customer updated;
                Customer toupdate = new Customer{
                    ID =1,
                    Name = "Hugh",
                    Address = "Houston",
                    Email = "HttpStyleUriParser@gmail.com",
                    Phone = "0123456789",
                } ;
                updated = repo.UpdateCustomer(toupdate);
                updated = repo.GetCustomer(1);
                Assert.NotNull(updated);
                Assert.Equal("Hugh", updated.Name);
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
