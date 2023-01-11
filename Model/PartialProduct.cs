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
        private bool _isImageDownloaded = false;
        public Byte[] SaveImage { get; set; } = null;

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
                if (!String.IsNullOrEmpty(MainImage) && !_isImageDownloaded)
                {
                    Byte[] _res;
                    try 
                    {
                       _res = AppData.Context.GetImage(MainImage);
                    }
                    catch
                    {
                        _res = null;
                    }
                    SaveImage = _res;
                    _isImageDownloaded =true;
                    return _res;
                }
                else 
                { 
                    return SaveImage;
                }
            } 
        }

    }
}
