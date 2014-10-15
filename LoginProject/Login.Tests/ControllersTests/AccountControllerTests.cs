using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Login.Client.Controllers;
using System.Web.Mvc;
using Moq;
using Login.Data;
using Login.Models;
using Login.Client.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using System.Web;

namespace Login.Tests.ControllersTests
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var mocekdRepo = new Mock<IFakeRepository<ApplicationUser>>();
            AccountController controller = new AccountController(mocekdRepo.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SignUp()
        {
            // Arrange
            var mocekdRepo = new Mock<IFakeRepository<ApplicationUser>>();
            AccountController controller = new AccountController(mocekdRepo.Object);

            // Act
            ViewResult result = controller.SignUp() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SignUp_WithInvalidUserModel_MustReturnJsonResultWithValidFalseAndErrorMsgs()
        {
            // Arrange
            var mocekdRepo = new Mock<IFakeRepository<ApplicationUser>>();
            AccountController controller = new AccountController(mocekdRepo.Object);
            UserViewModel userModel = new UserViewModel();

            // Act
            controller.ViewData.ModelState.AddModelError("UserName", "ErrorMessage");
            JsonResult result = controller.SignUp(userModel) as JsonResult;
            string expected = "{ valid = False, validationErrors = {\"UserName\":[\"ErrorMessage\"]} }";
            string actual = result.Data.ToString();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, actual);
        }

         [TestMethod]
        public void SignUp_WithValidModel_MustSaveTheUser()
        {
            // Arrange
            List<ApplicationUser> fakeContext = new List<ApplicationUser>();
            var mocekdRepo = new Mock<IFakeRepository<ApplicationUser>>();
            mocekdRepo.Setup(repo => repo.All()).Returns(fakeContext.AsQueryable());
            mocekdRepo.Setup(repo => repo.Save(It.IsAny<ApplicationUser>()))
                .Callback((ApplicationUser user) => fakeContext.Add(user));
            AccountController controller = new AccountController(mocekdRepo.Object);
            //controller.Url = new UrlHelper(new RequestContext(new HttpContext(new HttpRequest(),new RouteData()));
            UserViewModel userModel = new UserViewModel
            {
                UserName = "TestUser",
                Password = "TestPass"
            };

            // Act
            JsonResult result = controller.SignUp(userModel) as JsonResult;
            string expected = "{ valid = True, url = /Account/LoginUser?UserName=TestUser&Password=7NN01Bz3%2b%2bf9Mjx0KbT1mw%3d%3d }";
            string actual = result.Data.ToString();

            // Assert
            Assert.AreEqual(1, fakeContext.Count);
            Assert.AreEqual(0, fakeContext[0].Id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Login()
        {
            // Arrange
            var mocekdRepo = new Mock<IFakeRepository<ApplicationUser>>();
            AccountController controller = new AccountController(mocekdRepo.Object);

            // Act
            ViewResult result = controller.Login() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
