using Practice.Entity;
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

namespace Practice.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddOrders.xaml
    /// </summary>
    public partial class AddOrders : Page
    {
        private Order selectedOrder;
        private Help help;
        public AddOrders(Order selectedOrder)
        {
            InitializeComponent();
            help = new Help();

            btnAdd_Click.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;
            btnClear.Visibility = Visibility.Collapsed;

            // Заполнение ComboBoxs
            cmbCustumers.ItemsSource = help.GetCustomersFullNameList();
            cmbEmployees.ItemsSource = help.GetCouriersFullNameList();
            cmbStatus.ItemsSource = help.GetUniqueOrderStatuses();
            cmbPaymentMethod.ItemsSource = help.GetUniquePaymentMethods();

            // Заполнение полей данными из объекта selectedOrder
            txtNumberOrder.Text = selectedOrder.orderid.ToString();
            dpDateOrder.Text = selectedOrder.orderdate.ToString();
            txtAdress.Text = selectedOrder.deliveryaddress;
            cmbPaymentMethod.SelectedItem = selectedOrder.paymentmethod;
            txtSumma.Text = selectedOrder.totalprice.ToString();
            cmbStatus.SelectedItem = selectedOrder.status;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnAdd_Click_Handler(object sender, RoutedEventArgs e)
        {

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtNumberOrder.Text = string.Empty;
            dpDateOrder.Text = string.Empty;
            txtAdress.Text = string.Empty;
            txtSumma.Text = string.Empty;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int orderId = int.Parse(txtNumberOrder.Text);
                help.DeleteOrder(orderId);
                MessageBox.Show("Заказ успешно удален");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при удалении заказа: " + ex.Message);
            }

        }

        private void btnToTake_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Тестовая кнопка для взятия заказ...");
        }
    }
}
