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

namespace RecordsAplication.Pages.Records
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public IEnumerable<Classes.State> AllState = Classes.State.AllStates();
        public IEnumerable<Classes.Record> AllRecords = Classes.Record.AllRecords();
        public IEnumerable<Classes.Manufacturer> AllManufactures = Classes.Manufacturer.AllManufacturers();
        private bool isCreated = false;

        public List<Classes.Record> searchRecords;

        public Main()
        {
            InitializeComponent();
            searchRecords = AllRecords.ToList();
            isCreated = true;
            LoadAllRecord(AllRecords.ToList());
            LoadAllManufacture();
            LoadAllState();
        }

        public void LoadRecord()
        {
            AllRecords = Classes.Record.AllRecords();
            LoadAllRecord(AllRecords.ToList());
        }

        public void LoadAllRecord(List<Classes.Record> AllRecords)
        {
            recordsParent.Children.Clear();
            foreach (var record in AllRecords)
                recordsParent.Children.Add(new Records.Elements.Elements(record, this));
        }

        public void LoadAllManufacture()
        {
            tbManufacturer.Items.Clear();
            foreach (var manufacturer in AllManufactures)
                tbManufacturer.Items.Add(manufacturer.Name);
            tbManufacturer.Items.Add("Выберите ...");
            tbManufacturer.SelectedIndex = tbManufacturer.Items.Count - 1;
        }

        public void LoadAllState()
        {
            tbState.Items.Clear();
            foreach (var state in AllState)
                tbState.Items.Add(state.Name);
            tbState.Items.Add("Выберите ...");
            tbState.SelectedIndex = tbState.Items.Count - 1;
        }

        public void RecordsFilter()
        {
            List<Classes.Record> FilterRecords = AllRecords.ToList();
            if (tbManufacturer.SelectedIndex != tbManufacturer.Items.Count - 1 && tbManufacturer.SelectedItem != null)
            {
                var selectedManufacturer = AllManufactures.FirstOrDefault(y => y.Name == tbManufacturer.SelectedItem.ToString());
                if (selectedManufacturer != null)
                    FilterRecords = FilterRecords.Where(x => x.IdManufacturer == selectedManufacturer.Id).ToList();
            }
            if (tbState.SelectedIndex != tbState.Items.Count - 1 && tbState.SelectedItem != null)
            {
                var selectedState = AllState.FirstOrDefault(y => y.Name == tbState.SelectedItem.ToString());
                if (selectedState != null)
                    FilterRecords = FilterRecords.Where(x => x.Status == selectedState.Id).ToList();
            }

            if (!string.IsNullOrWhiteSpace(tbName.Text))
            {
                string searchText = tbName.Text.ToLower();

                if ("mono".Contains(searchText))
                {
                    FilterRecords = FilterRecords.Where(x => x.Format == 0).ToList();
                }
                else if ("stereo".Contains(searchText))
                {
                    FilterRecords = FilterRecords.Where(x => x.Format == 1).ToList();
                }
                else
                {
                    FilterRecords = FilterRecords.Where(x =>
                        x.Name.ToLower().Contains(searchText) ||
                        x.Year.ToString().Contains(searchText) ||
                        x.Price.ToString().Contains(searchText) ||
                        (x.Description != null && x.Description.ToLower().Contains(searchText))).ToList();
                }
            }

            searchRecords = FilterRecords;
            LoadAllRecord(FilterRecords);
        }

        private void FilterRecords(object sender, SelectionChangedEventArgs e)
        {
            if (isCreated)
                RecordsFilter();
        }

        private void SearchRecords(object sender, KeyEventArgs e)
        {
            RecordsFilter();
        }
        private void ClearFilters(object sender, RoutedEventArgs e)
        {
            tbName.Text = "";
            tbManufacturer.SelectedIndex = tbManufacturer.Items.Count - 1;
            tbState.SelectedIndex = tbState.Items.Count - 1;
            RecordsFilter();
        }
    }
}
