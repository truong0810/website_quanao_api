using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Tag
{
    public class GetTagByIdResponse:BaseResponse
    {
        public TagDTO Tag { get; set; }
    }
}
