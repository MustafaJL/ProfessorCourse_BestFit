using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class PermissionsController : Controller
    {
        private readonly ProfessorCourseBestFitEntities _context;

        public PermissionsController()
        {
            _context = new ProfessorCourseBestFitEntities();
        }
        public ActionResult Index()
        {
            var permissions = _context.Permissions.Where(x => x.isDeleted == false).ToList();
            return View(permissions);
        }

        public ActionResult Upsert(int? id)
        {
            ViewBag.id = id;
            if (id == null || id == 0)
            {

                return View();
            }
            var permission = _context.Permissions.SingleOrDefault(x => x.PId == id);
            PermissionsViewModel roleView = new PermissionsViewModel
            {
                PId = permission.PId,
                PName = permission.PName
            };
            return View(roleView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upsert(PermissionsViewModel model)
        {

            if (ModelState.IsValid)
            {
                var isExist = _context.Permissions.SingleOrDefault(x => x.PId == model.PId);
                if (isExist == null)
                {
                    Permission permission = new Permission
                    {
                        PName = model.PName,
                        CreatedOn = DateTime.Now,


                    };
                    _context.Permissions.Add(permission);
                    _context.SaveChanges();
                }
                else
                {
                    isExist.PName = model.PName;
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }



        [HttpPost]
        public JsonResult Delete(int id)
        {
            var permission = _context.Permissions.SingleOrDefault(x => x.PId == id);
            permission.isDeleted = true;
            _context.SaveChanges();
            return Json("success");
        }
    }
}