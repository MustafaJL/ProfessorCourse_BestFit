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
    public class Program_DAL
    {
        private readonly SqlConnection _connection;
        private readonly string _Conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public Program_DAL()
        {
            _connection = new SqlConnection(_Conn);
        }

        //the id should not be null
        public List<ProgramViewModel> Get_Department_Programs(int id)
        {
            List<ProgramViewModel> Department_Programs = new List<ProgramViewModel>();

            // create command
            SqlCommand command = _connection.CreateCommand();
            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;
            // specify name of SP
            command.CommandText = "getAllPrograms";
            // pass the value of parameter
            command.Parameters.AddWithValue("@DepartmentID", id);
            command.Parameters.AddWithValue("@Query", 2);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dtMails = new DataTable();

            // open connection
            _connection.Open();
            adapter.Fill(dtMails);
            // close connection
            _connection.Close();

            foreach (DataRow dr in dtMails.Rows)
            {
                Department_Programs.Add(new ProgramViewModel
                {
                    PId = Convert.ToInt32(dr["PId"]),
                    Dep_Id = Convert.ToInt32(dr["Dep_Id"]),
                    Name = Convert.ToString(dr["Name"])

                });
            }

            return Department_Programs;
        }
    }
}