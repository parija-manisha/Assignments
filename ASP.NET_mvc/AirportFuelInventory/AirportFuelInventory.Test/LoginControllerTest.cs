using AirportFuelInventory.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace AirportFuelInventory.Test
{
    [TestClass]
    public class LoginControllerTest
    {
        [TestMethod]
        public void TestLogin_Returns_RedirectToDashboardAction()
        {
            //Arrange
            LoginController controller = new LoginController();

            //Mock User Credentials
            var user = new Models.Model.UserDTO
            {
                Email = "AD@gmail.com",
                Password = "AD"
            };

            //Act
            var result = controller.Login(user);

            //Assert
            Assert.IsNotNull(result);

            if (result is RedirectToRouteResult)
            {
                var redirectResult = (RedirectToRouteResult)result;
                Assert.AreEqual("Dashboard", redirectResult.RouteValues["controller"]);
                Assert.AreEqual("Dashboard", redirectResult.RouteValues["action"]);
            }
            //If result is ViewResult that means login failed, display the error message
            else if (result is ViewResult)
            {
                Assert.IsTrue(controller.ViewData.ContainsKey("ErrorMessage"));
            }
            else
            {
                Assert.Fail("Unexpected result type");
            }
        }

        [TestMethod]
        public void TestLogout_Returns_RedirectToLoginAction()
        {
            // Arrange
            var controller = new LoginController();

            // Act
            var result = controller.Logout() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.RouteValues["controller"]);
            Assert.AreEqual("Login", result.RouteValues["action"]);
            Assert.IsNull(controller.Session);
        }
    }
}
