using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TartugaLeksikovIzrancev.Classes
{
    //Класс для работы с базой данных
    class AppData
    {
        public static API.ApiController Context { get; set; } = new API.ApiController();

        public static void updateContext() {
            Context = new API.ApiController();
        }
    }
}
