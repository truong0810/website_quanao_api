using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Product
{
    public class GetListFilterProductRequest : BaseRequest
    {
        public List<Guid?> Categories { get; set; }
        public List<Guid?> Brands { get; set; }
        public PaginationDTO Pagination { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}
