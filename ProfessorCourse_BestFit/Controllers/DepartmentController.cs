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
        private readonly string _Conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        private readonly SqlConnection _connection;
        private readonly ProfessorCourseBestFitEntities _context;
        private readonly Department_DAL department_DAL;
        private readonly Messages messages;
        public DepartmentController()
        {
            _context = new ProfessorCourseBestFitEntities();
            department_DAL = new Department_DAL();
            messages = new Messages();
            _connection = new SqlConnection(_Conn);
        }


        // GET: All Department
        public ActionResult All_Departments()
        {   
            DepartmentViewModel departmentViewModel = new DepartmentViewModel();
            departmentViewModel.all_departments = _context.Departments.Where(x => x.isDeleted == false).ToList();
            return View(departmentViewModel);
        }

        // GET: Create Department
        public ActionResult Create_Department()
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Department(DepartmentViewModel departmentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentViewModel);
            }

            var department = _context.Departments.Where(
                x => x.Dep_Name.ToLower() == departmentViewModel.Dep_Name.ToLower() 
                && 
                x.isDeleted == false
                ).FirstOrDefault();

            if(department != null)
            {
                ViewBag.existName = messages.name_exist;
                ViewBag.data_not_saved = messages.data_not_saved;
                return View(departmentViewModel);
            }
            var create_department = new Department();
            create_department.Dep_Name = departmentViewModel.Dep_Name;
            if(departmentViewModel.List_User_Details != null)
            {
                for (int i = 0; i < departmentViewModel.List_User_Details.Count; i++)
                {
                    create_department.User_id += departmentViewModel.List_User_Details[i].ToString();
                    create_department.User_id += ",";
                }
            }

            _context.Departments.Add(create_department);
            try
            {
                _context.SaveChanges();
                ViewBag.savetitle = messages.message_success_submit_title;
                ViewBag.savebody = messages.message_success_submit_body;
            }
            catch
            {
                ViewBag.savetitle = messages.message_failed_submit_title;
                ViewBag.savebody = messages.message_failed_submit_body;
            }
            return View();
        }

        // GET: Show Department Information
        //the id should not be null
        public ActionResult Show_Department_Information(int? id)
        {
            if(id != null)
            {
                DepartmentViewModel departmentViewModel = new DepartmentViewModel();
                var department = _context.Departments.Where(x => x.Dep_Id == id).FirstOrDefault();
                departmentViewModel.Dep_Id = department.Dep_Id;
                departmentViewModel.Dep_Name = department.Dep_Name;
                departmentViewModel.User_id = department.User_id;
                
                if(departmentViewModel.User_id != null)
                {
                    var all_managers = department_DAL.Get_All_Department_Managers(departmentViewModel.User_id);
                    //need procedure (look at user_DAL.cs).
                    departmentViewModel.List_Managers_Details = all_managers;
                    ViewBag.allManagers = messages.message_not_null;
                }
                else
                {
                    ViewBag.allManagers = messages.message_null;
                    ViewBag.noManagers = messages.no_managers;
                }
                departmentViewModel.List_Department_Programs = department_DAL.Get_Department_Programs(departmentViewModel.Dep_Id);
                ViewBag.allPrograms = messages.no_programs;
                
                return View(departmentViewModel);
            }
            else
            {
                //should add a condition if the input id is null
                return View();
            }
        }

        public ActionResult Edit_Department(int id)
        {
            DepartmentViewModel departmentViewModel = new DepartmentViewModel();
            var department = _context.Departments.Where(x => x.Dep_Id == id).FirstOrDefault();
            departmentViewModel.Dep_Id = department.Dep_Id;
            departmentViewModel.Dep_Name = department.Dep_Name;
            return View(departmentViewModel);
        }

        public ActionResult Add_Department_Programs(int id)
        {
            return View();
        }

        public ActionResult Delete_Department(int id)
        {
            DepartmentViewModel departmentViewModel = new DepartmentViewModel();
            departmentViewModel.Department = _context.Departments.Where(x => x.Dep_Id == id).FirstOrDefault();
            return View(departmentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Department(DepartmentViewModel departmentViewModel, int id)
        {
            department_DAL.Delete_Department(id);
            ViewBag.test = true;
            return RedirectToAction("All_Departments");
        }

    }
}
