using Practice.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Page
    {
        private employee SelectedEmployee;
        private Help help;

        public AddEmployee(employee selectedEmployee)
        {
            InitializeComponent();
            SelectedEmployee = selectedEmployee;
            help = new Help();
           
            FillEmployeeData();

            if (selectedEmployee == null)
            {
                btnAdd_Click.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnAdd_Click.Visibility = Visibility.Collapsed;
                btnSave.Visibility = Visibility.Visible;
            }
        }

        private void FillEmployeeData()
        {
            if (SelectedEmployee != null)
            {
                txtLastName.Text = SelectedEmployee.lastname;
                txtFirstName.Text = SelectedEmployee.firstname;
                txtMiddleName.Text = SelectedEmployee.middlename;
                txtPassportSeries.Text = SelectedEmployee.passportseries;
                txtPassportNumber.Text = SelectedEmployee.passportnumber;
                txtEmail.Text = SelectedEmployee.email;
                cmbProfession.SelectedItem = SelectedEmployee.position;
                txtworkingHours.Text = SelectedEmployee.schedule;
                txtAbility.Text = SelectedEmployee.personalinfo;
                txtSalary.Text = Convert.ToString(SelectedEmployee.salary);
                txtWorkPhone.Text = SelectedEmployee.phonework;
                txtPersonalPhone.Text = SelectedEmployee.phonepersonal;
                txtPersonalInfo.Text = SelectedEmployee.personalinfo;
                txtDocuments.Text = SelectedEmployee.documents;
                txtWorkHistory.Text = SelectedEmployee.workhistory;
                txtSpecialFeatures.Text = SelectedEmployee.specialfeatures;
                //txtLogin.Text = SelectedEmployee.login;
                txtLogin.IsReadOnly = true; // Заблокировать элемент от редактирования

               // txtpassword.Text = SelectedEmployee.password;
                txtpassword.IsReadOnly = true; // Заблокировать элемент от редактирования
            }
            FillComboBoxWithRoles();
        }

        private void FillComboBoxWithRoles()
        {
            Help help = new Help();
            List<employeerole> employeeRoles = help.GetEmployeeRoles();

            cmbProfession.Items.Clear();
            foreach (var role in employeeRoles)
            {
                cmbProfession.Items.Add(role.rolename);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtLastName.Text = "";
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtPassportSeries.Text = "";
            txtPassportNumber.Text = "";
            txtEmail.Text = "";
            txtworkingHours.Text = "";
            txtAbility.Text = "";
            txtSalary.Text = "";
            txtWorkPhone.Text = "";
            txtPersonalPhone.Text = "";
            txtPersonalInfo.Text = "";
            txtDocuments.Text = "";
            txtWorkHistory.Text = "";
            txtSpecialFeatures.Text = "";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            UpdateEmployee();
        }

        private void btnAdd_Click_Handler(object sender, RoutedEventArgs e)
        {
            try
            {
                employee newEmployee = new employee
                {
                    lastname = txtLastName.Text,
                    firstname = txtFirstName.Text,
                    middlename = txtMiddleName.Text,
                    passportseries = txtPassportSeries.Text,
                    passportnumber = txtPassportNumber.Text,
                    email = txtEmail.Text,
                    position = cmbProfession.SelectedItem.ToString(),
                    schedule = txtworkingHours.Text,
                    personalinfo = txtPersonalInfo.Text,
                    salary = Convert.ToDecimal(txtSalary.Text),
                    phonework = txtWorkPhone.Text,
                    phonepersonal = txtPersonalPhone.Text,
                    skills = txtAbility.Text,
                    documents = txtDocuments.Text,
                    workhistory = txtWorkHistory.Text,
                    specialfeatures = txtSpecialFeatures.Text
                };

                try
                {
                    help.AddNewEmployee(newEmployee);
                }
                catch (DbEntityValidationException ex)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var failure in ex.EntityValidationErrors)
                    {
                        sb.AppendFormat("{0} неудачная проверка\n", failure.Entry.Entity.GetType());
                        foreach (var error in failure.ValidationErrors)
                        {
                            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                            sb.AppendLine();
                        }
                    }

                    MessageBox.Show(sb.ToString(), "Ошибки проверки", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении нового сотрудника: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

      
        private void UpdateEmployee()
        {
            try
            {
                if (SelectedEmployee != null)
                {
                    SelectedEmployee.lastname = txtLastName.Text;
                    SelectedEmployee.firstname = txtFirstName.Text;
                    SelectedEmployee.middlename = txtMiddleName.Text;
                    SelectedEmployee.passportseries = txtPassportSeries.Text;
                    SelectedEmployee.passportnumber = txtPassportNumber.Text;
                    SelectedEmployee.email = txtEmail.Text;
                    SelectedEmployee.position = cmbProfession.SelectedItem.ToString();
                    SelectedEmployee.schedule = txtworkingHours.Text;
                    SelectedEmployee.personalinfo = txtPersonalInfo.Text;
                    SelectedEmployee.salary = Convert.ToDecimal(txtSalary.Text);
                    SelectedEmployee.phonework = txtWorkPhone.Text;
                    SelectedEmployee.phonepersonal = txtPersonalPhone.Text;
                    SelectedEmployee.skills = txtAbility.Text;
                    SelectedEmployee.documents = txtDocuments.Text;
                    SelectedEmployee.workhistory = txtWorkHistory.Text;
                    SelectedEmployee.specialfeatures = txtSpecialFeatures.Text;

                    help.SaveEmployee(SelectedEmployee);
                    MessageBox.Show("Данные сотрудника обновлены!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных сотрудника: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedEmployee != null)
            {
                try
                {
                    int employeeId = SelectedEmployee.employeeid; 
                    help.DeleteEmployeeById(employeeId); 
                    MessageBox.Show("Сотрудник успешно удален!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении сотрудника: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для удаления.");
            }
        }
    }
}
