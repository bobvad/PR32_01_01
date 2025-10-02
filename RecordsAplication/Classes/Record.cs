using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace RecordsAplication.Classes
{
    public class Record
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int Format { get; set; }
        public int Size { get; set; }
        public int IdManufacturer { get; set; }
        public float Price { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }

        public static List<Record> AllRecords()
        {
            List<Record> records = new List<Record>();
            MySqlConnection connection;
            MySqlDataReader recordQuery = DBConnection.ExecuteReader("SELECT * FROM Record", out connection);
            while (recordQuery.Read())
            {
                records.Add(new Record()
                {
                    Id = recordQuery.GetInt32(0),
                    Name = recordQuery.GetString(1),
                    Year = recordQuery.GetInt32(2),
                    Format = recordQuery.GetInt32(3),
                    Size = recordQuery.GetInt32(4),
                    IdManufacturer = recordQuery.GetInt32(5),
                    Price = recordQuery.GetFloat(6),
                    Status = recordQuery.GetInt32(7),
                    Description = recordQuery.GetString(8)
                });
            }
            DBConnection.CloseConnection(connection);
            return records;
        }

        public void Save(bool update = false)
        {
            string priceString = this.Price.ToString().Replace(",", ".");
            MySqlConnection connection = null;
            if (!update)
            {
                DBConnection.ExecuteReader(
                    "INSERT INTO Record (" +
                    "Name, Year, Format, Size, IdManufacturer, Price, Status, Description) " +
                    "VALUES (" +
                    $"'{this.Name}', " +
                    $"{this.Year}, " +
                    $"{this.Format}, " +
                    $"{this.Size}, " +
                    $"{this.IdManufacturer}, " +
                    $"{priceString}, " +
                    $"{this.Status}, " +
                    $"'{this.Description}')", out connection);

                this.Id = AllRecords().OrderByDescending(x => x.Id).First().Id;
            }
            else
            {
                DBConnection.ExecuteReader(
                    "UPDATE Record SET " +
                    $"Name = '{this.Name}', " +
                    $"Year = {this.Year}, " +
                    $"Format = {this.Format}, " +
                    $"Size = {this.Size}, " +
                    $"IdManufacturer = {this.IdManufacturer}, " +
                    $"Price = {priceString}, " +
                    $"Status = {this.Status}, " +
                    $"Description = '{this.Description}' " +
                    $"WHERE Id = {this.Id}", out connection);
            }
            DBConnection.CloseConnection(connection);
        }

        public void Delete()
        {
            MySqlConnection connection;
            DBConnection.ExecuteReader($"DELETE FROM Record WHERE Id = {this.Id}", out connection);
            DBConnection.CloseConnection(connection);
        }
        public static void ExportToExcel(string filePath, List<Record> records)
        {
            try
            {
                using (var writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine("ID;Название;Год выпуска;Формат;Размер;Производитель;Цена;Состояние;Описание");

                    foreach (var record in records)
                    {
                        writer.WriteLine(
                            $"{record.Id};" +
                            $"{EscapeCsv(record.Name)};" +
                            $"{record.Year};" +
                            $"{record.Format};" +
                            $"{record.Size};" +
                            $"{GetManufacturerName(record.IdManufacturer)};" +
                            $"{record.Price.ToString().Replace(",", ".")};" +
                            $"{GetStatusName(record.Status)};" +
                            $"{EscapeCsv(record.Description)}"
                        );
                    }
                }

                MessageBox.Show($"Экспорт завершен!\nФайл: {filePath}", "Успех",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static string EscapeCsv(string field)
        {
            if (string.IsNullOrEmpty(field)) return "";
            if (field.Contains(";") || field.Contains("\"") || field.Contains("\n"))
            {
                return $"\"{field.Replace("\"", "\"\"")}\"";
            }
            return field;
        }

        private static string GetManufacturerName(int idManufacturer)
        {
            var manufacturer = Classes.Manufacturer.AllManufacturers()
                .FirstOrDefault(m => m.Id == idManufacturer);

            return manufacturer != null ? manufacturer.Name : $"Производитель {idManufacturer}";
        }

        private static string GetStatusName(int status)
        {
            var state = Classes.State.AllStates()
         .FirstOrDefault(s => s.Id == status);
            return state != null ? state.Name : (status == 0 ? "Новая" : "Б/у");
        }
    }
}