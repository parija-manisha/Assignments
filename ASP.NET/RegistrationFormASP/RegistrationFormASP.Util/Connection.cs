using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationFormASP.Util
{
    public class Connection
    {
        public static SqlConnection Connect()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["RegisterASPConnection"].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}
