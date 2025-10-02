using RecordsAplication.Classes;
using RecordsAplication.Pages.Manufacturer.Elements;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RecordsAplication.Pages.Manufacturer
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        IEnumerable<Classes.Country> AllCountrys = Country.AllCountrys();
        Classes.Manufacturer ChangeManufacturer;
        public Add(Classes.Manufacturer manufacturer = null)
        {
            InitializeComponent();
            foreach (Classes.Country Country in AllCountrys)
            {
                tbCountry.Items.Add(Country.Name);
            }
            if(AllCountrys.Count() > 0)
                tbCountry.SelectedIndex = 0;

            if (manufacturer != null)
            {
              ChangeManufacturer = manufacturer;
              tbName.Text = manufacturer.Name;
              tbCountry.SelectedIndex = AllCountrys.ToList().FindIndex(x => x.Id == manufacturer.CountryCode);
              tbPhone.Text = manufacturer.Phone;
              tbEmail.Text = manufacturer.Email;
              BthAdd.Content = "Изменить";
            }
        }

        private void AddManafacturer(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Пожалуйста, укажите наименование поставщика", "Уведомление");
                return;
            }

            if (string.IsNullOrEmpty(tbPhone.Text))
            {
                MessageBox.Show("Пожалуйста, укажите телефон поставщика", "Уведомление");
                return;
            }

            if (string.IsNullOrEmpty(tbEmail.Text))
            {
                MessageBox.Show("Пожалуйста, укажите почту поставщика", "Уведомление");
                return;
            }

            if (!CorrectData(tbEmail.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                MessageBox.Show("Пожалуйста, укажите почту поставщика в корректном формате", "Уведомление");
                return;
            }

            if (!CorrectData(tbPhone.Text, @"^8[0-9]{10}$"))
            {
                MessageBox.Show("Пожалуйста, укажите номер телефона поставщика в формате 8XXXXXXXXXX", "Уведомление");
                return;
            }
            if (ChangeManufacturer == null)
            {
                ChangeManufacturer = new Classes.Manufacturer()
                {
                    Name = tbName.Text,
                    Phone = tbPhone.Text,
                    Email = tbEmail.Text,
                    CountryCode = AllCountrys.Where(x => x.Name == tbCountry.SelectedItem.ToString()).First().Id,
                };
                ChangeManufacturer.Save();
                MessageBox.Show($"Поставщик {ChangeManufacturer.Name} успешно добавлен.", "Уведомление");
                MainWindow.mainWindow.OpenPage(new Add(ChangeManufacturer));
                MainWindow.mainWindow.OpenPage(new Pages.Manufacturer.Main());
            }
            else
            {
                ChangeManufacturer.Name = tbName.Text;
                ChangeManufacturer.Phone = tbPhone.Text;
                ChangeManufacturer.Email = tbEmail.Text;
                ChangeManufacturer.CountryCode = AllCountrys.Where(x => x.Name == tbCountry.SelectedItem.ToString()).First().Id;
                ChangeManufacturer.Save(true);
                MessageBox.Show($"Поставщик {ChangeManufacturer.Name} успешно изменён.", "Уведомление");
                MainWindow.mainWindow.OpenPage(new Pages.Manufacturer.Main());
            }
        }
        public bool CorrectData(string value, string sRegex)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            try
            {
                return Regex.IsMatch(value, sRegex);
            }
            catch
            {
                return false;
            }
        }
        private void tbPreviewNumber(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.Text, 0));
        }

        private void tbPreviewNumber(object sender, KeyEventArgs e)
        {

        }
    }
}
