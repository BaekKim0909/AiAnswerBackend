using AiAnswerBackend.Model;

namespace AiAnswerBackend.Interfaces;

public interface IUserAnswerRepository
{
    //添加UserAnswer
    public Task<bool> AddUserAnswerAsync(UserAnswer userAnswer);
    //根据Id查找UserAnswer
    public Task<UserAnswer?> GetUserAnswerByIdAsync(Guid id);
}