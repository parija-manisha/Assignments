using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportFuelInventory.Utils
{
    public class Constants
    {
        public enum TransactionType
        {
            In = 1,
            Out = 2
        }

        public static List<TransactionType> TransactionTypes = new List<TransactionType>
        {
            TransactionType.In,
            TransactionType.Out
        };
    }
}
