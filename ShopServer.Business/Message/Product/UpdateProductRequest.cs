using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Product
{
    public class UpdateProductRequest : BaseRequest
    {
        public ProductDTO Product { get; set; }
        public List<Guid> FileDeleted { get; set; }
    }
}
