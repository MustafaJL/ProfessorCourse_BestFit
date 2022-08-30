using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class ProgramController : Controller
    {
        private readonly ProfessorCourseBestFitEntities _context;
        private Program_DAL dal;
        public ProgramController()
        {
            _context = new ProfessorCourseBestFitEntities();
            dal = new Program_DAL();
        }

        // GET: List Of Program
        public ActionResult ListOfProgram()
        {
            /*
             get all Programs in the database using Proceduer.
             */
            return View(dal.GetAllPrograms(null));
        }

        // GET: Create Page
        public ActionResult CreateProgram()
        {
            ViewData["Message"] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProgram(ProgramViewModel programViewModel)
        {
            /*
             get all Programs in the database.
             */
            var program = new Program();
            program.Name = programViewModel.Name;
            _context.Programs.Add(program);
            try
            {
                _context.SaveChanges();
                ViewData["Message"] = "Done";
            }
            catch
            {
                ViewData["Message"] = "Fail";
            }
            return View("CreateProgram");
        }
        

    }
}