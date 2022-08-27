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

namespace ProfessorCourse_BestFit.Controllers
{
    public class DepartmentController : Controller
    {
        /*
         * My Stord Proseduer
         
         _context.my_InsertUpdateDelete_Department(int? id,String name,int? QueryNumber);
        QueryNumbres:
        1: INSERT
        2: UPDATE
        3: DELETE
        4: SELECT * (No Condition)
        5: SELECT * (Based on the id)

         */

        private readonly ApplicationDbContext _context;

        public DepartmentController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: ListDepartment
        public ActionResult ListDepartment(int? id)
        {
            /*
             get all Department in the database using 
            stord proseduer.
             */
            Department_DAL dal = new Department_DAL();
            return View(dal.GetDepartments(id, 4));
        }

        // GET: CreateDepartment
        public ActionResult CreateDepartment()
        {
            return View();
        }

        // post: CreateDepartment
        [HttpPost]
        public ActionResult CreateDepartment(Department department)
        {
            /*
             Create new Department using stored proseduer.
             */
            var newdepartment = new Department();
            newdepartment.Dep_Name = department.Dep_Name;
            _context.my_InsertUpdateDelete_Department(null, newdepartment.Dep_Name, 1);
            _context.SaveChanges();

            return View("CreateDepartment");
        }

        // GET: CreateDepartment
        public ActionResult ViewDepartmentsInfo(int id)
        {
            /*
             Select all specific Department information.
             */
            var department = _context.Departments.Find(id);

            //need to solve (we not useing proseduer)
            //Department_DAL dal = new Department_DAL();
            return View(department);
        }

        // GET: Edit Department
        public ActionResult EditDepartment(int id)
        {
            /*
             Select all specific Department information.
             */
            var department = _context.Departments.Where(s => s.Dep_Id == id).FirstOrDefault();
            //need to solve (we not useing proseduer)
            return View(department);
        }

        [HttpPost]
        public ActionResult EditDepartment(Department department,int id)
        {
            /*
             Select all specific Department information.
            in order to edit it.
             */
            var d = _context.Departments.Where(s => s.Dep_Id == id).FirstOrDefault();
            var newdepartment = new Department();
            newdepartment.Dep_Name= department.Dep_Name;
            _context.my_InsertUpdateDelete_Department(department.Dep_Id, newdepartment.Dep_Name,2);
            _context.SaveChanges();
            return RedirectToAction("ListDepartment");
        }
    }
}