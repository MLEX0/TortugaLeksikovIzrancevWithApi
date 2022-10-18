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

        public BasketPage()
        {
            InitializeComponent();

            Refresh();
        }

        //Метод для обновления ListView 
        public void Refresh()
        {
            lvOrder.ItemsSource = null;
            tbPrice.Text = "Итоговая стоимость: " + totalPrice();
            tbTable.Text = "Ваш столик: " + GlobalInformation.IDTable.IDTable;
            lvOrder.ItemsSource = GlobalInformation.ListOfOrder.Distinct().OrderBy(i=>i.IDProduct);
            if (GlobalInformation.ListOfOrder.Count < 1)
            {
                btnGoBasket.IsEnabled = false;
            }
            else 
            {
                btnGoBasket.IsEnabled = true;
            }
        }

        //Метод Высчитывающий итоговую стоимость заказа
        public string totalPrice()
        {
            decimal totalCost = 0;
            foreach(Model.Product prod in GlobalInformation.ListOfOrder)
            {

                totalCost += prod.Cost;
            }
            
            return Convert.ToString(totalCost);
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PageController.MainFrame.Navigate(new MenuPage(GlobalInformation.IDTable));
        }

        //мeтод удаляющий запись ил ListView при нажатии Delete
        private void lvOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if(lvOrder.SelectedItem is Model.Product)
                {
                    var prod = lvOrder.SelectedItem as Model.Product;
                    GlobalInformation.ListOfOrder.Remove(prod);
                    Refresh();
                }
            }
        }

        private async void btnGoBasket_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalInformation.ListOfOrder.Count < 1) 
            {
                MessageBox.Show($"Корзина пуста!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Model.Order order = new Model.Order();
                order.TotalCost = Convert.ToDecimal(totalPrice());
                order.IDRestourantTable = Convert.ToInt32(GlobalInformation.IDTable.IDTable);
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

                Model.OrderProduct orderProduct = new Model.OrderProduct();

                foreach (Model.Product prod in GlobalInformation.ListOfOrder.Distinct())
                {
                    orderProduct.IDOrder = order.IDOrder;
                    orderProduct.IDProduct = prod.IDProduct;
                    orderProduct.Count = prod.OrderProdCount;
                    orderProduct = await AppData.Context.PostCreateOrderProduct(orderProduct);
                }

                MessageBox.Show($"Заказ №{order.IDOrder} успешно оформлен!");
                GlobalInformation.ListOfOrder.Clear();
                PageController.MainFrame.Content = new StartPage();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message).ToString();
            }
        }



        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            Model.Product btn = (sender as Button).DataContext as Model.Product;
            GlobalInformation.ListOfOrder.Add(btn);
            Refresh();
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            lvOrder.SelectedItem = (sender as Button).DataContext;

            var prod = lvOrder.SelectedItem as Model.Product;
            GlobalInformation.ListOfOrder.Remove(prod);
            Refresh();
        }
    }
}
