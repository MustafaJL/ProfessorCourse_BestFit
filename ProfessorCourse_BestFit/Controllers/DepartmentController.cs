using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.messages;
using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using ProfessorCourse_BestFit.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace ProfessorCourse_BestFit.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ProfessorCourseBestFit1 _context;
        private readonly Department_DAL department_DAL;
        private readonly Messages messages;
        public DepartmentController()
        {
            _context = new ProfessorCourseBestFit1();
            department_DAL = new Department_DAL();
            messages = new Messages();
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
            if(Name_Required(departmentViewModel.Department.Dep_Name))
            {
                ViewBag.nameRequired = messages.name_Required;
                ViewBag.data_not_saved = messages.data_not_saved;
                return View(departmentViewModel);
            }

            if(Name_Exist(departmentViewModel.Department.Dep_Name))
            {
                ViewBag.nameExist = messages.name_exist;
                ViewBag.data_not_saved = messages.data_not_saved;
                return View(departmentViewModel);
            }

            _context.Departments.Add(departmentViewModel.Department);
            _context.SaveChanges();

            ViewBag.Done = messages.message_success_submit_title;
            ViewBag.Saved = messages.message_success_submit_body;

            return View(departmentViewModel);
        }

        //GET :
        public ActionResult view_Department_Information(int id)
        {
            var departmentViewModel = new DepartmentViewModel();

            departmentViewModel.Department = _context.Departments.Where(
                x => x.DepId == id
                ).FirstOrDefault();

            departmentViewModel.managers = department_DAL.Get_Department_Managers(id);

            departmentViewModel.employee = department_DAL.Get_Department_Employees(id);

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
                ViewBag.nameRequired = messages.name_Required;
                ViewBag.data_not_saved = messages.data_not_saved;
                return View(departmentViewModel);
            }

            if (Name_Exist(departmentViewModel.Department.Dep_Name))
            {
                ViewBag.nameExist = messages.name_exist;
                ViewBag.data_not_saved = messages.data_not_saved;
                return View(departmentViewModel);
            }

            var edit_Department = _context.Departments.Where(
                x => x.DepId == id
                ).FirstOrDefault();

            edit_Department.Dep_Name = departmentViewModel.Department.Dep_Name;
            _context.SaveChanges();

            ViewBag.Done = messages.message_success_submit_title;
            ViewBag.Saved = messages.message_success_submit_body;

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
            //Need some code
            return View();
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
