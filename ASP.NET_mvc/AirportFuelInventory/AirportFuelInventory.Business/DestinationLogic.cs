using AirportFuelInventory.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.Business
{
    public class DestinationLogic
    {
        public static List<DestinationDTO> GetDestinationList()
        {
            List<Destination> destinations = DestinationDataAccess.GetDestinationList();
            List<DestinationDTO> destinationList = destinations.Select(destination => new DestinationDTO
            {
               Destination_id = destination.Destination_id,
               Destination_name = destination.Destination_name,
            }).ToList();

            return destinationList;
        }

        //public static int GetCountryIDByName(string countryName)
        //{
        //    return CountryDataAccess.GetCountryIDByName(countryName);
        //}
    }
}
