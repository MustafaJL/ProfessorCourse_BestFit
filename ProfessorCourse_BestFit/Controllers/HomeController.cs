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

            ViewBag.userNumber = userNumber.ToString();
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