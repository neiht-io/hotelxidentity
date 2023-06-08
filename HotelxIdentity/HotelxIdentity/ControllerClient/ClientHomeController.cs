using HotelxIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelxIdentity.ModelClient;
using Microsoft.AspNet.Identity;
using HotelxIdentity.ViewModel;

namespace HotelxIdentity.ControllerClient
{
   
    
    
    public class ClientHomeController : Controller
    { private HotelDB db = new HotelDB();
        // GET: ClientHome
        public ActionResult Index()
        {
            return View();
        }

        // GET: Client/Home/RoomList\
        [AllowAnonymous]
        public ActionResult RoomList()
        {
            var roomTypes = db.RoomTypes.ToList();
            var roomViewModels = new List<RoomViewModelCL>();

            foreach (var roomType in roomTypes)
            {
                var rooms = db.Rooms.Where(r => r.RoomTypeId == roomType.RoomTypeId && r.IsActive).ToList();
                var roomVMs = rooms.Select(r => new RoomViewModelCL
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    RoomImage = r.RoomImage,
                    RoomPrice = r.RoomPrice,
                    RoomTypeName = roomType.RoomTypeName,
                    RoomCapacity = r.RoomCapacity,
                    RoomDescription = r.RoomDescription

                }).ToList();

                roomViewModels.AddRange(roomVMs);
            }

            return View(roomViewModels);
        }

        // GET: Client/Home/SearchRooms
        [AllowAnonymous]
        public ActionResult SearchRooms()
        {
            return View();
        }

        // POST: Client/Home/SearchRooms
        [HttpPost]
        public ActionResult SearchRooms(BookingViewModelCL bvm, bool showLayout = true)
        {
            //ViewBag.ShowLayout = showLayout;
            /*iewBag.ShowLayout = showLayout ?? true;*/
            ViewBag.ShowLayout = showLayout;
            if (!ModelState.IsValidField("BookingFrom") || !ModelState.IsValidField("BookingTo") || !ModelState.IsValidField("NoOfMember"))
            {
                ModelState.AddModelError(string.Empty, "Nhập đầy đủ thông tin ");
                return View();
                //return PartialView("SearchRoomPartial", bvm);
                //return RedirectToAction("Index", "Home");
            }

            if(bvm.NoOfMember <= 0)
            {
                ModelState.AddModelError(string.Empty, "Số người không hợp lệ");
                return View();
                //return PartialView("SearchRoomPartial", bvm);
            }

                var availableRooms = db.Rooms.Where(r =>
                           r.IsActive &&
                           r.RoomCapacity >= bvm.NoOfMember &&
                           r.RoomBookings.All(rb =>
                            (bvm.BookingFrom >= rb.BookingTo || bvm.BookingTo <= rb.BookingFrom)
                           && (bvm.BookingFrom >= rb.BookingTo || bvm.BookingTo <= rb.BookingFrom)
                           )
                       ).ToList();

                        ViewData["RoomCapacity"] = bvm.NoOfMember;
                        var roomViewModels = availableRooms.Select(r => new RoomViewModelCL
                        {

                            RoomId = r.RoomId,
                            RoomNumber = r.RoomNumber,
                            RoomImage = r.RoomImage,
                            RoomPrice = r.RoomPrice,
                            RoomTypeName = r.RoomType.RoomTypeName,
                            RoomCapacity = r.RoomCapacity,
                            RoomDescription = r.RoomDescription,

                        }).ToList();

                        return PartialView("_RoomList", roomViewModels);
                
                




        }

        // GET: Client/Home/BookRoom/5
        [Authorize(Roles ="Admin,Client")]
        public ActionResult BookRoom(int id)
        {
            var room = db.Rooms.Find(id);

            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomId = room.RoomId;
            ViewBag.RoomNumber = room.RoomNumber;
            ViewBag.RoomImage = room.RoomImage;
            ViewBag.RoomPrice = room.RoomPrice;


            var bookingViewModel = new BookingViewModelCL
            {
                RoomId = room.RoomId
            };

            return View(bookingViewModel);
        }

