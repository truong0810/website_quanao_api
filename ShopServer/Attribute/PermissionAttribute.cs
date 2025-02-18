using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message;
using ShopServer.DataAccess.Models;

namespace ShopServer.Attribute
{
    public class PermissionAttribute : ActionFilterAttribute
    {
        IUserBusiness _userBusiness;
        private readonly string _code;

        public PermissionAttribute(IUserBusiness userBusiness, string code)
        {
            _userBusiness = userBusiness;
            _code = code;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                var model = filterContext.ActionArguments["request"] as BaseRequest;
                var permissions = _userBusiness.GetUserPermission(model.AuthUserId);
                if (permissions == null)
                {
                    filterContext.HttpContext.Response.StatusCode = 403;
                    filterContext.Result = new JsonResult(new BaseResponse()
                    {
                        Code = 403
                    });
                }
                else if (permissions.Any(p => p.Code == _code))
                {
                    filterContext.HttpContext.Response.StatusCode = 200;
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = 403;
                    filterContext.Result = new JsonResult(new BaseResponse()
                    {
                        Code = 403,
                    });
                }
            }
        }
    }
}
