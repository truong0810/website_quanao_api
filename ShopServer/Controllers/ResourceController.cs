using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopServer.Attribute;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Resource;

namespace ShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceBusiness _business;
        public ResourceController(IResourceBusiness business)
        {
            _business = business;
        }
        [HttpPost("upload")]
        public Task<UploadFileResponse> UploadFile([FromForm] UploadFileRequest request)
        {
            var upload = _business.UploadFile(request);
            return upload;
        }
        [HttpPost("delete")]
        public DeleteFileResponse DeleteFile(DeleteFileRequest request)
        {
            var delete = _business.DeleteFile(request);
            return delete;
        }
        [HttpGet("get-link-image/{id}")]
        public IActionResult GetLinkImage([FromRoute] string Id)
        {
            var response = _business.GetLink(Id);
            FileStreamResult imageLink = null;
            if (response.Code == 1)
            {
                imageLink = File(new MemoryStream(response.File), "image/jpeg");
            }
            return imageLink;
        }
        [HttpPost("download-file")]
        public IActionResult DownLoadFile([FromBody] DownLoadFileRequest request)
        {
            var response = _business.DownLoadFile(request);
            var filename = response.Resource.FileName;
            var net = new System.Net.WebClient();
            var data = net.DownloadData(response.Resource.FilePath);
            var content = new MemoryStream(data);
            var contentType = "APPLICATION/octet-stream";
            return File(content, contentType, filename);
        }
    }
}