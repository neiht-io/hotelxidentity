using HotelxIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace HotelxIdentity.ViewModel
{
    public class RoomViewModel
    {
        public RoomViewModel()
        {
            ListOfBookingStatus = new List<SelectListItem>();
            ListOfRoomType = new List<SelectListItem>();
            this.RoomBookings = new HashSet<RoomBooking>();
        }

        public RoomViewModel(int roomId, string roomNumber, string roomImage, decimal roomPrice,
            int bookingStatusId, int roomTypeId, int roomCapacity, string roomDescription)
        {
            RoomId = roomId;
            RoomNumber = roomNumber;
            RoomImage = roomImage;
            this.RoomPrice = roomPrice;
            BookingStatusId = bookingStatusId;
            RoomTypeId = roomTypeId;
            RoomCapacity = roomCapacity;
            RoomDescription = roomDescription;
            ListOfBookingStatus = new List<SelectListItem>();
            ListOfRoomType = new List<SelectListItem>();
            this.RoomBookings = new HashSet<RoomBooking>();
        }

        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomImage { get; set; }
        public decimal RoomPrice { get; set; }
        public virtual int BookingStatusId { get; set; }
        public virtual int RoomTypeId { get; set; }

        public int RoomCapacity { get; set; }
        public string RoomDescription { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public IEnumerable<SelectListItem> ListOfBookingStatus { get; set; }
        public IEnumerable<SelectListItem> ListOfRoomType { get; set; }
        public virtual ICollection<RoomBooking> RoomBookings { get; set; }
    }
}