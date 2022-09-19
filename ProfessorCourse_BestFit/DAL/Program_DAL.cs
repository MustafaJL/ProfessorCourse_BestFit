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
    public class Program_DAL
    {
        private readonly SqlConnection _connection;
        private readonly string _Conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        //private readonly User_DAL user_DAL;
        //private readonly Course_DAL course_DAL;

        public Program_DAL()
        {
            _connection = new SqlConnection(_Conn);
            //user_DAL = new User_DAL();
            //course_DAL = new Course_DAL();
        }

        public IEnumerable<Course> Get_Course_Programs(int programID, int option)
        {
            //the options come from department_DAL base on the method action

            List<Course> resut = new List<Course>();

            // create command
            SqlCommand command = _connection.CreateCommand();

            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;

            // specify name of SP
            if (option == 1)
            {
                command.CommandText = "ProgramCourses";
            }
            if (option == 2)
            {
                command.CommandText = "ProgramCoursesToAdd";
            }

            command.Parameters.AddWithValue("@ProgramID", programID);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dtMails = new DataTable();

            // open connection
            _connection.Open();
            adapter.Fill(dtMails);
            // close connection
            _connection.Close();

            foreach (DataRow dr in dtMails.Rows)
            {
                resut.Add(new Course
                {
                    CId = Convert.ToInt32(dr["CId"]),
                    CName = Convert.ToString(dr["CName"]),
                    Code = Convert.ToString(dr["Code"])
                });
            }

            return resut;
        }

    }
}