using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfessorCourse_BestFit.Models.ViewModels
{
    public class SemesterViewModel
    {
        public Semester semester { get; set; }

        public IEnumerable<Semester> all_semester { get; set; }
    }
}