using AiAnswerBackend.Data;
using AiAnswerBackend.Interfaces;
using AiAnswerBackend.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AiAnswerBackend.Repository;

public class QuestionRepository:IQuestionRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public QuestionRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<bool> AddQuestionAsync(Question question)
    {
        EntityEntry entityEntry = await _applicationDbContext.Questions.AddAsync(question);
        var result = await _applicationDbContext.SaveChangesAsync();
        if (result > 0)
        {
            return true;
        }

        return false;
    }

    public async Task<Question?> GetQuestionByAddIdAsync(Guid appId)
    {
        var question = await _applicationDbContext.Questions.FirstOrDefaultAsync(item => item.AppId == appId);
        return question;
    }

    public async Task<Question?> GetQuestionByIdAsync(Guid id)
    {
        var question = await _applicationDbContext.Questions.FirstOrDefaultAsync(item => item.Id == id);
        return question;
    }

    public async Task<bool> UpdateQuestionAsync(Question newQuestion)
    {
        var question =  await _applicationDbContext.Questions.FirstOrDefaultAsync(item => item.Id == newQuestion.Id);
        if (question != null)
        {
            question.QuestionContent = newQuestion.QuestionContent;
            question.UpdateTime = DateTime.Now;
        }
        int result = await _applicationDbContext.SaveChangesAsync();
        if (result > 0)
        {
            return true;
        }
        return false;

    }
}