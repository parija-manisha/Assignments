//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AirportFuelInventory.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction
    {
        public int Transaction_id { get; set; }
        public System.DateTime Transaction_date_time { get; set; }
        public int Transaction_type { get; set; }
        public int Airport_id { get; set; }
        public int Aircraft_id { get; set; }
        public int Quantity { get; set; }
        public Nullable<int> Transaction_id_parent { get; set; }
    
        public virtual Aircraft Aircraft { get; set; }
        public virtual Airport Airport { get; set; }
    }
}
