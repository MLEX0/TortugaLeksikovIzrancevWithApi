using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TartugaLeksikovIzrancev.Classes;

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
                try
                {
                    GlobalProducts = await AppData.Context.GetAllProducts();
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
            if (categories.Count == 0) 
            {
                Model.Category defaultCategory = new Model.Category{
                    IDCategory = 0,
                    NameCategory = "Все"
                };
                try
                {
                    categories = await AppData.Context.GetAllCategories();
                }
                    catch
                {
                    MessageBox.Show("Ошибка соединения с сервером!" +
                        "\nПожалуйста, запустите сервер и заново запустите приложение!",
                        "Ошибка", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    Application.Current.Shutdown();
                }
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
            if (GlobalInformation.Cart.Count > 0)
            {
                var result = MessageBox.Show("Внимание!\n" +
                "Если вы измените столик ваша корзина будет очищена!",
                "Вы уверены?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    GoBackToSelectTable();
                }
            }
            else 
            {
                GoBackToSelectTable();
            }
        }

        private void GoBackToSelectTable() 
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


        private void lvCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button) 
            {
                if (button.DataContext is Model.Product prod)
                {
                    if (prod.QuantityInCart >= 1)
                    {
                        prod.QuantityInCart++;
                    }
                    else if (prod.InCart == "Hidden")
                    {
                        prod.QuantityInCart++;
                        GlobalInformation.Cart.Add(prod);
                        //MessageBox.Show(prod.ProductName + " добавлено в корзину");
                    }
                    Filter();
                }
            }
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button) 
            {
                if (button.DataContext is Model.Product prod)
                {
                    prod.QuantityInCart--;
                    if(prod.QuantityInCart == 0)
                    {
                        GlobalInformation.Cart.Remove(prod);
                    }
                    Filter();
                }
            }
        }
    }
}
