using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProfessorCourse_BestFit.DAL
{
    public class CourseKeywords_DAL
    {
        private readonly SqlConnection _connection;
        private readonly string _Conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public CourseKeywords_DAL()
        {
            _connection = new SqlConnection(_Conn);
        }

        public bool CreateCourseKeywords(int UserId)
        {
            int success = 0;
            // create command
            SqlCommand command = new SqlCommand("spCreateCourseKeywords", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CourseId", UserId);


            _connection.Open();
            success = command.ExecuteNonQuery();
            _connection.Close();

            return success > 0 ? true : false;


        }

        public bool UpdateCourseKeyword(int UserId, string keywords)
        {
            int success = 0;
            // create command
            SqlCommand command = new SqlCommand("spUpdateCourseKeywords", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CourseId", UserId);
            command.Parameters.AddWithValue("@Keywords", keywords);


            _connection.Open();
            success = command.ExecuteNonQuery();
            _connection.Close();

            return success > 0 ? true : false;


        }



        public List<KeywordsViewModel> GetKeywordsByCourseId(int UserId)
        {
            List<KeywordsViewModel> keywordsList = new List<KeywordsViewModel>();

            // create command
            SqlCommand command = _connection.CreateCommand();
            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;
            // specify name of SP
            command.CommandText = "spKeywordsByCourseId";
            // pass the value of parameter
            command.Parameters.AddWithValue("@CourseId", UserId);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dtKeywords = new DataTable();

            // open connection
            _connection.Open();
            adapter.Fill(dtKeywords);
            // close connection
            _connection.Close();

            foreach (DataRow dr in dtKeywords.Rows)
            {
                keywordsList.Add(new KeywordsViewModel
                {
                    KId = Convert.ToInt32(dr["KId"]),
                    kName = Convert.ToString(dr["KName"]),
                    IsActive = Convert.ToBoolean(dr["isActive"])

                });
            }


            return keywordsList;
        }
    }

}