using AiAnswerBackend.Common;
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

public class UserRepository:IUserRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UserRepository(ApplicationDbContext applicationDbContext)
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

    public async Task<UserVO?> GetUserInfoByIdAsync(Guid id)
    {
       var user =  await _applicationDbContext.Users.FirstOrDefaultAsync(item => item.Id == id);
       if (user == null)
       {
           return null;
       }
       var userVO = user.ToUserVOFromUser();
       return userVO;
    }

    public async Task<PageResult<User>> GetUsersByQueryAsync(UserQueryRequest userQueryRequest)
    {
        IQueryable<User> users = _applicationDbContext.Users;

        if (!string.IsNullOrWhiteSpace(userQueryRequest.Id))
        {
            if (Guid.TryParse(userQueryRequest.Id, out Guid userId))
            {
                users = users.Where(item => item.Id == userId);
            }
        }

        if (! string.IsNullOrWhiteSpace(userQueryRequest.UserAccount))
        {
            users = users.Where(item => item.UserAccount == userQueryRequest.UserAccount);
        }
        int total = await users.CountAsync();
        int skipNums = (userQueryRequest.PageIndex - 1) * userQueryRequest.PageSize;
        users = users.Skip(skipNums).Take(userQueryRequest.PageSize);
        var userList = await users.ToListAsync();
        return new PageResult<User>(total, userList);
    }

    public async Task<bool> DeleteUserByIdAsync(Guid id)
    {
        var user = await _applicationDbContext.Users.FirstOrDefaultAsync(item => item.Id == id);
        if (user == null)
        {
            return false;
        }
        user.IsDelete = 1;
        await _applicationDbContext.SaveChangesAsync();
        return true;
    }
}