using System.Windows.Controls;

namespace RecordsAplication.Pages.Manufacturer
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        Classes.Manufacturer Manufacturer;
        public Add(Classes.Manufacturer manufacturer = null)
        {
            InitializeComponent();
            if(manufacturer != null)
            {
                Manufacturer = manufacturer;
            }
        }

        private void AddManafacturer(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
