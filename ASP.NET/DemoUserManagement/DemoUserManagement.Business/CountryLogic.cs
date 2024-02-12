using DemoUserManagement.DataAccess;
using DemoUserManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Business
{
    public class CountryLogic
    {
        public static List<CountryDTO> GetCountryList()
        {
            List<Country> countries = CountryDataAccess.GetCountry();
            List<CountryDTO> countryList = countries.Select(country => new CountryDTO
            {
                CountryID = country.CountryID,
                CountryName = country.CountryName
            }).ToList();

            return countryList;
        }
    }
}
