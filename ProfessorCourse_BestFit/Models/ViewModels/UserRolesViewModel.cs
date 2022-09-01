using System;

namespace ProfessorCourse_BestFit.Models.ViewModels
{
    public class UserRolesViewModel
    {
        public int? UserId { get; set; }
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }


        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedOn { get; set; }


        public string RoleName { get; set; }


    }
}