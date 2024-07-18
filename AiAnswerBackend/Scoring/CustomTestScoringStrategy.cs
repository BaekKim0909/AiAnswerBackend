using System.Text.Json;
using AiAnswerBackend.Dtos.Question;
using AiAnswerBackend.Interfaces;
using AiAnswerBackend.Model;

namespace AiAnswerBackend.Scoring;

public class CustomTestScoringStrategy:IScoringStrategy
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IScoringResultRepository _scoringResultRepository;

    public CustomTestScoringStrategy(IQuestionRepository questionRepository, IScoringResultRepository scoringResultRepository)
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
        //初始化一个
        // 2. 统计用户每个选择对应的属性个数，如 I = 10 个，E = 5 个
        // 初始化一个Map，用于存储每个选项的计数
        Dictionary<string, int> optionCount = new Dictionary<string, int>();
        //题目列表
        List<QuestionContent> questionContents =
            JsonSerializer.Deserialize<List<QuestionContent>>(question.QuestionContent);

        // 遍历题目列表
        foreach (var questionContentDTO in questionContents)
        {
            // 遍历答案列表
            foreach (var answer in choices)
            {
                // 遍历题目中的选项
                foreach (var option in questionContentDTO.Options)
                {
                    // 如果答案和选项的key匹配
                    if (option.Key.Equals(answer))
                    {
                        // 获取选项的result属性
                        string result = option.Result;

                        // 如果result属性不在optionCount中，初始化为0
                        if (!optionCount.ContainsKey(result))
                        {
                            optionCount[result] = 0;
                        }

                        // 在optionCount中增加计数
                        optionCount[result]++;
                    }
                }
            }
        }
        // 3. 遍历每种评分结果，计算哪个结果的得分更高
        // 初始化最高分数和最高分数对应的评分结果
        int maxScore = 0;
        ScoringResult maxScoringResult = scoringResultList[0];

        // 遍历评分结果列表
        foreach (var scoringResult in scoringResultList)
        {
            List<string> resultProp = JsonSerializer.Deserialize<List<string>>(scoringResult.ResultProp);
            // 计算当前评分结果的分数，[I, E] => [10, 5] => 15
            int score = resultProp.Sum(prop => optionCount.ContainsKey(prop) ? optionCount[prop] : 0);
            //比如说对于ISTP这条记录，计算optionCount中键为I，S，T，P的所有值的和  与其他人格对应的和比较，谁的分数高，就对应哪一个人格。
            // 如果分数高于当前最高分数，更新最高分数和最高分数对应的评分结果
            if (score > maxScore)
            {
                maxScore = score;
                maxScoringResult = scoringResult;
            }
        }

        // 4. 构造返回值，填充答案对象的属性
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

        return userAnswer;

    }
}