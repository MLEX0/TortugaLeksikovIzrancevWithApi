using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TartugaLeksikovIzrancev.Pages;

namespace TartugaLeksikovIzrancev.Classes
{
    //Класс для работы с страницами
    public class PageController
    {
       public static Frame MainFrame { get; set; } = new Frame();
       public static MenuPage StaticMenu { get; set; }
    }
}
