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
        Classes.Record _Record;
        Main Main;
        public Elements(Classes.Record record,Main main)
        {
            InitializeComponent();

            this._Record = record;
            Main = main;
        }
    }
}
