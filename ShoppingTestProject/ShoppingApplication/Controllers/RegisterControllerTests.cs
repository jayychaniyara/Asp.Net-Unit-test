using AutoFixture;
using Helper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShoppingApplication.Controllers;
using ShoppingApplication.Models;
using ShoppingTestProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingTestProject.ShoppingApplication.Controllers
{
    public class RegisterControllerTests : ApiUnitTestBase<RegisterController>
    {

        private Mock<IValidationHelper> mockValidationHelper;

        public override void TestSetup()
        {
            mockValidationHelper = this.CreateAndInjectMock<IValidationHelper>();
            Target = new RegisterController(mockValidationHelper.Object);
        }

        public override void TestTearDown()
        {
            mockValidationHelper.VerifyAll();
        }

        [Fact]
        public void Index_RetrunView()
        {
            //Arrange

            // Act
            ViewResult view = Target.Index() as ViewResult;

            // Assert           
            RedirectResult redirect = (RedirectResult)view.Model;
        }

        [Fact]
        public void Index_InvalidModelState_UserDetails()
        {
            // Arrange
            var user = new UserDetails
            {
                //This is a required property and so this value is invalid
                Name = null,
                Email = null,
                Phone = null
            };

            Target.ModelState.AddModelError("Name", "Name is required.");
            // Act
            ViewResult view = Target.Index(user) as ViewResult;
            //var result = Target.Index(user);

            //Assert
            Assert.NotNull(view);
            Assert.Null(view.Model);


        }

        [Fact]
        public void Index_AgeRequired_UserDetails()
        {
            // Arrange
            var users = Fixture.Create<ValidationHelper>();
            string msg = "";
            this.mockValidationHelper.Setup(m => m.IsAgeOrDobSupplied(null, null, ref msg)).Returns(false);
            var user = new UserDetails
            {
                //This is a required property and so this value is invalid
                Name = "Aditi",
                Email = "a@a.com",
                Phone = "923",
            };

            // Act
            ViewResult view = Target.Index(user) as ViewResult;

            //Assert
            Assert.NotNull(view);
            Assert.False(Target.ModelState.IsValid);
            Assert.True(Target.ModelState.Count == 2);

        }

        [Fact]
        public void Index_AgeAndDobSupplied_UserDetails()
        {
            // Arrange
            var user = new UserDetails
            {
                //This is a required property and so this value is invalid
                Name = "Aditi",
                Email = "a@a.com",
                Phone = "923",
                //Age and Dob Supplied
                Age = 20,
                DateOfBirth = DateTime.Now,
            };
            string msg = "";
            this.mockValidationHelper.Setup(m => m.IsAgeOrDobSupplied(user.Age, user.DateOfBirth, ref msg)).Returns(false);

            // Act
            ViewResult view = Target.Index(user) as ViewResult;

            //Assert
            Assert.NotNull(view);
            Assert.False(Target.ModelState.IsValid);
            Assert.True(Target.ModelState.Count == 2);

        }

        [Fact]
        public void Index_AgeSupplied_UserDetails()
        {
            // Arrange
            var user = new UserDetails
            {
                //This is a required property and so this value is invalid
                Name = "Aditi",
                Email = "a@a.com",
                Phone = "923",
                //Age and Dob Supplied
                Age = 20,
                DateOfBirth = null
            };
            string msg = "";
            this.mockValidationHelper.Setup(m => m.IsAgeOrDobSupplied(user.Age, user.DateOfBirth, ref msg)).Returns(true);
            this.mockValidationHelper.Setup(m => m.IsValidAge(user.Age.Value, ref msg)).Returns(false);

            // Act
            ViewResult view = Target.Index(user) as ViewResult;

            //Assert
            Assert.NotNull(view);
            Assert.False(Target.ModelState.IsValid);
            Assert.True(Target.ModelState.Count == 1);

        }

        [Fact]
        public void Index_DobSupplied_UserDetails()
        {
            // Arrange
            var user = new UserDetails
            {
                //This is a required property and so this value is invalid
                Name = "Aditi",
                Email = "a@a.com",
                Phone = "923",
                //Age and Dob Supplied
                Age = null,
                DateOfBirth = DateTime.Now.AddDays(-55)
            };
            string msg = "";
            this.mockValidationHelper.Setup(m => m.IsAgeOrDobSupplied(user.Age, user.DateOfBirth, ref msg)).Returns(true);
            this.mockValidationHelper.Setup(m => m.IsValidDateOfBirth(user.DateOfBirth.Value, ref msg)).Returns(false);

            // Act
            ViewResult view = Target.Index(user) as ViewResult;

            //Assert
            Assert.NotNull(view);
            Assert.False(Target.ModelState.IsValid);
            Assert.True(Target.ModelState.Count == 1);

        }
    }
}
