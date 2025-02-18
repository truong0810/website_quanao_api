using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Partner
{
    public class GetPartnerByIdResponse : BaseResponse
    {
        public PartnerDTO Partner { get; set; }
        public ResourceDTO Resource { get; set; }
    }
}
