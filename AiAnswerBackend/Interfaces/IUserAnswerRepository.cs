using AiAnswerBackend.Model;

namespace AiAnswerBackend.Interfaces;

public interface IUserAnswerRepository
{
    public Task<bool> AddUserAnswerAsync(UserAnswer userAnswer);
}