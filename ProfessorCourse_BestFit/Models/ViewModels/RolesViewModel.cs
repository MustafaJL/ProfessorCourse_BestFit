using System.ComponentModel.DataAnnotations;

namespace ProfessorCourse_BestFit.Models.ViewModels
{
    public class RolesViewModel
    {

        public int? RoleId { get; set; }


        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}