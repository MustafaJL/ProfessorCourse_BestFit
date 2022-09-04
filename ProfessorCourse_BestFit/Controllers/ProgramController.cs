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

        public ProgramController()
        {
            _context = new ProfessorCourseBestFitEntities();
        }

        // GET: Programs
        public ActionResult All_Programs()
        {
            ProgramViewModel programViewModel = new ProgramViewModel();
            var all_programs = _context.Programs.Where(x => x.isDeleted == false).ToList();
            programViewModel.All_Programs = all_programs;
            return View(programViewModel);
        }

        // GET: Create Program
        public ActionResult Create_Program()
        {
            return View();
        }

        // POST: Save the New Program
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Program(ProgramViewModel programViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(programViewModel);
            }
            Program  new_Program = new Program();
            new_Program.Name = programViewModel.Name;
            _context.Programs.Add(new_Program);
            _context.SaveChanges();

            return View();
        }

        // GET: Show Program information
        public ActionResult Show_Program_information(int id)
        {
            var program = _context.Programs.Where(x => x.PId == id).FirstOrDefault();
            ProgramViewModel programViewModel = new ProgramViewModel();
            programViewModel.PId = program.PId;
            programViewModel.Name = program.Name;
            return View(programViewModel);
        }

        // GET: Edit Program
        public ActionResult Edit_Program(int id)
        {
            ProgramViewModel programViewModel = new ProgramViewModel();
            var program = _context.Programs.Where(x => x.PId == id).FirstOrDefault();
            programViewModel.PId = program.PId;
            programViewModel.Name = program.Name;
            return View(programViewModel);
        }

        // POST: Edit old Program
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Program(ProgramViewModel programViewModel,int id)
        {
            if (!ModelState.IsValid)
            {
                return View(programViewModel);
            }
            var program = _context.Programs.Where(x => x.PId == id).FirstOrDefault();
            program.Name = programViewModel.Name;
            _context.SaveChanges();
            return RedirectToAction("Show_Program_information" , new {id = id});
        }


        // GET: Hide Program
        public ActionResult Delete_Program(int id)
        {
            ProgramViewModel programViewModel = new ProgramViewModel();
            var program = _context.Programs.Where(x => x.PId == id).FirstOrDefault();
            programViewModel.PId = program.PId;
            programViewModel.Name = program.Name;
            return View(programViewModel);
        }

        // POST: Hide the program
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Program(ProgramViewModel programViewModel, int id)
        {
            var program = _context.Programs.Where(x => x.PId == id).FirstOrDefault();
            program.isDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("All_Programs");
        }

    }
}