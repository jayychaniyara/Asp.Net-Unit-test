using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShoppingApi.Controllers;
using ShoppingApi.Models;
using ShoppingTestProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ShoppingTestProject.ShoppingApi.Controllers
{
    public class ProductControllerTests : ApiUnitTestBase<ProductController>
    {
        private Mock<IProductRepository> mockProductRepository;
        public override void TestSetup()
        {
            mockProductRepository = this.CreateAndInjectMock<IProductRepository>();
            Target = new ProductController(mockProductRepository.Object);
        }

        public override void TestTearDown()
        {

            mockProductRepository.VerifyAll();
        }

        [Fact]
        public void GetAll_ValueFound()
        {
            // Arrange.
            var products = Fixture.CreateMany<Product>();
            this.mockProductRepository.Setup(c => c.GetAll()).Returns(products);

            // Act.
            var actual = Target.GetAll();

            // Assert.
            Assert.NotNull(actual);
            Assert.Equal(products, actual);
            this.mockProductRepository.Verify(c => c.GetAll(), Times.Once);
        }

        [Fact]
        public void GetAll_ValueNotFound()
        {
            //Arrange
            IEnumerable<Product> product = null;
            this.mockProductRepository.Setup(c => c.GetAll()).Returns(product);
            
            // Act
            var result = Target.GetAll();

            // Assert
            Assert.Null(result);
            Assert.Equal(product, result); ;
            mockProductRepository.Verify(m => m.GetAll(), Times.Once);

        }

        [Fact]
        public void GetById_ValueFound()
        {
            //Arrange
            var id = Fixture.Create<int>();
            var product = Fixture.Create<Product>();
            product.Id = id;
            this.mockProductRepository.Setup(c => c.GetById(id)).Returns(product);

            //Act
            var result = Target.GetById(id) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(product, result.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            this.mockProductRepository.Verify(m => m.GetById(id), Times.Once);
        }

        [Fact]
        public void GetById_ValueNotFound()
        {
            //Arrange
            Product product = null;
            var id = Fixture.Create<int>();
            this.mockProductRepository.Setup(c => c.GetById(id)).Returns(product);

            //Act
            var result = Target.GetById(id) as StatusCodeResult;

            //Assert
            Assert.NotNull(result);
            //Assert.Equal(product, result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
            this.mockProductRepository.Verify(m => m.GetById(id), Times.Once);

        }

        [Fact]
        public void Create_ValueAdded_ReturnOK()
        {
            //var id = Fixture.Create<int>();
            var product = Fixture.Create<Product>();
            //product.Id = id;
            this.mockProductRepository.Setup(c => c.Add(product));

            //Act
            var result = Target.Create(product) as StatusCodeResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            mockProductRepository.Verify(c => c.Add(product), Times.Once);
        }

        [Fact]
        public void Create_ValueAdded_BadRequest()
        {
            //Arrange

            //Act
            var result = Target.Create(null) as StatusCodeResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public void Update_ValueUpdate_RetrunOK()
        {
            var id = Fixture.Create<int>();
            var product = Fixture.Create<Product>();
            product.Id = id;
            this.mockProductRepository.Setup(c => c.GetById(id)).Returns(product);
            this.mockProductRepository.Setup(c => c.Update(product));

            //Act
            var result = Target.Update(id,product) as StatusCodeResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            mockProductRepository.Verify(c => c.GetById(id), Times.Once);
            mockProductRepository.Verify(c => c.Update(product), Times.Once);
        }

        [Fact]
        public void Update_ValueUpdate_NotFound()
        {
            //Arrange
            var id = Fixture.Create<int>();
            var product = Fixture.Create<Product>();
            product.Id = id;

            Product p = null;
            this.mockProductRepository.Setup(c => c.GetById(id)).Returns(p);

            //Act
            var result = Target.Update(id, product) as StatusCodeResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
            mockProductRepository.Verify(c => c.GetById(id), Times.Once);
        }

        [Fact]
        public void Update_ValueUpdate_BadRequest()
        {
            //Arrange

            //Act
            var result = Target.Update(0, null) as StatusCodeResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public void Delete_ValueRemoved()
        {
            //Arrange
            var id = Fixture.Create<int>();
            var product = Fixture.Create<Product>();
            product.Id = id;
            this.mockProductRepository.Setup(c => c.Remove(id)).Returns(product);

            //Act
            var result = Target.Delete(id);

            //Assert
            Assert.NotNull(result);
            this.mockProductRepository.Verify(m => m.Remove(id), Times.Once);
        }

        [Fact]
        public void Delete_ValueNotFound()
        {
            //Arrange
            var id = Fixture.Create<int>();
            Product product = null;
            this.mockProductRepository.Setup(c => c.Remove(id)).Returns(product);

            //Act
            var result = Target.Delete(id);

            //Assert
            Assert.Null(result); 
            this.mockProductRepository.Verify(m => m.Remove(id), Times.Once);
        }

        [Fact(Skip = "Never Gonna Happen")]
        public void GetProductWithPaging_BadRquest()
        {
            //Arrange
            //PagedList<Product> product = null;
            //this.mockProductRepository.Setup(c => c.GetPagedProducts(null)).Returns(product);

            //Act
            var result = Target.GetProductsWithPaging(null) as StatusCodeResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            //this.mockProductRepository.Verify(m => m.GetPagedProducts(null), Times.Once);
        }

        [Fact]
        public void GetProductsWithPaging_ReturnOK()
        {
            //Arrange
            List<Product> products = new List<Product>();
            products.Add(new Product { Id = 1, Name = "ABC", Description = "weewetr", IsMerchantDiscountAllowed = true, Price = 90, Quantity = 7 });
            products.Add(new Product { Id = 2, Name = "ABC111", Description = "weewsdfsdfetr", IsMerchantDiscountAllowed = false, Price = 80, Quantity = 17 });
            PagedList<Product> productList = new PagedList<Product>(products, 33, 1, 10);
            QueryParameters queryParameters = new QueryParameters();
            queryParameters.PageNumber = 1;
            queryParameters.PageSize = 10;

            this.mockProductRepository.Setup(c => c.GetPagedProducts(queryParameters)).Returns(productList);
            var jsonConverterQueryParameters = JsonConvert.SerializeObject(queryParameters);

            //Act
            var result = Target.GetProductsWithPaging(queryParameters) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(productList, result.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            this.mockProductRepository.Verify(c => c.GetPagedProducts(queryParameters), Times.Once);
        }

        [Fact]
        public void GetProductsWithPaging_ReturnBadRequest()
        {
            //Arrange            
            Target.ModelState.AddModelError("key", "error message");

            //Act
            var result = Target.GetProductsWithPaging(null) as StatusCodeResult;

            //Assert            
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.NotNull(result);
        }

    }
    }

