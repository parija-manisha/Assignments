using AirportFuelInventory.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirportFuelInventory.DataAccess
{
    public class SourceDataAccess
    {
        public static List<Source> GetSourceList()
        {
            List<Source> sourceList = new List<Source>();
            try
            {
                using (AirportFuelInventoryEntities context = new AirportFuelInventoryEntities())
                {
                    sourceList = context.Sources.ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not retrieve Country Details", ex);
            }
            return sourceList;
        }
    }
}
