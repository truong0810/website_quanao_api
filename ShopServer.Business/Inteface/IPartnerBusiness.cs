using ShopServer.Business.Message.Partner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Inteface
{
    public interface IPartnerBusiness
    {
        CreatePartnerResponse CreatePartner(CreatePartnerRequest request);
        UpdatePartnerResponse UpdatePartner(UpdatePartnerRequest request);
        DeletePartnerResponse DeletePartner(DeletePartnerRequest request);
        GetPartnerByIdResponse GetPartnerById(GetPartnerByIdRequest request);
        GetListPartnerResponse GetListPartner(GetListPartnerRequest request);
        GetAllPartnerResponse GetAllPartner(GetAllPartnerRequest request);
    }
}
