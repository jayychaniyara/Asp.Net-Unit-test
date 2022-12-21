using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shopping.BLL;
using Shopping.Lib;
using ShoppingApplication.Controllers;
using ShoppingTestProject.Utilities;
using System;
using Xunit;

namespace ShoppingTestProject.ShoppingApplication.Controllers
{
    public class ProductControllerTests : ApiUnitTestBase<ProductController>
    {
        private Mock<IShoppingBusiness> mockShoppingBusiness;

        public override void TestSetup()
        {
            mockShoppingBusiness = this.CreateAndInjectMock<IShoppingBusiness>();
            Target = new ProductController(mockShoppingBusiness.Object);
        }

        public override void TestTearDown()
        {
            mockShoppingBusiness.VerifyAll();
        }

        [Fact]
        public void Index_ById()
        {
            //Arrange
            var product = Fixture.Create<ShoppingProduct>();
            //ShoppingBusiness product = null;
            var id = Fixture.Create<int>();
            this.mockShoppingBusiness.Setup((c) => c.GetProductById(id)).Returns(product);

            // Act
            ViewResult view = Target.Index(id) as ViewResult;

            // Assert
            Assert.NotNull(view);
            Assert.Equal(product, view.Model);
            mockShoppingBusiness.Verify(m => m.GetProductById(id), Times.Once());
        }

        [Fact]
        public void Index_ById_NotFound()
        {
            //Arrange
            // var product = Fixture.Create<ShoppingProduct>();
            ShoppingProduct product = null;
            var id = Fixture.Create<int>();
            this.mockShoppingBusiness.Setup((c) => c.GetProductById(id)).Returns(product);

            // Act
            ViewResult view = Target.Index(id) as ViewResult;

            // Assert
            Assert.NotNull(view);
            Assert.Equal(product, view.Model);
            mockShoppingBusiness.Verify(m => m.GetProductById(id), Times.Once());
        }

        [Fact]
        public void Index_ModelState_ReturnView()
        {
            //Arrange
            ShoppingProduct product = null;
            var id = Fixture.Create<int>();
            this.mockShoppingBusiness.Setup(c => c.GetProductById(id)).Returns(product);

            //Act
            ViewResult view = Target.Index(id) as ViewResult;
            
            //Assert
            Assert.NotNull(view);
            mockShoppingBusiness.Verify(m => m.GetProductById(id), Times.Once);
        }

        [Fact]
        public void Index_PurchasedQuantityLessThanEqualToZero_ReturnView()
        {
            //Arrange          
            ShoppingProduct product = new ShoppingProduct()
            {
                Quantity = 5,
                PurchasedQuantity = 0
            };
            // var products = .Where(x => x.PurchasedQuantity <= 0);
            Target.ModelState.AddModelError("PurchasedQuantity", "Please enter valid quantity");

            //Act
            var actual = Target.Index(product) as ViewResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(product, actual.Model);
        }

        [Fact]
        public void Index_PurchasedQuantityGreaterThanQuantity_ReturnView()
        {
            //Arrange          
            ShoppingProduct product = new ShoppingProduct()
            {
                Quantity = 5,
                PurchasedQuantity = 10
            };
            // var products = .Where(x => x.PurchasedQuantity <= 0);
            Target.ModelState.AddModelError("PurchasedQuantity", "Sorry!!! There is not enough stock available");

            //Act
            var actual = Target.Index(product) as ViewResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(product, actual.Model);
        }

        [Fact]
        public void Index_ApplyFestivalDiscount_ReturnView()
        {
            //Arrange
            var products = Fixture.Create<ShoppingProduct>();
            this.mockShoppingBusiness.Setup(c => c.ApplyFestivalDiscount(products)).Returns(products);

            //Act
            var result = Target.Index(products) as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(products, result.Model);
            this.mockShoppingBusiness.Verify(c => c.ApplyFestivalDiscount(products));
        }

        [Fact]
        public void Index_ReturnView()
        {
            //Arrange
            var id = Fixture.Create<int>();
            var products = Fixture.Create<ShoppingProduct>();
            mockShoppingBusiness.Setup(c => c.GetProductById(id)).Returns(products);

            //Act
            ViewResult view = Target.Index(id) as ViewResult;

            //Assert
            Assert.NotNull(view);
            Assert.Equal(products, view.Model);
            this.mockShoppingBusiness.Verify(c => c.GetProductById(id), Times.Once);
        }

        [Fact]
        public void Thanks_RetrunView()
        {
            //Arrange

            // Act
            ViewResult view = Target.Thanks() as ViewResult;

            // Assert           
            RedirectResult redirect = (RedirectResult)view.Model;
        }
    }
}
