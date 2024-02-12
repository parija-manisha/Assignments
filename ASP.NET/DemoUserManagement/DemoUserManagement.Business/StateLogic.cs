using DemoUserManagement.DataAccess;
using DemoUserManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Business
{
    public class StateLogic
    {
        public static List<StateDTO> GetStateList(int countryId)
        {
            List<State> states = GetStateNameByCountry.StateCountry(countryId);
            List<StateDTO> stateList = states.Select(state => new StateDTO
            {
                StateID = state.StateID,
                StateName = state.StateName,
                CountryID = (int)state.CountryID
            }).ToList();

            return stateList;
        }

    }
}
