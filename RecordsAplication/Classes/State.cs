using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsAplication.Classes
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subname { get; set; }
        public string Description { get; set; }
        public static IEnumerable<State> AllStates()
        {
            List<State> allStates = new List<State>();
            MySqlConnection connection;
            MySqlDataReader stateQuery = DBConnection.ExecuteReader("SELECT * FROM State", out connection);
            while (stateQuery.Read())
            {
                allStates.Add(new State()
                {
                    Id = stateQuery.GetInt32(0),
                    Name = stateQuery.GetString(1),
                    Subname = stateQuery.GetString(2),
                    Description = stateQuery.GetString(3)
                });
            }
            DBConnection.CloseConnection(connection);
            return allStates;
        }
        public void Save(bool update = false)
        {
            MySqlConnection connection = null;
            if (update == false)
            {
                DBConnection.ExecuteReader(
                    "INSERT INTO State (Name, Subname, Description) " +
                    $"VALUES ('{this.Name}', '{this.Subname}', '{this.Description}')", out connection);

                this.Id = AllStates().Where(x => x.Name == this.Name &&
                                                      x.Subname == this.Subname &&
                                                      x.Description == this.Description).First().Id;
            }
            else
            {
                DBConnection.ExecuteReader(
                    "UPDATE State " +
                    "SET " +
                    $"Name = '{this.Name}', " +
                    $"Subname = '{this.Subname}', " +
                    $"Description = '{this.Description}' " +
                    $"WHERE Id = {this.Id}", out connection
                );
            }
            DBConnection.CloseConnection(connection);
        }
        public void Delete()
        {
            MySqlConnection connection;
            DBConnection.ExecuteReader($"DELETE FROM State WHERE Id = {this.Id}", out connection);
            DBConnection.CloseConnection(connection);
        }
    }
}
