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

        public int QuantityInCart { get; set; }

        public string InCart
        {
            get
            {
                if (QuantityInCart > 0)
                {
                    return "Visible";
                }
                else
                {
                    return "Hidden";
                }
            }
        }

        public Byte[] ByteImage 
        {
            get 
            {
                if (!String.IsNullOrEmpty(MainImage))
                {

                    return AppData.Context.GetImage(MainImage);
                }
                else 
                { 
                    return null;
                }
            } 
        }

    }
}
