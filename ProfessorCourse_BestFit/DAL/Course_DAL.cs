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
    public class Course_DAL
    {
        private readonly SqlConnection _connection;
        private readonly string _Conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        private readonly User_DAL user_DAL;
        private readonly Program_DAL program_DAL;
        public Course_DAL()
        {
            _connection = new SqlConnection(_Conn);
            user_DAL = new User_DAL();
            program_DAL = new Program_DAL();
        }

        public IEnumerable<User> Get_Course_Professors(int CourseID)
        {
            return user_DAL.Get_Users_Course(CourseID);
        }

        public IEnumerable<Program> Get_Course_Programs(int CourseID)
        {
            //this one for get all programs that this course is in them.
            return program_DAL.Get_Course_Programs(CourseID);
        }

        public IEnumerable<Course> Get_Program_Courses(int ProgramID)
        {
            //to get all courses in this program

            //the options come from department_DAL base on the method action

            List<Course> resut = new List<Course>();

            // create command
            SqlCommand command = _connection.CreateCommand();

            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;

            // specify name of SP
            command.CommandText = "ProgramCourses";


            command.Parameters.AddWithValue("@ProgramID", ProgramID);

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
                    Code = Convert.ToString(dr["Code"]),
                    CName = Convert.ToString(dr["CName"]),
                    Duration = Convert.ToInt32(dr["Duration"]),
                    isDeleted = Convert.ToBoolean(dr["isDeleted"])
                });
            }

            return resut;
        }
    }
}