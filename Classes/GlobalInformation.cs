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
        public static int SelectedTableNumber { get; set; }
        public static List<Model.Product> Cart { get; set; } = new List<Model.Product>();

        public static decimal GlobalDiscount
        {
            get
            {
                if (IsWhiteSaturday())
                {
                    DiscountReason = "Белая суббота";
                    return 0.11M;
                }
                else
                {
                    return 0;
                }
            }
        }

        public static string DiscountReason = "";

        //Скидка белой субботы
        private static bool IsWhiteSaturday()
        {
            if (WeekNumber(DateTime.Now) == 5 && DateTime.Now.DayOfWeek == DayOfWeek.Saturday) 
            {
                return true;
            }
            return false;
        }

        public static int WeekNumber(DateTime TargetDate)
        {
            return (TargetDate.Day - 1) / 7 + 1;
        }

        public static decimal AllCostWIthDIscount(decimal cost, DateTime nowDate) 
        {
            if (WeekNumber(nowDate) == 5 && nowDate.DayOfWeek == DayOfWeek.Saturday)
            {
                return cost - cost * 0.11M;
            }
            return cost;
        }

    }
}
