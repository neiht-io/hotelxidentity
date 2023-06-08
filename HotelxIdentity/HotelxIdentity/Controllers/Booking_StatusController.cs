using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelxIdentity.Models;

namespace HotelxIdentity.Controllers

{
    [Authorize(Roles = "Admin")]
    public class Booking_StatusController : Controller
    {
        private HotelDB db = new HotelDB();

        // GET: Booking_Status
        public ActionResult Index()
        {
            return View(db.BookingStatus.ToList());
        }

        // GET: Booking_Status/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingStatu bookingStatu = db.BookingStatus.Find(id);
            if (bookingStatu == null)
            {
                return HttpNotFound();
            }
            return View(bookingStatu);
        }

        // GET: Booking_Status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Booking_Status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingStatusId,BookingStatus")] BookingStatu bookingStatu)
        {
            if (ModelState.IsValid)
            {
                db.BookingStatus.Add(bookingStatu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookingStatu);
        }

        // GET: Booking_Status/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingStatu bookingStatu = db.BookingStatus.Find(id);
            if (bookingStatu == null)
            {
                return HttpNotFound();
            }
            return View(bookingStatu);
        }

        // POST: Booking_Status/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingStatusId,BookingStatus")] BookingStatu bookingStatu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingStatu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookingStatu);
        }

        // GET: Booking_Status/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingStatu bookingStatu = db.BookingStatus.Find(id);
            if (bookingStatu == null)
            {
                return HttpNotFound();
            }
            return View(bookingStatu);
        }

        // POST: Booking_Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookingStatu bookingStatu = db.BookingStatus.Find(id);
            db.BookingStatus.Remove(bookingStatu);
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
