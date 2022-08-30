using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProfessorCourse_BestFit.Models.ViewModels
{
    public class ProgramViewModel
    {
        public int PId { get; set; }

        public int Dep_Id { get; set; }

        [Display(Name = "Program Name")]
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        public bool isDeleted { get; set; }

    }
}