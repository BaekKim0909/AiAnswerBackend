using System.Text.Json;
using AiAnswerBackend.Model;
using AiAnswerBackend.Vo;

namespace AiAnswerBackend.Mappers;

public static class UserAnswerMapper
{
    public static UserAnswerVO ToVoFromUserAnswer(this UserAnswer userAnswer)
    {
        return new UserAnswerVO()
        {
            Id = userAnswer.Id.ToString(),
            AppId = userAnswer.AppId.ToString(),
            AppType = userAnswer.AppType,
            ScoreStrategy = userAnswer.ScoreStrategy,
            Choices = JsonSerializer.Deserialize<List<string>>(userAnswer.Choices),
            ResultId = userAnswer.ResultId.ToString(),

            ResultName = userAnswer.ResultName,

            ResultDesc = userAnswer.ResultDesc,

            ResultPicture = userAnswer.ResultPicture,

            ResultScore = userAnswer.ResultScore,

            UserId = userAnswer.UserId.ToString(),

            CreateTime = userAnswer.CreateTime,

            UpdateTime = userAnswer.UpdateTime,
            User = null
        };
    }
}