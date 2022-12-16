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

namespace PP2022
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public int type, IDUser = 0;

        public void InProgramm_Click(object sender, RoutedEventArgs e)
        {
            bool proverka  = Authorizatiya(LoginText.Text, PasswordText.Text);
            if (!proverka)
            {
                MessageBox.Show("Введите корректный логин и пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MainMenu MainMenu = new MainMenu(type, IDUser);
                MainMenu.Show();
                Hide();
            }
        }

        public bool Authorizatiya(string q, string e)
        {
            var users = PP2022Entities.GetContext().Rabotnikis.FirstOrDefault(a => a.Login == q && a.Password == e);
            int[] dostupOffice = { 1, 2, 3, 4, 5, 6 };

            if (users != null)
            {
                type = (int)users.IDType;
                if (dostupOffice.Contains(type))
                {
                    IDUser = users.ID;
                    return true;
                }
                else return false;

            }
            else return false;
        }

        private void Password_MouseEnter(object sender, MouseEventArgs e)
        {
            if (PasswordText.Text == "Пароль") { PasswordText.Text = ""; }
        }

        private void Login_MouseEnter(object sender, MouseEventArgs e)
        {
            if (LoginText.Text == "Логин") { LoginText.Text = ""; }
        }

        private void Login_MouseLeave(object sender, MouseEventArgs e)
        {
            if (LoginText.Text == "") { LoginText.Text = "Логин"; }
        }

        private void Password_MouseLeave(object sender, MouseEventArgs e)
        {
            if (PasswordText.Text == "") { PasswordText.Text = "Пароль"; }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
