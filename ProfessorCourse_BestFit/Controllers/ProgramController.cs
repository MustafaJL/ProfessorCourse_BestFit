using ProfessorCourse_BestFit.CustomSecurity;
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
        [CustomAuthorization("Admin")]
        public ActionResult CreateProgram()
        {
            ViewData["Message"] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorization("Admin")]
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

        //Get
        [CustomAuthorization("Admin")]
        public ActionResult EditProgram(int id)
        {
            /*
             Select all specific Program information.
             */
            var program = new ProgramViewModel();
            var getprogram = _context.Programs.Where(s => s.PId == id).FirstOrDefault();
            program.PId = getprogram.PId;
            program.Name = getprogram.Name;
            program.Dep_Id = getprogram.Dep_Id;
            return View(program);
        }

        [HttpPost]
        [CustomAuthorization("Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult EditProgram(ProgramViewModel programViewModel, int id)
        {
            /*
             Select all specific Department information.
            in order to edit it.
             */
            var program = _context.Programs.Where(s => s.PId == id).FirstOrDefault();
            program.Name = programViewModel.Name;
            _context.SaveChanges();
            return RedirectToAction("viewProgramInfo", new { id = id });
        }

        //Get
        public ActionResult viewProgramInfo(int id)
        {
            /*
             Select all specific Program information.
             */
            var program = new ProgramViewModel();
            var getprogram = _context.Programs.Find(id);
            program.PId = getprogram.PId;
            program.Name = getprogram.Name;
            program.Dep_Id = getprogram.Dep_Id;
            return View(program);
        }
    }
}