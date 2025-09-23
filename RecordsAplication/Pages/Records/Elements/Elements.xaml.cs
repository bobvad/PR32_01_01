using RecordsAplication.Classes;
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

namespace RecordsAplication.Pages.Records.Elements
{
    /// <summary>
    /// Логика взаимодействия для Elements.xaml
    /// </summary>
    public partial class Elements : UserControl
    {
        private IEnumerable<Classes.State> AllState = Classes.State.AllStates();
        private Classes.Record _Record;
        private Main Main;
        public Elements(Classes.Record record, Main main)
        {
            InitializeComponent();
            IEnumerable<Classes.Manufacturer> AllManufacturer = Classes.Manufacturer.AllManufacturers();
            this._Record = record;
            Main = main;
            tbName.Text = record.Name;
            tbYear.Text = record.Year.ToString();
            tbFormat.Text = record.Format == 0 ? "МОНО" : "Стерео";
            switch (record.Size)
            {
                case 0:
                    tbSize.Text = "7 дюймов";
                    break;
                case 1:
                    tbSize.Text = "10 дюймов";
                    break;
                case 2:
                    tbSize.Text = "12 дюймов";
                    break;
                case 3:
                    tbSize.Text = "Иной";
                    break;
            }
            tbManufacturer.Text = AllManufacturer.Where(x => x.Id == record.IdManufacturer).First().Name;
            tbPrice.Text = record.Price.ToString();
            tbState.Text = AllState.Where(x => x.Id == record.Id).First().Name;
            tbDescription.Text = record.Description;
        }
        private void EditRecord(object sender, RoutedEventArgs e)
        {
            MainWindow.mainWindow.frame.Navigate(new Add(_Record));
        }
        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Удалить виниловую пластинку: {this._Record.Name}?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                IEnumerable<Classes.Supply> AllSupply = Classes.Supply.AllSupply();
                if (AllSupply.Where(x => x.IdRecord == _Record.Id).Count() > 0)
                {
                    MessageBox.Show($"Виниловую пластинку {this._Record.Name} невозможно удалить. Для начала удалите зависимости!", "Уведомление");
                }
                else
                {
                    this._Record.Delete();
                    Main.recordsParent.Children.Remove(this);
                    MessageBox.Show($"Пластинка {this._Record.Name} успешно удалена!", "Уведомление");
                }
            }
        }
    }
 }
