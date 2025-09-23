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
            SqlConnection connection;
            SqlDataReader manufacturersQuery = DBConnection.ExecuteReader("SELECT * FROM [dbo].[Manufacturer]", out connection);
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
            SqlConnection connection = null;
            if (Update = false)
            {   
                DBConnection.ExecuteReader($"INSERT INTO" +
                  $"[dbo].[Manufacturer](" +
                  $"[Name]," +
                  $"[CountryCode]" +
                  $"[Phone]," +
                  $"[Email])" +
                  $"VALUES" +
                  $"(N'{this.Name}'" +
                  $"N'{this.CountryCode}'" +
                  $"N'{this.Phone}'" +
                  $"N'{this.Email}')", out connection);
                  this.Id = AllManufacturers().Where(x => x.Name == Name &&
                                                          x.CountryCode == CountryCode &&
                                                          x.Phone == Phone &&
                                                          x.Email == Email).First().Id;
            }
            else
            {
                DBConnection.ExecuteReader(
                   $"UPDATE" +
                        $"[dbo].[Manufacturer]" +
                   $"SET" +
                      $"[Name] = N'{this.Name}'," +
                      $"[CountryCode] = N'{this.CountryCode}'," +
                      $"[Phone] = N'{this.Phone}'," +
                      $"[Email] = N'{this.Email}'," +
                   $"WHERE" +
                         $"[Id] = N'{this.Id}',", out connection
               );
            }
            DBConnection.CloseConnection(connection);
        }
        public void Delete()
        {
            SqlConnection connection;
            DBConnection.ExecuteReader(
                $"DELETE FROM [dbo].[Manufacturer] WHERE [Id] = {this.Id}", out connection
                );
            DBConnection.CloseConnection(connection);
        }
    }
}
