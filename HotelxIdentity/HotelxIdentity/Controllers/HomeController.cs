using HotelxIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelxIdentity.ModelClient;


namespace HotelxIdentity.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private HotelDB db = new HotelDB();

        // GET: Client/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}