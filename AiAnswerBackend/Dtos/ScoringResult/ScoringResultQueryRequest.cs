using AiAnswerBackend.Common;

namespace AiAnswerBackend.Dtos;

public class ScoringResultQueryRequest:PageRequest
{
    //评分结果对应的APP
    public Guid? AppId { get; set; }

    //当前用户ID
    public Guid? UserId { get; set; }
}