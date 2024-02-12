using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.DataAccess
{
    public class StateDataAccess
    {
        public static List<State> GetState()
        {
            List<State> stateList = new List<State>();
            try
            {
                using (UserManagementTableEntities2 context = new UserManagementTableEntities2())
                {
                    stateList = context.States.ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Couldnot retrieve State Details", ex);
            }
            return stateList;
        }
    }
}
