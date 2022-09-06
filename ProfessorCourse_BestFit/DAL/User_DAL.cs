using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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

        public List<UserRolesViewModel> Get_All_Professors()
        {
            List<UserRolesViewModel> All_Professors = new List<UserRolesViewModel>();

            // create command
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

            foreach (DataRow dr in dtMails.Rows)
            {
                All_Professors.Add(new UserRolesViewModel
                {
                    UserId = Convert.ToInt32(dr["UserId"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    LastName = Convert.ToString(dr["LastName"])
                });
            }

            return All_Professors;
        }


        //need test
        public IEnumerable<User> Get_All_Department_Managers(string ids)
        {
            List<User> All_Department_Managers = new List<User>();

            // create command
            SqlCommand command = _connection.CreateCommand();
            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;
            // specify name of SP
            command.CommandText = "getAllDepartmentManagers";

            command.Parameters.AddWithValue("@ids", ids);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dtMails = new DataTable();

                // open connection
                _connection.Open();
                adapter.Fill(dtMails);
                // close connection
                _connection.Close();

                foreach (DataRow dr in dtMails.Rows)
                {
                All_Department_Managers.Add(new User
                {
                        Uid = Convert.ToInt32(dr["Uid"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        MiddleName = Convert.ToString(dr["MiddleName"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        Email = Convert.ToString(dr["Email"])
                    });
                }

            return All_Department_Managers;
        }

        public List<User> Get_All_Potential_Employees(string ids, int departmentID)
        {
            List<User> All_Potential_Employees = new List<User>();

            // create command
            SqlCommand command = _connection.CreateCommand();
            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;
            // specify name of SP
            command.CommandText = "getAllAvailableEmployees";

            command.Parameters.AddWithValue("@ids", ids);
            
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
                All_Potential_Employees.Add(new User
                {
                    Uid = Convert.ToInt32(dr["Uid"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    MiddleName = Convert.ToString(dr["MiddleName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Email = Convert.ToString(dr["Email"])
                });
            }

            return All_Potential_Employees;
        }



    }
}