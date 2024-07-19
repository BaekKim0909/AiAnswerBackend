using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace AiAnswerBackend.Model;
[Table("user_answer")]
[Index(nameof(AppId),nameof(UserId))]
public class UserAnswer
{
    
    public Guid Id { get; set; }

    //应用ID
    [Required]
    public Guid AppId { get; set; }
    
    //应用类型  0--得分类  1--测试类
    [Required]
    public byte AppType { get; set; }
    
    //评分策略  0--自定义 1--AI
    [Required]
    public byte ScoreStrategy { get; set; }
    
    //用户答案(JSON数组)
    [Column(TypeName = "text")]
    public string Choices { get; set; }
    
    //评分结果ID
    public Guid ResultId { get; set; }
    
    //结果名称
    [Column(TypeName = "varchar(255)")]
    public string ResultName { get; set; }
    
    //结果描述
    [Column(TypeName = "text")]
    public string ResultDesc { get; set; }
    
    //结果图片
    [Column(TypeName = "varchar(1024)")]
    public string ResultPicture { get; set; }
    
    //结果得分
    public int ResultScore { get; set; }
    
    //创建用户ID
    [Required]
    public Guid UserId { get;set;  }
    
    //创建时间
    [Required]
    public DateTime CreateTime { get; set; }
    
    //更新时间
    [Required]
    public DateTime UpdateTime { get; set; }
    
    //是否删除
    public byte IsDelete { get; set; }
}