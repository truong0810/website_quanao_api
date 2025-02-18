using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Partner;

namespace ShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private readonly IPartnerBusiness _business;
        public PartnerController(IPartnerBusiness business)
        {
            _business = business;
        }
        [HttpPost("create")]
        public CreatePartnerResponse CreatePartner(CreatePartnerRequest request)
        {
            return _business.CreatePartner(request);
        }
        [HttpPost("update")]
        public UpdatePartnerResponse UpdatePartner(UpdatePartnerRequest request)
        {
            return _business.UpdatePartner(request);
        }
        [HttpPost("delete")]
        public DeletePartnerResponse DeletePartner(DeletePartnerRequest request)
        {
            return _business.DeletePartner(request);
        }
        [HttpPost("get-by-id")]
        public GetPartnerByIdResponse GetPartnerById(GetPartnerByIdRequest request)
        {
            return _business.GetPartnerById(request);
        }
        [HttpPost("get-list")]
        public GetListPartnerResponse GetListPartner(GetListPartnerRequest request)
        {
            return _business.GetListPartner(request);
        }
        [HttpPost("get-all")]
        public GetAllPartnerResponse GetAllPartner(GetAllPartnerRequest request)
        {
            return _business.GetAllPartner(request);
        }
    }
}
