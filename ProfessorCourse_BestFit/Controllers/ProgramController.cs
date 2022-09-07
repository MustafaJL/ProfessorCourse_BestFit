using ProfessorCourse_BestFit.messages;
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
        private readonly Messages messages;

        public ProgramController()
        {
            _context = new ProfessorCourseBestFitEntities();
            messages = new Messages();
        }

        private bool Name_Required(string name)
        {
            if(name == null)
            {
                return true;
            }
            return false;
        }

        private bool Name_Exist(string name)
        {
            var check_Name = _context.Programs.Where(
                x => x.Name == name
                ).ToList();

            if (check_Name != null)
            {
                return true;
            }
            return false;
        }


        // GET: 
        public ActionResult All_Programs()
        {
            var programViewModel = new ProgramViewModel();

            programViewModel.active_Programs = _context.Programs.Where(
                x => x.isDeleted == false
                ).ToList();

            programViewModel.disActive_Programs = _context.Programs.Where(
                x => x.isDeleted == true
                ).ToList();

            return View(programViewModel);
        }

        //GET :
        public ActionResult Create_Program()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create_Program(ProgramViewModel programViewModel)
        {
            if(Name_Required(programViewModel.Program.Name))
            {
                //message needed
                ViewBag.nameRequired = true;
                return View(programViewModel);
            }

            if (Name_Exist(programViewModel.Program.Name))
            {
                //message needed
                ViewBag.nameExist = true;
                return View(programViewModel);
            }

            _context.Programs.Add(programViewModel.Program);
            _context.SaveChanges();

            //message needed
            ViewBag.Done = true;

            return View();
        }

        //GET :
        public ActionResult view_Program_Information(int Pid)
        {
            var programViewModel = new ProgramViewModel();

            programViewModel.Program = _context.Programs.Where(
                x => x.PId == Pid
                ).FirstOrDefault();

            return View(programViewModel);
        }

        //GET :
        public ActionResult Edit_Program(int Pid)
        {
            var programViewModel = new ProgramViewModel();

            programViewModel.Program = _context.Programs.Where(
                x => x.PId == Pid
                ).FirstOrDefault();

            return View(programViewModel);
        }

        [HttpPost]
        public ActionResult Edit_Program(ProgramViewModel programViewModel, int Pid)
        {
            if (Name_Required(programViewModel.Program.Name))
            {
                //message needed
                ViewBag.nameRequired = true;
                return View(programViewModel);
            }

            if (Name_Exist(programViewModel.Program.Name))
            {
                //message needed
                ViewBag.nameExist = true;
                return View(programViewModel);
            }

            var program = _context.Programs.Where(
                x => x.PId == Pid
                ).FirstOrDefault();

            program = programViewModel.Program;
            _context.SaveChanges();

            //message needed
            ViewBag.Done = true;

            return View(programViewModel);
        }


        public ActionResult Delete_Program(int Pid)
        {
            //Need some code
            return View();
        }


        /////////////////////////
        //the rest or code here//
        /////////////////////////

    }
}