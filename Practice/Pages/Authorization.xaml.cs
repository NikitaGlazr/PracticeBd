using Practice.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

namespace Practice.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        private Help helper;

        public Authorization()
        {
            InitializeComponent();
            helper = new Help();
        }

        private async void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string enteredLogin = txtBoxLogin.Text;
            string enteredPassword = pswbPassword.Password;

            int roleId = helper.AuthenticateUser(enteredLogin, enteredPassword);

            if (roleId != 0)
            {
                switch (roleId)
                {
                    case 1:
                        MessageBox.Show("Добрый день! Администратор.");
                        NavigationService?.Navigate(new PageWorkers());
                        break;
                    case 2:
                        MessageBox.Show("Добрый день! Курьер.");
                        NavigationService?.Navigate(new Orders());
                        break;
                    case 3:
                        MessageBox.Show("Добрый день! Оператор колл-центра.");
                        NavigationService?.Navigate(new Orderitem());
                        break;
                    case 4:
                        MessageBox.Show("Добрый день! Менеджер по закупкам.");
                        NavigationService?.Navigate(new Money());//Заглушка
                        break;
                    case 5:
                        MessageBox.Show("Добрый день! Маркетолог.");
                        NavigationService?.Navigate(new Money());//Заглушка
                        break;
                    case 6:
                        MessageBox.Show("Добрый день! Бухгалтер.");
                        NavigationService?.Navigate(new Money());//Заглушка
                        break;
                    default:
                        MessageBox.Show("Неверный пароль или пользователя не существует.");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Неверный пароль или пользователя не существует.");
                SolidColorBrush errorColor = new SolidColorBrush(Colors.Red);
                txtBoxLogin.BorderBrush = errorColor;
                pswbPassword.BorderBrush = errorColor;

                await Task.Delay(500);

                txtBoxLogin.ClearValue(Border.BorderBrushProperty);
                pswbPassword.ClearValue(Border.BorderBrushProperty);
            }
        }
    }
}
