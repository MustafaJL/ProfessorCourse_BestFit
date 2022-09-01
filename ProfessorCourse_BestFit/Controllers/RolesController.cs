using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class RolesController : Controller
    {
        private readonly ProfessorCourseBestFitEntities2 _context;

        public RolesController()
        {
            _context = new ProfessorCourseBestFitEntities2();
        }

        public ActionResult Index()
        {
            var roles = _context.Roles.Where(x => x.deleted == false).ToList();
            return View(roles);
        }




        public ActionResult Upsert(int? id)
        {
            ViewBag.id = id;
            if (id == null || id == 0)
            {

                return View();
            }
            var role = _context.Roles.SingleOrDefault(x => x.RoleId == id);
            RolesViewModel roleView = new RolesViewModel
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName
            };
            return View(roleView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upsert(RolesViewModel model)
        {

            if (ModelState.IsValid)
            {
                var isExist = _context.Roles.SingleOrDefault(x => x.RoleId == model.RoleId);
                if (isExist == null)
                {
                    Role role = new Role
                    {
                        RoleName = model.RoleName
                    };
                    _context.Roles.Add(role);
                    _context.SaveChanges();
                }
                else
                {
                    isExist.RoleName = model.RoleName;
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }



        [HttpPost]
        public JsonResult Delete(int id)
        {
            var role = _context.Roles.SingleOrDefault(x => x.RoleId == id);
            role.deleted = true;
            _context.SaveChanges();
            return Json("success");
        }
    }
}