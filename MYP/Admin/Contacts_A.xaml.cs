using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace MYP.Admin
{
    /// <summary>
    /// Логика взаимодействия для Contacts_A.xaml
    /// </summary>
    public partial class Contacts_A : Page
    {
        public Contacts_A()
        {
            InitializeComponent();
            address.Text = MYPEntities.GetContext().Contact_Page.Where(x => x.id == 2).FirstOrDefault().address.ToString();
            email.Text = MYPEntities.GetContext().Contact_Page.Where(x => x.id == 2).FirstOrDefault().email.ToString();
            tel_priem.Text = MYPEntities.GetContext().Contact_Page.Where(x => x.id == 2).FirstOrDefault().tel1.ToString();
            tel_prod.Text = MYPEntities.GetContext().Contact_Page.Where(x => x.id == 2).FirstOrDefault().tel2.ToString();
            tel_priv.Text = MYPEntities.GetContext().Contact_Page.Where(x => x.id == 2).FirstOrDefault().tel3.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(address.Text) || string.IsNullOrWhiteSpace(email.Text) || string.IsNullOrWhiteSpace(tel_priem.Text)
                    || string.IsNullOrWhiteSpace(tel_prod.Text) || string.IsNullOrWhiteSpace(tel_priv.Text))
                {
                    MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!IsValidEmail(email.Text))
                {
                    MessageBox.Show("Введите корректный адрес электронной почты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if(tel_priem.Text.Length != 11 || tel_prod.Text.Length != 11 || tel_priv.Text.Length != 11 || (!tel_priem.Text.StartsWith("7") && !tel_priem.Text.StartsWith("8")) || (!tel_prod.Text.StartsWith("7") && !tel_prod.Text.StartsWith("8")) || (!tel_priv.Text.StartsWith("7") && !tel_priv.Text.StartsWith("8")))
                {
                    MessageBox.Show("Телефон должен содержать 11 символов и начинаться с 7 или 8", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var privatization = MYPEntities.GetContext().Contact_Page.Where(x => x.id == 2).FirstOrDefault();
                privatization.address = address.Text;
                privatization.email = email.Text;
                privatization.tel1 = tel_priem.Text;
                privatization.tel2 = tel_prod.Text;
                privatization.tel3 = tel_priv.Text;
                MessageBox.Show("Данные успешно обновлены!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            return Regex.IsMatch(email, pattern);
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
    }
}
