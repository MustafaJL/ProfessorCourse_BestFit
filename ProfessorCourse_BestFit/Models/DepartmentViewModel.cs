using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProfessorCourse_BestFit.Models
{
    public class DepartmentViewModel
    {
        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Department Name :")]
        public string Dep_Name { get; set; }
    }
}