using System.Collections.Generic;
using System.Web;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.Utils
{
    public class Constants
    {
        public static List<TransactionType> TransactionTypes = new List<TransactionType>
        {
            TransactionType.In,
            TransactionType.Out
        };

        public static UserSession SessionDetail
        {
            get
            {
                if (HttpContext.Current.Session["UserSession"] is UserSession userSession)
                {
                    return userSession;
                }
                return null;
            }

            set
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    if (value != null)
                    {
                        HttpContext.Current.Session["UserSession"] = value;
                    }
                    else
                    {
                        HttpContext.Current.Session.Remove("UserSession");
                    }
                }
            }
        }
    }
}
