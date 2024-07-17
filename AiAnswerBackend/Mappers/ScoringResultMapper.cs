using System.Text.Json;
using AiAnswerBackend.Model;
using AiAnswerBackend.Vo;

namespace AiAnswerBackend.Mappers;

public static class ScoringResultMapper
{
    public static ScoringResultVO ToScoringResultVoFromScoringResult(this ScoringResult scoringResult)
    {
        return new ScoringResultVO()
        {
            Id = scoringResult.Id.ToString(),
            ResultName = scoringResult.ResultName,
            ResultDesc = scoringResult.ResultDesc,
            ResultPicture = scoringResult.ResultPicture,
            ResultProp = JsonSerializer.Deserialize<List<string>>(scoringResult.ResultProp),
            ResultScoreRange = scoringResult.ResultScoreRange,
            AppId = scoringResult.AppId.ToString(),
            UserId = scoringResult.UserId,
            CreateTime = scoringResult.CreateTime,
            UpdateTime = scoringResult.UpdateTime
        };
    }
}