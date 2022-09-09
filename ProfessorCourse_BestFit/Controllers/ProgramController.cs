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
        private readonly ProfessorCourseBestFit1 _context;
        private readonly Messages messages;

        public ProgramController()
        {
            _context = new ProfessorCourseBestFit1();
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
                x => x.ProgramName == name
                ).FirstOrDefault();

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

            programViewModel.all_Programs = _context.Programs.ToList();

            return View(programViewModel);
        }

        //GET :
        public ActionResult Create_Program()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Program(ProgramViewModel programViewModel)
        {
            if(Name_Required(programViewModel.Program.ProgramName))
            {
                ViewBag.nameRequired = messages.name_Required;
                ViewBag.data_not_saved = messages.data_not_saved;
                return View(programViewModel);
            }

            if (Name_Exist(programViewModel.Program.ProgramName))
            {
                ViewBag.nameExist = messages.name_exist;
                ViewBag.data_not_saved = messages.data_not_saved;
                return View(programViewModel);
            }

            programViewModel.Program.CreatedOn = DateTime.Now;
            _context.Programs.Add(programViewModel.Program);
            _context.SaveChanges();

            ViewBag.Done = messages.message_success_submit_title;
            ViewBag.Saved = messages.message_success_submit_body;

            return View();
        }

        //GET :
        public ActionResult view_Program_Information(int id)
        {
            var programViewModel = new ProgramViewModel();

            programViewModel.Program = _context.Programs.Where(
                x => x.ProgramId == id
                ).FirstOrDefault();

            return View(programViewModel);
        }

        //GET :
        public ActionResult Edit_Program_Name(int id)
        {
            var programViewModel = new ProgramViewModel();

            programViewModel.Program = _context.Programs.Where(
                x => x.ProgramId == id
                ).FirstOrDefault();

            return View(programViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Program_Name(ProgramViewModel programViewModel, int id)
        {
            if (Name_Required(programViewModel.Program.ProgramName))
            {
                ViewBag.nameRequired = messages.name_Required;
                ViewBag.data_not_saved = messages.data_not_saved;
                return View(programViewModel);
            }

            if (Name_Exist(programViewModel.Program.ProgramName))
            {
                ViewBag.nameExist = messages.name_exist;
                ViewBag.data_not_saved = messages.data_not_saved;
                return View(programViewModel);
            }

            var program = _context.Programs.Where(
                x => x.ProgramId == id
                ).FirstOrDefault();

            program.ProgramName = programViewModel.Program.ProgramName;
            _context.SaveChanges();

            ViewBag.Done = messages.message_success_submit_title;
            ViewBag.Saved = messages.message_success_submit_body;

            return View(programViewModel);
        }


        [HttpPost]
        public JsonResult Deactivate_Program(int id)
        {
            var delete_Program = _context.Programs.Where(
                x => x.ProgramId == id
                ).FirstOrDefault();

            delete_Program.isDeleted = true;
            _context.SaveChanges();

            return Json(new
            {
                redirectUrl = Url.Action("All_Programs", "Program"),
                isRedirect = true
            });
        }

        [HttpPost]
        public JsonResult Activate_Program(int id)
        {
            var deleted_Program = _context.Programs.Where(
                x => x.ProgramId == id
                ).FirstOrDefault();

            deleted_Program.isDeleted = false;
            _context.SaveChanges();

            return Json(new
            {
                redirectUrl = Url.Action("All_Programs", "Program"),
                isRedirect = true
            });
        }


        public ActionResult Add_Remove_Program_Managers(int id)
        {
            //Need some code
            return View();
        }

        public ActionResult Add_Remove_Program_Courses(int id)
        {
            //Need some code
            return View();
        }

    }
}