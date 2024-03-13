using AirportFuelInventory.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AirportFuelInventory.Test
{
    [TestClass]
    public class SignUpControllerTest
    {
        [TestMethod]
        public void SignUp_ReturnView()
        {
            SignUpController controller = new SignUpController();
            var result = controller.SignUp();
        }

        [TestMethod]
        public void IsEmailExist_ReturnView()
        {
            SignUpController signUpController = new SignUpController();
            var email = "manishaaa@gmail.com";

            var result = signUpController.IsEmailExist(email);

            Assert.IsNotNull(result);

            if (!(bool)result.Data)
            {
                var newUser = new Models.Model.UserDTO
                {
                    Name = "Test",
                    Email = email,
                    Password = "123123"
                };

                var addActionResult = signUpController.NewUser(newUser);

                Assert.IsNotNull(addActionResult);

                if (addActionResult is RedirectToRouteResult)
                {
                    var redirectResult = (RedirectToRouteResult)addActionResult;
                    Assert.AreEqual("Login", redirectResult.RouteValues["controller"]);
                    Assert.AreEqual("Login", redirectResult.RouteValues["action"]);
                }
                else
                {
                    Assert.Fail("Unexpected result type");
                }
            }
            else
            {
                Assert.Fail("Email already exists");
            }
        }

    }
}
