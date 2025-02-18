using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Blog
{
    public class GetBlogByIdRequest:BaseRequest
    {
        public Guid? Id { get; set; }   
    }
}
