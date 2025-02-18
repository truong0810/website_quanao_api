using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Tag
{
    public class GetListTagRequest :BaseRequest
    {
        public string Name { get; set; }
        public PaginationDTO Pagination { get; set; }
        
    }
}
