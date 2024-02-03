using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsLayers.Util
{
    public class Connection
    {
        public static SqlConnection Connected()
        {
            string conn = ConfigurationManager.ConnectionStrings["StudentDetailsEntities"].ConnectionString;

            return new SqlConnection(conn);
        }
    }
}
