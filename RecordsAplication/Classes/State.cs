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
        public static IEnumerable<State> AllStates ()
        {
            List<State> allStates = new List<State>();
            SqlConnection connection;
            SqlDataReader stateQuery = DBConnection.ExecuteReader("SELECT * FROM [dbo].[State]", out connection);
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
            SqlConnection connection = null;
            if (update == false)
            {
                DBConnection.ExecuteReader($"INSERT INTO" +
                    $"[dbo].[State](" + 
                    $"[Name]," +
                    $"[Subname]" +
                    $"[Description])" +
                    $"VALUES" + 
                    $"(N'{this.Name}'" +
                    $"N'{this.Subname}'" +
                    $"N'{this.Description}')", out connection);
                DBConnection.CloseConnection(connection);

                this.Id = AllStates().Where(x => x.Name == this.Name &&
                                                      this.Subname == this.Subname &&
                                                      this.Description == this.Description).First().Id;
                ;
            }
            else
            {
                DBConnection.ExecuteReader(
                    $"UPDATE" +
                         $"[dbo].[State]" +
                    $"SET" +
                       $"[Name] = N'{this.Name}'," +
                       $"[Subname] = N'{this.Subname}'," +
                       $"[Description] = N'{this.Description}'," +
                    $"WHERE" +
                          $"[Id] = N'{this.Id}',", out connection
                );
                DBConnection.CloseConnection(connection);
            }
        }
        public void Delete()
        {
            SqlConnection connection;
            DBConnection.ExecuteReader($"DELETE FROM [dbo].[State] WHERE [Id] = {this.Id}", out connection);
            DBConnection.CloseConnection(connection);
        }
    }
}
