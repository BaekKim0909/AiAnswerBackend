using AiAnswerBackend.Common;
using AiAnswerBackend.Dtos.User;
using AiAnswerBackend.Model;
using AiAnswerBackend.Vo;
using Microsoft.AspNetCore.Mvc;

namespace AiAnswerBackend.Interfaces;

public interface IUserRepository
{
    public Task<User> UserRegisterAsync(UserRegisterRequest userRegisterRequest);

    public Task<User?> GetUserByUserAccountAsync(string userAccount);

    public Task<UserVO?> GetUserInfoByIdAsync(Guid id);

    //分页查询
    public Task<PageResult<User>> GetUsersByQueryAsync(UserQueryRequest userQueryRequest);
    
    //删除用户
    public Task<Boolean> DeleteUserByIdAsync(Guid id);

}