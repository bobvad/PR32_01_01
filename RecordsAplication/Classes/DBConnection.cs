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
        public static DataTable Connection(string SQL)
        {
            DataTable dataTable = new DataTable("Datatable");
            SqlConnection sqlConnection = new SqlConnection("Server = student.permaviat.ru; Database = base1_ISP_22_4_5; uid = ISP_22_4_5; Password = mBmaR32aEsTb_; Trusted_Connection = True;");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = SQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
    }
}
