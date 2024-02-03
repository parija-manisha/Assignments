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
        public static SqlConnection Connect()
        {
            string con = ConfigurationManager.ConnectionStrings["StudentDetails"].ConnectionString;
            return new SqlConnection(con);
        }
    }
}
