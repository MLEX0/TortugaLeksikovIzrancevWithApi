using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using TartugaLeksikovIzrancev.Classes;

namespace TartugaLeksikovIzrancev.Model
{
    public partial class Product
    {
        public int OrderProdCount
        {
            get
            {
                int res = GlobalInformation.ListOfOrder.Count(i => i.ProductName == ProductName);
                return res;
            }

        }
        public decimal OrderTotalCostOfProduct
        {
            get
            {
                decimal res = GlobalInformation.ListOfOrder.Count(i => i.ProductName == ProductName) * Cost;
                return res;
            }
        }
    }
}
