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

        private readonly ProfessorCourseBestFitEntities _context;
        private readonly Messages messages;

        public CourseController()
        {
            _context = new ProfessorCourseBestFitEntities();
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
                x => x.CName == name
                ).ToList();

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

            courseViewModel.active_Courses = _context.Courses.Where(
                x => x.isDeleted == false
                ).ToList();

            courseViewModel.disActive_Courses = _context.Courses.Where(
                x => x.isDeleted == true
                ).ToList();

            return View(courseViewModel);
        }

        //GET :
        public ActionResult Create_Course()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create_Course(CourseViewModel courseViewModel)
        {
            if (Name_Required(courseViewModel.Course.CName))
            {
                //message needed
                ViewBag.nameRequired = true;
                return View(courseViewModel);
            }

            if (Name_Exist(courseViewModel.Course.CName))
            {
                //message needed
                ViewBag.nameExist = true;
                return View(courseViewModel);
            }

            _context.Courses.Add(courseViewModel.Course);
            _context.SaveChanges();

            //message needed
            ViewBag.Done = true;

            return View();
        }

        //GET :
        public ActionResult view_Course_Information(int CId)
        {
            var courseViewModel = new CourseViewModel();

            courseViewModel.Course = _context.Courses.Where(
                x => x.CId == CId
                ).FirstOrDefault();

            return View(courseViewModel);
        }

        //GET :
        public ActionResult Edit_Course(int CId)
        {
            var courseViewModel = new CourseViewModel();

            courseViewModel.Course = _context.Courses.Where(
                x => x.CId == CId
                ).FirstOrDefault();

            return View(courseViewModel);
        }

        [HttpPost]
        public ActionResult Edit_Course(CourseViewModel courseViewModel, int CId)
        {
            if (Name_Required(courseViewModel.Course.CName))
            {
                //message needed
                ViewBag.nameRequired = true;
                return View(courseViewModel);
            }

            if (Name_Exist(courseViewModel.Course.CName))
            {
                //message needed
                ViewBag.nameExist = true;
                return View(courseViewModel);
            }

            var course = _context.Courses.Where(
                x => x.CId == CId
                ).FirstOrDefault();

            course = courseViewModel.Course;
            _context.SaveChanges();

            //message needed
            ViewBag.Done = true;

            return View(courseViewModel);
        }


        public ActionResult Delete_Course(int CId)
        {
            //Need some code
            return View();
        }


        /////////////////////////
        //the rest or code here//
        /////////////////////////
    }
}