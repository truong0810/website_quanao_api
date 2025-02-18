using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.DTO
{
    public class BlogImageDTO
    {
        public Guid Id { get; set; }
        public Guid? BlogId { get; set; }
        public Guid? BlogImageId { get; set; }
        public decimal? SortIndex { get; set; }

        public ResourceDTO Resource { get; set; }
    }
}
