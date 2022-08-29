using ProfessorCourse_BestFit.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Models.ViewModels;
using ProfessorCourse_BestFit.CustomSecurity;

namespace ProfessorCourse_BestFit.Controllers
{
    public class DepartmentController : Controller
    {
        /*
         * My Stord Procedure
         
         _context.my_InsertUpdateDelete_Department(int? id,
                                                   String name,
                                                   int userID,
                                                   int? QueryNumber);
        QueryNumbres:
        1: INSERT
        2: UPDATE
        3: DELETE
        4: SELECT * (No Condition)
        5: SELECT * (Based on the id)

         */

        private readonly ProfessorCourseBestFitEntities _context;
        private Department_DAL dal;

        public DepartmentController()
        {
            _context = new ProfessorCourseBestFitEntities();
            dal = new Department_DAL();
        }

        // GET: ListDepartment
        public ActionResult ListDepartment(int? id)
        {
            /*
             get all Department in the database using 
            stord procedure.
             */
            return View(dal.GetDepartments(id, 4));
        }

        // GET: CreateDepartment
        [CustomAuthorization("Admin")]
        public ActionResult CreateDepartment()
        {
            ViewData["Message"] = null;
            //need to fix (do not add the admin)
            //listusers.listUsers = _context.Users.ToList<User>();
            return View();
        }

        // post: CreateDepartment
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorization("Admin")]
        public ActionResult CreateDepartment(DepartmentViewModel department)
        {

            ///modelstate.isvalid
            /*
             Create new Department using stored Procedure.
             */
            var newDepartment = new Department();
            newDepartment.Dep_Name = department.Dep_Name;
            //Problem
            //newDepartment.User_Id = department.User_Id;
            _context.Departments.Add(newDepartment);
            //To Display message to the user.
            try
            {
                _context.SaveChanges();
                ViewData["Message"] = "Done";
            }
            catch
            {
                ViewData["Message"] = "Fail";
            }

            return View("CreateDepartment");
        }

        // GET: CreateDepartment
        public ActionResult ViewDepartmentsInfo(int id)
        {
            /*
             Select all specific Department information.
             */
            var department = new DepartmentViewModel();
            var getdepartment = _context.Departments.Find(id);
            department.Dep_Id = getdepartment.Dep_Id;
            department.Dep_Name = getdepartment.Dep_Name;
            //Problem
            department.User_Id = getdepartment.User_Id;
            return View(department);
        }

        // GET: Edit Department
        [CustomAuthorization("Admin")]
        public ActionResult EditDepartment(int id)
        {
            /*
             Select all specific Department information.
             */
            var department = new DepartmentViewModel();
            var getdepartment = _context.Departments.Where(s => s.Dep_Id == id).FirstOrDefault();
            department.Dep_Id = getdepartment.Dep_Id;
            department.Dep_Name = getdepartment.Dep_Name;
            //Problem
            department.User_Id = getdepartment.User_Id;
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorization("Admin")]
        public ActionResult EditDepartment(DepartmentViewModel department,int id)
        {
            /*
             Select all specific Department information.
            in order to edit it.
             */
            var d = _context.Departments.Where(s => s.Dep_Id == id).FirstOrDefault();
            d.Dep_Name = department.Dep_Name;
            //To Display message to the user.
            _context.SaveChanges();
            return RedirectToAction("ViewDepartmentsInfo", new { id = id });
        }

        // GET: isDeleted Department
        [CustomAuthorization("Admin")]
        public ActionResult isDeletedDepartment(int id)
        {
            /*
             Select all specific Department information.
             */
            var department = new DepartmentViewModel();
            var getdepartment = _context.Departments.Where(s => s.Dep_Id == id).FirstOrDefault();
            department.Dep_Id = getdepartment.Dep_Id;
            department.Dep_Name = getdepartment.Dep_Name;
            //Problem
            department.User_Id = getdepartment.User_Id;
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorization("Admin")]
        public ActionResult isDeletedDepartment(DepartmentViewModel department, int id)
        {
            /*
             Select all specific Department information.
            in order to edit it.
             */
            var d = _context.Departments.Where(s => s.Dep_Id == id).FirstOrDefault();
            d.isDeleted = true;
            //To Display message to the user.
            _context.SaveChanges();
            return RedirectToAction("ListDepartment");
        }

        public ActionResult Container()
        {
         return View();
        }
    }
}