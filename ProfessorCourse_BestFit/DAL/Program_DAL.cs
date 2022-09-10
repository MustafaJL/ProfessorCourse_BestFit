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
        private readonly User_DAL user_DAL;
        private readonly Course_DAL course_DAL;

        public Program_DAL()
        {
            _connection = new SqlConnection(_Conn);
            user_DAL = new User_DAL();
            course_DAL = new Course_DAL();
        }

        public IEnumerable<Program> Get_Course_Programs(int courseID, int option)
        {
            //the options come from department_DAL base on the method action

            List<Program> resut = new List<Program>();

            // create command
            SqlCommand command = _connection.CreateCommand();

            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;

            // specify name of SP
            if(option == 1)
            {
                command.CommandText = "CoursePrograms";
            }
            if(option == 2)
            {
                command.CommandText = "CourseProgramToAdd";
            }

            command.Parameters.AddWithValue("@CourseID", courseID);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dtMails = new DataTable();

            // open connection
            _connection.Open();
            adapter.Fill(dtMails);
            // close connection
            _connection.Close();

            foreach (DataRow dr in dtMails.Rows)
            {
                resut.Add(new Program
                {
                    ProgramId = Convert.ToInt32(dr["ProgramId"]),
                    Dep_Id = Convert.ToInt32(dr["Dep_Id"]),
                    ProgramName = Convert.ToString(dr["ProgramName"])
                });
            }

            return resut;
        }

        public IEnumerable<User> Get_Program_Managers(int ProgramID)
        {
            return user_DAL.Get_Users_Program(ProgramID);
        }

        public IEnumerable<Course> Get_Program_Courses(int ProgramID)
        {
            return course_DAL.Get_Program_Courses(ProgramID);
        }
    }
}