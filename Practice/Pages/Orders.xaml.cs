using Practice.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Practice.Pages
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        private Help help;
        private ObservableCollection<Order> orders;
        public Orders()
        {
            InitializeComponent();
            help = new Help();
            LViewProduct.MouseDoubleClick += LViewProduct_MouseLeftButtonDown;
            LoadOrder();
        }
        private void LViewProduct_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Order selectedOrder = LViewProduct.SelectedItem as Order;
            if (selectedOrder != null)
            {
                AddOrders addOrderPage = new AddOrders(selectedOrder);
                NavigationService.Navigate(addOrderPage);
            }
        }

        private void btnTakeAnOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Тестовая кнопка для взятия заказ...");
        }

        private void LoadOrder()
        {
            orders = new ObservableCollection<Order>(help.GetOrders());
            LViewProduct.ItemsSource = orders;
        }
    }
}
