using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsAplication.Classes
{
    public class Supply
    {
            public int Id { get; set; }
            public int IdManufacturer { get; set; }
            public int IdRecord { get; set; }
            public DateTime DateDelivery { get; set; }
            public int Count { get; set; }
            public static IEnumerable<Supply> AllSupples()
            {
                List<Supply> supples = new List<Supply>();
                SqlConnection connection;
                SqlDataReader recordQuery = DBConnection.ExecuteReader("SELECT * FROM [dbo].[Supply]", out connection);
                while (recordQuery.Read())
                {
                    supples.Add(new Supply()
                    {
                        Id = recordQuery.GetInt32(0),
                        IdManufacturer = recordQuery.GetInt32(1),
                        IdRecord = recordQuery.GetInt32(2),
                        DateDelivery = recordQuery.GetDateTime(3),
                        Count = Convert.ToInt32(4)
                    });
                }

                return supples;
            }
            public void Save(bool update = false)
            {
                SqlConnection connection;
                if (!update)
                {
                    Classes.DBConnection.ExecuteReader(
                        "INSERT INTO [dbo].[Supply] ([IdManufacturer], [IdRecord], [DateDelivery], [Count]) " +
                        $"VALUES ({this.IdManufacturer}, {this.IdRecord}, '{this.DateDelivery}', {this.Count})",out connection);

                    this.Id = Supply.AllSupples().Where(
                        x => x.IdManufacturer == this.IdManufacturer &&
                             x.IdRecord == this.IdRecord &&
                             x.DateDelivery == this.DateDelivery &&
                             x.Count == this.Count).First().Id;
                }
                else
                {
                    Classes.DBConnection.ExecuteReader(
                        "UPDATE [dbo].[Supply] " +
                        "SET " +
                        $"[IdManufacturer] = {this.IdManufacturer}, " +
                        $"[IdRecord] = {this.IdRecord}, " +
                        $"[DateDelivery] = '{this.DateDelivery}', " +
                        $"[Count] = {this.Count} " +
                        $"WHERE [Id] = {this.Id}",out connection);
                }
            }

            public void Delete()
            {
                SqlConnection connection;
                Classes.DBConnection.ExecuteReader($"DELETE FROM [dbo].[Supply] WHERE [Id] = {this.Id}", out connection);
            }
        }
    }
