using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для Privatization_A.xaml
    /// </summary>
    public partial class Privatization_A : Page
    {
        public Privatization_A()
        {
            InitializeComponent();
            link.Text = MYPEntities.GetContext().Privatization_Page.Where(x => x.id == 1).FirstOrDefault().link.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(link.Text))
                {
                    MessageBox.Show("Введите ссылку на документ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (IsURL(link.Text))
                {
                    var privatization = MYPEntities.GetContext().Privatization_Page.Where(x => x.id == 1).FirstOrDefault();
                    privatization.link = link.Text;
                    MessageBox.Show("Ссылка успешно обновлена!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Введенный текст не является ссылкой", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsURL(string str)
        {
            string pattern = @"^(http|https):\/\/([\w\-]+(\.[\w\-]+)+\/?)";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(str);
        }
    }
}
