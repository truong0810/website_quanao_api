using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message
{
    public class BaseResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public Guid RecordId { get; set; }
    }
}
