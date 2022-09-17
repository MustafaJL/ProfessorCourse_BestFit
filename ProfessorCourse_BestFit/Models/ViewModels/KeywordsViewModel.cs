using System.ComponentModel.DataAnnotations;

namespace ProfessorCourse_BestFit.Models.ViewModels
{
    public class KeywordsViewModel
    {

        public int? KId { get; set; }


        [Display(Name = "Permission")]
        public string kName { get; set; }

        public string IsActive { get; set; }

    }
}