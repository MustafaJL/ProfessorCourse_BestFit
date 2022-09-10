using ProfessorCourse_BestFit.DAL;
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
    public class CourseController : Controller
    {

        private readonly ProfessorCourseBestFit1 _context;
        private readonly Course_DAL course_DAL;
        private readonly Messages messages;

        public CourseController()
        {
            _context = new ProfessorCourseBestFit1();
            course_DAL = new Course_DAL();
            messages = new Messages();
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
            var check_Name = _context.Courses.Where(
                x => x.CName == name && x.isDeleted == false
                ).FirstOrDefault();

            if (check_Name != null)
            {
                return true;
            }
            return false;
        }

        private bool Code_Exist(string name)
        {
            var check_Name = _context.Courses.Where(
                x => x.Code == name && x.isDeleted == false
                ).FirstOrDefault();

            if (check_Name != null)
            {
                return true;
            }
            return false;
        }


        // GET: 
        public ActionResult All_Courses()
        {
            var courseViewModel = new CourseViewModel();

            courseViewModel.all_Courses = _context.Courses.Where(
                x => x.isDeleted == false
                ).ToList();

            return View(courseViewModel);
        }

        //GET :
        public ActionResult Create_Course()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Course(CourseViewModel courseViewModel)
        {
            if (Name_Required(courseViewModel.Course.CName))
            {
                ViewBag.nameRequired = messages.name_Required;
                ViewBag.data_not_saved = messages.data_not_saved;
                return View(courseViewModel);
            }

            if (Name_Exist(courseViewModel.Course.CName))
            {
                ViewBag.nameExist = messages.name_exist;
                ViewBag.data_not_saved = messages.data_not_saved;
                return View(courseViewModel);
            }

            if (Code_Exist(courseViewModel.Course.Code))
            {
                ViewBag.code_exist = messages.code_exist;
                ViewBag.data_not_saved = messages.data_not_saved;
                return View(courseViewModel);
            }

            courseViewModel.Course.CreatedOn = DateTime.Now;
            _context.Courses.Add(courseViewModel.Course);
            _context.SaveChanges();

            ViewBag.Done = messages.message_success_submit_title;
            ViewBag.Saved = messages.message_success_submit_body;

            return View();
        }

        //GET :
        public ActionResult view_Course_Information(int id)
        {
            var courseViewModel = new CourseViewModel();

            courseViewModel.Course = _context.Courses.Where(
                x => x.CId == id
                ).FirstOrDefault();

            courseViewModel.course_Professors = course_DAL.Get_Course_Professors(id);

            courseViewModel.course_Programs = course_DAL.Get_Course_Programs(id);

            return View(courseViewModel);
        }

        //GET :
        public ActionResult Edit_Course_Name(int id)
        {
            var courseViewModel = new CourseViewModel();

            courseViewModel.Course = _context.Courses.Where(
                x => x.CId == id
                ).FirstOrDefault();

            return View(courseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Course_Name(CourseViewModel courseViewModel, int id)
        {
            if (Name_Required(courseViewModel.Course.CName))
            {
                ViewBag.nameRequired = messages.name_Required;
                ViewBag.data_not_saved = messages.data_not_saved;
                return View(courseViewModel);
            }

            if (Name_Exist(courseViewModel.Course.CName))
            {
                ViewBag.nameExist = messages.name_exist;
                ViewBag.data_not_saved = messages.data_not_saved;
                return View(courseViewModel);
            }

            if (Code_Exist(courseViewModel.Course.Code))
            {
                ViewBag.code_exist = messages.code_exist;
                ViewBag.data_not_saved = messages.data_not_saved;
                return View(courseViewModel);
            }

            var course = _context.Courses.Where(
                x => x.CId == id
                ).FirstOrDefault();

            course.CName = courseViewModel.Course.CName;
            course.Code = courseViewModel.Course.Code;
            course.Duration = courseViewModel.Course.Duration;
            _context.SaveChanges();

            ViewBag.Done = messages.message_success_submit_title;
            ViewBag.Saved = messages.message_success_submit_body;

            return View(courseViewModel);
        }

        [HttpPost]
        public JsonResult Delete_Course(int id)
        {
            var course = _context.Courses.Where(
                x => x.CId == id
                ).FirstOrDefault();

            _context.CourseDeleteRelations(id);

            course.isDeleted = true;
            _context.SaveChanges();

            return Json(new
            {
                redirectUrl = Url.Action("All_Courses", "Course"),
                isRedirect = true
            });
        }

        //GET :
        public ActionResult Add_Remove_Course_Professors(int id)
        {
            var courseViewModel = new CourseViewModel();

            courseViewModel.Course = _context.Courses.Where(
                x => x.CId == id
                ).FirstOrDefault();

            courseViewModel.optionList = course_DAL.Get_Users_To_Be_Professors(id)
               .Select(c => new SelectListItem
               {
                   Value = c.Uid.ToString(),
                   Text = c.FirstName+" "+c.MiddleName+" "+c.LastName
               }).ToList();

            return View(courseViewModel);
        }

        [HttpPost]
        public JsonResult Add_Remove_Course_Professors_(int id)
        {
            return Json(new
            {
                redirectUrl = Url.Action("All_Courses", "Course"),
                isRedirect = true
            });
        }

        public ActionResult Add_Remove_Course_Programs(int id)
        {
            //Need some code
            return View();
        }
    }
}