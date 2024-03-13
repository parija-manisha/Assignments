using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.Models
{
    public class ReportSummary
    {
        public class AirportSummary
        {
            public int Airport_id { get; set; }
            public string Airport_name { get; set; }
            public decimal AvailableFuel { get; set; }

            public int CurrentPage { get; set; }
            public int TotalPages { get; set; }
        }

        public class FuelSummary
        {
            public string AirportName { get; set; }
            public List<TransactionDTO> TransactionDTO { get; set; }
            public decimal AvailableFuel { get; set; }

            public int CurrentPage { get; set; }
            public int TotalPages { get; set; }
        }
    }
}
