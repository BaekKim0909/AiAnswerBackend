using AiAnswerBackend.Dtos.App;
using AiAnswerBackend.Model;
using AiAnswerBackend.Vo;

namespace AiAnswerBackend.Mappers;

public static class AppMapper
{
    public static App ToAppFromAppAddRequest(this AppAddRequest appAddRequest)
    {
        return new App()
        {
            AppName = appAddRequest.AppName,
            AppDesc = appAddRequest.AppDesc,
            AppType = appAddRequest.AppType,
            AppIcon = appAddRequest.AppIcon,
            ScoreStrategy = appAddRequest.ScoringStrategy,
            CreateTime = DateTime.Now,
            ReviewerId = null,
            ReviewTime = null,
            UpdateTime = DateTime.Now
        };
    }

    public static AppVO ToAppVOFromApp(this App app)
    {
        return new AppVO()
        {
            Id = app.Id,
            AppName = app.AppName,
            AppDesc = app.AppDesc,
            AppIcon = app.AppIcon,
            AppType = app.AppType,
            ScoringStrategy = app.ScoreStrategy,
            ReviewMessage = app.ReviewMessage,
            ReviewStatus = app.ReviewStatus,
            ReviewerId = app.ReviewerId,
            ReviewTime = app.ReviewTime,
            CreateTime = app.CreateTime,
            UpdateTime = app.CreateTime,
            CreateUserId = app.CreateUserId,
            UserVo = null
        };
    }
}