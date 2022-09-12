using ProfessorCourse_BestFit.CustomSecurity;
using ProfessorCourse_BestFit.Models;
using System.Linq;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProfessorCourseBestFit1Entities _context;

        public HomeController()
        {
            _context = new ProfessorCourseBestFit1Entities();
        }
        public ActionResult Index()
        {
            var userNumber = _context.Users.ToList().Count;
            var courseNumber = _context.Courses.ToList().Count;
            var profNumber = _context.Users.Where(u => u.RoleId == 20).ToList().Count();

            ViewBag.userNumber = userNumber.ToString();
            ViewBag.courseNumber = courseNumber.ToString();
            ViewBag.profNumber = profNumber.ToString();
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