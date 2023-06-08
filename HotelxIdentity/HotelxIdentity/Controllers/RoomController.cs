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
   
    public class RoomController : Controller
    {
        private HotelDB db = new HotelDB();

        // GET: Room

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            IEnumerable<RoomDetailsViewModel> roomDetailsViewModels =
                (from room in db.Rooms
                 join bookingstatus in db.BookingStatus on room.BookingStatusId equals bookingstatus.BookingStatusId
                 join roomtype in db.RoomTypes on room.RoomTypeId equals roomtype.RoomTypeId
                 where room.IsActive == true
                 select new RoomDetailsViewModel()
                 {
                     RoomNumber = room.RoomNumber,
                     RoomDescription = room.RoomDescription,
                     RoomCapacity = room.RoomCapacity,
                     RoomPrice = room.RoomPrice,
                     BookingStatus = bookingstatus.BookingStatus,
                     RoomType = roomtype.RoomTypeName,
                     RoomImage = room.RoomImage,
                     RoomId = room.RoomId,
                 }).ToList();
            //var rooms = db.Rooms.Include(r => r.BookingStatu).Include(r => r.RoomType);
            return View(roomDetailsViewModels);
        }

        // GET: Room/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: Room/Create

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            RoomViewModel roomViewModel = new RoomViewModel();
            roomViewModel.ListOfBookingStatus = (from bookingstatus in db.BookingStatus
                                                 select new SelectListItem()
                                                 {
                                                     Text = bookingstatus.BookingStatus,
                                                     Value = bookingstatus.BookingStatusId.ToString()
                                                 }).ToList();

            roomViewModel.ListOfRoomType = (from roomtype in db.RoomTypes
                                            select new SelectListItem()
                                            {
                                                Text = roomtype.RoomTypeName,
                                                Value = roomtype.RoomTypeId.ToString()
                                            }).ToList();

            //ViewBag.BookingStatusId = new SelectList(db.BookingStatus, "BookingStatusId", "BookingStatus");
            //ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName");
            return View(roomViewModel);
        }

        // POST: Room/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoomViewModel roomViewModel)
        {
            //string ImageUniqueName = Guid.NewGuid().ToString();
            //string ActualImageName = ImageUniqueName + Path.GetExtension(roomViewModel.Image.FileName);
            //roomViewModel.Image.SaveAs(Server.MapPath("~/Hinh/" + ActualImageName));

            HttpPostedFileBase file = Request.Files["RoomImage"];
            if (file != null && file.FileName != "")
            {
                string serverPath = HttpContext.Server.MapPath("~/Hinh");
                string filePath = serverPath + "/" + file.FileName;
                file.SaveAs(filePath);
                roomViewModel.RoomImage = file.FileName;
            }
            Room room = new Room()
            {
                RoomNumber = roomViewModel.RoomNumber,
                RoomDescription = roomViewModel.RoomDescription,
                IsActive = true,
                BookingStatusId = roomViewModel.BookingStatusId,
                RoomCapacity = roomViewModel.RoomCapacity,
                RoomTypeId = roomViewModel.RoomTypeId,
                RoomPrice = roomViewModel.RoomPrice,
                RoomImage = roomViewModel.RoomImage,
            };
            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roomViewModel);
        }
        //public ActionResult Create()
        //{
        //    ViewBag.BookingStatusId = new SelectList(db.BookingStatus, "BookingStatusId", "BookingStatus");
        //    ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName");
        //    return View();
        //}

        //// POST: Room/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "RoomId,RoomNumber,RoomImage,RoomPrice,BookingStatusId,RoomTypeId,RoomCapacity,RoomDescription,IsActive")] Room room)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Rooms.Add(room);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.BookingStatusId = new SelectList(db.BookingStatus, "BookingStatusId", "BookingStatus", room.BookingStatusId);
        //    ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName", room.RoomTypeId);
        //    return View(room);
        //}

        // GET: Room/Edit/5

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookingStatusId = new SelectList(db.BookingStatus, "BookingStatusId", "BookingStatus", room.BookingStatusId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName", room.RoomTypeId);
            return View(room);
        }

        // POST: Room/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoomId,RoomNumber,RoomImage,RoomPrice,BookingStatusId,RoomTypeId,RoomCapacity,RoomDescription,IsActive")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookingStatusId = new SelectList(db.BookingStatus, "BookingStatusId", "BookingStatus", room.BookingStatusId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "RoomTypeId", "RoomTypeName", room.RoomTypeId);
            return View(room);
        }

        // GET: Room/Delete/5

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Room/Delete/5

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
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
