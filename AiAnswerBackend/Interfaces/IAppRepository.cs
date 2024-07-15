using AiAnswerBackend.Common;
using AiAnswerBackend.Dtos.App;
using AiAnswerBackend.Model;
using AiAnswerBackend.Vo;

namespace AiAnswerBackend.Interfaces;

public interface IAppRepository
{
    public Task<bool> AddAppAsync(App app);

    public Task<App?> GetAppByIdAsync(Guid id);

    public Task<bool> DoReviewAsync(ReviewRequest reviewRequest,Guid reviewerId);

    public Task<PageResult<App>> GetAppsByQueryAsync(AppQueryRequest appQueryRequest);

    public Task<User> GetUserByCreateUserIdAsync(Guid id);


}