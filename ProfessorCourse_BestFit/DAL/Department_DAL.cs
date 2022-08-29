using ProfessorCourse_BestFit.Models;
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

        public Department_DAL()
        {
            _connection = new SqlConnection(_Conn);
        }

        // Get All Containers
        public List<Department> GetDepartments(int? id, int? queryNum)
        {
            List<Department> allDepartment = new List<Department>();

            // create command
            SqlCommand command = _connection.CreateCommand();
            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;
            // specify name of SP
            command.CommandText = "my_InsertUpdateDelete_Department";
            // pass the value of parameter
            command.Parameters.AddWithValue("@DepartmentID", id);
            command.Parameters.AddWithValue("@Query", queryNum);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dtMails = new DataTable();

            // open connection
            _connection.Open();
            adapter.Fill(dtMails);
            // close connection
            _connection.Close();

            foreach (DataRow dr in dtMails.Rows)
            {
                allDepartment.Add(new Department
                {
                    Dep_Id = Convert.ToInt32(dr["Dep_Id"]),
                    Dep_Name = Convert.ToString(dr["Dep_Name"])
                });
            }


            return allDepartment;
        }
    }
}