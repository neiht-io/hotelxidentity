//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelxIdentity.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RoomBooking
    {
        public int BookingId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public System.DateTime BookingFrom { get; set; }
        public System.DateTime BookingTo { get; set; }
        public int RoomId { get; set; }
        public int NoOfMember { get; set; }
        public decimal Total { get; set; }
        public int PaymentTypeId { get; set; }
        public string Id { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual Room Room { get; set; }
        
    }
}
