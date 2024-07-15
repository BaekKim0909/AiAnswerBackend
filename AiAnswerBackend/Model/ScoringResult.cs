using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace AiAnswerBackend.Model;

[Table("scoring_result")]
[Index(nameof(AppId))]
public class ScoringResult
{
    public Guid Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    //结果名称
    public string ResultName { get; set; }
    
    [Column(TypeName = "text")]
    //结果描述
    public string ResultDesc { get; set; }
    
    [Column(TypeName = "varchar(1024)")]
    //结果图片
    public string ResultPicture { get; set; }
    
    //结果属性集合(JSON)
    [Column(TypeName = "varchar(256)")]
    public string ResultProp { get;set;  }
    
    //结果得分范围
    public int ResultScoreRange { get; set; }
    
    //应用ID
    [Required]
    public Guid AppId { get; set; }
    
    //创建用户ID
    [Required]
    public Guid UserId { get;set;  }
    
    //创建时间
    [Required]
    public DateTime CreateTime { get; set; }
    
    //更新时间
    [Required]
    public DateTime UpdateTime { get; set; }
    
    public byte IsDelete { get; set; }
}