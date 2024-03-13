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
                return HttpContext.Current.Session["UserSession"] as UserSession;
            }

            set
            {
                HttpContext.Current.Session["UserSession"] = value;
            }
        }

        public static string ToggleSortDirection(string currentDirection)
        {
            return currentDirection?.ToUpper() == "ASC" ? "DESC" : "ASC";
        }
    }
}
