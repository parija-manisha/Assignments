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
        public int UserID { get; set; }
        public int AddressType { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int Pincode { get; set; }
        public int CountryID { get; set; }
        public int StateID { get; set; }
        
        public CountryDTO Country { get; set; }
        public StateDTO State { get; set; }
    }
}
