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
    public class User_DAL
    {
        private readonly SqlConnection _connection;
        private readonly string _Conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        public User_DAL()
        {
            _connection = new SqlConnection(_Conn);
        }

        /*
        public List<UserRolesViewModel> GetUserRoles()
        {
            List<UserRolesViewModel> UserRoles = new List<UserRolesViewModel>();

            // create command
            SqlCommand command = _connection.CreateCommand();
            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;
            // specify name of SP
            command.CommandText = "spGetUserRoles";

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dtMails = new DataTable();

            // open connection
            _connection.Open();
            adapter.Fill(dtMails);
            // close connection
            _connection.Close();

            foreach (DataRow dr in dtMails.Rows)
            {
                UserRoles.Add(new UserRolesViewModel
                {
                    UserId = Convert.ToInt32(dr["Uid"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    MiddleName = Convert.ToString(dr["MiddleName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Gender = Convert.ToString(dr["Gender"]),
                    Phone = Convert.ToString(dr["Phone"]),
                    Email = Convert.ToString(dr["Email"]),
                    DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]),
                    CreatedOn = Convert.ToDateTime(dr["CreatedOn"]),
                    RoleName = Convert.ToString(dr["RoleName"])

                });
            }


            return UserRoles;
        }
        */
        /*
        public List<UserRolesViewModel> GetUserRolesById(int? id)
        {
            List<UserRolesViewModel> UserRoles = new List<UserRolesViewModel>();

            // create command
            SqlCommand command = _connection.CreateCommand();
            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;
            // specify name of SP
            command.CommandText = "spGetUserRolesById";
            // pass the value of parameter
            command.Parameters.AddWithValue("@UserId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dtMails = new DataTable();

            // open connection
            _connection.Open();
            adapter.Fill(dtMails);
            // close connection
            _connection.Close();

            foreach (DataRow dr in dtMails.Rows)
            {
                UserRoles.Add(new UserRolesViewModel
                {
                    FirstName = Convert.ToString(dr["FirstName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Email = Convert.ToString(dr["Email"]),
                    DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]),
                    CreatedOn = Convert.ToDateTime(dr["CreatedOn"]),
                    RoleName = Convert.ToString(dr["RoleName"])

                });
            }


            return UserRoles;
        }
        */
        /*
        public string UserRoleNames(int? id)
        {
            List<UserRolesViewModel> userRolesViewModels = GetUserRolesById(id);
            string roles = "";
            foreach (UserRolesViewModel item in userRolesViewModels)
            {
                roles += item.RoleName + ",";
            }

            return roles;

        }
        */
        public IEnumerable<User> Get_Users_Department(int departmentID, int option)
        {
            //the options come from department_DAL base on the method action

            List<User> resut = new List<User>();

            // create command
            SqlCommand command = _connection.CreateCommand();

            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;

            // specify name of SP
            if (option == 1)
            {
                command.CommandText = "DepartmentManagers";
            }
            if (option == 2)
            {
                command.CommandText = "Departmentemployees";
            }
            if (option == 3)
            {
                command.CommandText = "DepartmentAddRemoveManagers";
            }
            if (option == 4)
            {
                command.CommandText = "DepartmentEmployeesToAdd";
            }

            command.Parameters.AddWithValue("@DepartmentID", departmentID);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dtMails = new DataTable();

            // open connection
            _connection.Open();
            adapter.Fill(dtMails);
            // close connection
            _connection.Close();

            foreach (DataRow dr in dtMails.Rows)
            {
                resut.Add(new User
                {
                    Uid = Convert.ToInt32(dr["Uid"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    MiddleName = Convert.ToString(dr["MiddleName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Email = Convert.ToString(dr["Email"]),
                    Phone = Convert.ToString(dr["Phone"])
                });
            }

            return resut;
        }

        public IEnumerable<User> Get_Users_Course(int courseID, int option)
        {
            //the options come from department_DAL base on the method action

            List<User> resut = new List<User>();

            // create command
            SqlCommand command = _connection.CreateCommand();

            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;

            // specify name of SP
            if (option == 1)
            {
                command.CommandText = "CourseProfessors";
            }
            else if (option == 2)
            {
                command.CommandText = "CourseUsersToBeProfessors";
            }

            command.Parameters.AddWithValue("@CourseID", courseID);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dtMails = new DataTable();

            // open connection
            _connection.Open();
            adapter.Fill(dtMails);
            // close connection
            _connection.Close();

            foreach (DataRow dr in dtMails.Rows)
            {
                resut.Add(new User
                {
                    Uid = Convert.ToInt32(dr["Uid"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    MiddleName = Convert.ToString(dr["MiddleName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Email = Convert.ToString(dr["Email"]),
                    Phone = Convert.ToString(dr["Phone"])
                });
            }

            return resut;
        }

        public IEnumerable<User> Get_Users_Program(int programID)
        {
            //the options come from department_DAL base on the method action

            List<User> resut = new List<User>();

            // create command
            SqlCommand command = _connection.CreateCommand();

            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;

            // specify name of SP
            command.CommandText = "ProgramManagers";


            command.Parameters.AddWithValue("@ProgramID", programID);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dtMails = new DataTable();

            // open connection
            _connection.Open();
            adapter.Fill(dtMails);
            // close connection
            _connection.Close();

            foreach (DataRow dr in dtMails.Rows)
            {
                resut.Add(new User
                {
                    Uid = Convert.ToInt32(dr["Uid"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    MiddleName = Convert.ToString(dr["MiddleName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Email = Convert.ToString(dr["Email"]),
                    Phone = Convert.ToString(dr["Phone"])
                });
            }

            return resut;
        }
    }
}