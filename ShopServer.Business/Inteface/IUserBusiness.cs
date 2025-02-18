using ShopServer.Business.DTO;
using ShopServer.Business.Message.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Inteface
{
    public interface IUserBusiness
    {
        List<PermissionDTO> GetUserPermission(Guid? authUserId);
        CreateUserResponse CreateUser(CreateUserRequest request);
        UpdateUserResponse UpdateUser(UpdateUserRequest request);
        DeleteUserResponse DeleteUser(DeleteUserRequest request);   
        GetAllUserResponse GetAllUser(GetAllUserRequest request);
        GetListUserResponse GetListUser(GetListUserRequest request);
        GetUserByIdResponse GetUserById(GetUserByIdRequest request);
    }
}
