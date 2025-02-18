using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Product
{
    public class GetListProductRequest : BaseRequest
    {
        public PaginationDTO Pagination { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
