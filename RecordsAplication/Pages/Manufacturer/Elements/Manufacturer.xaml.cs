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

namespace RecordsAplication.Pages.Manufacturer.Elements
{
    /// <summary>
    /// Логика взаимодействия для Manufacturer.xaml
    /// </summary>
    public partial class Manufacturer : UserControl
    {
        IEnumerable<Classes.Country> AllCountrys = Country.AllCountrys();
        Pages.Manufacturer.Main Main;
        Classes.Manufacturer _Manufacturer;
        public Manufacturer(Pages.Manufacturer.Main main, Classes.Manufacturer manufacturer)
        {
            InitializeComponent();
            Main = main;
            _Manufacturer = manufacturer;
            tbName.Text = _Manufacturer.Name;
            tbCountry.Text = AllCountrys.Where(x => x.Id == _Manufacturer.CountryCode).First().Name;
            tbPhone.Text = _Manufacturer.Phone;
            tbEmail.Text = _Manufacturer.Email;
        }

        private void EditState(object sender, RoutedEventArgs e)
            => MainWindow.mainWindow.OpenPage(new Pages.Manufacturer.Add(_Manufacturer));

        private void DeleteState(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Удалить поставщика {_Manufacturer.Name}?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (Classes.Record.AllRecords().Where(s => s.IdManufacturer == _Manufacturer.Id).Count() > 0)
                {
                    MessageBox.Show($"Поставщика {_Manufacturer.Name} невозможно удалить. Для начала удалите зависимости.", "Ошибка");
                }
                else
                {
                    _Manufacturer.Delete();
                   Main.manufacterParent.Children.Remove(this);
                    MessageBox.Show("Поставщик успешно удален.", "Уведомление");
                }
            }
        }
    }
}
