using System.ComponentModel.DataAnnotations;

namespace ProfessorCourse_BestFit.Models.ViewModels
{
    public class PermissionsViewModel
    {

        public int? PId { get; set; }


        [Display(Name = "Permission")]
        public string PName { get; set; }

        public bool IsActive { get; set; }


    }
}