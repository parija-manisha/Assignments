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

            if (states != null)
            {
                List<StateDTO> stateList = states.Select(state => new StateDTO
                {
                    StateName = state.StateName,
                }).ToList();

                return stateList;
            }

            return new List<StateDTO>(); 
        }


    }
}
