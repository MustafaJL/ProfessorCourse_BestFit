using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProfessorCourse_BestFit.DAL
{
    public class Department_DAL
    {
        private readonly SqlConnection _connection;
        private readonly string _Conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        private readonly User_DAL user_DAL;
        private readonly Program_DAL program_DAL;
        public Department_DAL()
        {
            _connection = new SqlConnection(_Conn);
            user_DAL = new User_DAL();
            program_DAL = new Program_DAL();
        }


        public List<DepartmentViewModel> Get_All_Departments()
        {
            List<DepartmentViewModel> All_Departments = new List<DepartmentViewModel>();

            // create command
            SqlCommand command = _connection.CreateCommand();
            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;
            // specify name of SP
            command.CommandText = "getAllDepartments";

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dtMails = new DataTable();

            // open connection
            _connection.Open();
            adapter.Fill(dtMails);
            // close connection
            _connection.Close();

            foreach (DataRow dr in dtMails.Rows)
            {
                All_Departments.Add(new DepartmentViewModel
                {
                    Dep_Id = Convert.ToInt32(dr["Dep_Id"]),
                    Dep_Name = Convert.ToString(dr["Dep_Name"])
                });
            }

            return All_Departments;
        }

        public List<UserRolesViewModel> Get_All_Professors()
        {
            return user_DAL.Get_All_Professors();
        }

        public IEnumerable<User> Get_All_Department_Managers(string id)
        {
            return user_DAL.Get_All_Department_Managers(id);
        }

        public List<ProgramViewModel> Get_Department_Programs(int id)
        {
            return program_DAL.Get_Department_Programs(id);
        }

        public void Delete_Department(int id)
        {
            // create command
            SqlCommand command = _connection.CreateCommand();
            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;
            // specify name of SP
            command.CommandText = "Delete_Department";
            // pass the value of parameter
            command.Parameters.AddWithValue("@DepartmentID", id);

            _connection.Open();

            command.ExecuteReader();

            _connection.Close();
        }
    }
}