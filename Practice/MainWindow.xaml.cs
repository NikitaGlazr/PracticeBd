using Practice.Pages;
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

namespace Practice
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frmMain.Navigated += frmMain_Navigated;
            frmMain.Navigate(new Authorization());

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (!(frmMain.Content is Authorization) && !(frmMain.Content is AddEmployee) && !(frmMain.Content is AddOrders))
            {
                frmMain.Navigate(new Authorization());
                btnBack.Visibility = Visibility.Hidden;
            }
            else if (frmMain.Content is AddEmployee)
            {
                frmMain.Navigate(new PageWorkers());
            }
            else if (frmMain.Content is AddOrders)
            {
                frmMain.Navigate(new Orders());
            }
        }

        private void frmMain_Navigated(object sender, NavigationEventArgs e)
        {
            if (frmMain.Content is Authorization)
            {
               btnBack.Visibility = Visibility.Hidden;
            }
            else
            {
                btnBack.Visibility = Visibility.Visible;
               
            }
        }

        private void frmMain_ContentRendered(object sender, EventArgs e)
        {

        }
    }
}
