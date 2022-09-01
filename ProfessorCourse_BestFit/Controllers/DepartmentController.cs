using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using ProfessorCourse_BestFit.StyleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ProfessorCourseBestFitEntities _context;
        private readonly Department_DAL department_DAL;
        public DepartmentController()
        {
            _context = new ProfessorCourseBestFitEntities();
            department_DAL = new Department_DAL();
        }

        // GET: All Department
        public ActionResult All_Departments()
        {   
            return View(department_DAL.Get_All_Departments());
        }

        // GET: Create Department
        public ActionResult Create_Department()
        {
            var create_department = new DepartmentViewModel();
            create_department.List_User_Details = department_DAL.Get_All_Professors();
            return View(create_department);
            
        }

        [HttpPost]
        public ActionResult Create_Department(DepartmentViewModel departmentViewModel)
        {
            return View();
        }

        // GET: Show Department Information
        public ActionResult Show_Department_Information(int id)
        {
            TreeData td = new TreeData();
            td.ProcessRequest(id);
            return View();
        }

        public ActionResult Edit_Department()
        {
            return View();
        }

        public ActionResult Edit_Department_name()
        {
            ViewBag.Edit_Department_name = "Edit_Department_name";
            return View("Edit_Department");
        }

        public ActionResult Edit_Department_Managers()
        {
            return View();
        }

        public ActionResult Edit_Department_Programs()
        {
            return View();
        }


    }
}