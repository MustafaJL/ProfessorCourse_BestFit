using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProfessorCourse_BestFit.DAL
{
    public class CourseBestFitDAL
    {

        private readonly SqlConnection _connection;
        private readonly string _Conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public CourseBestFitDAL()
        {
            _connection = new SqlConnection(_Conn);
        }



        public List<CourseBestFitViewModel> GetUsersByCourseId(int CourseId)
        {
            List<CourseBestFitViewModel> usersList = new List<CourseBestFitViewModel>();

            // create command
            SqlCommand command = _connection.CreateCommand();
            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;
            // specify name of SP
            command.CommandText = "spGetUsersByCourseId";
            // pass the value of parameter
            command.Parameters.AddWithValue("@CourseId", CourseId);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dtKeywords = new DataTable();

            // open connection
            _connection.Open();
            adapter.Fill(dtKeywords);
            // close connection
            _connection.Close();

            foreach (DataRow dr in dtKeywords.Rows)
            {
                usersList.Add(new CourseBestFitViewModel
                {
                    FirstName = Convert.ToString(dr["FirstName"]),
                    MiddleName = Convert.ToString(dr["MiddleName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Email = Convert.ToString(dr["Email"]),
                    Gender = Convert.ToString(dr["Gender"]),
                    Count = Convert.ToInt32(dr["counting"]).ToString(),



                });
            }


            return usersList;
        }
    }
}