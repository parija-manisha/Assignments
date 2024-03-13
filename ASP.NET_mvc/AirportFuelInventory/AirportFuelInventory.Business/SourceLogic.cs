using AirportFuelInventory.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.Business
{
    public class SourceLogic
    {
        public static List<SourceDTO> GetSourceList()
        {
            List<Source> sources = SourceDataAccess.GetSourceList();
            List<SourceDTO> sourceList = sources.Select(source => new SourceDTO
            {
               Source_id = source.Source_id,
               Source_name = source.Source_name,
            }).ToList();

            return sourceList;
        }

        //public static int GetCountryIDByName(string countryName)
        //{
        //    return CountryDataAccess.GetCountryIDByName(countryName);
        //}
    }
}
