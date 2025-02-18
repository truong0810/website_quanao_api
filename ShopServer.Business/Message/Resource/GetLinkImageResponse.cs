using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Resource
{
    public class GetLinkImageResponse : BaseResponse
    {
        public byte[] File { get; set; }
    }
}