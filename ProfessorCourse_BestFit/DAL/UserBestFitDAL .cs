using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProfessorCourse_BestFit.DAL
{
    public class UserBestFitDAL
    {

        private readonly SqlConnection _connection;
        private readonly string _Conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public UserBestFitDAL()
        {
            _connection = new SqlConnection(_Conn);
        }



        public List<UserBestFitViewModel> GetCourseByUserId(int userId)
        {
            List<UserBestFitViewModel> usersList = new List<UserBestFitViewModel>();

            // create command
            SqlCommand command = _connection.CreateCommand();
            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;
            // specify name of SP
            command.CommandText = "spGetCoursesByUserId";
            // pass the value of parameter
            command.Parameters.AddWithValue("@UserId", userId);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dtKeywords = new DataTable();

            // open connection
            _connection.Open();
            adapter.Fill(dtKeywords);
            // close connection
            _connection.Close();

            foreach (DataRow dr in dtKeywords.Rows)
            {
                usersList.Add(new UserBestFitViewModel
                {
                    CourseName = Convert.ToString(dr["CourseName"]),
                    CourseCode = Convert.ToString(dr["CourseCode"]),
                    Count = Convert.ToInt32(dr["counting"]).ToString(),



                });
            }


            return usersList;
        }
    }
}