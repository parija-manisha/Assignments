using AirportFuelInventory.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AirportFuelInventory.Attributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var returnURL = filterContext.HttpContext.Request.Url;
            var userSession = Constants.SessionDetail;

            if (userSession == null)
            {
                if (!returnURL.ToString().EndsWith("SignUp", StringComparison.OrdinalIgnoreCase))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "SignUp", action = "SignUp", returnURL = returnURL }));
                }
            }
            else
            {
                if (returnURL != null)
                {
                    if (userSession.UserId != -1)
                    {
                        if (!returnURL.ToString().Contains($"UserID={userSession.UserId}"))
                        {
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Login", UserID = userSession.UserId }));
                        }
                    }
                }
                else
                {
                    base.HandleUnauthorizedRequest(filterContext);
                }
            }
        }

    }

}