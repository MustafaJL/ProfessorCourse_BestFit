using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfessorCourse_BestFit.Models.ViewModels
{
    public class DepartmentViewModel
    {
        public Department Department { get; set; }

        public IEnumerable<Department> all_Departments { get; set; }

        //To choose managers and employees as an example
        public IEnumerable<User> normal_Users { get; set; }

        public IEnumerable<User> managers { get; set; }

        public IEnumerable<User> employee { get; set; }

        //To choose Programs as an example
        public IEnumerable<Program> all_Programs { get; set; }

        public IEnumerable<Program> department_Programs { get; set; }
    }
}