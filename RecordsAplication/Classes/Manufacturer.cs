using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsAplication.Classes
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public static IEnumerable<Manufacturer> AllManufacturers()
        {
            List<Manufacturer> allManufacturers = new List<Manufacturer>();
            MySqlConnection connection;
            MySqlDataReader manufacturersQuery = DBConnection.ExecuteReader("SELECT * FROM Manufacturer", out connection);
            while (manufacturersQuery.Read())
            {
                allManufacturers.Add(new Manufacturer()
                {
                    Id = manufacturersQuery.GetInt32(0),
                    Name = manufacturersQuery.GetString(1),
                    CountryCode = manufacturersQuery.GetInt32(2),
                    Phone = manufacturersQuery.GetString(3),
                    Email = manufacturersQuery.GetString(4)
                });
            }
            DBConnection.CloseConnection(connection);
            return allManufacturers;
        }

        public void Save(bool Update = false)
        {
            MySqlConnection connection = null;
            if (!Update) 
            {
                DBConnection.ExecuteReader(
                    $"INSERT INTO Manufacturer (" +
                    $"Name, " +
                    $"CountryCode, " +
                    $"Phone, " +
                    $"Email) " +
                    $"VALUES (" +
                    $"'{this.Name}', " +
                    $"{this.CountryCode}, " +
                    $"'{this.Phone}', " +
                    $"'{this.Email}')", out connection);
                this.Id = AllManufacturers().OrderByDescending(x => x.Id).First().Id;
            }
            else
            {
                DBConnection.ExecuteReader(
                    $"UPDATE Manufacturer " +
                    $"SET " +
                    $"Name = '{this.Name}', " +
                    $"CountryCode = {this.CountryCode}, " +
                    $"Phone = '{this.Phone}', " +
                    $"Email = '{this.Email}' " +
                    $"WHERE Id = {this.Id}", out connection);
            }
            DBConnection.CloseConnection(connection);
        }

        public void Delete()
        {
            MySqlConnection connection;
            DBConnection.ExecuteReader(
                $"DELETE FROM Manufacturer WHERE Id = {this.Id}", out connection);
            DBConnection.CloseConnection(connection);
        }
    }
}
