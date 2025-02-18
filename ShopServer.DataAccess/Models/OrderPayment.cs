using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class OrderPayment
    {
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int? PaymentMethod { get; set; }
        public string? Notes { get; set; }
        public string? PayerName { get; set; }
    }
}
