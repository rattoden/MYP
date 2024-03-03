using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MYP.Admin
{
    /// <summary>
    /// Логика взаимодействия для Flats_A.xaml
    /// </summary>
    public partial class Flats_A : Page
    {
        public int flag;
        public Flats_A()
        {
            InitializeComponent();
            flats.ItemsSource = MYPEntities.GetContext().Flat.ToList();
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
                Flat cl = (Flat)button.DataContext;
                if (cl.purchased != 2)
                {
                    id1.Text = cl.id.ToString();
                    plan1.Text = cl.photo.ToString();
                    price1.Text = cl.price.ToString();
                    rooms1.SelectedIndex = cl.rooms - 1;
                    area1.Text = cl.area.ToString();
                    floor1.SelectedIndex = cl.floor - 1;
                    if(cl.purchased == 0)
                        img_purchased.Source = new BitmapImage(new Uri("../resources/x.png", UriKind.Relative));
                    else if(cl.purchased == 1)
                        img_purchased.Source = new BitmapImage(new Uri("../resources/v.png", UriKind.Relative));
                    else
                        img_purchased.Source = new BitmapImage(new Uri("../resources/x.png", UriKind.Relative));
                    edit_popup.IsOpen = true;
                }
                else
                    MessageBox.Show("Удалённую квартиру нельзя редактировать", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleteFlat = ((FrameworkElement)sender).DataContext as Flat;
                if (MessageBox.Show("Удалить квартиру № " + deleteFlat.id + "?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var order = MYPEntities.GetContext().Order.Where(p => p.id_flat == deleteFlat.id && p.id_status != 1002).Select(p => p.id).ToList();
                    var order1 = MYPEntities.GetContext().Order.Where(p => p.id_flat == deleteFlat.id && p.id_status == 1002).Select(p => p.id).ToList();
                    var unclosed_orders = order.Select(id => $"№ {id}").ToList();
                    var closed_orders = order1.Select(id => $"№ {id}").ToList();
                    //если есть отмененные и неотмененные, то нужно отменить неотмененные и предупредить о невозможность удалить квартиру, так как сначала нужно полностью отменить заявки отменённые
                    if (unclosed_orders.Any() && closed_orders.Any())
                    {
                        if (MessageBox.Show("Будут отменены заявки:\n" + string.Join("\n", unclosed_orders), "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
                        {
                            var ordersToUpdate = MYPEntities.GetContext().Order.Where(p => p.id_flat == deleteFlat.id).ToList();
                            foreach (var canceled_order in ordersToUpdate)
                            {
                                canceled_order.id_status = 1002;
                            }
                            deleteFlat.purchased = 2;
                            MYPEntities.GetContext().SaveChanges();
                            flats.ItemsSource = MYPEntities.GetContext().Flat.ToList();
                            var order2 = MYPEntities.GetContext().Order.Where(p => p.id_flat == deleteFlat.id && p.id_status == 1002).Select(p => p.id).ToList();
                            var closed_orders1 = order2.Select(id => $"№ {id}").ToList();
                            MessageBox.Show("Для удаления квартиры из системы необходимо отменить заявки:\n" + string.Join("\n", closed_orders1), "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        else
                            return;
                    }
                    //если есть только отменённые, то нужно предупредить о невозможность удалить квартиру, так как сначала нужно полностью отменить заявки отменённые
                    else if (!unclosed_orders.Any() && closed_orders.Any())
                    {
                        MessageBox.Show("Для удаления квартиры из системы необходимо отменить заявки:\n" + string.Join("\n", closed_orders), "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                        MYPEntities.GetContext().SaveChanges();
                        flats.ItemsSource = MYPEntities.GetContext().Flat.ToList();
                        return;
                    }
                    //если есть только неотменённые, то сначала нужно отменить неотмененные и предупредить о невозможность удалить квартиру, так как сначала нужно полностью отменить заявки отменённые
                    else if (unclosed_orders.Any() && !closed_orders.Any())
                    {
                        if (MessageBox.Show("Будут отменены заявки:\n" + string.Join("\n", unclosed_orders), "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
                        {
                            var ordersToUpdate = MYPEntities.GetContext().Order.Where(p => p.id_flat == deleteFlat.id).ToList();
                            foreach (var canceled_order in ordersToUpdate)
                            {
                                canceled_order.id_status = 1002;
                            }
                            deleteFlat.purchased = 2;
                            MYPEntities.GetContext().SaveChanges();
                            flats.ItemsSource = MYPEntities.GetContext().Flat.ToList();
                            var order2 = MYPEntities.GetContext().Order.Where(p => p.id_flat == deleteFlat.id && p.id_status == 1002).Select(p => p.id).ToList();
                            var closed_orders1 = order2.Select(id => $"№ {id}").ToList();
                            MessageBox.Show("Для удаления квартиры из системы необходимо отменить заявки:\n" + string.Join("\n", closed_orders1), "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        else
                            return;
                    }
                    else if(!unclosed_orders.Any() && !closed_orders.Any())
                    {
                        MYPEntities.GetContext().Flat.Remove(deleteFlat);
                        MYPEntities.GetContext().SaveChanges();
                        MessageBox.Show("Квартира удалена", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                        flats.ItemsSource = MYPEntities.GetContext().Flat.ToList();
                        return;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Add_Flat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(plan.Text) || string.IsNullOrWhiteSpace(price.Text) || string.IsNullOrWhiteSpace(area.Text))
                {
                    MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    add_popup.IsOpen = true;
                    return;
                }
                if (!IsURL(plan.Text))
                {
                    MessageBox.Show("Введенный текст не является ссылкой", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    add_popup.IsOpen = true;
                    return;
                }
                int a = int.Parse(price.Text);
                double b = double.Parse(area.Text);
                int c = rooms.SelectedIndex + 1;
                int d = floor.SelectedIndex + 1;
                if (MYPEntities.GetContext().Flat.Where(x => (x.photo == plan.Text && x.price == a && x.area == b && x.rooms == c && x.floor == d)).FirstOrDefault() == null)
                {
                    Flat flat = new Flat()
                    {
                        photo = plan.Text,
                        price = a,
                        area = b,
                        rooms = c,
                        floor = d,
                        purchased = 0
                    };
                    MYPEntities.GetContext().Flat.Add(flat);
                    MYPEntities.GetContext().SaveChanges();
                    add_popup.IsOpen = false;
                    plan.Text = null;
                    price.Text = null;
                    area.Text = null;
                    rooms.SelectedIndex = 0;
                    floor.SelectedIndex = 0;
                    MessageBox.Show("Квартира добавлена", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    flats.ItemsSource = MYPEntities.GetContext().Flat.ToList();
                }
                else
                {
                    MessageBox.Show("Квартира с такими данными уже существует");
                    add_popup.IsOpen = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                add_popup.IsOpen = true;
            }
        }

        private void Edit_Flat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int a = Convert.ToInt32(id1.Text);
                if (string.IsNullOrWhiteSpace(plan1.Text) || string.IsNullOrWhiteSpace(price1.Text) || string.IsNullOrWhiteSpace(area1.Text))
                {
                    MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    edit_popup.IsOpen = true;
                    return;
                }
                if (!IsURL(plan1.Text))
                {
                    MessageBox.Show("Введенный текст не является ссылкой", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    edit_popup.IsOpen = true;
                    return;
                }
                var flat = MYPEntities.GetContext().Flat.Where(x => x.id == a).FirstOrDefault();
                flat.photo = plan1.Text;
                flat.price = int.Parse(price1.Text);
                flat.rooms = rooms1.SelectedIndex + 1;
                flat.area = double.Parse(area1.Text);
                flat.floor = floor1.SelectedIndex + 1;
                if(flag == 0)
                {
                    flat.purchased = 0;
                    MYPEntities.GetContext().SaveChanges();
                    edit_popup.IsOpen = false;
                    MessageBox.Show("Данные обновлены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    flats.ItemsSource = MYPEntities.GetContext().Flat.ToList();
                }
                else
                {
                    var order = MYPEntities.GetContext().Order.Where(p => p.id_flat == flat.id && p.id_status != 1002).Select(p => p.id).ToList();
                    var unclosed_orders = order.Select(id => $"№ {id}").ToList();
                    if (unclosed_orders.Any())
                    {
                        if (MessageBox.Show("Будут отменены заявки:\n" + string.Join("\n", unclosed_orders), "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
                        {
                            var ordersToUpdate = MYPEntities.GetContext().Order.Where(p => p.id_flat == flat.id).ToList();
                            foreach (var canceled_order in ordersToUpdate)
                            {
                                canceled_order.id_status = 1002;
                            }
                            flat.purchased = 1;
                            MYPEntities.GetContext().SaveChanges();
                            MessageBox.Show("Данные обновлены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                            flats.ItemsSource = MYPEntities.GetContext().Flat.ToList();
                        }
                        else
                        {
                            edit_popup.IsOpen = true;
                            return;
                        }
                    }
                    else
                    {
                        flat.purchased = 1;
                        MYPEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные обновлены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                        flats.ItemsSource = MYPEntities.GetContext().Flat.ToList();
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                edit_popup.IsOpen = true;
            }
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

        private bool IsDouble(string str)
        {
            double result;
            return double.TryParse(str, out result);
        }

        private bool IsURL(string str)
        {
            string pattern = @"^(http|https):\/\/([\w\-]+(\.[\w\-]+)+\/?)";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(str);
        }

        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string newText = (sender as TextBox).Text + e.Text;
            if (!IsDigitsOnly(newText) || Convert.ToInt32(newText) > 50000000)
            {
                e.Handled = true;
            }
        }

        private void Double_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string newText = (sender as TextBox).Text + e.Text;
            if (!IsDouble(newText) || Convert.ToDouble(newText) >= 100.0)
            {
                e.Handled = true;
            }
        }

        private bool isImageChanged = false;

        private void Change_image_Click(object sender, RoutedEventArgs e)
        {
            if (isImageChanged)
            {
                img_purchased.Source = new BitmapImage(new Uri("../resources/v.png", UriKind.Relative));
                flag = 1;
            }
            else
            {
                img_purchased.Source = new BitmapImage(new Uri("../resources/x.png", UriKind.Relative));
                flag = 0;
            }
            isImageChanged = !isImageChanged;
        }

        private void Photo_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var dataContext = button?.DataContext;
            if (dataContext != null && dataContext is Flat yourData)
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
