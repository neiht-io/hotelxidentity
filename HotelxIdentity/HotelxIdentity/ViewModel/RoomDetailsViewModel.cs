using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelxIdentity.Models;

namespace HotelxIdentity.ViewModel
{
    public class RoomDetailsViewModel
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomImage { get; set; }
        public decimal RoomPrice { get; set; }
        public virtual string BookingStatus { get; set; }
        public virtual string RoomType { get; set; }
        public int RoomCapacity { get; set; }
        public string RoomDescription { get; set; }
    }
}