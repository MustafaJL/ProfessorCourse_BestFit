using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProfessorCourse_BestFit.Messages;

namespace ProfessorCourse_BestFit.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ProfessorCourseBestFit1Entities _context;
        private readonly Department_DAL department_DAL;
        private readonly User_DAL user_DAL;
        //private readonly Messages messages;
        public DepartmentController()
        {
            _context = new ProfessorCourseBestFit1Entities();
            department_DAL = new Department_DAL();
            user_DAL = new User_DAL();
            //messages = new Messages();
        }

        private bool Name_Required(string name)
        {
            if (name == null)
            {
                return true;
            }
            return false;
        }

        private bool Name_Exist(string name)
        {
            var check_Name = _context.Departments.Where(
                x => x.Dep_Name == name
                ).FirstOrDefault();

            if (check_Name != null)
            {
                return true;
            }
            return false;
        }

        //GET :
        public ActionResult All_Departments()
        {
            var departmentViewModel = new DepartmentViewModel();

            departmentViewModel.all_Departments = _context.Departments.ToList();

            return View(departmentViewModel);
        }

        //GET :
        public ActionResult Create_Department()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Department(DepartmentViewModel departmentViewModel)
        {
            if (Name_Required(departmentViewModel.Department.Dep_Name))
            {
               // ViewBag.nameRequired = messages.name_Required;
                //.data_not_saved = messages.data_not_saved;
                return View(departmentViewModel);
            }

            if (Name_Exist(departmentViewModel.Department.Dep_Name))
            {
                //ViewBag.nameExist = messages.name_exist;
                //ViewBag.data_not_saved = messages.data_not_saved;
                return View(departmentViewModel);
            }

            _context.Departments.Add(departmentViewModel.Department);
            _context.SaveChanges();

            //ViewBag.Done = messages.message_success_submit_title;
            //ViewBag.Saved = messages.message_success_submit_body;

            return View(departmentViewModel);
        }

        //GET :
        public ActionResult view_Department_Information(int id)
        {
            var departmentViewModel = new DepartmentViewModel();

            departmentViewModel.Department = _context.Departments.Where(
                x => x.DepId == id
                ).FirstOrDefault();

            departmentViewModel.managers = user_DAL.Get_Users_Department(id, 1);

            departmentViewModel.employee = user_DAL.Get_Users_Department(id, 2);

            return View(departmentViewModel);
        }

        //GET :
        public ActionResult Edit_Department_Name(int id)
        {
            var departmentViewModel = new DepartmentViewModel();

            departmentViewModel.Department = _context.Departments.Where(
                x => x.DepId == id
                ).FirstOrDefault();

            return View(departmentViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Department_Name(DepartmentViewModel departmentViewModel, int id)
        {
            if (Name_Required(departmentViewModel.Department.Dep_Name))
            {
               // ViewBag.nameRequired = messages.name_Required;
//ViewBag.data_not_saved = messages.data_not_saved;
                return View(departmentViewModel);
            }

            if (Name_Exist(departmentViewModel.Department.Dep_Name))
            {
               // ViewBag.nameExist = messages.name_exist;
               // ViewBag.data_not_saved = messages.data_not_saved;
                return View(departmentViewModel);
            }

            var edit_Department = _context.Departments.Where(
                x => x.DepId == id
                ).FirstOrDefault();

            edit_Department.Dep_Name = departmentViewModel.Department.Dep_Name;
            _context.SaveChanges();

           // ViewBag.Done = messages.message_success_submit_title;
           // ViewBag.Saved = messages.message_success_submit_body;

            return View(departmentViewModel);
        }

        [HttpPost]
        public JsonResult Deactivate_Department(int id)
        {
            var delete_Department = _context.Departments.Where(
                x => x.DepId == id
                ).FirstOrDefault();

            delete_Department.isDeleted = true;
            _context.SaveChanges();

            return Json(new
            {
                redirectUrl = Url.Action("All_Departments", "Department"),
                isRedirect = true
            });
        }

        [HttpPost]
        public JsonResult Activate_Department(int id)
        {
            var delete_Department = _context.Departments.Where(
                x => x.DepId == id
                ).FirstOrDefault();

            delete_Department.isDeleted = false;
            _context.SaveChanges();

            return Json(new
            {
                redirectUrl = Url.Action("All_Departments", "Department"),
                isRedirect = true
            });
        }

        //GET :
        public ActionResult Add_Remove_Department_Managers(int id)
        {
            var departmentViewModel = new DepartmentViewModel();
            departmentViewModel.normal_Users = user_DAL.Get_Users_Department(id, 3);
            departmentViewModel.managers = user_DAL.Get_Users_Department(id, 1);
            return View(departmentViewModel);
        }

        [HttpPost]
        public JsonResult Activate_Department(int[] data)
        {
            if (data == null || data.Length == 0)
            {
                return Json(new
                {
                    redirectUrl = Url.Action("All_Departments", "Department"),
                    isRedirect = true
                });
            }
            return Json(new
            {
                redirectUrl = Url.Action("Add_Remove_Department_Managers", "Department"),
                isRedirect = true
            });
        }

        //GET :
        public ActionResult Add_Remove_Department_Employees(int id)
        {
            //Need some code
            return View();
        }

        //GET :
        public ActionResult Add_Remove_Department_Programs(int id)
        {
            //Need some code
            return View();
        }
    }
}