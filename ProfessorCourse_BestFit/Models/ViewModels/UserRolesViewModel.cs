using System.Collections.Generic;

namespace ProfessorCourse_BestFit.Models.ViewModels
{
    public class UserRolesViewModel
    {


        public UserViewModel User { get; set; }

        public IEnumerable<Role> Roles { get; set; }



    }
}