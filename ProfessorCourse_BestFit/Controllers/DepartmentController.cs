using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.messages;
using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ProfessorCourse_BestFit.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly string _Conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        private readonly SqlConnection _connection;
        private readonly ProfessorCourseBestFitEntities _context;
        private readonly Department_DAL department_DAL;
        private readonly Messages messages;
        public DepartmentController()
        {
            _context = new ProfessorCourseBestFitEntities();
            department_DAL = new Department_DAL();
            messages = new Messages();
            _connection = new SqlConnection(_Conn);
        }

        // GET: All Department
        public ActionResult All_Departments()
        {   
            DepartmentViewModel departmentViewModel = new DepartmentViewModel();
            departmentViewModel.all_departments = _context.Departments.Where(x => x.isDeleted == false).ToList();
            return View(departmentViewModel);
        }

        // GET: Create Department
        public ActionResult Create_Department()
        {
            var create_department = new DepartmentViewModel();
            create_department.List_User_Details = department_DAL.Get_All_Professors();
            /*
            SqlCommand command = _connection.CreateCommand();
            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;
            // specify name of SP
            command.CommandText = "getAllProfessors";

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dtMails = new DataTable();

            // open connection
            _connection.Open();
            adapter.Fill(dtMails);
            // close connection
            _connection.Close();

            IList<Choice> MyList = new List<Choice>();
            foreach (DataRow mydataRow in dtMails.Rows)
            {
                MyList.Add(new Choice()
                {
                    Id = mydataRow["Uid"].ToString().Trim(),
                    Text = mydataRow["FirstName"].ToString().Trim() + mydataRow["LastName"].ToString().Trim()
                });
            }

            ViewBag.CityList = new SelectList(MyList, "Id", "Text");
            */
            return View(create_department);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Department(DepartmentViewModel departmentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentViewModel);
            }
            var create_department = new Department();
            create_department.Dep_Name = departmentViewModel.Dep_Name;
            if(departmentViewModel.List_User_Details.Count() > 0)
            {
                for (int i = 0; i < departmentViewModel.List_User_Details.Count; i++)
                {
                    create_department.User_id += departmentViewModel.List_User_Details[i].ToString();
                    create_department.User_id += ",";
                }
            }

            _context.Departments.Add(create_department);
            try
            {
                _context.SaveChanges();
                ViewBag.savetitle = messages.message_success_submit_title;
                ViewBag.savebody = messages.message_success_submit_body;
            }
            catch
            {
                ViewBag.savetitle = messages.message_failed_submit_title;
                ViewBag.savebody = messages.message_failed_submit_body;
            }
            return View();
        }

        // GET: Show Department Information
        //the id should not be null
        public ActionResult Show_Department_Information(int? id)
        {
            if(id != null)
            {
                DepartmentViewModel departmentViewModel = new DepartmentViewModel();
                List<UserRolesViewModel> userRolesViewModels = new List<UserRolesViewModel>();
                UserRolesViewModel userRolesViewModel = new UserRolesViewModel();
                var department = _context.Departments.Where(x => x.Dep_Id == id).FirstOrDefault();
                departmentViewModel.Dep_Id = department.Dep_Id;
                departmentViewModel.Dep_Name = department.Dep_Name;
                departmentViewModel.User_id = department.User_id;
                if(departmentViewModel.User_id != null)
                {
                    var all_managers = department_DAL.Get_All_Department_Managers(departmentViewModel.User_id);
                    for(int i = 0; i < all_managers.Count; i++)
                    {
                        userRolesViewModel.UserId = all_managers[i].UserId;
                        userRolesViewModel.FirstName = all_managers[i].FirstName;
                        userRolesViewModel.LastName = all_managers[i].LastName;
                        userRolesViewModels.Add(userRolesViewModel);
                    }
                    departmentViewModel.List_Managers_Details = userRolesViewModels;
                    ViewBag.allManagers = messages.message_not_null;
                }
                else
                {
                    ViewBag.allManagers = messages.message_null;
                }
                departmentViewModel.List_Department_Programs = department_DAL.Get_Department_Programs(departmentViewModel.Dep_Id);

                return View(departmentViewModel);
            }
            else
            {
                //should add a condition if the input id is null
                return View();
            }
        }

        public ActionResult Edit_Department(int id)
        {
            return View();
        }

        public ActionResult Edit_Department_Programs(int id)
        {
            return View();
        }

        public ActionResult Delete_Department(int id)
        {
            var department = _context.Departments.Where(x => x.Dep_Id == id).FirstOrDefault();
            department.isDeleted = true;
            _context.SaveChanges();
            return View("All_Departments");
        }

    }
}