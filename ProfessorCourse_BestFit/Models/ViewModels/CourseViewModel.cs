using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Models.ViewModels
{
    public class CourseViewModel
    {
        public Course Course { get; set; }

        public IEnumerable<Course> all_Courses { get; set; }

        public IEnumerable<Program> course_Programs { get; set; }

        //To choose Professors as an example
        public IEnumerable<User> normal_Users { get; set; }

        public IEnumerable<User> course_Professors { get; set; }

        public List<SelectListItem> optionList { get; set; }
    }
}