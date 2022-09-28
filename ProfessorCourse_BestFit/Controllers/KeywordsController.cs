using ProfessorCourse_BestFit.CustomSecurity;
using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class KeywordsController : Controller
    {
        private readonly ProfessorCourseBestFit1Entities1 _context;
        private readonly UserKeywords_DAL _sp;
        public KeywordsController()
        {
            _context = new ProfessorCourseBestFit1Entities1();
            _sp = new UserKeywords_DAL();
        }

        // GET: Keywords
        [CustomAuthorization(Permissions: "View-Keywrod")]
        public ActionResult Index()
        {
            var keywords = _context.Keywords.Where(u => u.isDeleted == false).ToList();
            return View(keywords);
        }
        [CustomAuthorization(Permissions: "Upsert-Keyword")]
        public ActionResult Upsert(int? id)
        {
            ViewBag.id = id;
            if (id == null || id == 0)
            {

                return View();
            }
            var keywords = _context.Keywords.SingleOrDefault(x => x.KId == id);
            KeywordsViewModel keywordView = new KeywordsViewModel
            {
                KId = keywords.KId,
                kName = keywords.KName
            };
            return View(keywordView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upsert(KeywordsViewModel model)
        {

            if (ModelState.IsValid)
            {
                var isExist = _context.Keywords.SingleOrDefault(x => x.KId == model.KId);
                var KeywordName = _context.Keywords.Where(u => u.KName == model.kName);

                if (KeywordName.Any())
                {
                    ViewBag.id = model.KId;
                    ViewBag.Error = "It seems the Keyword name" +
                        " you are trying to add or update is already exist.";
                    return View(model);

                }


                if (isExist == null)
                {
                    Keyword keyword = new Keyword
                    {
                        KName = model.kName,


                    };
                    _context.Keywords.Add(keyword);
                    _context.SaveChanges();



                }
                else
                {
                    isExist.KName = model.kName;
                    _context.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [CustomAuthorization(Permissions: "Delete-Keyword")]

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var keywords = _context.Keywords.SingleOrDefault(x => x.KId == id);
            keywords.isDeleted = true;
            _context.SaveChanges();
            return Json("success");
        }
    }
}