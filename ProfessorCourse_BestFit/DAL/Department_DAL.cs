using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProfessorCourse_BestFit.DAL
{
    public class Department_DAL
    {
        private readonly SqlConnection _connection;
        private readonly string _Conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        private readonly User_DAL user_DAL;
        public Department_DAL()
        {
            _connection = new SqlConnection(_Conn);
            user_DAL = new User_DAL();
        }

        /*
        public IEnumerable<User> Get_Department_Managers(int departmentID)
        {
            return user_DAL.Get_Users_Department(departmentID, 1);
        }

        public IEnumerable<User> Get_Department_Employees(int departmentID)
        {
            return user_DAL.Get_Users_Department(departmentID, 2);
        }
        */
    }
}