using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
namespace RecordsAplication.Classes
{
    public class Supply
    {
        public int Id { get; set; }
        public int IdManufacturer { get; set; }
        public int IdRecord { get; set; }
        public DateTime DateDelivery { get; set; }
        public int Count { get; set; }

        public static IEnumerable<Supply> AllSupply()
        {
            List<Supply> supples = new List<Supply>();
            MySqlConnection connection;
            MySqlDataReader recordQuery = DBConnection.ExecuteReader("SELECT * FROM Supply", out connection);
            while (recordQuery.Read())
            {
                supples.Add(new Supply()
                {
                    Id = recordQuery.GetInt32(0),
                    IdManufacturer = recordQuery.GetInt32(1),
                    IdRecord = recordQuery.GetInt32(2),
                    DateDelivery = recordQuery.GetDateTime(3),
                    Count = recordQuery.GetInt32(4) 
                });
            }
            DBConnection.CloseConnection(connection); 
            return supples;
        }

        public void Save(bool update = false)
        {
            MySqlConnection connection = null;

            try
            {
                string dateString = this.DateDelivery.ToString("yyyy-MM-dd HH:mm:ss");

                if (!update)
                {
                    string query = $"INSERT INTO Supply (IdManufacturer, IdRecord, DateDelivery, Count) " +
                                   $"VALUES ({this.IdManufacturer}, {this.IdRecord}, '{dateString}', {this.Count})";

                    DBConnection.ExecuteReader(query, out connection);
                    this.Id = GetLastInsertId();
                }
                else
                {
                    string query = $"UPDATE Supply " +
                                   $"SET " +
                                   $"IdManufacturer = {this.IdManufacturer}, " +
                                   $"IdRecord = {this.IdRecord}, " +
                                   $"DateDelivery = '{dateString}', " +
                                   $"Count = {this.Count} " +
                                   $"WHERE Id = {this.Id}";

                    DBConnection.ExecuteReader(query, out connection);
                }
            }
            finally
            {
                DBConnection.CloseConnection(connection);
            }
        }

        private int GetLastInsertId()
        {
            MySqlConnection connection;
            MySqlDataReader reader = DBConnection.ExecuteReader("SELECT LAST_INSERT_ID()", out connection);

            int lastId = 0;
            if (reader.Read())
            {
                lastId = reader.GetInt32(0);
            }

            DBConnection.CloseConnection(connection);
            return lastId;
        }

        public void Delete()
        {
            MySqlConnection connection;
            DBConnection.ExecuteReader($"DELETE FROM Supply WHERE Id = {this.Id}", out connection);
            DBConnection.CloseConnection(connection); 
        }
    }
}