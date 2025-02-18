using ShopServer.Business.DTO;
using ShopServer.Business.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Brand
{
    public class GetAllBrandResponse : BaseResponse
    {
        public List<BrandDTO> Brands { get; set; }
    }
}
