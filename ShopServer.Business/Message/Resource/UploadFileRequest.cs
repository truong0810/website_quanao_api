using Microsoft.AspNetCore.Http;
using ShopServer.Business.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Resource
{
    public class UploadFileRequest : BaseRequest
    {
        public IFormFile File { get; set; }
        public Guid? Id { get; set; }
    }
}
