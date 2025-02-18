using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Promotion
{
    public class GetListPromotionResponse : BaseResponse
    {
        public List<PromotionDTO> Promotions { get; set; }
        public PaginationDTO Pagination { get; set; }
    }
}
