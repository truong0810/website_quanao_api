using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.DTO
{
    public class ViewBlogDTO
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public DateTime? BlogDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Description { get; set; }
        public Guid? BlogImageId { get; set; }
        public Guid? TagId { get; set; }
        public Guid? BlogCategoryId { get; set; }

        public BlogCategoryDTO BlogCategory { get; set; }
        public TagDTO Tag { get; set; }

    }
}
