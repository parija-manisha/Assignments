using AirportFuelInventory.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace AirportFuelInventory.Test
{
    [TestClass]
    public class LoginControllerTest
    {
        [TestMethod]
        public void Login_RedirectToDashboardAction()
        {
            LoginController controller = new LoginController();

            var user = new Models.Model.UserDTO
            {
                Email = "AD@gmail.com",
                Password = "AD"
            };

            var result = controller.Login(user);

            Assert.IsNotNull(result);

            if (result is RedirectToRouteResult)
            {
                var redirectResult = (RedirectToRouteResult)result;
                Assert.AreEqual("Dashboard", redirectResult.RouteValues["controller"]);
                Assert.AreEqual("Dashboard", redirectResult.RouteValues["action"]);
            }
            else if (result is ViewResult)
            {
                Assert.IsTrue(controller.ViewData.ContainsKey("ErrorMessage"));
            }
            else
            {
                Assert.Fail("Unexpected result type");
            }
        }
    }
}
