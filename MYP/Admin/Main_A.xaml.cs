using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
using System.Windows.Shapes;

namespace MYP.Admin
{
    /// <summary>
    /// Логика взаимодействия для Main_A.xaml
    /// </summary>
    public partial class Main_A : Page
    {
        public Main_A()
        {
            InitializeComponent();
            main_photo.Text = MYPEntities.GetContext().Main_Page.Where(x => x.id == 1).FirstOrDefault().main_photo.ToString();
            text.Text = MYPEntities.GetContext().Main_Page.Where(x => x.id == 1).FirstOrDefault().text.ToString();
            photos.ItemsSource = MYPEntities.GetContext().List_Photo.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            add_popup.IsOpen = true;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                List_Photo cl = (List_Photo)button.DataContext;
                id1.Text = cl.id.ToString();
                photo1.Text = cl.photo.ToString();
                edit_popup.IsOpen = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var deletePhoto = ((FrameworkElement)sender).DataContext as List_Photo;
                MYPEntities.GetContext().List_Photo.Remove(deletePhoto);
                MYPEntities.GetContext().SaveChanges();
                MessageBox.Show("Фотография удалена", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                photos.ItemsSource = MYPEntities.GetContext().List_Photo.ToList();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Add_Photo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(photo.Text))
                {
                    MessageBox.Show("Введите ссылку на фотографию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    add_popup.IsOpen = true;
                    return;
                }
                if (!IsURL(photo.Text))
                {
                    MessageBox.Show("Введенный текст не является ссылкой", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    add_popup.IsOpen = true;
                    return;
                }
                if (MYPEntities.GetContext().List_Photo.Where(x => (x.photo == photo.Text)).FirstOrDefault() == null)
                {
                    List_Photo photoo = new List_Photo()
                    {
                        id_main_page = 1,
                        photo = photo.Text
                    };
                    MYPEntities.GetContext().List_Photo.Add(photoo);
                    MYPEntities.GetContext().SaveChanges();
                    add_popup.IsOpen = false;
                    photo.Text = null;
                    MessageBox.Show("Фотография добавлена", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    photos.ItemsSource = MYPEntities.GetContext().List_Photo.ToList();
                }
                else
                {
                    MessageBox.Show("Данная фотография уже существует");
                    add_popup.IsOpen = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                add_popup.IsOpen = true;
            }
        }

        private void Edit_Photo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int a = Convert.ToInt32(id1.Text);
                if (string.IsNullOrWhiteSpace(photo1.Text))
                {
                    MessageBox.Show("Введите ссылку на фотографию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    edit_popup.IsOpen = true;
                    return;
                }
                if (!IsURL(photo1.Text))
                {
                    MessageBox.Show("Введенный текст не является ссылкой", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    edit_popup.IsOpen = true;
                    return;
                }
                var photoo = MYPEntities.GetContext().List_Photo.Where(x => x.id == a).FirstOrDefault();
                photoo.photo = photo1.Text;
                MYPEntities.GetContext().SaveChanges();
                edit_popup.IsOpen = false;
                MessageBox.Show("Данные обновлены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                photos.ItemsSource = MYPEntities.GetContext().List_Photo.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                edit_popup.IsOpen = true;
            }
        }

        private bool IsURL(string str)
        {
            string pattern = @"^(http|https):\/\/([\w\-]+(\.[\w\-]+)+\/?)";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(str);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(main_photo.Text) || string.IsNullOrWhiteSpace(text.Text))
                {
                    MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!IsURL(main_photo.Text))
                {
                    MessageBox.Show("Введенный текст не является ссылкой", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var photoo = MYPEntities.GetContext().Main_Page.Where(x => x.id == 1).FirstOrDefault();
                photoo.main_photo = main_photo.Text;
                photoo.text = text.Text;
                MYPEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные обновлены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                main_photo.Text = MYPEntities.GetContext().Main_Page.Where(x => x.id == 1).FirstOrDefault().main_photo.ToString();
                text.Text = MYPEntities.GetContext().Main_Page.Where(x => x.id == 1).FirstOrDefault().text.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Photo_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var dataContext = button?.DataContext;
            if (dataContext != null && dataContext is List_Photo yourData)
            {
                string photoPath = yourData.photo;
                if (!string.IsNullOrEmpty(photoPath))
                {
                    try
                    {
                        System.Diagnostics.Process.Start(photoPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при открытии фото: {ex.Message}");
                    }
                }
            }
        }
    }
}
