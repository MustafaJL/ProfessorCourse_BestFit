using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfessorCourse_BestFit.Models.ViewModels
{
    public class ProgramViewModel
    {
        public Program Program { get; set; }

        public IEnumerable<Program> all_Programs { get; set; }

        //To choose courses as an example
        public IEnumerable<Course> all_courses { get; set; }

        public IEnumerable<Course> program_Courses { get; set; }

        //To choose managers as an example
        public IEnumerable<User> normal_Users { get; set; }

        public IEnumerable<User> managers { get; set; }
    }
}