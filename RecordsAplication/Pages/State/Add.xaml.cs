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

namespace RecordsAplication.Pages.State
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        Classes.State ChangeState;
        public Add(Classes.State state = null)
        {
            InitializeComponent();
            if (state != null)
            {
                ChangeState = state;
                tbName.Text = state.Name;
                tbSurname.Text = state.Subname;
                tbDescription.Text = state.Description;
                BthAdd.Content = "Изменить";
            }
        }

        private void AddState(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(tbName.Text))
                {
                    if (!string.IsNullOrEmpty(tbSurname.Text))
                    {
                        if (ChangeState == null)
                        {
                            Classes.State newState = new Classes.State()
                            {
                                Name = tbName.Text,
                                Subname = tbSurname.Text,
                                Description = tbDescription.Text,
                            };
                            newState.Save();
                            MessageBox.Show($"Состояние {newState.Name} успешно добавлено.", "Уведомление");
                            MainWindow.mainWindow.OpenPage(new Add(newState));
                            MainWindow.mainWindow.OpenPage(new Pages.State.Main());
                        }
                        else
                        {
                            ChangeState.Name = tbName.Text;
                            ChangeState.Subname = tbSurname.Text;
                            ChangeState.Description = tbDescription.Text;
                            ChangeState.Save(true);
                            MessageBox.Show($"Состояние {ChangeState.Name} успешно изменено.", "Уведомление");
                            MainWindow.mainWindow.OpenPage(new Pages.State.Main());
                        }
                    }
                    else
                        MessageBox.Show("Пожалуйста, укажите сокращённое наименование состояния", "Уведомление");
                }
                else
                    MessageBox.Show("Пожалуйста, укажите наименование состояния", "Уведомление");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

