using ShopServer.Business.Message.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Inteface
{
    public interface IAuthBusiness
    {
        ChangePasswordResponse ChangePassword(ChangePasswordRequest request);
        LoginResponse Login(LoginRequest request);
        RefreshTokenResponse RefreshToken(RefreshTokenRequest request);
        LoginIsStaffResponse LoginIsStaff(LoginIsStaffRequest request);
        RegisterResponse Register(RegisterRequest request);
    }
}
