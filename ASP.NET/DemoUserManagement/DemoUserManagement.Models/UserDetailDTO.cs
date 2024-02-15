﻿
using System;
using System.Collections.Generic;
using System.Linq;
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
        public int PhoneNumber { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string Hobbies { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public System.Guid FileNameGuid { get; set; }
        public string FileName { get; set; }

        public AddressDetailDTO PresentAddress { get; set; }
        public AddressDetailDTO PermanentAddress { get; set; }

        public DocumentTypeDTO DocumentType { get; set; }
    }
}