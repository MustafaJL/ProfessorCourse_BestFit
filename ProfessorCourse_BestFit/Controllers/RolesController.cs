using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class RolesController : Controller
    {
        private readonly ProfessorCourseBestFit1Entities1 _context;
        private readonly RolePermissions_DAL _sp;

        private readonly isExistChecker isExistChecker;
        public RolesController()
        {
            _context = new ProfessorCourseBestFit1Entities1();
            _sp = new RolePermissions_DAL();
            isExistChecker = new isExistChecker();
        }

        public ActionResult Index()
        {
            var roles = _context.Roles.Where(x => x.isDeleted == false).ToList();
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
                var isExistName = _context.Roles.Where(u => u.RoleName == model.RoleName);
                if (isExistName.Any())
                {
                    RolesViewModel roleView = new RolesViewModel
                    {
                        RoleId = 0,
                        RoleName = model.RoleName
                    };
                    ViewBag.id = model.RoleId;
                    ViewBag.IsExist = "It seems the role you are trying to add or update is already exist.";
                    return View(roleView);
                }
                if (isExist == null)
                {

                    Role role = new Role
                    {
                        RoleName = model.RoleName
                    };
                    _context.Roles.Add(role);

                    _context.SaveChanges();
                    // add spCreateRolePermisssions
                    _sp.CreateRolePermissions(role.RoleId);
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


        public ActionResult RolePermission(int? id)
        {
            var role = _context.Roles.SingleOrDefault(x => x.RoleId == id);

            var permission = _sp.GetPermissionsByRoleId((int)id);

            ViewBag.roleid = role.RoleId;
            RolesPermissionsViewModel roleView = new RolesPermissionsViewModel
            {

                Role = role,
                Permissions = permission
            };
            return View(roleView);
        }

        [HttpPost]
        public JsonResult RolePermission(int roleId, string[] permissions)
        {
            string perms = "";
            if (permissions.Length > 0 && permissions != null)
            {
                perms = string.Join(",", permissions);

            }
            _sp.UpdateRolePermissions(roleId, perms);
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var role = _context.Roles.SingleOrDefault(x => x.RoleId == id);
            role.isDeleted = true;
            _context.SaveChanges();
            return Json("success");
        }
    }
}