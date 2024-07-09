using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AiAnswerBackend.Model;
[Table("question")]
[Index(nameof(AppId))]
public class Question
{
    //题目ID
    public Guid Id { get; set; }
    
    //题目内容(JSON格式)
    public string QuestionContent { get; set; }
    
    //题目所属应用ID
    [Required]
    public Guid AppId { get; set; }
    
    //创建用户ID
    [Required]
    public Guid CreateUserId { get; set; }
    
    //创建时间
    public DateTime CreateTime { get; set; }
    
    //更新时间
    public DateTime UpdateTime { get; set; }
    
}