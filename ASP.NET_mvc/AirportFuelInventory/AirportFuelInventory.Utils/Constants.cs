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

        public static UserSession GetSessionDetail()
        {
            return HttpContext.Current.Session["UserSession"] as UserSession;
        }

        public static void SetSessionDetail(UserSession userSession)
        {
            HttpContext.Current.Session["UserSession"] = userSession;
        }

        public static string ToggleSortDirection(string currentDirection)
        {
            return currentDirection?.ToUpper() == "ASC" ? "DESC" : "ASC";
        }
    }
}
