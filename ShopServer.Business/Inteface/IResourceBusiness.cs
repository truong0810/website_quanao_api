using ShopServer.Business.Message.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Inteface
{
    public interface IResourceBusiness
    {
        Task<UploadFileResponse> UploadFile(UploadFileRequest request);
        DeleteFileResponse DeleteFile(DeleteFileRequest request);
        GetLinkImageResponse GetLink(string Id);
        DownLoadFileResponse DownLoadFile(DownLoadFileRequest request);
    }
}
