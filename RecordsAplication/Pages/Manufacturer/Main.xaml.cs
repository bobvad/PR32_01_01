using System.Collections;
using System.Collections.Generic;
using System.Windows.Controls;

namespace RecordsAplication.Pages.Manufacturer
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public IEnumerable<Classes.Manufacturer> AllManufacturer = Classes.Manufacturer.AllManufacturers();
        public Main()
        {
            InitializeComponent();
            foreach (Classes.Manufacturer manufacturer in AllManufacturer)
                manufacterParent.Children.Add(new Elements.Manufacturer(this,manufacturer));
        }
    }
}
