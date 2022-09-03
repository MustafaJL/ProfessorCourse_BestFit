using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Models.ViewModels
{
    public class DepartmentViewModel
    {
        public int Dep_Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Department Name")]
        public string Dep_Name { get; set; }

        public bool isDeleted { get; set; }

        public string User_id { get; set; }

        [Display(Name = "Manager User Name")]
        [Required(ErrorMessage = "Select Name before submit")]
        public string Manager_User_Name { get; set; }

        //this list for chose the managers of the department
        public List<UserRolesViewModel> List_User_Details { get; set; }

        //this list to save the managers of the department
        public IEnumerable<SelectListItem> choice { get; set; }
        
        public IEnumerable<SelectListItem> CityList { get; set; }

        public List<UserRolesViewModel> List_Managers_Details { get; set; }

        //public List<UserRolesViewModel> List_Managers_Details { get; set; }

        //this list to save the programs in the department
        public List<ProgramViewModel> List_Department_Programs { get; set; }

        public int id { get; set; }
    }
}