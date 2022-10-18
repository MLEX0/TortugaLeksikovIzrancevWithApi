using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TartugaLeksikovIzrancev.Model
{
    public partial class Product
    {
        public int IDProduct { get; set; }
        public string ProductName { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
        public string Composition { get; set; }
        public int IDCategory { get; set; }
        public string MainImage { get; set; }
    }
}
