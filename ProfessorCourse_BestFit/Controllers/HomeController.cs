﻿using ProfessorCourse_BestFit.CustomSecurity;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [CustomAuthorization("Admin,Professor")]

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