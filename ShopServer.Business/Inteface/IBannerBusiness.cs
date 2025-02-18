using ShopServer.Business.Message.Banner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Inteface
{
    public interface IBannerBusiness
    {
        CreateBannerResponse CreateBanner(CreateBannerRequest request);
        UpdateBannerResponse UpdateBanner(UpdateBannerRequest request);
        DeleteBannerResponse DeleteBanner(DeleteBannerRequest request);
        GetBannerByIdResponse GetBannerById(GetBannerByIdRequest request);
        GetListBannerResponse GetListBanner(GetListBannerRequest request);
        GetAllBannerResponse GetAllBanner(GetAllBannerRequest request);
    }
}
