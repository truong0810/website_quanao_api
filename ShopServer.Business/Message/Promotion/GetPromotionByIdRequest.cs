using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Promotion
{
    public class GetPromotionByIdRequest : BaseRequest
    {
        public Guid Id { get; set; }
    }
}
