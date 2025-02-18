using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.DTO
{
    public class PaginationDTO
    {
        public PaginationDTO()
        {
            Limit = 10;
            Page = 1;
            Total = 0;
        }

        public int Limit { get; set; }
        public int Page { get; set; }
        public int Total { get; set; }
    }
}
