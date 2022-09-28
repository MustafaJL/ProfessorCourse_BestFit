using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProfessorCourse_BestFit.DAL
{
    public class UserKeywords_DAL
    {
        private readonly SqlConnection _connection;
        private readonly string _Conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public UserKeywords_DAL()
        {
            _connection = new SqlConnection(_Conn);
        }

        public bool CreateUserKeywords(int UserId)
        {
            int success = 0;
            // create command
            SqlCommand command = new SqlCommand("spCreateUserKeywords", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserId", UserId);


            _connection.Open();
            success = command.ExecuteNonQuery();
            _connection.Close();

            return success > 0 ? true : false;


        }

        public bool CreateKeywordUser(int keywordId)
        {
            int success = 0;
            // create command
            SqlCommand command = new SqlCommand("spCreateKeywordUser", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@KeywordId", keywordId);


            _connection.Open();
            success = command.ExecuteNonQuery();
            _connection.Close();

            return success > 0 ? true : false;


        }

        public bool CreateKeywordCourse(int keywordId)
        {
            int success = 0;
            // create command
            SqlCommand command = new SqlCommand("spCreateKeywordCourse", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@KeywordId", keywordId);


            _connection.Open();
            success = command.ExecuteNonQuery();
            _connection.Close();

            return success > 0 ? true : false;


        }


        public bool UpdateUserKeyword(int UserId, string keywords)
        {
            int success = 0;
            // create command
            SqlCommand command = new SqlCommand("spUpdateUserKeywords", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserId", UserId);
            command.Parameters.AddWithValue("@Keywords", keywords);


            _connection.Open();
            success = command.ExecuteNonQuery();
            _connection.Close();

            return success > 0 ? true : false;


        }



        public List<KeywordsViewModel> GetKeywordsByUserId(int UserId)
        {
            List<KeywordsViewModel> keywordsList = new List<KeywordsViewModel>();

            // create command
            SqlCommand command = _connection.CreateCommand();
            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;
            // specify name of SP
            command.CommandText = "spKeywordsByUserId";
            // pass the value of parameter
            command.Parameters.AddWithValue("@UserId", UserId);

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
                    IsActive = Convert.ToString(dr["isActive"])

                });
            }


            return keywordsList;
        }


        public List<KeywordsViewModel> GetAllKeywordsIncludesMatchingByUserId(int UserId)
        {
            List<KeywordsViewModel> keywordsList = new List<KeywordsViewModel>();

            // create command
            SqlCommand command = _connection.CreateCommand();
            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;
            // specify name of SP
            command.CommandText = "spGetAllKeywordsIncludeMatching";
            // pass the value of parameter
            command.Parameters.AddWithValue("@UserId", UserId);

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
                    IsActive = Convert.ToString(dr["Matching"])

                });
            }


            return keywordsList;
        }

        public List<UserGraphViewModel> GetUsersKeywords()
        {
            List<UserGraphViewModel> usersList = new List<UserGraphViewModel>();

            // create command
            SqlCommand command = _connection.CreateCommand();
            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;
            // specify name of SP
            command.CommandText = "spGetKeywordsUsers";


            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dtKeywords = new DataTable();

            // open connection
            _connection.Open();
            adapter.Fill(dtKeywords);
            // close connection
            _connection.Close();

            foreach (DataRow dr in dtKeywords.Rows)
            {
                usersList.Add(new UserGraphViewModel
                {
                    Email = Convert.ToString(dr["Email"]),
                    KeywordsCount = Convert.ToInt32(dr["counting"])


                });
            }


            return usersList;

        }
    }

}