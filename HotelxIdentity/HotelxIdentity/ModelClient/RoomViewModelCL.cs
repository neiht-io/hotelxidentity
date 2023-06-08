using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelxIdentity.ModelClient
{
    public class RoomViewModelCL
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomImage { get; set; }
        public decimal RoomPrice { get; set; }
        public string RoomTypeName { get; set; }
        public int RoomCapacity { get; set; }
    
        public string RoomDescription { get; set; }
    }
}