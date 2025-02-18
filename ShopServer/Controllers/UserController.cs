using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.User;

namespace ShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _business;
        public UserController(IUserBusiness business)
        {
            _business = business;
        }
        [HttpPost("create")]
       public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            return _business.CreateUser(request);
        }
        [HttpPost("update")]
        public UpdateUserResponse UpdateUser(UpdateUserRequest request)
        {
            return _business.UpdateUser(request);
        }
        [HttpPost("delete")]
        public DeleteUserResponse DeleteUser(DeleteUserRequest request)
        {
            return _business.DeleteUser(request);
        }
        [HttpPost("get-all")]
        public GetAllUserResponse GetAllUser(GetAllUserRequest request)
        {
            return _business.GetAllUser(request);
        }
        [HttpPost("get-list")]
        public GetListUserResponse GetListUser(GetListUserRequest request)
        {
            return _business.GetListUser(request);
        }
        [HttpPost("get-by-id")]
        public GetUserByIdResponse GetUserById(GetUserByIdRequest request)
        {
            return _business.GetUserById(request);
        }
    }
}
