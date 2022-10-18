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
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        List<Model.Product> GlobalProducts = new List<Model.Product>();
        List<Model.Category> categories = new List<Model.Category>();

        public MenuPage(Model.RestaurantTable table)
        {
            InitializeComponent();
            txtTable.Text = $"Ваш столик: {table.IDTable}";
            lvCategory.SelectedIndex = 0;
            Filter();
        }

        //Метод Фильрации Продуктов
        public async void Filter()
        {
            List<Model.Product> products = new List<Model.Product>();
            if (GlobalProducts.Count == 0) 
            {
                GlobalProducts = await AppData.Context.GetAllProducts();
            }
            if (categories.Count == 0) 
            {
                Model.Category defaultCategory = new Model.Category{
                    IDCategory = 0,
                    NameCategory = "Все"
                };
                categories = await AppData.Context.GetAllCategories();
                categories.Insert(0, defaultCategory);
                lvCategory.ItemsSource = categories;
            }

            //выборка по категориям
            switch (lvCategory.SelectedIndex)
            {
                case 1:
                    {
                        products = GlobalProducts.Where(i => i.IDCategory == 1).ToList();
                        break;
                    }
                case 2:
                    {
                        products = GlobalProducts.Where(i => i.IDCategory == 2).ToList();
                        break;
                    }
                case 3:
                    {
                        products = GlobalProducts.Where(i => i.IDCategory == 3).ToList();
                        break;
                    }
                case 4:
                    {
                        products = GlobalProducts.Where(i => i.IDCategory == 4).ToList();
                        break;
                    }
                case 5:
                    {
                        products = GlobalProducts.Where(i => i.IDCategory == 5).ToList();
                        break;
                    }
                default:
                    {
                        products = GlobalProducts.ToList();
                        break;
                    }
            }

            lvMenu.ItemsSource = products;
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PageController.MainFrame.Navigate(new StartPage());
        }

        private void btnGoBasket_Click(object sender, RoutedEventArgs e)
        {
            PageController.MainFrame.Navigate(new BasketPage());
        }

        private void lvMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(lvMenu.SelectedItem is Model.Product)
            {
                var prod = lvMenu.SelectedItem as Model.Product;
                PageController.MainFrame.Navigate(new MoreDetailsPage(prod));
            }
        }


        //Метод добавления продукта в корзину 
        private void lvMenu_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(lvMenu.SelectedItem is Model.Product)
            {
                var prod = lvMenu.SelectedItem as Model.Product;
                GlobalInformation.ListOfOrder.Add(prod);
                MessageBox.Show(prod.ProductName + " Добавлено в корзину");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lvMenu.SelectedItem = (sender as Button).DataContext;
        
            var prod = lvMenu.SelectedItem as Model.Product;
            GlobalInformation.ListOfOrder.Add(prod);
            MessageBox.Show(prod.ProductName + " Добавлено в корзину");
            
        }

        private void lvCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
    }
}
