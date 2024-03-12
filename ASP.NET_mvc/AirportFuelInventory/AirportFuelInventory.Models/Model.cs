﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AirportFuelInventory.Models
{
    public class Model
    {
        public enum TransactionType
        {
            In = 1,
            Out = 2
        }

        public class Pagination
        {
            public int CurrentPage { get; set; }
            public int TotalPages { get; set; }
        }

        public class AircraftDTO
        {
            public int Aircraft_Id { get; set; }
            public string Aircraft_Name { get; set; }
            public string Airline { get; set; }
            public string Source { get; set; }
            public string Destination { get; set; }

            public Pagination Pagination { get; set; }

        }

        public class AirportDTO
        {
            public int Airport_Id { get; set; }
            public string Airport_Name { get; set; }
            public decimal Fuel_Capacity { get; set; }

            public Pagination Pagination { get; set; }

        }

        public class TransactionDTO
        {
            public int Transaction_Id { get; set; }
            public System.DateTime Transaction_date_time { get; set; }
            public int Transaction_type { get; set; }
            public int Airport_id { get; set; }
            public int Aircraft_id { get; set; }
            public int Quantity { get; set; }
            public int? Transaction_id_parent { get; set; }
            public string AircraftName { get; set; }

            public string TransactionTypeString
            {
                get
                {
                    return ((TransactionType)Transaction_type).ToString();
                }
            }

            public List<TransactionDTO> Transactions { get; set; }
            public List<TransactionType> TransactionTypes { get; set; }
            public List<AirportDTO> AirportDTOs { get; set; }
            public List<AircraftDTO> AircraftDTOs { get; set; }

        }

        public class UserDTO
        {
            public int User_Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }

            public List<UserDTO> Users { get; set; }
        }

        public class UserSession
        {
            public int UserId { get; set; }
        }
    }

}

