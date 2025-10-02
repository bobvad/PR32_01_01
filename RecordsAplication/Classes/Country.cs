using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace RecordsAplication.Classes
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public static IEnumerable<Country> AllCountrys()
        {
            List<Country> allCountrys = new List<Country>();
            MySqlConnection connection;
            MySqlDataReader countrysQuery = DBConnection.ExecuteReader("SELECT * FROM Country",out connection);
            while (countrysQuery.Read())
            {
                allCountrys.Add(new Country()
                {
                    Id = countrysQuery.GetInt32(0),
                    Name = countrysQuery.GetString(1)
                });
            }
            DBConnection.CloseConnection(connection);
            return allCountrys;
        }
    }
}
