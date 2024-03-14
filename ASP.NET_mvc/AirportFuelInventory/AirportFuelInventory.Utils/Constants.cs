using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if (HttpContext.Current.Session["UserSession"] != null)
                {
                    return HttpContext.Current.Session["UserSession"] as UserSession;
                }
                return null;
            }

            set
            {
                if (HttpContext.Current.Session["UserSession"] != null)
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

        public static string ToggleSortDirection(string currentDirection)
        {
            return currentDirection?.ToUpper() == "ASC" ? "DESC" : "ASC";
        }
    }
}
