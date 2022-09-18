using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class CourseBestFitController : Controller
    {
        private readonly CourseBestFitDAL _sp;
        private readonly ProfessorCourseBestFit1Entities1 _context;


        public CourseBestFitController()
        {
            _sp = new CourseBestFitDAL();
            _context = new ProfessorCourseBestFit1Entities1();
        }

        public ActionResult Index(int? courseId)
        {
            ViewBag.Courses = _context.Courses.Where(u => u.isDeleted == false).ToList();

            if (courseId != null)
            {
                List<CourseBestFitViewModel> list = _sp.GetUsersByCourseId((int)courseId);
                ViewBag.selectedCourse = courseId;
                return View(list);
            }


            return View();
        }





    }
}