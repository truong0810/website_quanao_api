using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.BlogCategory
{
    public class CreateBlogCategoryRequest:BaseRequest
    {
        public BlogCategoryDTO BlogCategory { get; set; }
    }
}
