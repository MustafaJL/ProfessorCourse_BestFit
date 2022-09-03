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

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Program Name")]
        public string Name { get; set; }

        public bool isDeleted { get; set; }

        public IEnumerable<Program> All_Programs { get; set; }
    }
}