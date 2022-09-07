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
        private readonly ProfessorCourseBestFitEntities _context;
        private readonly Department_DAL department_DAL;
        private readonly Messages messages;
        public DepartmentController()
        {
            _context = new ProfessorCourseBestFitEntities();
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
                ).ToList();

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

            departmentViewModel.active_Departments = _context.Departments.Where(
                x => x.isDeleted == false
                ).ToList();

            departmentViewModel.disActive_Departments = _context.Departments.Where(
                x => x.isDeleted == true
                ).ToList();

            return View(departmentViewModel);
        }

        //GET :
        public ActionResult Create_Department()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create_Department(DepartmentViewModel departmentViewModel)
        {
            if(Name_Required(departmentViewModel.Department.Dep_Name))
            {
                //message needed
                ViewBag.nameRequired = true;
                return View(departmentViewModel);
            }

            if(Name_Exist(departmentViewModel.Department.Dep_Name))
            {
                //message needed
                ViewBag.nameExist = true;
                return View(departmentViewModel);
            }

            _context.Departments.Add(departmentViewModel.Department);
            _context.SaveChanges();

            //message needed
            ViewBag.Done = true;

            return View(departmentViewModel);
        }

        //GET :
        public ActionResult view_Department_Information(int Dep_id)
        {
            var departmentViewModel = new DepartmentViewModel();

            departmentViewModel.Department = _context.Departments.Where(
                x => x.Dep_Id == Dep_id
                ).FirstOrDefault();

            return View(departmentViewModel);
        }

        //GET :
        public ActionResult Edit_Department_Name(int Dep_id)
        {
            var departmentViewModel = new DepartmentViewModel();

            departmentViewModel.Department = _context.Departments.Where(
                x => x.Dep_Id == Dep_id
                ).FirstOrDefault();

            return View(departmentViewModel);
        }


        [HttpPost]
        public ActionResult Edit_Department_Name(DepartmentViewModel departmentViewModel, int Dep_id)
        {
            if (Name_Required(departmentViewModel.Department.Dep_Name))
            {
                //message needed
                ViewBag.nameRequired = true;
                return View(departmentViewModel);
            }

            if (Name_Exist(departmentViewModel.Department.Dep_Name))
            {
                //message needed
                ViewBag.nameExist = true;
                return View(departmentViewModel);
            }

            var edit_Department = _context.Departments.Where(
                x => x.Dep_Id == Dep_id
                ).FirstOrDefault();

            edit_Department = departmentViewModel.Department;
            _context.SaveChanges();

            //message needed
            ViewBag.Done = true;

            return View(departmentViewModel);
        }


        public ActionResult Delete_Department(int Dep_id)
        {
            //Need some code
            return View();
        }

        //GET :
        public ActionResult Add_Department_Managers(int Dep_id)
        {
            //Need some code
            return View();
        }

        //GET :
        public ActionResult Add_Department_Employees(int Dep_id)
        {
            //Need some code
            return View();
        }

        //GET :
        public ActionResult Add_Department_Programs(int Dep_id)
        {
            //Need some code
            return View();
        }

        public ActionResult Activate_Department(int Dep_id)
        {
            //Need some code
            return View();
        }

    }
}
