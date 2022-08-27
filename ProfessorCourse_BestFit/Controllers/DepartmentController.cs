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
            //var department = _context.Departments.ToList();
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
            var newdepartment = new Department();
            newdepartment.Dep_Name = department.Dep_Name;

            _context.Departments.Add(newdepartment);
            _context.SaveChanges();
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
            var department = _context.Departments.Find(id);
            //Department_DAL dal = new Department_DAL();
            return View(department);
        }

        // GET: Edit Department
        public ActionResult EditDepartment(int id)
        {
            var department = _context.Departments.Where(s => s.Dep_Id == id).FirstOrDefault();
            return View(department);
        }

        [HttpPost]
        public ActionResult EditDepartment(Department department,int id)
        {
            //var d = _context.my_InsertUpdateDelete_Department(id,null, 5);
            var d = _context.Departments.Where(s => s.Dep_Id == id).FirstOrDefault();
            var newdepartment = new Department();
            newdepartment.Dep_Name= department.Dep_Name;
            _context.my_InsertUpdateDelete_Department(department.Dep_Id, newdepartment.Dep_Name,2);
            _context.SaveChanges();
            return RedirectToAction("ListDepartment");
        }
    }
}