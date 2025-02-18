using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Order
{
    public class GetListOrderRequest : BaseRequest
    {
        public PaginationDTO Pagination { get; set; }
        public string? Name { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? OrderStatus { get; set; }
        public int? PaymentStatus { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Guid? CustomerId { get; set; }

    }
}
