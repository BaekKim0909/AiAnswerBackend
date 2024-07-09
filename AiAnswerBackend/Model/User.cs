using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiAnswerBackend.Model;
[Table("user")]//表名
public class User
{
    [Column(TypeName = "char(36)")]
    public Guid Id { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(256)")]
    public string UserAccount { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(512)")]
    public string UserPassword { get; set; }
    
    [Column(TypeName = "varchar(256)")]
    public string? UserName { get; set; }
    
    [Column(TypeName = "varchar(1024)")]
    public string? UserAvatar { get; set; }
    
    [Column(TypeName = "varchar(512)")]
    public string? UserProfile { get; set; } 
    
    [Required]
    [Column(TypeName = "varchar(128)")]
    //user:普通用户 admin:管理员 ban:被封禁;
    public string UserRole { get; set; }= "user";
    
    public DateTime CreateTime { get; set; } = DateTime.Now;

    public DateTime UpdateTime { get; set; } = DateTime.Now;

}