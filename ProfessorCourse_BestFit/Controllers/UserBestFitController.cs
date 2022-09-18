using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class UserBestFitController : Controller
    {
        private readonly UserBestFitDAL _sp;
        private readonly ProfessorCourseBestFit1Entities1 _context;


        public UserBestFitController()
        {
            _sp = new UserBestFitDAL();
            _context = new ProfessorCourseBestFit1Entities1();
        }

        public ActionResult Index(int? userId)
        {
            ViewBag.Users = _context.Users.Where(u => u.isDeleted == false).ToList();

            if (userId != null)
            {
                List<UserBestFitViewModel> list = _sp.GetCourseByUserId((int)userId);
                ViewBag.selectedCourse = userId;
                return View(list);
            }


            return View();
        }



    }
}