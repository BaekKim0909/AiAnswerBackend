using AiAnswerBackend.Data;
using AiAnswerBackend.Interfaces;
using AiAnswerBackend.Model;
using Microsoft.EntityFrameworkCore;

namespace AiAnswerBackend.Repository;

public class UserAnswerRepository:IUserAnswerRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UserAnswerRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<bool> AddUserAnswerAsync(UserAnswer userAnswer)
    {
        await _applicationDbContext.UserAnswers.AddAsync(userAnswer);
        int result = await _applicationDbContext.SaveChangesAsync();
        if (result > 0)
        {
            return true;
        }

        return false;
    }

    public async Task<UserAnswer?> GetUserAnswerByIdAsync(Guid id)
    {
        var userAnswer = await _applicationDbContext.UserAnswers.FirstOrDefaultAsync(item => item.Id == id);
        return userAnswer;
    }
}