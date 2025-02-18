using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Blog
{
    public class GetListBlogRequest : BaseRequest
    {
        public Guid? BlogCategoryId { get; set; }
        public Guid? BlogTagId { get; set; }
        public string Title { get; set; }
        public PaginationDTO Pagination { get; set; }
       
    }
}
