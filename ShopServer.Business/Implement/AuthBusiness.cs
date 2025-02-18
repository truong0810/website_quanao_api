using Microsoft.Extensions.Configuration;
using ShopServer.Business.DTO;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Auth;
using ShopServer.Business.Ultis;
using ShopServer.Business.Ultis.Jwt;
using ShopServer.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static ShopServer.Business.Ultis.Constant;


namespace ShopServer.Business.Implement
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly IConfiguration _configuration;
        private readonly IUserBusiness _userBusiness;

        public AuthBusiness(IConfiguration configuration, IUserBusiness userBusiness)
        {
            _configuration = configuration;
            _userBusiness = userBusiness;
        }

        public ChangePasswordResponse ChangePassword(ChangePasswordRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _dbTransaction = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var user = _sqlContext.Users.FirstOrDefault(u => u.Id == request.UserId);
                        if (user == null || !PasswordUtils.VerifyHashedPassword(user.Password, request.Password))
                            throw new Exception("Mật khẩu không chính xác!");
                        user.Password = PasswordUtils.HashPassword(request.NewPassword);

                        var now = DateTime.Now;
                        var oldToken = _sqlContext.RefreshTokens.Where(t => t.UserId == user.Id && t.TokenCreated <= now).ToList();
                        oldToken.ForEach(t => t.UpdatePassword = now);
                        _sqlContext.SaveChanges();
                        _dbTransaction.Commit();
                        return new ChangePasswordResponse()
                        {
                            Code = 1,
                            Message = "Đổi mật khẩu thành công"
                        };
                    }
                    catch (Exception e)
                    {
                        _dbTransaction.Rollback();
                        return new ChangePasswordResponse()
                        {
                            Code = 0,
                            Message = e.Message
                        };
                    }
                }
            }
        }

        public LoginResponse Login(LoginRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                string secketKey = _configuration["secret-key-name"];
                string issuer = _configuration["token-valid-issuer"];
                string audience = _configuration["token-valid-audience"];
                int expire = Int32.Parse(_configuration["token-expire"]);
                var user = _sqlContext.Users.FirstOrDefault(u => u.Username == request.Username);
                if (user == null || !PasswordUtils.VerifyHashedPassword(user.Password, request.Password))
                {
                    throw new Exception("Tài khoản hoặc mật khẩu không chính xác");
                }
                JwtToken token = new JwtTokenBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create(secketKey))
                    .AddSubject(user.Username)
                    .AddIssuer(issuer)
                    .AddAudience(audience)
                    .AddClaim(AuthKey.USER_ID, user.Id.ToString())
                    .AddClaim(AuthKey.USER_NAME, user.Username)
                    .AddExpiry(expire)
                    .Build();
                // gen refresh token
                var refreshToken = new RefreshToken
                {
                    Id = Guid.NewGuid(),
                    RefreshCode = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                    RefreshCodeExpire = DateTime.Now.AddDays(7),
                    RefreshCodeCreated = DateTime.Now,
                    Token = token.Value,
                    TokenCreated = DateTime.Now,
                    UserId = user.Id
                };
                _sqlContext.RefreshTokens.Add(refreshToken);
                _sqlContext.SaveChanges();

                var userDTO = AutoMapperUtils.AutoMap<User, UserDTO>(user);
                return new LoginResponse()
                {
                    Code = 1,
                    Message = "Đăng nhập thành công",
                    User = userDTO,
                    Token = token.Value,
                    RefreshToken = refreshToken.RefreshCode
                };

            }
            catch (Exception e)
            {
                return new LoginResponse()
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }
        public RefreshTokenResponse RefreshToken(RefreshTokenRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var refreshToken = _sqlContext.RefreshTokens.FirstOrDefault(r => r.Token == request.Token && r.RefreshCode == request.RefreshToken);
                    if (refreshToken == null || refreshToken.RefreshCodeExpire <= DateTime.Now || refreshToken.UpdatePassword != null) throw new Exception("refresh token invalid");

                    string secretKey = _configuration["secret-key-name"];
                    string issuer = _configuration["token-valid-issuer"];
                    string audience = _configuration["token-valid-audience"];
                    int expire = Int32.Parse(_configuration["token-expire"]);
                    var user = _sqlContext.Users.FirstOrDefault(u => u.Id == refreshToken.UserId);


                    JwtToken token = new JwtTokenBuilder()
                        .AddSecurityKey(JwtSecurityKey.Create(secretKey))
                        .AddSubject(user.Username)
                        .AddIssuer(issuer)
                        .AddAudience(audience)
                        .AddClaim(AuthKey.USER_ID, user.Id.ToString())
                        .AddClaim(AuthKey.USER_NAME, user.Username)
                        .AddExpiry(expire)
                        .Build();

                    refreshToken.Token = token.Value;
                    refreshToken.TokenCreated = DateTime.Now;

                    _sqlContext.SaveChanges();
                    return new RefreshTokenResponse()
                    {
                        Code = 1,
                        Message = "thành công",
                        Token = token.Value,
                    };
                }
                catch (Exception e)
                {
                    return new RefreshTokenResponse()
                    {
                        Code = 0,
                        Message = e.Message
                    };
                }
            }
        }
        public LoginIsStaffResponse LoginIsStaff(LoginIsStaffRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                string secketKey = _configuration["secret-key-name"];
                string issuer = _configuration["token-valid-issuer"];
                string audience = _configuration["token-valid-audience"];
                int expire = Int32.Parse(_configuration["token-expire"]);
                var user = _sqlContext.Users.FirstOrDefault(u => u.Username == request.Username);
                if (user == null || !PasswordUtils.VerifyHashedPassword(user.Password, request.Password))
                {
                    throw new Exception("Tài khoản hoặc mật khẩu không chính xác");
                }
                if (user.IsStaff == false || user.IsStaff == null)
                {
                    throw new Exception("Tài khoản này không có quyền truy cập");
                }
                JwtToken token = new JwtTokenBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create(secketKey))
                    .AddSubject(user.Username)
                    .AddIssuer(issuer)
                    .AddAudience(audience)
                    .AddClaim(AuthKey.USER_ID, user.Id.ToString())
                    .AddClaim(AuthKey.USER_NAME, user.Username)
                    .AddExpiry(expire)
                    .Build();
                // gen refresh token
                var refreshToken = new RefreshToken
                {
                    Id = Guid.NewGuid(),
                    RefreshCode = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                    RefreshCodeExpire = DateTime.Now.AddDays(7),
                    RefreshCodeCreated = DateTime.Now,
                    Token = token.Value,
                    TokenCreated = DateTime.Now,
                    UserId = user.Id
                };
                _sqlContext.RefreshTokens.Add(refreshToken);
                _sqlContext.SaveChanges();

                var userDTO = AutoMapperUtils.AutoMap<User, UserDTO>(user);
                return new LoginIsStaffResponse()
                {
                    Code = 1,
                    Message = "Đăng nhập thành công",
                    User = userDTO,
                    Token = token.Value,
                    RefreshToken = refreshToken.RefreshCode
                };

            }
            catch (Exception e)
            {
                return new LoginIsStaffResponse()
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }
        public RegisterResponse Register(RegisterRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _db = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var userCheck = _sqlContext.Users.FirstOrDefault(c => c.Username == request.User.Username);
                        if(userCheck != null)
                        {
                            throw new Exception("Tài khoản này đã tồn tại, xin mời nhập tên tài khoản khác!");
                        }
                        var user = AutoMapperUtils.AutoMap<UserDTO, User>(request.User);
                        user.Id = Guid.NewGuid();
                        user.Username = request.User.Username;
                        user.Password = PasswordUtils.HashPassword(request.User.Password);

                        _sqlContext.Users.Add(user);
                        _sqlContext.SaveChanges();
                        _db.Commit(); 
                        return new RegisterResponse()
                        {
                            Code = 1,
                            Message = "Thành công",
                        };
                    }
                    catch (Exception e)
                    {
                        _db.Rollback();
                        return new RegisterResponse()
                        {
                            Code = 0,
                            Message = e.Message
                        };
                    }
                }
            }
        }
    }
}
