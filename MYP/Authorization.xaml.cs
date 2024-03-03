using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
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
using FontFamily = System.Windows.Media.FontFamily;

namespace MYP
{
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }
        private void TextBox1_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Логин")
            {
                textBox.Text = "";
                textBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }
        private void TextBox2_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Пароль")
            {
                textBox.Text = "";
                textBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void TextBox1_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Логин";
                textBox.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#787272"));
            }
        }
        private void TextBox2_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Пароль";
                textBox.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#787272"));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = MYPEntities.GetContext().User.Where(u => u.login == login.Text && u.password == password.Text).FirstOrDefault();
                if (user == null)
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    switch (user.id_role)
                    {
                        case 1:
                            MessageBox.Show("Вы вошли под учётной записью администратора", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                            Admin.Admin admin = new Admin.Admin();
                            admin.Show();
                            this.Close();
                            break;
                        case 2:
                            MessageBox.Show("Вы вошли под учётной записью специалиста", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                            Employee.Employee employee = new Employee.Employee();
                            employee.Show();
                            this.Close();
                            break;
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}