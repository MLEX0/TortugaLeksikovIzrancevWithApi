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
            InitializeComponent();
            AppData.updateContext();
            AppData.Context.RunAsync().GetAwaiter().GetResult();
;           GetTables();
            GlobalInformation.Cart.Clear();
        }

        public async void GetTables() 
        {
            restaurantTables = await AppData.Context.GetAllTables();
            lvTables.ItemsSource = restaurantTables.ToList();
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
