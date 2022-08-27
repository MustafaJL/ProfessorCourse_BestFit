using ProfessorCourse_BestFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class ProgramController : Controller
    {
        /*
         * My Stord Procedure
         
         _context.my_InsertUpdateDelete_Program(int? id, 
                                                String name,
                                                int id,
                                                int? QueryNumber);
        QueryNumbres:
        1: INSERT
        2: UPDATE
        3: DELETE
        4: SELECT * (No Condition)
        5: SELECT * (Based on the id)

         */

        private readonly ApplicationDbContext _context;

        public ProgramController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Program
        public ActionResult Index()
        {
            return View();
        }
    }
}