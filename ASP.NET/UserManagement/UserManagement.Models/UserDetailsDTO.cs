﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Models
{
    public class UserDetailsDTO
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

        public AddressDetailDTO AddressDetail { get; set; }
        public UserRoleDTO UserRole { get; set; }
        public DocumentListDTO DocumentList { get; set; }
        public NoteDTO Note { get; set; }
    }
}