
using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Models
{
    public class UserDetailDTO
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string Hobbies { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public AddressDetailDTO PresentAddress { get; set; }
        public AddressDetailDTO PermanentAddress { get; set; }
        public UserRoleDTO UserRole { get; set; }
        public DocumentListDTO DocumentList { get; set; }


        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public List<UserDetailDTO> Users { get; set; }
        public List<CountryDTO> Countries { get; set; }
        public List<StateDTO> States { get; set; }
        public List<DocumentTypeDTO> DocumentType { get; set; }
    }
}
