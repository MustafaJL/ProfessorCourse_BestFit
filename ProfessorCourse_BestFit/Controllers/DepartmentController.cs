using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult ListDepartment()
        {
            return View();
        }
    }
}