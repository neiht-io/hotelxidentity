using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelxIdentity.Models;
using System.Xml.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HotelxIdentity.ViewModel
{
    public class BookingViewModel
    {
       public BookingViewModel() {
            ListOfRoom = new List<SelectListItem>();
            ListOfPaymentType = new List<SelectListItem>();
        }

        //public int BookingId { get; set; }

        [Display(Name = "Tên khách hàng")]
        public string CustomerName { get; set; }

        [Display(Name = "Địa chỉ")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Số điện thoại")]
        public string CustomerPhone { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Booking From")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BookingFrom { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Booking To")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BookingTo { get; set; }

        [Display(Name = "Số phòng")]
        public int RoomId { get; set; }

        [Display(Name = "Số người ở")]
        public int NoOfMember { get; set; }

        //[Display(Name = "Tổng tiền")]
        //public decimal Total { get; set; }

        [Display(Name = "Thanh toán")]
        public int PaymentTypeId { get; set; }


        public IEnumerable<SelectListItem> ListOfRoom { get; set; }
        public IEnumerable<SelectListItem> ListOfPaymentType { get; set; }
        public List<Room> listRoom { get; set; } = new List<Room>();


    }
}