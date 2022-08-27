using ProfessorCourse_BestFit.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult ListDepartment()
        { //metadata=res://*/Models.Model.csdl|res://*/Models.Model.ssdl|res://*/Models.Model.msl;provider=System.Data.SqlClient;provider connection string="data source=MSI;initial catalog=CourseProfessor;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"
            /*String connectionstring = "Data Source=MSI;database=CourseProfessor; providerName = System.Data.SqlClient";
            SqlConnection sqlcon = new SqlConnection(connectionstring);
            String pname = "my_InsertUpdateDelete_Department";
            sqlcon.Open();
            SqlCommand com = new SqlCommand(pname, sqlcon);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = com.ExecuteReader();
            var department = dr;*/
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