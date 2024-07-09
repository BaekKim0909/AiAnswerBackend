using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace AiAnswerBackend.Model;
[Table("app")]
[Index(nameof(AppName))]
public class App
{
    //应用Id
    public Guid Id { get; set; }

    //应用名
    [Required]
    [Column(TypeName = "varchar(256)")]
    public string AppName { get; set; }

    //应用描述
    [Column(TypeName ="varchar(2048)")]
    public string? AppDesc { get; set; }
    
    //应用图标
    [Column(TypeName="varchar(1024)")]
    public string? AppIcon { get; set; }
    
    //应用类型  0--得分类  1--测试类
    [Required]
    public byte AppType { get;set; }
    
    //评分策略  0--自定义 1--AI
    [Required]
    public byte ScoreStrategy { get; set; }
    
    //审核状态  0--待审核 1--通过 2--拒绝
    [Required]
    public byte ReviewStatus { get; set; }
    
    //审核信息
    [Column(TypeName = "varchar(512)")]
    public string? ReviewMessage { get; set; }
    
    //审核人Id
    public Guid ReviewerId { get; set; }
    
    //审核时间
    public DateTime ReviewTime { get; set; }
    
    //创建用户
    [Required]
    public Guid CreateUserId { get; set; }
    
    //创建时间
    [Required]
    public DateTime CreateTime { get; set; }
    
    //更新时间
    [Required]
    public DateTime UpdateTime { get; set; }
}