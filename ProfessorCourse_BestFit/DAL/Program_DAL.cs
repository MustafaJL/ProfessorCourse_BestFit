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
        private readonly ApplicationDbContext _context;

        public Program_DAL()
        {
            _context = new ApplicationDbContext();
            _connection = new SqlConnection(_Conn);
        }

        // Get All Containers
        public List<Program> GetPrograms(int? id, int? queryNum)
        {
            List<Program> allProgram = new List<Program>();

            // create command
            SqlCommand command = _connection.CreateCommand();
            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;
            // specify name of SP
            command.CommandText = "my_InsertUpdateDelete_Program";
            // pass the value of parameter
            command.Parameters.AddWithValue("@ProgramID", id);
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
                allProgram.Add(new Program
                {
                    PId = Convert.ToInt32(dr["PId"]),
                    Dep_Id = Convert.ToInt32(dr["Dep_Id"]),
                    Name = Convert.ToString(dr["Name"])
                });
            }


            return allProgram;
        }
    }
}