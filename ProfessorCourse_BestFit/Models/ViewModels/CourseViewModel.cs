using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProfessorCourse_BestFit.Models.ViewModels
{
    public class CourseViewModel
    {
        public int CId { get; set; }

        [Required(ErrorMessage = "Code is required")]
        [Display(Name = "Course Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Course Name")]
        public string CName { get; set; }

        public int Duration { get; set; }

        public bool isDeleted { get; set; }
    }
}