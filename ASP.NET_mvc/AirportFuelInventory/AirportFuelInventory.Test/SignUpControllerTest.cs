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
        public void TestIsEmailExist_ReturnView()
        {
            //Arrange
            SignUpController signUpController = new SignUpController();

            //Act
            var email = "manishaaa@gmail.com";
            var result = signUpController.IsEmailExist(email);

            //Assert
            Assert.IsNotNull(result);

            if (!(bool)result.Data)
            {
                //Mock New User Details
                var newUser = new Models.Model.UserDTO
                {
                    Name = "Test",
                    Email = email,
                    Password = "123123"
                };

                //Act
                var addActionResult = signUpController.NewUser(newUser);

                //Assert
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
