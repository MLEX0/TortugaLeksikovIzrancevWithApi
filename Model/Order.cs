using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TartugaLeksikovIzrancev.Model
{
    public class Order
    {
        public int IDOrder { get; set; }
        public System.DateTime OrderTime { get; set; }
        public Nullable<decimal> TotalCost { get; set; }
        public int IDEmployee { get; set; }
        public int IDRestourantTable { get; set; }
        public bool IsCashless { get; set; }
        public Nullable<int> IDPromocode { get; set; }
        public int IDStatus { get; set; }
        public bool status { get; set; }
    }
}
