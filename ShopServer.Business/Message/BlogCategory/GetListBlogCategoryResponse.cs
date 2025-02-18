using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.BlogCategory
{
    public class GetListBlogCategoryResponse:BaseResponse
    {
        public List<BlogCategoryDTO> BlogCategories { get; set; }
        public PaginationDTO Pagination { get; set; }
    }
}
