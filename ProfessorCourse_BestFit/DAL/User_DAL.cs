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
                    LastName = Convert.ToString(dr["LastName"]),
                    Email = Convert.ToString(dr["Email"]),
                    DateOfBirth = Convert.ToString(dr["DateOfBirth"]),
                    CreatedOn = Convert.ToString(dr["CreatedOn"]),
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
                    DateOfBirth = Convert.ToString(dr["DateOfBirth"]),
                    CreatedOn = Convert.ToString(dr["CreatedOn"]),
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

        //the id should nnot be null
        public List<UserRolesViewModel> Get_All_Department_Managers(int id)
        {
            List<UserRolesViewModel> All_Department_Managers = new List<UserRolesViewModel>();

            // create command
            SqlCommand command = _connection.CreateCommand();
            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;
            // specify name of SP
            command.CommandText = "getAllDepartmentManager";

            command.Parameters.AddWithValue("@DepartmentID", id);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dtMails = new DataTable();

            // open connection
            _connection.Open();
            adapter.Fill(dtMails);
            // close connection
            _connection.Close();

            foreach (DataRow dr in dtMails.Rows)
            {
                All_Department_Managers.Add(new UserRolesViewModel
                {
                    UserId = Convert.ToInt32(dr["UserId"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    LastName = Convert.ToString(dr["LastName"])
                });
            }

            return All_Department_Managers;
        }

    }
}