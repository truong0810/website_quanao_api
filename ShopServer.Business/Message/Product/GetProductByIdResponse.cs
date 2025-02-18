using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Product
{
    public class GetProductByIdResponse : BaseResponse
    {
        public ProductDTO Product { get; set; }
        public List<ResourceDTO> Images { get; set; }
        public List<ProductDTO> RelatedProducts { get; set; }
    }
}
