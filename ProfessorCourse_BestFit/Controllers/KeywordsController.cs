using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class KeywordsController : Controller
    {
        private readonly ProfessorCourseBestFit1Entities _context;
        private readonly UserKeywords_DAL _sp;
        public KeywordsController()
        {
            _context = new ProfessorCourseBestFit1Entities();
            _sp = new UserKeywords_DAL();
        }

        // GET: Keywords
        public ActionResult Index()
        {
            var keywords = _context.Keywords.Where(u => u.isDeleted == false).ToList();
            return View(keywords);
        }

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
                if (isExist == null)
                {
                    Keyword keyword = new Keyword
                    {
                        KName = model.kName,


                    };
                    _context.Keywords.Add(keyword);
                    _context.SaveChanges();

                    _sp.CreateKeywordUser(keyword.KId);
                    _sp.CreateKeywordCourse(keyword.KId);

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