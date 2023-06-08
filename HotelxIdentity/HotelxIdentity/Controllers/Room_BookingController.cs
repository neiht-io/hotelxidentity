using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelxIdentity.Models;
using HotelxIdentity.ViewModel;

namespace HotelxIdentity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class Room_BookingController : Controller
    {
        private HotelDB db = new HotelDB();

        // GET: Room_Booking
        public ActionResult Index()
        {
            List<RoomBookingViewModel> listBookingViewModel =
                (from roomBooking in db.RoomBookings
                 join room in db.Rooms on roomBooking.RoomId equals room.RoomId
                 join paymenttype in db.PaymentTypes on roomBooking.PaymentTypeId equals paymenttype.PaymentTypeId
                 select new RoomBookingViewModel()
                 {
                     CustomerName = roomBooking.CustomerName,
                     CustomerAddress = roomBooking.CustomerAddress,
                     CustomerPhone = roomBooking.CustomerPhone,
                     BookingFrom = roomBooking.BookingFrom,
                     BookingTo = roomBooking.BookingTo,
                     NoOfMember = roomBooking.NoOfMember,
                     RoomNumber = room.RoomNumber,
                     RoomPrice = room.RoomPrice,
                     BookingId = roomBooking.BookingId,
                     PaymentType = paymenttype.PaymentType1,
                     NumberOfDays = System.Data.Entity.DbFunctions.DiffDays(roomBooking.BookingFrom, roomBooking.BookingTo).Value,
                     Total = roomBooking.Total
                 }).ToList();
            return View(listBookingViewModel);
        }

        // GET: Room_Booking/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBooking roomBooking = db.RoomBookings.Find(id);
            if (roomBooking == null)
            {
                return HttpNotFound();
            }
            return View(roomBooking);
        }

        // GET: Room_Booking/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "PaymentTypeId", "PaymentType1");
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber");
            return View();
        }

        // POST: Room_Booking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,CustomerName,CustomerAddress,CustomerPhone,BookingFrom,BookingTo,RoomId,NoOfMember,Total,PaymentTypeId,Id")] RoomBooking roomBooking)
        {
            if (ModelState.IsValid)
            {
                db.RoomBookings.Add(roomBooking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", roomBooking.Id);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "PaymentTypeId", "PaymentType1", roomBooking.PaymentTypeId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", roomBooking.RoomId);
            return View(roomBooking);
        }

        // GET: Room_Booking/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBooking roomBooking = db.RoomBookings.Find(id);
            if (roomBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", roomBooking.Id);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "PaymentTypeId", "PaymentType1", roomBooking.PaymentTypeId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", roomBooking.RoomId);
            return View(roomBooking);
        }

        // POST: Room_Booking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,CustomerName,CustomerAddress,CustomerPhone,BookingFrom,BookingTo,RoomId,NoOfMember,Total,PaymentTypeId,Id")] RoomBooking roomBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", roomBooking.Id);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "PaymentTypeId", "PaymentType1", roomBooking.PaymentTypeId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", roomBooking.RoomId);
            return View(roomBooking);
        }

        // GET: Room_Booking/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBooking roomBooking = db.RoomBookings.Find(id);
            if (roomBooking == null)
            {
                return HttpNotFound();
            }
            return View(roomBooking);
        }

        // POST: Room_Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomBooking roomBooking = db.RoomBookings.Find(id);
            db.RoomBookings.Remove(roomBooking);
            db.SaveChanges();
            return RedirectToAction("Index");
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
