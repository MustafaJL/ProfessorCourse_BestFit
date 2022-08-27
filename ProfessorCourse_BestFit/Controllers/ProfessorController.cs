using ProfessorCourse_BestFit.DAL;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class ProfessorController : Controller
    {

        User_DAL userDAL = new User_DAL();
        // GET: Professor
        public ActionResult Index()
        {

            return View();
        }
    }
}