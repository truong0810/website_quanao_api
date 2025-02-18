using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.DTO
{
    public class BlogDTO
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public DateTime? BlogDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Description { get; set; }

        public List<TagDTO> Tags { get; set; }
        public List<BlogCategoryDTO> BlogCategories { get; set;}
        public List<BlogImageDTO> BlogImages { get; set;}
    }
}
