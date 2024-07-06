using AiAnswerBackend.Dtos.User;
using AiAnswerBackend.Model;
using Microsoft.AspNetCore.Mvc;

namespace AiAnswerBackend.Interfaces;

public interface IUserRepository
{
    public Task<User> UserRegisterAsync(UserRegisterRequest userRegisterRequest);

    public Task<User?> GetUserByUserAccountAsync(string userAccount);
    
}