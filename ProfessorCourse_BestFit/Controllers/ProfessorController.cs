using ProfessorCourse_BestFit.CustomSecurity;
using ProfessorCourse_BestFit.DAL;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class ProfessorController : Controller
    {

        User_DAL userDAL = new User_DAL();
        // GET: Professor

        [CustomAuthorization("Admin")]
        public ActionResult Index()
        {
            var userList = userDAL.GetUserRoles();
            return View(userList);
        }
    }
}