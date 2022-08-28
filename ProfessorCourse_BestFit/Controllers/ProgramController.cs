using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class ProgramController : Controller
    {
        /*
         * My Stord Procedure
         
         _context.my_InsertUpdateDelete_Program(int? id, 
                                                String name,
                                                int id,
                                                int? QueryNumber);
        QueryNumbres:
        1: INSERT
        2: UPDATE
        3: DELETE
        4: SELECT * (No Condition)
        5: SELECT * (Based on the id)

         */

        private readonly ApplicationDbContext _context;
        private Program_DAL dal;
        public ProgramController()
        {
            _context = new ApplicationDbContext();
            dal = new Program_DAL();
        }

        // GET: Program
        public ActionResult ListPrograms(int? id)
        {
            /*
             get all Programs in the database using 
             stord procedure.
            */
            return View(dal.GetPrograms(id, 4));
        }

        // GET: CreateProgram
        public ActionResult CreateProgram()
        {
            ViewData["Message"] = null;
            return View();
        }

        // post: CreateProgram
        [HttpPost]
        public ActionResult CreateProgram(Program program)
        {
            /*
             Create new Program using stored Procedure.
             */
            var newprogram = new Program();
            newprogram.Name = program.Name;
            //newprogram.Dep_Id = program.Dep_Id;
            _context.my_InsertUpdateDelete_Program(null, newprogram.Name, null, 1);
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
    }
}