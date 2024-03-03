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
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace MYP.Admin
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
            MainFrame.Navigate(new Main_A());
            main.FontFamily = new FontFamily("IBM Plex Mono Medium");
            main.FontSize = 22;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            switch (index)
            {
                case 0:
                    main.FontFamily = new FontFamily("IBM Plex Mono Medium"); main.FontSize = 22;
                    flats.FontFamily = new FontFamily("IBM Plex Mono Light"); flats.FontSize = 18;
                    privat.FontFamily = new FontFamily("IBM Plex Mono Light"); privat.FontSize = 18;
                    contacts.FontFamily = new FontFamily("IBM Plex Mono Light"); contacts.FontSize = 18;
                    logout.FontFamily = new FontFamily("IBM Plex Mono Light"); logout.FontSize = 18;
                    MainFrame.Navigate(new Main_A());
                    break;
                case 1:
                    main.FontFamily = new FontFamily("IBM Plex Mono Light"); main.FontSize = 18;
                    flats.FontFamily = new FontFamily("IBM Plex Mono Medium"); flats.FontSize = 22;
                    privat.FontFamily = new FontFamily("IBM Plex Mono Light"); privat.FontSize = 18;
                    contacts.FontFamily = new FontFamily("IBM Plex Mono Light"); contacts.FontSize = 18;
                    logout.FontFamily = new FontFamily("IBM Plex Mono Light"); logout.FontSize = 18;
                    MainFrame.Navigate(new Flats_A());
                    break;
                case 2:
                    main.FontFamily = new FontFamily("IBM Plex Mono Light"); main.FontSize = 18;
                    flats.FontFamily = new FontFamily("IBM Plex Mono Light"); flats.FontSize = 18;
                    privat.FontFamily = new FontFamily("IBM Plex Mono Medium"); privat.FontSize = 22;
                    contacts.FontFamily = new FontFamily("IBM Plex Mono Light"); contacts.FontSize = 18;
                    logout.FontFamily = new FontFamily("IBM Plex Mono Light"); logout.FontSize = 18;
                    MainFrame.Navigate(new Privatization_A());
                    break;
                case 3:
                    main.FontFamily = new FontFamily("IBM Plex Mono Light"); main.FontSize = 18;
                    flats.FontFamily = new FontFamily("IBM Plex Mono Light"); flats.FontSize = 18;
                    privat.FontFamily = new FontFamily("IBM Plex Mono Light"); privat.FontSize = 18;
                    contacts.FontFamily = new FontFamily("IBM Plex Mono Medium"); contacts.FontSize = 22;
                    logout.FontFamily = new FontFamily("IBM Plex Mono Light"); logout.FontSize = 18;
                    MainFrame.Navigate(new Contacts_A());
                    break;
                case 4:
                    main.FontFamily = new FontFamily("IBM Plex Mono Light"); main.FontSize = 18;
                    flats.FontFamily = new FontFamily("IBM Plex Mono Light"); flats.FontSize = 18;
                    privat.FontFamily = new FontFamily("IBM Plex Mono Light"); privat.FontSize = 18;
                    contacts.FontFamily = new FontFamily("IBM Plex Mono Light"); contacts.FontSize = 18;
                    logout.FontFamily = new FontFamily("IBM Plex Mono Light"); logout.FontSize = 18;
                    Authorization authorization = new Authorization();
                    authorization.Show();
                    this.Close();
                    break;
            }
        }
    }
}
