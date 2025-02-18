using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? FinalAmount { get; set; }
        public int? OrderStatus { get; set; }
        public string? Notes { get; set; }
        public int? PaymentStatus { get; set; }
        public string? Address { get; set; }
        public string? Receiver { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public UserDTO User { get; set; }   
    }
}
