using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Banner
{
    public class GetAllBannerResponse : BaseResponse
    {
        public List<BannerDTO> Banners { get; set; }
    }
}
