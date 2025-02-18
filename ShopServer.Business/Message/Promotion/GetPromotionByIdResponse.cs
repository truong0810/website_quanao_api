using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Promotion
{
    public class GetPromotionByIdResponse : BaseResponse
    {
        public PromotionDTO Promotion { get; set; }
        public List<ResourceDTO> Images { get; set; }
    }
}
