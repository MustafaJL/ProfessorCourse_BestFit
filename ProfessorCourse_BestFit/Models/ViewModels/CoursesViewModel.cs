using System;
using System.ComponentModel.DataAnnotations;

namespace ProfessorCourse_BestFit.Models.ViewModels
{
    public class CoursesViewModel
    {
        public int? CId { get; set; }


        [Required]
        [Display(Name = "Course")]
        public string CName { get; set; }

        public string CourseCode { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Duration { get; set; }

    }
}