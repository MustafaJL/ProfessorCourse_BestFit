using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

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

        private readonly ProfessorCourseBestFitEntities2 _context;
        private Department_DAL dal;

        public DepartmentController()
        {
            _context = new ProfessorCourseBestFitEntities2();
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
        public ActionResult CreateDepartment()
        {
            ViewData["Message"] = null;
            return View();
        }

        // post: CreateDepartment
        [HttpPost]
        public ActionResult CreateDepartment(Department department)
        {

            ///modelstate.isvalid
            /*
             Create new Department using stored Procedure.
             */
            //_context.my_InsertUpdateDelete_Department(null, department.Dep_Name, null, 1);
            _context.Departments.Add(department);
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
            var department = _context.Departments.Find(id);

            //need to solve (we not useing Procedure)
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
            //need to solve (we not useing Procedure)
            return View(department);
        }

        [HttpPost]
        public ActionResult EditDepartment(Department department, int id)
        {
            /*
             Select all specific Department information.
            in order to edit it.
             */
            var d = _context.Departments.Where(s => s.Dep_Id == id).FirstOrDefault();
            d.Dep_Name = department.Dep_Name;
            //var newdepartment = new Department();
            //newdepartment.Dep_Name= department.Dep_Name;
            //_context.my_InsertUpdateDelete_Department(department.Dep_Id, newdepartment.Dep_Name, null, 2);
            _context.SaveChanges();
            return RedirectToAction("ViewDepartmentsInfo", new { id = id });
        }
    }
}