using System.Net;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class EventsController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Event()
        {
            WebClient webClient = new WebClient();
            string text = webClient.DownloadString("https://erasmusplus-lebanon.org/events");
            ////string wrd = "eventsBlock row eventsBlock1";

            int Start = text.IndexOf("<!--Start of News_Block -->") + 27;
            int End = text.LastIndexOf("<!--End of News_Block -->") + 28;
            int length = End - Start;

            string minmized = text.Substring(Start, length);




            return Json(minmized);
        }
    }
}