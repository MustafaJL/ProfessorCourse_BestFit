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
        private readonly ProfessorCourseBestFitEntities _context;
        private readonly Department_DAL department_DAL;
        private readonly Messages messages;
        public DepartmentController()
        {
            _context = new ProfessorCourseBestFitEntities();
            department_DAL = new Department_DAL();
            messages = new Messages();
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

            _context.Departments.Add(create_department);
            try
            {
                _context.SaveChanges();
                List<Department> departments = _context.Departments.ToList();
                for(int i = departments.Count()-1; i > departments.Count()-2; i--){
                    ViewBag.Dep_Id = departments[i].Dep_Id;
                    departmentViewModel.Dep_Id = departments[i].Dep_Id;
                }
                departmentViewModel.List_User_Details = department_DAL.Get_All_Professors();
                departmentViewModel.List_All_Programs = _context.Programs.Where(
                    x => x.isDeleted == false 
                    &&
                    x.Dep_Id == 0).ToList();
            }
            catch
            {
                ViewBag.savetitle = messages.message_failed_submit_title;
                ViewBag.savebody = messages.message_failed_submit_body;
            }
            return View(departmentViewModel);
        }

        [HttpPost]
        public JsonResult Add_Manager(int dep_id, int id)
        {
            var department = _context.Departments.Where(x => x.Dep_Id == dep_id).FirstOrDefault();
            department.User_id += id+",";
            _context.SaveChanges();
            return Json("success");
        }

        [HttpPost]
        public JsonResult Add_Program(int dep_id, int id)
        {
            var program = _context.Programs.Where(x => x.PId == id).FirstOrDefault();
            program.Dep_Id = dep_id;
            _context.SaveChanges();
            return Json("success");
        }


        // GET: Show Department Information
        public ActionResult Show_Department_Information(int? id)
        {
                DepartmentViewModel departmentViewModel = new DepartmentViewModel();
                var department = _context.Departments.Where(x => x.Dep_Id == id).FirstOrDefault();
                departmentViewModel.Dep_Id = department.Dep_Id;
                departmentViewModel.Dep_Name = department.Dep_Name;
                departmentViewModel.User_id = department.User_id;
                
                if(departmentViewModel.User_id != null)
                {
                //need procedure (because of split).
                departmentViewModel.List_Managers_Details = department_DAL.Get_All_Department_Managers(departmentViewModel.User_id);
                    ViewBag.allManagers = messages.message_not_null;
                }

                departmentViewModel.List_All_Programs = _context.Programs.Where(x => x.Dep_Id == departmentViewModel.Dep_Id && x.isDeleted == false).ToList();
                return View(departmentViewModel);
        }

        public ActionResult Edit_Department(int id)
        {
            DepartmentViewModel departmentViewModel = new DepartmentViewModel();
            departmentViewModel.Department = _context.Departments.Where(x => x.Dep_Id == id).FirstOrDefault();
            departmentViewModel.List_Managers_Details = department_DAL.Get_All_Department_Managers(departmentViewModel.User_id);
            departmentViewModel.List_All_Programs = _context.Programs.Where(x => x.Dep_Id == id && x.isDeleted == false).ToList();
            //var u = _context.UserDepartments.Where(x => x.Dep_Id == id && x.isDeleted == false).ToString();
            return View(departmentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Department(DepartmentViewModel departmentViewModel, int id)
        {
            var department = _context.Departments.Where(
                x => x.Dep_Name.ToLower() == departmentViewModel.Department.Dep_Name.ToLower()
                &&
                x.isDeleted == false
                ).FirstOrDefault();

            if (department != null)
            {
                ViewBag.existName = messages.name_exist;
                ViewBag.data_not_saved = messages.data_not_saved;
                return View(departmentViewModel);
            }
            var edit_department = _context.Departments.Where(x => x.Dep_Id == id).FirstOrDefault();
            edit_department.Dep_Name= departmentViewModel.Department.Dep_Name;
            _context.SaveChanges();
            departmentViewModel.List_Managers_Details = department_DAL.Get_All_Department_Managers(departmentViewModel.User_id);
            departmentViewModel.List_All_Programs = _context.Programs.Where(x => x.Dep_Id == id && x.isDeleted == false).ToList();
            return View(departmentViewModel);
        }


        [HttpPost]
        public JsonResult Edit_Department_name(int dep_id, string name)
        {
            var department = _context.Departments.Where(x => x.Dep_Id == dep_id).FirstOrDefault();
            department.Dep_Name = name;
            try
            {
                _context.SaveChanges();
                return Json("success");
            }
            catch
            {
                return Json("Failed");
            }
        }

        [HttpPost]
        public JsonResult Remove_Department_Manager(int dep_id, int id)
        {
            var department = _context.Departments.Where(x => x.Dep_Id == dep_id).FirstOrDefault();
            var list_managers = department.User_id.Split(',');
            department.User_id = "";
            foreach (var manager in list_managers)
            {
                if(id != Convert.ToInt32(manager))
                {
                    department.User_id += manager+",";
                }
            }
            try
            {
                _context.SaveChanges();
                return Json("success");
            }
            catch
            {
                return Json("Failed");
            }
        }

        [HttpPost]
        public JsonResult Remove_Department_Program(int id)
        {
            var program = _context.Programs.Where(x => x.PId == id).FirstOrDefault();
            program.Dep_Id = 0;
            try
            {
                _context.SaveChanges();
                return Json("success");
            }
            catch
            {
                return Json("Failed");
            }
        }


        [HttpPost]
        public JsonResult Remove_Department_User(int id)
        {
            var user = _context.UserDepartments.Where(x => x.User_ID == id).FirstOrDefault();
            user.EndDate = DateTime.Now;
            user.isDeleted = true;

            try
            {
                _context.SaveChanges();
                return Json("success");
            }
            catch
            {
                return Json("Failed");
            }
        }

        [HttpPost]
        public JsonResult Delete_Department_Information(int dep_id)
        {
            department_DAL.Delete_Department(dep_id);
            _context.SaveChanges();
            return Json("success");
        }


        public ActionResult Add_Employees(int id)
        {
            DepartmentViewModel departmentViewModel = new DepartmentViewModel();
            var department = _context.Departments.Where(x => x.Dep_Id == id).FirstOrDefault();
            departmentViewModel.Department = department;
            departmentViewModel.all_users = department_DAL.Get_All_Potential_Employees(department.User_id ,id);
            return View(departmentViewModel);
        }

        [HttpPost]
        public JsonResult Add_Employee(int dep_id, int id)
        {
            var userDepartment = new UserDepartment();
            userDepartment.Dep_Id = dep_id;
            userDepartment.User_ID = id;
            userDepartment.StartDate = DateTime.Now;
            _context.UserDepartments.Add(userDepartment);
            _context.SaveChanges();
            return Json("success");
        }




    }
}
