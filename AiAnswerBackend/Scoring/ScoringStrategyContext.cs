using AiAnswerBackend.Model;

namespace AiAnswerBackend.Scoring;

public class ScoringStrategyContext
{
    private readonly CustomScoreScoringStrategy _customScoreScoringStrategy;
    private readonly CustomTestScoringStrategy _customTestScoringStrategy;

    public ScoringStrategyContext(CustomScoreScoringStrategy customScoreScoringStrategy, CustomTestScoringStrategy customTestScoringStrategy)
    {
        _customScoreScoringStrategy = customScoreScoringStrategy;
        _customTestScoringStrategy = customTestScoringStrategy;
    }

    public async Task<UserAnswer?> DoScore(List<String> choiceList, App app)
    {
        byte appType = app.AppType;
        byte scoreStrategy = app.ScoreStrategy;
        switch (appType)
        {
            case 0:
                switch (scoreStrategy)
                {
                    case 0:
                        return await _customScoreScoringStrategy.DoScore(choiceList, app);
                    case 1:
                        break;
                }
                break;
            case 1:
                switch (scoreStrategy)
                {
                    case 0 :
                        return await _customTestScoringStrategy.DoScore(choiceList, app);
                    case 1 :
                        break;
                }
                break;
        }
        return null;
    }
}