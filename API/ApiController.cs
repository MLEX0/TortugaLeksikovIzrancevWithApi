using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TartugaLeksikovIzrancev.Model;

namespace TartugaLeksikovIzrancev.API
{
    public class ApiController
    {

        public  HttpClient client = new HttpClient();

        public async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://php/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<RestaurantTable[]> GetAllTables()
        {
            RestaurantTable[] restaurantTable = null;
            HttpResponseMessage response = await client.GetAsync($"tables/");
            restaurantTable = await response.Content.ReadAsAsync<RestaurantTable[]>();

            return restaurantTable;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            List<Category> categories = null;
            HttpResponseMessage response = await client.GetAsync($"categories/");
            categories = await response.Content.ReadAsAsync<List<Category>>();

            return categories;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> products = null;
            HttpResponseMessage response = await client.GetAsync($"products/");
            products = await response.Content.ReadAsAsync<List<Product>>();

            return products;
        }

        public async Task<Order> PostCreateOrder(Order order)
        {
            Order responce = null;
            HttpResponseMessage response = await client.PostAsJsonAsync("order/", order);
            responce = await response.Content.ReadAsAsync<Order>();
            order.IDOrder = responce.IDOrder;
            order.status = responce.status;
            return order;
        }

        public async Task<OrderProduct> PostCreateOrderProduct(OrderProduct orderProduct)
        {
            OrderProduct responce = null;
            HttpResponseMessage response = await client.PostAsJsonAsync("orderproduct/", orderProduct);
            responce = await response.Content.ReadAsAsync<OrderProduct>();
            orderProduct.IDOrderProduct = responce.IDOrderProduct;
            orderProduct.status = responce.status;
            return orderProduct;
        }

        public Byte[] GetImage(string path)
        {
            using (WebClient webClient = new WebClient())
            {
                    byte[] data = webClient.DownloadData($"http://php/uploads/{path}");
                    return data;
            }
        }

    }
}
