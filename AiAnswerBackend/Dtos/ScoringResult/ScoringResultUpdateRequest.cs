namespace AiAnswerBackend.Dtos;

public class ScoringResultUpdateRequest
{
    //Id
    public string Id { get; set; }
    
    //结果名称
    public string ResultName { get; set; }
    
    //结果描述
    public string ResultDesc { get; set; }
    
    //结果图片
    public string ResultPicture { get; set; }
    
    //结果属性集合(JSON)
    public List<string> ResultProp { get;set;  }
    
    //结果得分范围，如 80，表示 80及以上的分数命中此结果
    public int ResultScoreRange { get; set; }
}