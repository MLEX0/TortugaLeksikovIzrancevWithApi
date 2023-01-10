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



namespace TartugaLeksikovIzrancev.Pages
{
    /// <summary>
    /// Логика взаимодействия для BasketPage.xaml
    /// </summary>
    public partial class BasketPage : Page
    {
        public static decimal TotalPrice 
        {
            get 
            {
                decimal totalCost = 0;
                foreach (Model.Product prod in GlobalInformation.Cart) 
                {
                    totalCost += prod.Cost * prod.QuantityInCart;   
                }
                totalCost = totalCost - (totalCost * GlobalInformation.GlobalDiscount);
                return totalCost;
            }
        }

        public BasketPage()
        {
            InitializeComponent();

            Refresh();
        }

        //Метод для обновления ListView 
        public void Refresh()
        {
            lvOrder.ItemsSource = null;
            tbPrice.Text = "Итоговая стоимость: " + TotalPrice;
            tbTable.Text = "Ваш столик: " + GlobalInformation.SelectedTableNumber;
            lvOrder.ItemsSource = GlobalInformation.Cart.OrderBy(i=>i.IDProduct);
            if (GlobalInformation.Cart.Count < 1)
            {
                btnGoBasket.IsEnabled = false;
            }
            else 
            {
                btnGoBasket.IsEnabled = true;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PageController.StaticMenu.Filter();
            PageController.MainFrame.Navigate(PageController.StaticMenu);
        }

        //мeтод удаляющий запись ил ListView при нажатии Delete
        private void lvOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if(lvOrder.SelectedItem is Model.Product)
                {
                    var prod = lvOrder.SelectedItem as Model.Product;
                    GlobalInformation.Cart.Remove(prod);
                    Refresh();
                }
            }
        }

        private async void btnGoBasket_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalInformation.Cart.Count < 1) 
            {
                MessageBox.Show($"Корзина пуста!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Model.Order order = new Model.Order();
                order.TotalCost = TotalPrice;
                order.IDRestourantTable = Convert.ToInt32(GlobalInformation.SelectedTableNumber);
                order.OrderTime = DateTime.Now;
                order.IDEmployee = 1;
                order.IDPromocode = null;
                if (rbCard.IsChecked == true)
                {
                    order.IsCashless = true;
                }
                else
                {
                    order.IsCashless = false;
                }
                order.IDStatus = 1;

                order = await AppData.Context.PostCreateOrder(order);

                //Проверка на то, что заказ создан
                if (order.status)
                {
                    Model.OrderProduct orderProduct = new Model.OrderProduct();

                    foreach (Model.Product prod in GlobalInformation.Cart)
                    {
                        orderProduct.IDOrder = order.IDOrder;
                        orderProduct.IDProduct = prod.IDProduct;
                        orderProduct.Count = prod.QuantityInCart;
                        orderProduct = await AppData.Context.PostCreateOrderProduct(orderProduct);
                    }

                    MessageBox.Show($"Заказ №{order.IDOrder} успешно оформлен!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    GlobalInformation.Cart.Clear();
                    PageController.MainFrame.Content = new StartPage();
                }
                else 
                {
                    MessageBox.Show("Отсутствует соединение с сервером, невозможно создать заказ", 
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message).ToString();
            }
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
                    Refresh();
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
                    if (prod.QuantityInCart == 0)
                    {
                        GlobalInformation.Cart.Remove(prod);
                    }
                    Refresh();
                }
            }
        }
    }
}
