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

        public CourseController()
        {
            _context = new ProfessorCourseBestFitEntities();
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
            Course new_Course = new Course();
            new_Course.CName = courseViewModel.CName;
            new_Course.Code = courseViewModel.Code;
            new_Course.Duration = courseViewModel.Duration;
            new_Course.CreatedOn = DateTime.Now;
            _context.Courses.Add(new_Course);
            _context.SaveChanges();

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
            var course = _context.Courses.Where(x => x.CId == id).FirstOrDefault();
            course.CName = courseViewModel.CName;
            course.Code = courseViewModel.Code;
            course.Duration = courseViewModel.Duration;
            _context.SaveChanges();
            return RedirectToAction("Show_Course_information", new { id = id });
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