using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Product
{
    public class GetSearchProductsResponse : BaseResponse
    {
        public PaginationDTO Pagination { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
