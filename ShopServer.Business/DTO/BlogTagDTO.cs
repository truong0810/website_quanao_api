using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.DTO
{
    public class BlogTagDTO
    {
        public Guid Id { get; set; }
        public Guid? BlogId { get; set; }
        public Guid? TagId { get; set; }

        public TagDTO Tag { get; set; }
    }
}
