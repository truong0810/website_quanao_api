using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Resource
{
    public class UploadFileResponse : BaseResponse
    {
        public ResourceDTO Resource { get; set; }
    }
}
