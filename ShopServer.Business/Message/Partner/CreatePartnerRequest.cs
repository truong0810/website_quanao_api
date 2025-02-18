using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Partner
{
    public class CreatePartnerRequest : BaseRequest
    {
        public PartnerDTO Partner { get; set; }
    }
}
