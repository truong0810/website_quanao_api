using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Blog
{
    public class UpdateBlogRequest : BaseRequest
    {
        public BlogDTO Blog { get; set; }
        public List<BlogTagDTO> BlogTags { get; set; }
        public List<BlogImageDTO> BlogImages { get; set; }
        public List<BlogOfCategoryDTO> BlogOfCategories { get; set;}
    }
}
