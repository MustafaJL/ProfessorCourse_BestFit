using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProfessorCourse_BestFit.DAL
{
    public class isExistChecker
    {

        private readonly SqlConnection _connection;
        private readonly string _Conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        public isExistChecker()
        {
            _connection = new SqlConnection(_Conn);
        }



        public bool isExist(string tableName, string columnName, string rowValue)
        {
            bool isExist = false;
            SqlCommand command = _connection.CreateCommand();

            // specify the type of cammand
            command.CommandType = CommandType.StoredProcedure;


            command.CommandText = "spIsRowExist";


            command.Parameters.AddWithValue("@tableName", tableName);
            command.Parameters.AddWithValue("@columnName", columnName);
            command.Parameters.AddWithValue("@rowValue", rowValue);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dtMails = new DataTable();

            // open connection
            _connection.Open();
            adapter.Fill(dtMails);
            // close connection
            _connection.Close();

            foreach (DataRow dr in dtMails.Rows)
            {
                if (Convert.ToBoolean(dr["COUNTER"]))
                {
                    isExist = true;
                }
            }

            return isExist;
        }
    }

}