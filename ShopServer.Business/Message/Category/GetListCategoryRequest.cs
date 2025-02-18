
using ShopServer.Business.DTO;
using ShopServer.Business.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Category
{
    public class GetListCategoryRequest : BaseRequest
    {
        public string Name { get; set; }
        public PaginationDTO Pagination { get; set; }
    }
}
