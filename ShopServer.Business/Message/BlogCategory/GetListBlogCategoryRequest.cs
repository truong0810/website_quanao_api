using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.BlogCategory
{
    public class GetListBlogCategoryRequest:BaseRequest
    {
        public string? Name {  get; set; }
        public string? Description { get; set; }
        public PaginationDTO Pagination { get; set; }
    }
}
