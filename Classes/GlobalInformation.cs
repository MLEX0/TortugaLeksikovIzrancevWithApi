using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TartugaLeksikovIzrancev.Classes;

namespace TartugaLeksikovIzrancev.Classes
{
    //Класс с различной глобальной информацией
    public static class GlobalInformation
    {
        public static Model.RestaurantTable IDTable { get; set; }
        public static List<Model.Product> ListOfOrder = new List<Model.Product>();
    }
}
