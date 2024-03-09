using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportFuelInventory.Models
{
    public class ReportSummary
    {
        public int AirportId { get; set; }
        public string AirportName { get; set; }
        public decimal AvailableFuel { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
