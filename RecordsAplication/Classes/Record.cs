using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsAplication.Classes
{
    public class Record
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public int View { get; set; }
            public int Format { get; set; }
            public int Size { get; set; }
            public int IdManufacturer { get; set; }
            public float Price { get; set; }
            public int Status { get; set; }
            public string Description { get; set; }

            public static List<Record> AllRecords()
            {
                List<Record> records = new List<Record>();
                SqlConnection connection;
                SqlDataReader manufacturersQuery = DBConnection.ExecuteReader("SELECT * FROM [dbo].[Record]", out connection);
                while (manufacturersQuery.Read())
                {
                    records.Add(new Record()
                    {
                        Id = manufacturersQuery.GetInt32(0),
                        Name = manufacturersQuery.GetString(1),
                        View = manufacturersQuery.GetInt32(2),
                        Format = manufacturersQuery.GetInt32(3),
                        Size = manufacturersQuery.GetInt32(4),
                        IdManufacturer = manufacturersQuery.GetInt32(5),
                        Price = manufacturersQuery.GetInt32(6),
                        Status = manufacturersQuery.GetInt32(7),
                        Description = manufacturersQuery.GetString(8)
                    });
                }
                DBConnection.CloseConnection(connection);
                return records;
            }

            public void Save(bool update = false)
            {
                string priceString = this.Price.ToString().Replace(",", ".");
                SqlConnection connection = null;
                if (update == false)
                {
                    DBConnection.ExecuteReader(
                        "INSERT INTO [dbo].[Record] (" +
                        "[Name], [View], [Format], [Size], [ManufacturerId], [Price], [Status], [Description]) " +
                        "VALUES (" +
                        $"'{this.Name}', " +
                        $"{this.View}, " +
                        $"{this.Format}, " +
                        $"{this.Size}, " +
                        $"{this.IdManufacturer}, " +
                        $"{priceString}, " +
                        $"{this.Status}, " +
                        $"'{this.Description}')",out connection);

                    this.Id = AllRecords().Where(
                        x => x.Name == this.Name &&
                        x.View == this.View &&
                        x.Format == this.Format &&
                        x.Size == this.Size &&
                        x.IdManufacturer == this.IdManufacturer &&
                        x.Price == this.Price &&
                        x.Status == this.Status &&
                        x.Description == this.Description).First().Id;
                }
                else
                {
                    DBConnection.ExecuteReader(
                        "UPDATE [dbo].[Record] SET " +
                        $"[Name] = '{this.Name}', " +
                        $"[View] = {this.View}, " +
                        $"[Format] = {this.Format}, " +
                        $"[Size] = {this.Size}, " +
                        $"[ManufacturerId] = {this.IdManufacturer}, " +
                        $"[Price] = {priceString}, " +
                        $"[Status] = {this.Status}, " +
                        $"[Description] = '{this.Description}' " +
                        $"WHERE [Id] = {this.Id}",out connection);
                }
            }
            public void Delete()
            {
                SqlConnection connection;
                Classes.DBConnection.ExecuteReader($"DELETE FROM [dbo].[Record] WHERE [Id] = {this.Id}",out connection);
                DBConnection.CloseConnection(connection);
            }
        }
    }
