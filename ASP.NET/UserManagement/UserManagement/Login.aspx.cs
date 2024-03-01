﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserManagement.Business;
using UserManagement.Models;
using UserManagement.Utils;

namespace UserManagement
{
    public partial class Login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static int LoginUser(string username, string password)
        {
            try
            {
                int userId = UserLogic.LoginUser(username, password);
                if (userId > 0)
                {
                    bool isAdmin = UserLogic.IsAdmin(userId);

                    UserSession session = new UserSession
                    {
                        UserId = userId,
                        IsAdmin = isAdmin,
                    };

                    Constants.SetSessionDetail(session);

                    return userId;
                }

                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Login Failed\n", ex);
                return -1;
            }
        }

    }
}