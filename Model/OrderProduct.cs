using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TartugaLeksikovIzrancev.Model
{
    public class OrderProduct
    {
        public int IDOrderProduct { get; set; }
        public int IDOrder { get; set; }
        public int IDProduct { get; set; }
        public int Count { get; set; }
    }
}
