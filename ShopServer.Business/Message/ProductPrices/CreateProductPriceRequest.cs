using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.ProductPrices
{
    public class CreateProductPriceRequest : BaseRequest
    {
        public ProductPriceDTO ProductPrice { get; set; }
    }
}
