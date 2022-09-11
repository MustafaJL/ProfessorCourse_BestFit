using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class SemesterController : Controller
    {
        private readonly ProfessorCourseBestFit1Entities _context;

        public SemesterController() 
        {
            _context = new ProfessorCourseBestFit1Entities();
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
            var check_Name = _context.Semesters.Where(
                x => x.SemesterName == name
                ).FirstOrDefault();

            if (check_Name != null)
            {
                return true;
            }
            return false;
        }

        public ActionResult Index()
        {
            var semesterViewModel = new SemesterViewModel();
            semesterViewModel.all_semester = _context.Semesters.Where(x => x.isDeleted == false).ToList();
            return View(semesterViewModel);
        }

        public ActionResult Upsert(int? id)
        {
            ViewBag.id = id;
            if (id == null || id == 0)
            {

                return View();
            }
            SemesterViewModel theSemester = new SemesterViewModel();
            theSemester.semester = _context.Semesters.SingleOrDefault(x => x.SemesterId == id);

            return View(theSemester);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upsert(SemesterViewModel semesterViewModel)
        {
            if (Name_Required(semesterViewModel.semester.SemesterName))
            {
                return View(semesterViewModel);
            }

            if (Name_Exist(semesterViewModel.semester.SemesterName))
            {
                return View(semesterViewModel);
            }

            var isExist = _context.Semesters.SingleOrDefault(x => x.SemesterId == semesterViewModel.semester.SemesterId);
            if (isExist == null)
            {
                Semester semester = new Semester();
                semester.SemesterName = semesterViewModel.semester.SemesterName;
                semester.StartDate = DateTime.Now;
                semester.EndDate = DateTime.Now;
                _context.Semesters.Add(semester);

                _context.SaveChanges();
            }
            else
            {
                isExist.SemesterName = semesterViewModel.semester.SemesterName;
                _context.SaveChanges();
            }
            return View(semesterViewModel);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var semesters = _context.Semesters.SingleOrDefault(x => x.SemesterId == id);
            semesters.isDeleted = true;
            _context.SaveChanges();
            return Json("success");
        }
    }
}