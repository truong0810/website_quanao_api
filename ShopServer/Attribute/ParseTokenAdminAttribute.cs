using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopServer.Business.DTO;
using ShopServer.Business.Message;
using ShopServer.Business.Ultis;
using ShopServer.Business.Ultis.Jwt;
using ShopServer.DataAccess.Models;


namespace ShopServer.Attribute
{
    public class ParseTokenAdminAttribute : ActionFilterAttribute
    {
        public ParseTokenAdminAttribute()
        {

        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                var model = filterContext.ActionArguments["request"] as BaseRequest;
                filterContext.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorization);
                if (authorization.Count == 0 || authorization == "")
                {
                    filterContext.HttpContext.Response.StatusCode = 401;
                    filterContext.Result = new JsonResult(new BaseResponse() { Code = 401 });
                }
                else
                {
                    var token = authorization.FirstOrDefault().Substring("Bearer ".Length);
                    if (token.Length > 20)
                    {
                        var tokenIsValid = TokenProvider.TokenValidate(token, out var data);
                        double datetimeNow = DateTime.Now.Subtract(DateTime.UnixEpoch).TotalSeconds;
                        if (data.AuthUserId != null
                            && _sqlContext.Users.Any(u => u.Id == data.AuthUserId)
                            && data.Expire >= datetimeNow
                            && _sqlContext.RefreshTokens.Any(t => t.Token == token && t.UpdatePassword == null)
                            )
                        {
                            model.AuthUserId = data.AuthUserId;
                            model.AuthUsername = data.AuthUsername;
                            var user = _sqlContext.Users.FirstOrDefault(u => u.Id == data.AuthUserId);
                            model.AuthUser = AutoMapperUtils.AutoMap<User, UserDTO>(user);
                            filterContext.HttpContext.Response.StatusCode = 200;
                            base.OnActionExecuting(filterContext);
                        }else
                        {
                            filterContext.HttpContext.Response.StatusCode = 401;
                            filterContext.Result = new JsonResult(new BaseResponse()
                            {
                                Code = 401
                            });
                        }
                    }else
                    {
                        filterContext.HttpContext.Response.StatusCode = 401;
                        filterContext.Result = new JsonResult(new BaseResponse()
                        {
                            Code = 401
                        });
                    }
                }
            }
        }
    }
}
