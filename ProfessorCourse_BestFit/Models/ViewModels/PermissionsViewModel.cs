using System.ComponentModel.DataAnnotations;

namespace ProfessorCourse_BestFit.Models.ViewModels
{
    public class PermissionsViewModel
    {

        public int? PId { get; set; }


        [Required]
        [Display(Name = "Permission")]
        public string PName { get; set; }
    }
}