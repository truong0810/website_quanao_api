using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Tag
{
    public class GetListTagResponse:BaseResponse
    {
        public List<TagDTO> Tags { get; set; }
        public PaginationDTO Pagination { get; set; }
    }
}
