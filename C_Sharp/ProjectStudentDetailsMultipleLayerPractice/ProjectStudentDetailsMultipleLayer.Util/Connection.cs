using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectStudentDetailsMultipleLayer.Util
{
    public class Connection
    {
        public static SqlConnection Connected()
        {
            string conn = ConfigurationManager.ConnectionStrings["ENTITY_FRAMEWORK_ASSIGNMENTEntities"].ConnectionString;

            return new SqlConnection(conn);
        }
    }
}
