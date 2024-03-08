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
        public int AvailableFuel { get; set; }
    }
}
