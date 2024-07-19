namespace AiAnswerBackend.Vo;

public class UserAnswerVO
{
    public string Id { get; set; }

    //应用ID
    public string AppId { get; set; }
    
    //应用类型  0--得分类  1--测试类
    public byte AppType { get; set; }
    
    //评分策略  0--自定义 1--AI
    public byte ScoreStrategy { get; set; }
    
    //用户答案(JSON数组)
    public List<string> Choices { get; set; }
    
    //评分结果ID
    public string ResultId { get; set; }
    
    //结果名称
    public string ResultName { get; set; }
    
    //结果描述
    public string ResultDesc { get; set; }
    
    //结果图片
    public string ResultPicture { get; set; }
    
    //结果得分
    public int ResultScore { get; set; }
    
    //创建用户ID
    public string UserId { get;set;  }
    
    //创建时间
    public DateTime CreateTime { get; set; }
    
    //更新时间
    public DateTime UpdateTime { get; set; } 
    
    //创建用户信息
    public UserVO? User { get; set; }
}