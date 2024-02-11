using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsMultipleLayers.Util
{
    public class Connection
    {
        public static SqlConnection ConnectADO()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudentDetailsADO"].ConnectionString;
            return new SqlConnection(connectionString);
        }
        public static SqlConnection Connect()
        {
            string con = ConfigurationManager.ConnectionStrings["StudentDetailsEntities"].ConnectionString;
            return new SqlConnection(con);
        }
    }
}
