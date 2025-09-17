using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows;

namespace RecordsAplication.Classes
{
    public class DBConnection 
    {
        public static string config= "public static string ConnectionConfig = Server=student.permaviat.ru;Database=base1_ISP_22_4_5;uid=ISP_22_4_5;Password=mBmaR32aEsTb_;Trusted_Connection=True;";
        public static SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(config);
            connection.Open();
            return connection;
        }
        public static SqlDataReader ExecuteReader(string SQL,out SqlConnection connection)
        {
            connection = OpenConnection();
            SqlCommand command = new SqlCommand(SQL,connection);
            return command.ExecuteReader();
        }
        public static void CloseConnection(SqlConnection connection)
        {
            connection.Close();
            SqlConnection.ClearPool(connection);
        }
    }
}
