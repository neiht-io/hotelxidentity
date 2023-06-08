using HotelxIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace HotelxIdentity.ViewModel
{
    public class RoomBookingViewModel
    {
        public int BookingId { get; set; }

        [Display(Name = "Tên")]
        public string CustomerName { get; set; }

        [Display(Name = "Số ngày")]
        public int NumberOfDays { get; set; }

        [Display(Name = "Địa chỉ")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Số điện thoại")]
        public String CustomerPhone { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Từ ngày")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BookingFrom { get; set; } = null;

        [DataType(DataType.Date)]
        [Display(Name = "Đến ngày")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BookingTo { get; set; } = null;

        [Display(Name = "Số phòng")]
        public String RoomNumber { get; set; }

        [Display(Name = "Số người ở")]
        public int NoOfMember { get; set; }

        [Display(Name = "Tổng tiền")]
        public decimal Total { get; set; }

        [Display(Name = "Giá phòng")]

        public decimal RoomPrice { get; set; }

        //public int PaymentTypeId { get; set; }


        public virtual String PaymentType { get; set; }

        public IList<Room> listRoom { get; set; } = new List<Room>();
    }
}