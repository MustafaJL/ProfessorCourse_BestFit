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
        // Get All Containers
        public List<UserRolesViewModel> GetUserRoles(int? id)
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
            List<UserRolesViewModel> userRolesViewModels = GetUserRoles(id);
            string roles = "";
            foreach (UserRolesViewModel item in userRolesViewModels)
            {
                roles += item.RoleName + ",";
            }

            return roles;

        }
    }
}