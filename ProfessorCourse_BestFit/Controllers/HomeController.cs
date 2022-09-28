using ProfessorCourse_BestFit.CustomSecurity;
using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Models;
using System.Linq;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProfessorCourseBestFit1Entities1 _context;
        private readonly UserKeywords_DAL _sp;

        public HomeController()
        {
            _context = new ProfessorCourseBestFit1Entities1();
            _sp = new UserKeywords_DAL();
        }
        public ActionResult Index()
        {
            var courseNumber = _context.Courses.Where(u => u.isDeleted == false).ToList().Count;
            var userNumber = _context.Users.Where(u => u.isDeleted == false).ToList().Count();

            ViewBag.userNumber = userNumber.ToString();
            ViewBag.courseNumber = courseNumber.ToString();
            ViewBag.profNumber = userNumber.ToString();


            return View();
        }

        [HttpPost]
        public JsonResult UserKeywords(string name)
        {
            var userKeyowds = _sp.GetUsersKeywords();


            return Json(userKeyowds);
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