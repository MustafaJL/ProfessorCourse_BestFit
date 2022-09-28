using ProfessorCourse_BestFit.CustomSecurity;
using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class ProgramController : Controller
    {
        private readonly ProfessorCourseBestFit1Entities1 _context;
        private readonly Program_DAL program_DAL;
        private readonly User_DAL user_DAL;
        private readonly Department_DAL department_DAL;
        //private readonly Course_DAL course_DAL;
        //private readonly Messages messages;

        public ProgramController()
        {
            _context = new ProfessorCourseBestFit1Entities1();
            program_DAL = new Program_DAL();
            user_DAL = new User_DAL();
            department_DAL = new Department_DAL();
            //course_DAL = new Course_DAL();
            //messages = new Messages();
        }

        private bool Name_Required(string name)
        {
            if (name == null)
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


        [CustomAuthorization(Permissions: "View-Program")]
        public ActionResult All_Programs()
        {
            var programViewModel = new ProgramViewModel();

            programViewModel.all_Programs = _context.Programs.ToList();

            return View(programViewModel);
        }

        //GET :
        [CustomAuthorization(Permissions: "Create-Program")]
        public ActionResult Create_Program()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Program(ProgramViewModel programViewModel)
        {
            if (Name_Required(programViewModel.Program.ProgramName))
            {
                ViewBag.nameRequired = "Please you should write name first.";
                ViewBag.data_not_saved = "The data is not saved.";
                return View(programViewModel);
            }

            if (Name_Exist(programViewModel.Program.ProgramName))
            {
                ViewBag.nameExist = "The name you entered is already exist please chose another name.";
                ViewBag.data_not_saved = "The data is not saved.";
                return View(programViewModel);
            }

            programViewModel.Program.CreatedOn = DateTime.Now;
            _context.Programs.Add(programViewModel.Program);
            _context.SaveChanges();

            ViewBag.Done = "Done...";
            ViewBag.Saved = "The data has been saved successfully.";

            return View();
        }

        //GET :
        [CustomAuthorization(Permissions: "Update-Program")]

        public ActionResult view_Program_Information(int id)
        {
            var programViewModel = new ProgramViewModel();

            programViewModel.Program = _context.Programs.Where(
                x => x.ProgramId == id
                ).FirstOrDefault();

            programViewModel.managers = user_DAL.Get_Users_Program(id, 1);

            //programViewModel.program_Courses = course_DAL.Get_Program_Courses(id);

            return View(programViewModel);
        }

        [CustomAuthorization(Permissions: "Update-Program")]
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
                ViewBag.nameRequired = "Please you should write name first.";
                ViewBag.data_not_saved = "The data is not saved.";
                return View(programViewModel);
            }

            if (Name_Exist(programViewModel.Program.ProgramName))
            {
                ViewBag.nameExist = "The name you entered is already exist please chose another name.";
                ViewBag.data_not_saved = "The data is not saved.";
                return View(programViewModel);
            }

            var program = _context.Programs.Where(
                x => x.ProgramId == id
                ).FirstOrDefault();

            program.ProgramName = programViewModel.Program.ProgramName;
            _context.SaveChanges();

            ViewBag.Done = "Done...";
            ViewBag.Saved = "The data has been saved successfully.";

            return View(programViewModel);
        }

        [CustomAuthorization(Permissions: "Update-Program")]

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

        [CustomAuthorization(Permissions: "Update-Program")]
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

        [CustomAuthorization(Permissions: "Update-Program")]

        public ActionResult Add_Remove_Program_Managers(int id)
        {
            var programViewModel = new ProgramViewModel();
            programViewModel.Program = _context.Programs.Where(x => x.ProgramId == id).FirstOrDefault();
            programViewModel.normal_Users = user_DAL.Get_Users_Program(id, 2);
            programViewModel.managers = user_DAL.Get_Users_Program(id, 1);
            return View(programViewModel);
        }

        [HttpPost]

        public JsonResult Add_Remove_Program_Managers(int id, string[] ids)
        {
            var data = ids;
            if (data == null || data.Length == 0)
            {
                return Json(new
                {
                    redirectUrl = Url.Action("All_Programs", "Program"),
                    isRedirect = true
                });
            }

            if (data[0] == "null data")
            {
                _context.DeleteAll(id, "programs", "managers");
                _context.SaveChanges();

                return Json(new
                {
                    redirectUrl = Url.Action("All_Programs", "Program"),
                    isRedirect = true
                });
            }

            var s = "";
            for (int i = 0; i < data.Length; i++)
            {
                s += id + "," + data[i] + "," + true + "]";
            }

            _context.AddRemove(id, s, "programmanagers", "]", ",");
            _context.SaveChanges();


            return Json(new
            {
                redirectUrl = Url.Action("All_Programs", "Program"),
                isRedirect = true
            });
        }
        [CustomAuthorization(Permissions: "Update-Program")]

        public ActionResult Add_Remove_Program_Courses(int id)
        {
            var programViewModel = new ProgramViewModel();
            programViewModel.Program = _context.Programs.Where(x => x.ProgramId == id).FirstOrDefault();
            programViewModel.all_courses = program_DAL.Get_Course_Programs(id, 2);
            programViewModel.program_Courses = program_DAL.Get_Course_Programs(id, 1);
            return View(programViewModel);
        }

        [HttpPost]
        public JsonResult Add_Remove_Program_Courses(int id, string[] ids)
        {
            var data = ids;
            if (data == null || data.Length == 0)
            {
                return Json(new
                {
                    redirectUrl = Url.Action("All_Programs", "Program"),
                    isRedirect = true
                });
            }

            if (data[0] == "null data")
            {
                _context.DeleteAll(id, "programs", "courses");
                _context.SaveChanges();

                return Json(new
                {
                    redirectUrl = Url.Action("All_Programs", "Program"),
                    isRedirect = true
                });
            }

            var s = "";
            for (int i = 0; i < data.Length; i++)
            {
                s += id + "," + data[i] + "]";
            }

            _context.AddRemove(id, s, "programcourses", "]", ",");
            _context.SaveChanges();


            return Json(new
            {
                redirectUrl = Url.Action("All_Programs", "Program"),
                isRedirect = true
            });
        }
    }
}