using System;
using System.ComponentModel.DataAnnotations;

namespace ProfessorCourse_BestFit.Models
{
    public class UserViewModel
    {
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
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        //[Required(ErrorMessage = "Password is Required")]B
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        //[Display(Name = "Confirm Password")]
        //[Required(ErrorMessage = "Confirm Password is required")]
        //[Compare("Password", ErrorMessage = "Password Doesn't Match")]
        //[DataType(DataType.Password)]
        //public string ConfirmPassword { get; set; }

        [Required]
        public string Gender { get; set; }
        [Required]
        public string PhoneNumber { get; set; }



        public int User_id { get; set; }
    }
}