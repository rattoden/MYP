using MYP.Admin;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

namespace MYP.Employee
{
    /// <summary>
    /// Логика взаимодействия для Employee.xaml
    /// </summary>
    public partial class Employee : Window
    {
        public Employee()
        {
            InitializeComponent();
            MainFrame.Navigate(new Inspection_E());
            inspection.FontFamily = new FontFamily("IBM Plex Mono Medium"); 
            inspection.FontSize = 22;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            switch (index)
            {
                case 0:
                    inspection.FontFamily = new FontFamily("IBM Plex Mono Medium"); inspection.FontSize = 22;
                    purchase.FontFamily = new FontFamily("IBM Plex Mono Light"); purchase.FontSize = 18;
                    privat.FontFamily = new FontFamily("IBM Plex Mono Light"); privat.FontSize = 18;
                    //documents.FontFamily = new FontFamily("IBM Plex Mono Light"); documents.FontSize = 18;
                    logout.FontFamily = new FontFamily("IBM Plex Mono Light"); logout.FontSize = 18;
                    MainFrame.Navigate(new Inspection_E());
                    break;
                case 1:
                    inspection.FontFamily = new FontFamily("IBM Plex Mono Light"); inspection.FontSize = 18;
                    purchase.FontFamily = new FontFamily("IBM Plex Mono Medium"); purchase.FontSize = 22;
                    privat.FontFamily = new FontFamily("IBM Plex Mono Light"); privat.FontSize = 18;
                    //documents.FontFamily = new FontFamily("IBM Plex Mono Light"); documents.FontSize = 18;
                    logout.FontFamily = new FontFamily("IBM Plex Mono Light"); logout.FontSize = 18;
                    MainFrame.Navigate(new Purchase_E());
                    break;
                case 2:
                    inspection.FontFamily = new FontFamily("IBM Plex Mono Light"); inspection.FontSize = 18;
                    purchase.FontFamily = new FontFamily("IBM Plex Mono Light"); purchase.FontSize = 18;
                    privat.FontFamily = new FontFamily("IBM Plex Mono Medium"); privat.FontSize = 22;
                    //documents.FontFamily = new FontFamily("IBM Plex Mono Light"); documents.FontSize = 18;
                    logout.FontFamily = new FontFamily("IBM Plex Mono Light"); logout.FontSize = 18;
                    MainFrame.Navigate(new Privatization_E());
                    break;
                case 3:
                    inspection.FontFamily = new FontFamily("IBM Plex Mono Light"); inspection.FontSize = 18;
                    purchase.FontFamily = new FontFamily("IBM Plex Mono Light"); purchase.FontSize = 18;
                    privat.FontFamily = new FontFamily("IBM Plex Mono Light"); privat.FontSize = 18;
                    //documents.FontFamily = new FontFamily("IBM Plex Mono Medium"); documents.FontSize = 22;
                    logout.FontFamily = new FontFamily("IBM Plex Mono Light"); logout.FontSize = 18;
                    //MainFrame.Navigate(new Documents_E());
                    break;
                case 4:
                    inspection.FontFamily = new FontFamily("IBM Plex Mono Light"); inspection.FontSize = 18;
                    purchase.FontFamily = new FontFamily("IBM Plex Mono Light"); purchase.FontSize = 18;
                    privat.FontFamily = new FontFamily("IBM Plex Mono Light"); privat.FontSize = 18;
                    //documents.FontFamily = new FontFamily("IBM Plex Mono Light"); documents.FontSize = 18;
                    logout.FontFamily = new FontFamily("IBM Plex Mono Light"); logout.FontSize = 18;
                    Authorization authorization = new Authorization();
                    authorization.Show();
                    this.Close();
                    break;
            }
        }
    }
}
