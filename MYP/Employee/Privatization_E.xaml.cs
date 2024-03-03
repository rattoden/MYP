using MYP.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
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
using System.Xml.Linq;

namespace MYP.Employee
{
    /// <summary>
    /// Логика взаимодействия для Privatization_E.xaml
    /// </summary>
    public partial class Privatization_E : Page
    {
        public Privatization_E()
        {
            InitializeComponent();
            privatization.ItemsSource = MYPEntities.GetContext().Order.Where(x => x.id_operation == 1 && x.id_status != 3).ToList();
            status.ItemsSource = MYPEntities.GetContext().Status.Where(x => x.id != 1002).ToList();
            status1.ItemsSource = MYPEntities.GetContext().Status.Where(x => x.id != 1002).ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            add_popup.IsOpen = true;
        }

        private void Add_Oder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name.Text) || string.IsNullOrWhiteSpace(tel.Text))
                {
                    MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    add_popup.IsOpen = true;
                    return;
                }
                if (tel.Text.Length != 11 || (!tel.Text.StartsWith("7") && !tel.Text.StartsWith("8")))
                {
                    MessageBox.Show("Телефон должен содержать 11 символов и начинаться с 7 или 8", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    add_popup.IsOpen = true;
                    return;
                }
                int a = MYPEntities.GetContext().Status.Where(x => x.name == (string)status.SelectedValue).FirstOrDefault().id;
                if (MYPEntities.GetContext().Order.Where(x => (x.id_operation == 1 && x.id_status == a && x.name == name.Text && x.phone == tel.Text && x.id_flat == null)).FirstOrDefault() == null)
                {
                    Order order = new Order()
                    {
                        id_operation = 1,
                        id_status = a,
                        name = name.Text,
                        phone = tel.Text,
                        id_flat = null,
                        date = DateTime.Now
                    };
                    MYPEntities.GetContext().Order.Add(order);
                    MYPEntities.GetContext().SaveChanges();
                    add_popup.IsOpen = false;
                    name.Text = null;
                    tel.Text = null;
                    status.SelectedIndex = 0;
                    MessageBox.Show("Заявка на услугу по приватизации добавлена", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    privatization.ItemsSource = MYPEntities.GetContext().Order.Where(x => x.id_operation == 1 && x.id_status != 3).ToList();
                }
                else
                {
                    MessageBox.Show("Заявка с такими данными уже существует");
                    add_popup.IsOpen = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                add_popup.IsOpen = true;
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                Order cl = (Order)button.DataContext;
                if (cl.id_status != 1002)
                {
                    id1.Text = cl.id.ToString();
                    name1.Text = cl.name.ToString();
                    tel1.Text = cl.phone.ToString();
                    status1.SelectedValue = cl.Status.name;
                    edit_popup.IsOpen = true;
                }
                else
                    MessageBox.Show("Заявку со статусом 'ожидает отмены' нельзя редактировать", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Edit_Order_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int a = Convert.ToInt32(id1.Text);
                if (string.IsNullOrWhiteSpace(name1.Text) || string.IsNullOrWhiteSpace(tel1.Text))
                {
                    MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    edit_popup.IsOpen = true;
                    return;
                }
                if (tel1.Text.Length != 11 || (!tel1.Text.StartsWith("7") && !tel1.Text.StartsWith("8")))
                {
                    MessageBox.Show("Телефон должен содержать 11 символов и начинаться с 7 или 8", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    edit_popup.IsOpen = true;
                    return;
                }
                var order = MYPEntities.GetContext().Order.Where(x => x.id == a).FirstOrDefault();
                order.name = name1.Text;
                order.phone = tel1.Text;
                order.id_status = MYPEntities.GetContext().Status.Where(x => x.name == (string)status1.SelectedValue).FirstOrDefault().id;
                MYPEntities.GetContext().SaveChanges();
                privatization.ItemsSource = MYPEntities.GetContext().Order.Where(x => x.id_operation == 1 && x.id_status != 3).ToList();
                MessageBox.Show("Данные обновлены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                edit_popup.IsOpen = true;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleteOrder = ((FrameworkElement)sender).DataContext as Order;
                if (MessageBox.Show("Удалить заявку № " + deleteOrder.id + "?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    MYPEntities.GetContext().Order.Remove(deleteOrder);
                    MYPEntities.GetContext().SaveChanges();
                    MessageBox.Show("Заявка удалена", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    privatization.ItemsSource = MYPEntities.GetContext().Order.Where(x => x.id_operation == 1 && x.id_status != 3).ToList();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string newText = (sender as TextBox).Text + e.Text;
            if (!IsDigitsOnly(newText))
            {
                e.Handled = true;
            }
        }

        private void Status_button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Order or = (Order)button.DataContext;
            if (or.id_status == 1)
            {
                or.id_status = 2;
                MYPEntities.GetContext().SaveChanges();
                privatization.ItemsSource = MYPEntities.GetContext().Order.Where(x => x.id_operation == 1 && x.id_status != 3).ToList();
            }
            else if (or.id_status == 2)
            {
                or.id_status = 3;
                MYPEntities.GetContext().SaveChanges();
                privatization.ItemsSource = MYPEntities.GetContext().Order.Where(x => x.id_operation == 1 && x.id_status != 3).ToList();
            }
            else if (or.id_status == 1002)
            {
                MYPEntities.GetContext().Order.Remove(or);
                MYPEntities.GetContext().SaveChanges();
                privatization.ItemsSource = MYPEntities.GetContext().Order.Where(x => x.id_operation == 1 && x.id_status != 3).ToList();
            }
        }
    }
}
