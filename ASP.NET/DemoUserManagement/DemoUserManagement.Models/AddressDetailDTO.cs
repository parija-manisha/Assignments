using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Models
{
    public class AddressDetailDTO
    {
        public int AddressID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> AddressType { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public Nullable<int> Pincode { get; set; }
        public int CountryID { get; set; }
        public Nullable<int> StateID { get; set; }
    }
}
