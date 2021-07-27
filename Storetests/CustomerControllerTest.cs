using Microsoft.AspNetCore.Mvc;
using Moq;
using StorefrontBL;
using StorefrontModels;
using StorefrontUI2.Controllers;
using StorefrontUI2.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StoreTests
{
    public class CustomerControllerTest
    {
        [Fact]
        public void CustomerControllerIndexShouldReturnList()
        {
            //Arrange
            var mockBL = new Mock<ICustomerBL>();
            mockBL.Setup(x => x.GetAllCustomers()).Returns(
                new List<Customer>()
                {
                    new Customer{Name="Jack", Address="Dallas", ID=1, Email="Fakeemail", Phone="0123456789"},
                    new Customer{Name="Jill", Address="Raccoon City", ID=2, Email="Jill's Email", Phone="1234567890"}
                }
                );
            var controller = new CustomerController(mockBL.Object);
            //Act
            string search = null;
            var result = controller.Index(search);
            //Assert
            //Check that we're getting a view as a result
            var viewResult = Assert.IsType<ViewResult>(result);
            //Check that the model of the viewResult is a list of Customer VMs
            var model = Assert.IsAssignableFrom<IEnumerable<CustomerVM>>(viewResult.ViewData.Model);
            //Check that we're getting the same amount of Customers that we're returning
            Assert.Equal(2, model.Count());
        }
    }
}