using DemoUserManagement.Business;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DemoUserManagement.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var returnURL = filterContext.HttpContext.Request.Url;
            var userSession = Constants.GetSessionDetail();

            if (userSession == null)
            {
                if (!returnURL.ToString().EndsWith("UserDetailForm", StringComparison.OrdinalIgnoreCase))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Login", returnURL = returnURL }));
                }
            }
            else
            {
                if (returnURL != null)
                {
                    if (userSession.UserId != -1)
                    {
                        if (!userSession.IsAdmin)
                        {
                            if (!returnURL.ToString().Contains($"UserID={userSession.UserId}"))
                            {
                                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Registration", action = "UserDetailForm", UserID = userSession.UserId }));
                            }
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
