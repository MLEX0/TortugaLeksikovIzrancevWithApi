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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TartugaLeksikovIzrancev.Classes;
using TartugaLeksikovIzrancev.API;

namespace TartugaLeksikovIzrancev.Pages
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        static Model.RestaurantTable[] restaurantTables;
        public StartPage()
        {
            try 
            {
                InitializeComponent();
                AppData.updateContext();
                AppData.Context.RunAsync().GetAwaiter().GetResult();
                GetTables();
            }
            catch
            {
                MessageBox.Show("Ошибка соединения с сервером!" +
                    "\nПожалуйста, запустите сервер заново запустите приложение!",
                    "Ошибка", MessageBoxButton.OK, 
                    MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            GlobalInformation.Cart.Clear();
        }

        public async void GetTables() 
        {
            try
            {
                restaurantTables = await AppData.Context.GetAllTables();
                lvTables.ItemsSource = restaurantTables.ToList();
            }
            catch
            {
                MessageBox.Show("Ошибка соединения с сервером!" +
                    "\nПожалуйста, запустите сервер и заново запустите приложение!",
                    "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }



        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid obj)
            {
                if (obj.DataContext is Model.RestaurantTable table)
                {
                    GlobalInformation.SelectedTableNumber = table.IDTable;
                    PageController.StaticMenu = new MenuPage(table);
                    PageController.MainFrame.Navigate(PageController.StaticMenu);
                }
            }
        }
    }
}
