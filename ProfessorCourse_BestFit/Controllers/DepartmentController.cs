using ProfessorCourse_BestFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: ListDepartment
        public ActionResult ListDepartment()
        {
            var department = _context.Departments.ToList();
            return View(department);
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
            var newdepartment = new Department();
            newdepartment.Dep_Name = department.Dep_Name;

            _context.Departments.Add(newdepartment);
            _context.SaveChanges();

            return View("CreateDepartment");
        }

        // GET: CreateDepartment
        public ActionResult ViewDepartmentsInfo(int id)
        {
            var department = _context.Departments.Find(id);
            return View(department);
        }
        
        // GET: CreateDepartment
        public ActionResult EditDepartment(int id)
        {
            var department = _context.Departments.Find(id);
            return View(department);
        }

        [HttpPost]
        public ActionResult EditDepartment(Department department,int id)
        {
            var d = _context.Departments.Where(s => s.Dep_Id == id).FirstOrDefault();
            var newdepartment = new Department();
            newdepartment.Dep_Name= department.Dep_Name;
            _context.Departments.Remove(d);
            _context.Departments.Add(newdepartment);
            _context.SaveChanges();
            return RedirectToAction("ListDepartment");
        }
    }
}