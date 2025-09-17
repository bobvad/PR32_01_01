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

namespace RecordsAplication.Pages.State.Elements
{
    /// <summary>
    /// Логика взаимодействия для State.xaml
    /// </summary>
    public partial class State : UserControl
    {
        Main Main;
        Classes.State _State;

        public State(Main main, Classes.State state)
        {
            InitializeComponent();
            Main = main;
            _State = state;
            tbName.Text = state.Name;
            tbSurname.Text = state.Subname;
            tbDescription.Text = state.Description;
        }

        private void EditState(object sender, RoutedEventArgs e)
            => MainWindow.mainWindow.OpenPage(new Add(_State));

        private void DeleteState(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Удалить состояние: {_State.Name}?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                IEnumerable<Classes.Record> AllRecord = Classes.Record.AllRecords();
                if (AllRecord.Where(x => x.IdState == _State.Id).Count() > 0)
                {
                    MessageBox.Show($"Состояние {_State.Name} невозможно удалить. Для начала удалите зависимости.", "Уведомление");
                }
                else
                {
                    this._State.Delete();
                    if (Main.Parent is Panel panel)
                    {
                        panel.Children.Remove(this);
                    }
                    MessageBox.Show($"Состояние {_State.Name} успешно удалено.", "Уведомление");
                }
            }
        }
    }
}
