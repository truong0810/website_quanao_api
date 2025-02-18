using ShopServer.Business.DTO;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.User;
using ShopServer.Business.Ultis;
using ShopServer.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Implement
{
    public class UserBusiness : IUserBusiness
    {
        private readonly ICachingBusiness _cachingBusiness;
        private string PERMISSION = "PERMISSION";

        public UserBusiness(ICachingBusiness business)
        {
            _cachingBusiness = business;
        }

        public List<PermissionDTO> GetUserPermission(Guid? authUserId)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var user = _sqlContext.Users.FirstOrDefault(u => u.Id == authUserId);
                    //if (user == null) return null;
                    //var permissionDTOs = _cachingBusiness.Get<PermissionDTO>(PERMISSION + authUserId);
                    //if (permissionDTOs != null)
                    //{
                    //    return permissionDTOs;
                    //}
                    //var userPermissions = _sqlContext.UserPermissions.Where(up => up.UserId == authUserId);
                    //var rolePermissions = _sqlContext.RolePermissions.Where(rp => rp.RoleId == user.RoleId);
                    //var permissions = _sqlContext.Permissions.Where(p => userPermissions.Any(up => up.PermissionId == p.Id) || rolePermissions.Any(rp => rp.PermissionId == p.Id));
                    //permissionDTOs = AutoMapperUtils.AutoMap<Permission, PermissionDTO>(permissions.ToList());
                    //_cachingBusiness.Upsert(PERMISSION + authUserId, permissionDTOs);
                    //return permissionDTOs;
                    return null;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
        public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _db = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var user = AutoMapperUtils.AutoMap<UserDTO, User>(request.User);
                        user.Id = Guid.NewGuid();
                        user.Password = PasswordUtils.HashPassword(request.User.Password);

                        _sqlContext.Users.Add(user);
                        _sqlContext.SaveChanges();
                        _db.Commit();
                        return new CreateUserResponse()
                        {
                            Code = 1,
                            Message = "Thành công",
                        };
                    }
                    catch (Exception e)
                    {
                        _db.Rollback();
                        return new CreateUserResponse()
                        {
                            Code = 0,
                            Message = e.Message,
                        };
                    }
                }
            }
        }
        public UpdateUserResponse UpdateUser(UpdateUserRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _db = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var user = _sqlContext.Users.FirstOrDefault(c => c.Id == request.User.Id);
                        if (user == null) throw new Exception("Không tồn tại");
                        user.Note = request.User.Note;
                        user.Email = request.User.Email;
                        user.Username = request.User.Username;
                        user.Phone = request.User.Phone;
                        user.RoleId = request.User.RoleId;
                        user.IsStaff = request.User.IsStaff;
                        user.Address = request.User.Address;
                        user.FullName = request.User.FullName;
                        //user.Password = PasswordUtils.HashPassword(request.User.Password);

                        _sqlContext.SaveChanges();
                        _db.Commit();
                        return new UpdateUserResponse()
                        {
                            Code = 1,
                            Message = "Thành công",
                        };
                    }
                    catch (Exception e)
                    {
                        _db.Rollback();
                        return new UpdateUserResponse()
                        {
                            Code = 0,
                            Message = e.Message,
                        };
                    }
                }
            }
        }
        public DeleteUserResponse DeleteUser(DeleteUserRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _db = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var user = _sqlContext.Users.FirstOrDefault(c => c.Id == request.Id);
                        if (user == null) throw new Exception("Không tồn tại");
                        _sqlContext.Users.Remove(user);
                        _sqlContext.SaveChanges();
                        _db.Commit();
                        return new DeleteUserResponse()
                        {
                            Code = 1,
                            Message = "Thành công",
                        };
                    }
                    catch (Exception e)
                    {
                        _db.Rollback();
                        return new DeleteUserResponse()
                        {
                            Code = 0,
                            Message = e.Message,
                        };
                    }
                }
            }
        }
        public GetAllUserResponse GetAllUser(GetAllUserRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var users = _sqlContext.Users.OrderBy(c=> c.Username).ToList();
                    var userDTOs = AutoMapperUtils.AutoMap<User, UserDTO>(users);
                    return new GetAllUserResponse()
                    {
                        Code = 1,
                        Message = "Thành công",
                        Users = userDTOs,
                    };
                }
                catch (Exception e)
                {
                    return new GetAllUserResponse()
                    {
                        Code = 0,
                        Message = e.Message,
                    };
                }
            }
        }
        public GetListUserResponse GetListUser(GetListUserRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var pagination = request.Pagination;
                    var users = _sqlContext.Users.Where(c =>
                    (string.IsNullOrEmpty(request.UserName) || c.Username.Contains(request.UserName))
                    ).OrderBy(obd => obd.Username);

                    pagination.Total = users.Count();
                    var listUser = users.Skip((pagination.Page - 1)* pagination.Limit).Take(pagination.Limit).ToList();
                    var userDTOs = AutoMapperUtils.AutoMap<User, UserDTO>(listUser);
                    return new GetListUserResponse()
                    {
                        Code = 1,
                        Message = "Thành công",
                        Pagination = pagination,
                        Users = userDTOs,   
                    };
                }
                catch (Exception e)
                {
                    return new GetListUserResponse()
                    {
                        Code = 0,
                        Message = e.Message,
                    };
                }
            }
        }
        public GetUserByIdResponse GetUserById(GetUserByIdRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var user = _sqlContext.Users.FirstOrDefault(c => c.Id == request.Id);
                    if (user == null) throw new Exception("Không tồn tại");
                    //user.Password = PasswordUtils.VerifyHashedPassword(user.Password);
                    var userDTO = AutoMapperUtils.AutoMap<User, UserDTO>(user);
                    return new GetUserByIdResponse()
                    {
                        Code = 1,
                        Message = "Thành công",
                        User = userDTO,
                    };
                }
                catch (Exception e)
                {
                    return new GetUserByIdResponse()
                    {
                        Code = 0,
                        Message = e.Message,
                    };
                }
            }
        }
    }
}
