using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class CourseController : Controller
    {
        private readonly ProfessorCourseBestFit1Entities1 _context;
        private readonly CourseKeywords_DAL _sp;
        public CourseController()
        {
            _context = new ProfessorCourseBestFit1Entities1();
            _sp = new CourseKeywords_DAL();
        }
        public ActionResult Index()
        {
            var courses = _context.Courses.Where(x => x.isDeleted == false).ToList();
            return View(courses);
        }

        public ActionResult Upsert(int? id)
        {
            ViewBag.id = id;
            if (id == null || id == 0)
            {

                return View();
            }
            var courses = _context.Courses.SingleOrDefault(x => x.CId == id);
            CoursesViewModel courseView = new CoursesViewModel
            {
                CId = courses.CId,
                CName = courses.CName,
                CourseCode = courses.Code,
                CreatedOn = courses.CreatedOn,
                Duration = courses.Duration
            };
            return View(courseView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upsert(CoursesViewModel model)
        {

            if (ModelState.IsValid)
            {
                var isExist = _context.Courses.SingleOrDefault(x => x.CId == model.CId);
                if (isExist == null)
                {
                    Course course = new Course
                    {
                        CName = model.CName,
                        Code = model.CourseCode,
                        CreatedOn = System.DateTime.Now,
                        Duration = model.Duration
                    };
                    _context.Courses.Add(course);

                    _context.SaveChanges();
                    // add spCreateRolePermisssions
                    //_sp.CreateRolePermissions(role.RoleId);
                    //_sp.CreateCourseKeywords(course.CId);
                }
                else
                {
                    isExist.CName = model.CName;
                    isExist.Duration = model.Duration;
                    isExist.Code = model.CourseCode;
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }



        [HttpPost]
        public JsonResult Delete(int id)
        {
            var course = _context.Courses.SingleOrDefault(x => x.CId == id);
            course.isDeleted = true;
            _context.SaveChanges();
            return Json("success");
        }


        public ActionResult courseKeywords(int? id)
        {
            var course = _context.Courses.SingleOrDefault(x => x.CId == id);

            var keywords = _sp.GetAllKeywordsIncludesMatchingByCourseId((int)id);

            CourseKeywordsViewModel userKeywordsView = new CourseKeywordsViewModel
            {

                Course = course,
                Keywords = keywords
            };
            return View(userKeywordsView);
        }

        [HttpPost]
        public JsonResult courseKeywords(int courseId, string[] keywords)
        {
            string keywordsString = "";
            if (keywords != null && keywords.Length > 0)
            {
                keywordsString = string.Join(",", keywords);
            }
            else
            {
                keywordsString = "0";
            }

            var a = _sp.UpdateCourseKeyword(courseId, keywordsString);
            return Json(new { success = true });
        }
    }
}