using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecordsAplication.Pages.Supply
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        IEnumerable<Classes.Manufacturer> AllManufacturers = Classes.Manufacturer.AllManufacturers();
        IEnumerable<Classes.Record> AllRecords = Classes.Record.AllRecords();
        Classes.Supply changeSupply;
        public Add(Classes.Supply changeSupply = null)
        {
            InitializeComponent();
            foreach (var manufacturer in AllManufacturers)
            {
               tbManufacturer.Items.Add(manufacturer.Name);
            }
            if (tbManufacturer.Items.Count > 0)
            {
               tbManufacturer.SelectedIndex = 0;
            }
            foreach (var record in AllRecords)
            {
                 tbRecord.Items.Add(record.Name);
            }
            if (tbRecord.Items.Count > 0)
            {
                tbRecord.SelectedIndex = 0;
            }
            if (changeSupply != null)
            {
                this.changeSupply = changeSupply;
                tbManufacturer.SelectedIndex = AllManufacturers.ToList().FindIndex(x => x.Id == changeSupply.IdManufacturer);
                tbRecord.SelectedIndex = AllRecords.ToList().FindIndex(x => x.Id == changeSupply.IdRecord);
                tbCount.Text = changeSupply.Count.ToString();
                DateTime dt = new DateTime();
                DateTime.TryParse(changeSupply.DateDelivery.ToString(), out dt);
                tbDateDelivery.SelectedDate = dt;
                addBtn.Content = "Изменить";
            }
        }

        private void AddSupply(object sender, RoutedEventArgs e)
        {
            DateTime dt = new DateTime();
            if (DateTime.TryParse(tbDateDelivery.SelectedDate.ToString(), out dt))
            {
               
                if (!string.IsNullOrEmpty(tbCount.Text))
                {
                    if (changeSupply == null)
                    {
                        Classes.Supply newSupply = new Classes.Supply();
                       newSupply.IdManufacturer = AllManufacturers.Where(x => x.Name == tbManufacturer.SelectedItem.ToString()).First().Id;
                        newSupply.IdRecord = AllRecords.Where(x => x.Name == tbRecord.SelectedItem.ToString()).First().Id;
                        newSupply.Count = Convert.ToInt32(tbCount.Text);
                        newSupply.DateDelivery = Convert.ToDateTime(tbDateDelivery.SelectedDate.ToString());
                        newSupply.Save();
                        MessageBox.Show($"Поставка №{newSupply.Id} успешно добавлена.", "Уведомление");
                        MainWindow mainWindow = App.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                        mainWindow.OpenPage(new Pages.Supply.Add(newSupply));
                        MainWindow.mainWindow.OpenPage(new Pages.Supply.Main());
                    }
                    else
                    {
                        changeSupply.IdManufacturer = AllManufacturers.Where(x => x.Name == tbManufacturer.SelectedItem.ToString()).First().Id;
                        changeSupply.IdRecord = AllRecords.Where(x => x.Name == tbRecord.SelectedItem.ToString()).First().Id;
                        changeSupply.Count = Convert.ToInt32(tbCount.Text);
                        changeSupply.DateDelivery = Convert.ToDateTime(tbDateDelivery.SelectedDate.ToString());
                        changeSupply.Save(true);
                        MessageBox.Show($"Поставка №{changeSupply.Id} успешно изменена.", "Уведомление");
                        MainWindow.mainWindow.OpenPage(new Pages.Supply.Main());
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, укажите количество поставки.", "Предупреждение");
                    MessageBox.Show("Пожалуйста, укажите дату поставки.", "Предупреждение");
                }
            }
        }
        private void tbPreviewNumber(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.Text, 0);
        }
        public string correctDate(string DateValue)
        {
            DateTime dt = new DateTime();
            DateTime.TryParse(DateValue, out dt);
            return dt.Year + "-" + dt.Month + "-" + dt.Day;
        }
    }
}
