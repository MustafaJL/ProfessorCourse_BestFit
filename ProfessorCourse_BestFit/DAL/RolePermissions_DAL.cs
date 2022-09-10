using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProfessorCourse_BestFit.DAL
{
    public class RolePermissions_DAL
    {
        private readonly SqlConnection _connection;
        private readonly string _Conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public RolePermissions_DAL()
        {
            _connection = new SqlConnection(_Conn);
        }

        public bool CreateRolePermissions(int RoleId)
        {
            int success = 0;
            // create command
            SqlCommand command = new SqlCommand("spCreateRolePermissions", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@RoleId", RoleId);


            _connection.Open();
            success = command.ExecuteNonQuery();
            _connection.Close();

            return success > 0 ? true : false;


        }

        public bool UpdateRolePermissions(int RoleId, string permissions)
        {
            int success = 0;
            // create command
            SqlCommand command = new SqlCommand("spUpdateRolePermissions", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@RoleId", RoleId);
            command.Parameters.AddWithValue("@permissions", permissions);


            _connection.Open();
            success = command.ExecuteNonQuery();
            _connection.Close();

            return success > 0 ? true : false;


        }



        public List<PermissionsViewModel> GetPermissionsByRoleId(int RoleId)
        {
            List<PermissionsViewModel> permissiobList = new List<PermissionsViewModel>();

            // create command
            SqlCommand command = _connection.CreateCommand();
            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;
            // specify name of SP
            command.CommandText = "spPermissionByRoleId";
            // pass the value of parameter
            command.Parameters.AddWithValue("@RoleId", RoleId);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dtPermissions = new DataTable();

            // open connection
            _connection.Open();
            adapter.Fill(dtPermissions);
            // close connection
            _connection.Close();

            foreach (DataRow dr in dtPermissions.Rows)
            {
                permissiobList.Add(new PermissionsViewModel
                {
                    PId = Convert.ToInt32(dr["PId"]),
                    PName = Convert.ToString(dr["PName"]),
                    IsActive = Convert.ToBoolean(dr["isActive"])

                });
            }


            return permissiobList;
        }
    }

}