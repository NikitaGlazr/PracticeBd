using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Practice.Entity;



namespace Practice
{
    internal class Help
    {
        private pracrikceOffEntities entities;

        public Help()
        {
            entities = GetContext();
        }

        public pracrikceOffEntities GetContext()
        {
            if (entities == null)
            {
                entities = new pracrikceOffEntities();

            }
            return entities;
        }

        public bool CheckUserExists(string login)
        {
            return entities.useraccount.Any(u => u.login == login);
        }

        public int AuthenticateUser(string login, string password)
        {      
            try
            {
                var user = entities.useraccount.FirstOrDefault(u => u.login == login && u.password == password);

                if (user != null)
                {
                    return user.roleid;
                }
            }
            catch { 
            
            }
            return 0;
        }

        public useraccount GetUserByLogin(string login)
        {
            return entities.useraccount.FirstOrDefault(u => u.login == login);
        }

        public List<employee> GetEmployees()
        {
            return entities.employee.ToList();
        }

        public List<employeerole> GetEmployeeRoles()
        {
            return entities.employeerole.ToList();
        }
        public List<Order> GetOrders()
        {
            return entities.Order.ToList();
        }


        public void SaveEmployee(employee emp)
        {
                var existingEmployee = entities.employee.Find(emp.employeeid);
                if (existingEmployee != null)
                {
                    entities.Entry(existingEmployee).CurrentValues.SetValues(emp); // Обновление существующего сотрудника
                }
            entities.SaveChanges(); // Сохранение изменений
        }
        public void AddNewEmployee(employee emp)
        {
            try
            {
                entities.employee.Add(emp);
                entities.SaveChanges();

                MessageBox.Show("Новый сотрудник добавлен!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении нового сотрудника: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteEmployeeById(int employeeId)
        {
            var employeeToDelete = entities.employee.Find(employeeId);
            if (employeeToDelete != null)
            {
                entities.employee.Remove(employeeToDelete);
                entities.SaveChanges();
            }
            else
            {
                MessageBox.Show("Сотрудник не найден.");
            }
        }

        public List<orderitem> GetOrderItems()
        {
            return entities.orderitem.ToList();
        }

        public string GetProductNameByProductID(int productID)
        {
            var product = entities.product.FirstOrDefault(p => p.productid == productID);
            return product != null ? product.name : "Товар не найден";
        }



        public void DeleteOrderItemById(int orderItemId)
        {
            var orderItemToDelete = entities.orderitem.Find(orderItemId);
            if (orderItemToDelete != null)
            {
                entities.orderitem.Remove(orderItemToDelete);
                entities.SaveChanges();
            }
            else
            {
                MessageBox.Show("Заказ не найден.");
            }
        }

        public void UpdateOrderItem(orderitem updatedOrderItem)
        {
            var existingOrderItem = entities.orderitem.Find(updatedOrderItem.orderitemid);
            if (existingOrderItem != null)
            {
               
                existingOrderItem.productid = updatedOrderItem.productid;
                existingOrderItem.quantity = updatedOrderItem.quantity;

                entities.SaveChanges();
            }
            else
            {
                MessageBox.Show("Заказ не найден.");
            }
        }

        public int GetProductIDByProductName(string productName)
        {
            var product = entities.product.FirstOrDefault(p => p.name == productName);
            return product != null ? product.productid : -1; // Вернуть -1 если продукт не найден
        }

        /*public List<customer> GetCustomers()
        {
            return entities.customer.ToList();
        }

        public List<employee> GetCouriers()
        {
            return entities.employee.Where(emp => emp.position == "Курьер").ToList();
        }*/

        public List<string> GetCustomersFullNameList()
        {
            List<string> customersFullNameList = new List<string>();

            var customers = entities.customer.ToList();
            foreach (var customer in customers)
            {
                string fullName = $"{customer.firstname} {customer.lastname} {customer.middlename}";
                customersFullNameList.Add(fullName);
            }

            return customersFullNameList;
        }

        public List<string> GetCouriersFullNameList()
        {
            List<string> couriersFullNameList = new List<string>();

            var couriers = entities.employee.Where(e => e.position == "Курьер").ToList();
            foreach (var courier in couriers)
            {
                string fullName = $"{courier.firstname} {courier.lastname} {courier.middlename}";
                couriersFullNameList.Add(fullName);
            }

            return couriersFullNameList;
        }

        public List<string> GetUniqueOrderStatuses()
        {
            return entities.Order.Select(o => o.status).Distinct().ToList();
        }

        public List<string> GetUniquePaymentMethods()
        {
            return entities.Order.Select(o => o.paymentmethod).Distinct().ToList();
        }


        public void UpdateOrder(Order order)
        {
        }

        public void DeleteOrder(int orderid)
        {
            using (var context = GetContext())
            {
                var order = context.Order.FirstOrDefault(o => o.orderid == orderid);

                if (order != null)
                {
                    context.Order.Remove(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Заказ не найден");
                }
            }
        }

    }
}
