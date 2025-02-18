using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Blog
{
    public  class GetListBlogResponse:BaseResponse
    {
        public List<BlogDTO> Blogs { get; set; }
       
        public PaginationDTO Pagination { get; set; }
    }
}
