using AiAnswerBackend.Data;
using AiAnswerBackend.Dtos.User;
using AiAnswerBackend.Interfaces;
using AiAnswerBackend.Mappers;
using AiAnswerBackend.Model;
using AiAnswerBackend.Vo;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AiAnswerBackend.Repository;

public class UserRepositroy:IUserRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UserRepositroy(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<User> UserRegisterAsync(UserRegisterRequest userRegisterRequest)
    {
        var user =  userRegisterRequest.ToUserFromUserRegisterRequest();
        var entityEntry =await _applicationDbContext.Users.AddAsync(user);
        await _applicationDbContext.SaveChangesAsync();
        if (entityEntry.State == EntityState.Added)
        {
            return user;
        }
        else
        {
            return null;
        }
    }

    public async Task<User?> GetUserByUserAccountAsync(string userAccount)
    {
       var user =  await _applicationDbContext.Users.FirstOrDefaultAsync(item => item.UserAccount == userAccount);
       return user;
    }

    public async Task<UserVO?> GetUserInfoById(Guid id)
    {
       var user =  await _applicationDbContext.Users.FirstOrDefaultAsync(item => item.Id == id);
       if (user == null)
       {
           return null;
       }
       var userVO = user.ToUserVOFromUser();
       return userVO;
    }
}