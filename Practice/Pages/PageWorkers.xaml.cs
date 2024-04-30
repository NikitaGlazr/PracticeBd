using Practice.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для PageWorkers.xaml
    /// </summary>
    public partial class PageWorkers : Page
    {
        private Help help;
        ObservableCollection<employee> employees;

        public PageWorkers()
        {
            InitializeComponent();
            help = new Help();
            LViewProduct.MouseDoubleClick += LViewProduct_MouseLeftButtonDown;
            LoadEmployees();
        }
        private void LViewProduct_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            employee selectedEmployee = (employee)LViewProduct.SelectedItem;

            AddEmployee addEmployeesPage = new AddEmployee(selectedEmployee);
            NavigationService.Navigate(addEmployeesPage);
        }


        private void LoadEmployees()
        {
            employees = new ObservableCollection<employee>(help.GetEmployees());
            LViewProduct.ItemsSource = employees;
        }


        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            employee selectedEmployee = (employee)LViewProduct.SelectedItem;
            AddEmployee addEmployeesPage = new AddEmployee(selectedEmployee);

            NavigationService.Navigate(addEmployeesPage);
        }
    }
}
