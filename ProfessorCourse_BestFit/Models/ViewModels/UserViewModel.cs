using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ProfessorCourse_BestFit.Models
{
    public class UserViewModel
    {

        public int? Id { get; set; }

        [Required(ErrorMessage = "FirtName is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "MiddleName is Required")]
        public string MiddleName { get; set; }


        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }

        [Display(Name = "Date Of Birth")]

        public string DateOfBirth { get; set; }

        [Required]
        public int RoleId { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Education { get; set; }

        public string ImageUrl { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}