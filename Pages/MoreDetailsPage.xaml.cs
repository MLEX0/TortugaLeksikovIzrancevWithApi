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
    /// Логика взаимодействия для MoreDetailsPage.xaml
    /// </summary>
    public partial class MoreDetailsPage : Page
    {
        Model.Product prod;
        public MoreDetailsPage(Model.Product product)
        {
            InitializeComponent();
            tbName.Text = product.ProductName;
            tbDescription.Text = product.Description;
            tbComposition.Text = "Состав:"+product.Composition;
            prod = product;

            //проверка на наличие ссылки на изображение
            if (product.MainImage != null)
            {
                //загрузка изображения в приложение
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(product.MainImage, UriKind.Absolute);
                bitmap.EndInit();

                ImageProd.Source = bitmap;
            }
           
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PageController.MainFrame.Navigate(new MenuPage(GlobalInformation.IDTable));

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            GlobalInformation.ListOfOrder.Add(prod);
            MessageBox.Show(prod.ProductName + " Добавлено в корзину");
            
        }
    }
}
