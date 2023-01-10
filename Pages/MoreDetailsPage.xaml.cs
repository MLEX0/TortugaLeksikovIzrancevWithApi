using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для MoreDetailsPage.xaml
    /// </summary>
    public partial class MoreDetailsPage : Page
    {
        Model.Product prod;
        int prodQuantity { get; set; } 
        public MoreDetailsPage(Model.Product product)
        {
            InitializeComponent();
            tbName.Text = product.ProductName;
            tbDescription.Text = product.Description;
            tbComposition.Text = "Состав:" + product.Composition;
            prod = product;
            tbCount.DataContext = prod;
            btnPlus.DataContext = prod;
            btnMinus.DataContext = prod; 

            //проверка на наличие ссылки на изображение
            if (!string.IsNullOrEmpty(product.MainImage))
            {
                //загрузка изображения в приложение
                BitmapImage biImg = new BitmapImage();
                MemoryStream ms = new MemoryStream(product.ByteImage);
                biImg.BeginInit();
                biImg.StreamSource = ms;
                biImg.EndInit();

                ImageSource imgSrc = biImg as ImageSource;

                ImageProd.Source = imgSrc;
            }

        }

        private void DataUpdate(Model.Product product) 
        {
            PageController.MainFrame.Content = null;
            tbCount.DataContext = null;
            btnPlus.DataContext = null;
            btnMinus.DataContext = null;
            PageController.MainFrame.Content = new MoreDetailsPage(product);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PageController.StaticMenu.Filter();
            PageController.MainFrame.Navigate(PageController.StaticMenu);
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
                    DataUpdate(prod);
                }
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
                    else if (prod.InCart == "Hidden")
                    {
                        prod.QuantityInCart++;
                        GlobalInformation.Cart.Add(prod);
                        MessageBox.Show(prod.ProductName + " добавлено в корзину");
                    }
                    DataUpdate(prod);
                }
            }
        }

    }
}
