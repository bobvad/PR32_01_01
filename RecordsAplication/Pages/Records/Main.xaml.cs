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
            List<Classes.Record> FilterRecords = new List<Classes.Record>();
            if (tbManufacturer.SelectedIndex != tbManufacturer.Items.Count - 1)
                FilterRecords = AllRecords.Where(x => x.IdManufacturer ==
                    AllManufactures.Where(y => y.Name == tbManufacturer.SelectedItem.ToString()).First().Id).ToList();
            else
                FilterRecords = AllRecords.ToList();

            if (tbState.SelectedIndex != tbState.Items.Count - 1)
                FilterRecords = FilterRecords.Where(x => x.Id ==
                    AllState.Where(y => y.Name == tbState.SelectedItem.ToString()).First().Id).ToList();

            if (tbName.Text != "")
            {
                if ("mono".Contains(tbName.Text.ToLower()))
                {
                    FilterRecords = FilterRecords.Where(x => x.Format == 0).ToList();
                }
                else if ("stereo".Contains(tbName.Text.ToLower()))
                {
                    FilterRecords = FilterRecords.Where(x => x.Format == 1).ToList();
                }
                else
                {
                    FilterRecords = FilterRecords.Where(
                        x =>
                            x.Name.ToLower().Contains(tbName.Text.ToLower()) ||
                            x.Year.ToString().Contains(tbName.Text) ||
                            x.Price.ToString().Contains(tbName.Text) ||
                            x.Description.ToLower().Contains(tbName.Text.ToLower())).ToList();
                }
            }

            searchRecords.Clear();
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
    }
}
