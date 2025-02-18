using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class Order
    {
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? FinalAmount { get; set; }
        public int? OrderStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? Notes { get; set; }
        public int? PaymentStatus { get; set; }
        public string? Address { get; set; }
        public string? Receiver { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
