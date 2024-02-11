using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsWebForms.Util
{
    public class Connection
    {
        public static SqlConnection Connect()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ENTITY_FRAMEWORK_ASSIGNMENTConnectionString"].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}
