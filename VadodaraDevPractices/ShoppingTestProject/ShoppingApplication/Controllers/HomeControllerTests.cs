using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Shopping.BLL;
using Shopping.Lib;
using ShoppingApplication.Controllers;
using ShoppingTestProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingTestProject.ShoppingApplication.Controllers
{
    public class HomeControllerTests : ApiUnitTestBase<HomeController>
    {
        private Mock<ILogger<HomeController>> mockLogger;

        private Mock<IShoppingBusiness> mockShoppingBusiness;

        public override void TestSetup()
        {
            mockShoppingBusiness = this.CreateAndInjectMock<IShoppingBusiness>();
            mockLogger = this.CreateAndInjectMock<ILogger<HomeController>>();

            Target = new HomeController(mockLogger.Object, mockShoppingBusiness.Object);
        }

        public override void TestTearDown()
        {
            mockLogger.VerifyAll();
            mockShoppingBusiness.VerifyAll();
        }

        [Fact]
        public void Index_ReturnOK()
        {
            //Arrange
            var product = Fixture.Create<IEnumerable<Shopping.Lib.ShoppingProduct>>();
            mockShoppingBusiness.Setup(m => m.GetProducts()).Returns(product);

            //Act
            
            ViewResult result = Target.Index() as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(product,result.Model);
            this.mockShoppingBusiness.Verify(m => m.GetProducts());
        }

        [Fact]
        public void Error_ReturnOK()
        {
            //Arrange

            //Act

            ViewResult result = Target.Error() as ViewResult;

            //Assert
            Assert.NotNull(result);
        }
    }

}

