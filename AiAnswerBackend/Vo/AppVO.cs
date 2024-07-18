namespace AiAnswerBackend.Vo;

public class AppVO
{
    //应用Id
    public Guid Id { get; set; }

    //应用名称
    public string AppName { get; set; }

    //应用描述
    public string? AppDesc { get; set; }
    
    //应用图标
    public string? AppIcon { get; set; }
    
    //应用类型  0--得分类  1--测试类
    public byte AppType { get;set; }
    
    //评分策略  0--自定义 1--AI
    public byte ScoringStrategy { get; set; }
    
    //审核状态  0--待审核 1--通过 2--拒绝
    public byte ReviewStatus { get; set; }
    
    //审核信息
    public string? ReviewMessage { get; set; }
    
    //审核人Id
    public Guid? ReviewerId { get; set; }
    
    //审核时间
    public DateTime? ReviewTime { get; set; }
    
    //创建用户
    public Guid CreateUserId { get; set; }
    
    //创建时间
    public DateTime CreateTime { get; set; }
    
    //更新时间
    public DateTime UpdateTime { get; set; }
    
    //创建用户信息
    public UserVO? UserVo { get; set; }
}