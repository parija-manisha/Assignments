using DemoUserManagement.DataAccess;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
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
            List<State> states = StateCountry.GetStateByCountry(countryId);
            List<StateDTO> stateList = new List<StateDTO>();  // Declare outside the try block

            if (states != null)
            {
                    stateList = states.Select(state => new StateDTO
                    {
                        StateID = state.StateID,
                        CountryID = state.CountryID,
                        StateName = state.StateName
                    }).ToList();
            }

            return stateList;
        }

        public static int GetStateIDByName(string stateName)
        {
            return StateDataAccess.GetStateIDByName(stateName);
        }

    }
}
