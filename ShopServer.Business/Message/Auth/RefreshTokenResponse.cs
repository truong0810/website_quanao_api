﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Auth
{
    public class RefreshTokenResponse : BaseResponse
    {
        public string Token { get; set; }
    }
}
