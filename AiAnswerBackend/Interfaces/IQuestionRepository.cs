using AiAnswerBackend.Model;

namespace AiAnswerBackend.Interfaces;

public interface IQuestionRepository
{
    //添加问题
    public Task<bool> AddQuestionAsync(Question question);
    
    //通过AppID查询Question
    public Task<Question?> GetQuestionByAddIdAsync(Guid appId);
    
    //通过ID查询Question
    public Task<Question?> GetQuestionByIdAsync(Guid id);
    
    //修改问题
    public Task<bool> UpdateQuestionAsync(Question newQuestion);
}