using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class Resource
    {
        public Guid Id { get; set; }
        public string? FileName { get; set; }
        public string? Extension { get; set; }
        public string? FileType { get; set; }
        public string? FilePath { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
