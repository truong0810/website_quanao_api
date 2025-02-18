using Microsoft.AspNetCore.Mvc;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Auth;

namespace ShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBusiness _business;

        public AuthController(IAuthBusiness business)
        {
            _business = business;
        }
        [HttpPost("login")]
        public LoginResponse Login(LoginRequest request)
        {
            var response = _business.Login(request);
            return response;
        }
        [HttpPost,Route("Change-Password")]
        public ChangePasswordResponse ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var response = _business.ChangePassword(request);
            return response;
        }
        [HttpPost("refresh-token")]
        public RefreshTokenResponse RefreshToken(RefreshTokenRequest request)
        {
            request.Token = Request.Headers["Authorization"];
            request.Token = request.Token.Substring("Bearer".Length);
            return _business.RefreshToken(request);
        }
        [HttpPost("login-isstaff")]
        public LoginIsStaffResponse LoginIsStaff(LoginIsStaffRequest request)
        {
            return _business.LoginIsStaff(request);
        }
        [HttpPost("register")]
        public RegisterResponse Register(RegisterRequest request)
        {
            return _business.Register(request);
        }
    }
}
