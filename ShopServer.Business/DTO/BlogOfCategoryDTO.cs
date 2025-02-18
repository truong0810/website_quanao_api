using ShopServer.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.DTO
{
    public class BlogOfCategoryDTO
    {
        public Guid Id { get; set; }
        public Guid? BlogId { get; set; }
        public Guid? BlogCategoryId { get; set; }

        public BlogCategoryDTO BlogCategory {  get; set; }
    }
}
