using AiAnswerBackend.Model;

namespace AiAnswerBackend.Scoring;

public interface IScoringStrategy
{
    //评分(choices:用户的答案;app:对应的题目)
    Task<UserAnswer?> DoScore(List<string> choices, App app);
}