        // POST: Client/Home/BookRoom
        [Authorize(Roles = "Admin,Client")]
        [HttpPost]
        public ActionResult BookRoom(BookingViewModelCL bookingViewModel)
        {
            if (ModelState.IsValid)
            {
                var room = db.Rooms.Find(bookingViewModel.RoomId);

                if (room == null)
                {
                    return HttpNotFound();
                }
                // t moi them doan nay, m xem coi dc ko
                ViewBag.RoomId = room.RoomId;
                ViewBag.RoomNumber = room.RoomNumber;
                ViewBag.RoomImage = room.RoomImage;
                ViewBag.RoomPrice = room.RoomPrice;


                // Kiểm tra người dùng nhập ngày đi > đến 
                if (bookingViewModel.BookingFrom > bookingViewModel.BookingTo)
                {
                    ModelState.AddModelError(string.Empty, "Ngày đặt phòng không hợp lệ. Vui lòng chọn lại.");
                    return View(bookingViewModel);
                }
                
                if (bookingViewModel.NoOfMember <= 0)
                {
                    ModelState.AddModelError(string.Empty, "Số người ở không hợp lệ. Vui lòng nhập > O");
                    return View(bookingViewModel);
                }
                // Kiểm tra xem phòng đã có người đặt trong khoảng thời gian đã chọn hay chưa
                bool isRoomAvailable = IsRoomAvailable(bookingViewModel.RoomId, bookingViewModel.BookingFrom, bookingViewModel.BookingTo);

                if (!isRoomAvailable)
                {
                    ModelState.AddModelError(string.Empty, "Phòng đã có người đặt trong khoảng thời gian này. Vui lòng chọn phòng khác.");
                    return View(bookingViewModel);
                }

                // Checkin 14h và Checkout 12h hôm sau
                bookingViewModel.BookingFrom = bookingViewModel.BookingFrom.Date.AddHours(14);
                bookingViewModel.BookingTo = bookingViewModel.BookingTo.Date.AddHours(12);

                string userId = Session["UserId"] as string;


                int numOfDays = Convert.ToInt32((bookingViewModel.BookingTo - bookingViewModel.BookingFrom).TotalDays);
                var booking = new RoomBooking

                {
                    Id = userId , //Vì đăng nhập là có Id nên là không càn phải so sánh
                    CustomerName = bookingViewModel.CustomerName,
                    CustomerAddress = bookingViewModel.CustomerAddress,
                    CustomerPhone = bookingViewModel.CustomerPhone,
                    BookingFrom = bookingViewModel.BookingFrom,
                    BookingTo = bookingViewModel.BookingTo,
                    RoomId = bookingViewModel.RoomId,
                    NoOfMember = bookingViewModel.NoOfMember,
                    Total = room.RoomPrice * numOfDays,
                    PaymentTypeId = 1,
                };

                db.RoomBookings.Add(booking);
                db.SaveChanges();

                return RedirectToAction("BookingConfirmation", new { id = booking.BookingId });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Vui lòng nhập đầy đủ thông tin");
            }

            return View(bookingViewModel);
        }

        private bool IsRoomAvailable(int roomId, DateTime bookingFrom, DateTime bookingTo)
        {

            //var existingBooking = db.RoomBookings.FirstOrDefault(r =>
            //    r.RoomId == roomId &&
            //    ((bookingFrom >= r.BookingFrom && bookingFrom <= r.BookingTo) ||
            //     (bookingTo >= r.BookingFrom && bookingTo <= r.BookingTo)));

            //return existingBooking == null;

            var checkBooking = db.RoomBookings
                   .Where(r =>
                           r.RoomId == roomId &&
                           ((bookingFrom >= r.BookingFrom && bookingFrom < r.BookingTo) ||
                           (bookingTo > r.BookingFrom && bookingTo <= r.BookingTo) ||
                           (bookingFrom <= r.BookingFrom && bookingTo >= r.BookingTo)))
                           .ToList();

            return checkBooking.Count == 0;
        }


        // GET: Client/Home/BookingConfirmation/5
        [Authorize(Roles = "Admin,Client")]
        public ActionResult BookingConfirmation(int id)
        {
            var booking = db.RoomBookings.Find(id);

            if (booking == null)
            {
                return HttpNotFound();
            }

            return View(booking);
        }

        // GET: Client/Home/BookingHistory
        [Authorize(Roles = "Admin,Client")]
        public ActionResult BookingHistory()
        {
            //var bookings = db.RoomBookings.Select(ur => ur.UserId).ToList();
            // Lấy UserId của người dùng từ Session
            string getUserId = (string)Session["UserId"];

            var bookings = db.RoomBookings
                .Where(r => r.Id == getUserId).ToList();


            return View(bookings);


        }

        [Authorize(Roles = "Admin,Client")]
        public ActionResult CancelBooking(int id)

        {

            var booking = db.RoomBookings.Find(id);

            if (booking == null)
            {
                return HttpNotFound();
            }

            db.RoomBookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("BookingHistory");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }


}