using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControlAssignment.Util
{
    public class Connection
    {
        public static SqlConnection Connect()
        {
            string con = ConfigurationManager.ConnectionStrings["AddNoteConnection"].ConnectionString;
            return new SqlConnection(con);
        }
    }
}
