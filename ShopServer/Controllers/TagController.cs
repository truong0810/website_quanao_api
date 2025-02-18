using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopServer.Attribute;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Tag;

namespace ShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagBusiness _business;
        public TagController(ITagBusiness business)
        {
            _business = business;
        }
        [HttpPost("create")]
        public CreateTagResponse CreateTag(CreateTagRequest request)
        {
            return _business.CreateTag(request);
        }
        [HttpPost("update")]
        public UpdateTagResponse UpdateTag(UpdateTagRequest request)
        {
            return _business.UpdateTag(request);
        }
        [HttpPost("delete")]
        public DeleteTagResponse DeleteTag(DeleteTagRequest request)
        {
            return _business.DeleteTag(request);
        }
        [HttpPost("get-list")]
        public GetListTagResponse GetListTag(GetListTagRequest request)
        {
            return _business.GetListTag(request);
        }
        [HttpPost("get-by-id")]
        public GetTagByIdResponse GetTagById(GetTagByIdRequest request)
        {
            return _business.GetTagById(request);
        }
        [HttpPost("get-all")]
        public GetAllTagResponse GetAllTag(GetAllTagRequest request)
        {
            return _business.GetAllTag(request);
        }
    }
}
