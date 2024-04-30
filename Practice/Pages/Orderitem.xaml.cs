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
    /// Логика взаимодействия для Orderitem.xaml
    /// </summary>
    public partial class Orderitem : Page
    {
        private Help helper;
        private int currentIndex = 0;

        public Orderitem()
        {
            InitializeComponent();
            helper = new Help();
            DisplayOrderItems();
        }

        private void DisplayOrderItems()
        {
            try
            {
                var orderItems = helper.GetOrderItems();

                DataGridOrderItems.ItemsSource = orderItems.Select(oi => new
                {
                    OrderId = oi.orderid,
                    ProductName = helper.GetProductNameByProductID(oi.productid),
                    Quantity = oi.quantity
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отображении заказов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

  

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridOrderItems.Items.Count > 0)
            {
                currentIndex = (currentIndex - 1 + DataGridOrderItems.Items.Count) % DataGridOrderItems.Items.Count;
                DataGridOrderItems.SelectedIndex = currentIndex;
            }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridOrderItems.Items.Count > 0)
            {
                currentIndex = (currentIndex + 1) % DataGridOrderItems.Items.Count;
                DataGridOrderItems.SelectedIndex = currentIndex;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
           
        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridOrderItems.SelectedItem != null)
            {
                try
                {
                    dynamic selectedItem = DataGridOrderItems.SelectedItem;
                    orderitem updatedOrderItem = new orderitem
                    {
                        orderitemid = selectedItem.OrderId,
                        productid = helper.GetProductIDByProductName(ProductNameTextBox.Text),
                        quantity = int.Parse(QuantityTextBox.Text)
                    };

                    helper.UpdateOrderItem(updatedOrderItem);

                    DisplayOrderItems();

                    MessageBox.Show("Успешно обновлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridOrderItems.SelectedItem != null)
            {
                try
                {
                    dynamic selectedItem = DataGridOrderItems.SelectedItem;
                    int orderItemId = selectedItem.OrderId;

                    helper.DeleteOrderItemById(orderItemId);

                    DisplayOrderItems();

                    MessageBox.Show("Успешно удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DataGridOrderItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (DataGridOrderItems.SelectedItem != null)
                {
                    dynamic selectedItem = DataGridOrderItems.SelectedItem;

                    ProductNameTextBox.Text = selectedItem.ProductName;
                    QuantityTextBox.Text = selectedItem.Quantity.ToString();
                    OrderIdTextBox.Text = selectedItem.OrderId.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обработке выбора: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
