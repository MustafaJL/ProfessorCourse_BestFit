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

        // GET: Course
        public ActionResult All_Course()
        {
            CourseViewModel courseViewModel = new CourseViewModel();
            courseViewModel.all_courses = _context.Courses.Where(x => x.isDeleted == false).ToList();
            return View(courseViewModel);
        }

        // GET: Create Course
        public ActionResult Create_Course()
        {
            return View();
        }

        // POST: Save the New Course
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Course(CourseViewModel courseViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(courseViewModel);
            }
            var course = _context.Courses.Where(
                x => x.CName.ToLower() == courseViewModel.CName.ToLower()
                &&
                x.isDeleted == false
                ).FirstOrDefault();
            if (course != null)
            {
                ViewBag.existName = messages.name_exist;
                ViewBag.data_not_saved = messages.data_not_saved;
                return View(courseViewModel);
            }
            Course new_Course = new Course();
            new_Course.CName = courseViewModel.CName;
            new_Course.Code = courseViewModel.Code;
            new_Course.Duration = courseViewModel.Duration;
            new_Course.CreatedOn = DateTime.Now;
            _context.Courses.Add(new_Course);
            try
            {
                _context.SaveChanges();
                ViewBag.savetitle = messages.message_success_submit_title;
                ViewBag.savebody = messages.message_success_submit_body;
            }
            catch
            {
                ViewBag.savetitle = messages.message_failed_submit_title;
                ViewBag.savebody = messages.message_failed_submit_body;
            }

            return View();
        }

        public ActionResult Show_Course_information(int id)
        {
            var course = _context.Courses.Where(x => x.CId == id).FirstOrDefault();
            CourseViewModel courseViewModel = new CourseViewModel();
            courseViewModel.CId = course.CId;
            courseViewModel.CName = course.CName;
            courseViewModel.Code = course.Code;
            courseViewModel.Duration = course.Duration;
            return View(courseViewModel);
        }

        // GET: Edit Course
        public ActionResult Edit_Course(int id)
        {
            CourseViewModel courseViewModel = new CourseViewModel();
            var course = _context.Courses.Where(x => x.CId == id).FirstOrDefault();
            courseViewModel.CId = course.CId;
            courseViewModel.CName = course.CName;
            courseViewModel.Code = course.Code;
            courseViewModel.Duration = course.Duration;
            return View(courseViewModel);
        }

        // POST: Edit old Course
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Course(CourseViewModel courseViewModel, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(courseViewModel);
            }
            var course = _context.Courses.Where(
                x => x.CName.ToLower() == courseViewModel.CName.ToLower()
                &&
                x.isDeleted == false
                ).FirstOrDefault();
            if (course != null)
            {
                ViewBag.existName = messages.name_exist;
                ViewBag.data_not_saved = messages.data_not_saved;
                return View(courseViewModel);
            }
            var new_course = _context.Courses.Where(x => x.CId == id).FirstOrDefault();
            new_course.CName = courseViewModel.CName;
            new_course.Code = courseViewModel.Code;
            new_course.Duration = courseViewModel.Duration;
            _context.SaveChanges();
            return View(courseViewModel);
        }

        // GET: Hide Course
        public ActionResult Delete_Course(int id)
        {
            CourseViewModel courseViewModel = new CourseViewModel();
            var course = _context.Courses.Where(x => x.CId == id).FirstOrDefault();
            courseViewModel.CId = course.CId;
            courseViewModel.CName = course.CName;
            courseViewModel.Code = course.Code;
            courseViewModel.Duration = course.Duration;
            return View(courseViewModel);
        }

        // POST: Hide the Course
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Course(CourseViewModel courseViewModel, int id)
        {
            var course = _context.Courses.Where(x => x.CId == id).FirstOrDefault();
            course.isDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("All_Course");
        }
    }
}