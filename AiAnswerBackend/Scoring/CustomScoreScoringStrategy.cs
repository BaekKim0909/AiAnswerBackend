using System.Text.Json;
using AiAnswerBackend.Dtos.Question;
using AiAnswerBackend.Interfaces;
using AiAnswerBackend.Model;
using AiAnswerBackend.Vo;

namespace AiAnswerBackend.Scoring;


//自定义得分类评分策略
public class CustomScoreScoringStrategy:IScoringStrategy
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IScoringResultRepository _scoringResultRepository;

    public CustomScoreScoringStrategy(IQuestionRepository questionRepository, IScoringResultRepository scoringResultRepository)
    {
        _questionRepository = questionRepository;
        _scoringResultRepository = scoringResultRepository;
    }

    public async Task<UserAnswer?> DoScore(List<string> choices, App app)
    {
        Guid appId = app.Id;
        //根据AppId查询到题目和题目结果信息
        var question = await _questionRepository.GetQuestionByAddIdAsync(appId);
        var scoringResultList = await _scoringResultRepository.GetScoringResultListByAppIdAsync(appId);
        //统计用户总得分
        int totalScore = 0;
        //题目列表
        List<QuestionContent> questionContents =
            JsonSerializer.Deserialize<List<QuestionContent>>(question.QuestionContent);
        if (questionContents == null)
        {
            return null;
        }
        // //遍历题目列表
        // foreach (var questionContent in questionContents)
        // {
        //     Console.WriteLine("题目："+ questionContent.Title+":");
        //     //遍历答案列表
        //     foreach (var answer in choices)
        //     {
        //         Console.WriteLine("用户答案："+answer);
        //         //遍历题目中的选项
        //         foreach (var option in questionContent.Options)
        //         {
        //             Console.WriteLine(option);
        //             //如果答案和选项的key匹配，则加分
        //             if (option.Key.Equals(answer))
        //             {
        //                 totalScore += option.Score; 
        //                 Console.WriteLine("得分："+option.Score + "总分:"+totalScore);
        //             }
        //         }
        //     }
        // }

        int index = 0;
        foreach (var questionContent in questionContents)
        {
            foreach (var option in questionContent.Options)
            {
                if (option.Key.Equals(choices[index]))
                {
                    totalScore += option.Score; 
                }
            }
            index++;
        }
        // 遍历得分结果，找到第一个用户分数大于得分范围的结果，作为最终结果
        ScoringResult maxScoringResult = scoringResultList[0];
        foreach (var scoringResult in scoringResultList)
        {
            if (totalScore >= scoringResult.ResultScoreRange)
            {
                maxScoringResult = scoringResult;
                break;
            }
        }
        //构造返回值，填充答案对象的属性
        UserAnswer userAnswer = new UserAnswer();
        userAnswer.AppId = appId;
        userAnswer.AppType = app.AppType;
        userAnswer.ScoreStrategy = app.ScoreStrategy;
        userAnswer.Choices = JsonSerializer.Serialize(choices);
        userAnswer.ResultId = maxScoringResult.Id;
        userAnswer.ResultName = maxScoringResult.ResultName;
        userAnswer.ResultDesc = maxScoringResult.ResultDesc;
        userAnswer.ResultPicture = maxScoringResult.ResultPicture;
        userAnswer.ResultScore = totalScore;
        
        return userAnswer;
    }
}