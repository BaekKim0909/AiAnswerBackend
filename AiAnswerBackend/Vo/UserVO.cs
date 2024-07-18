namespace AiAnswerBackend.Vo;

public class  UserVO
{
    public string UserAccount { get; set; }
    
    public string? UserName { get; set; }

    public string? UserAvatar { get; set; }

    public string? UserProfile { get; set; } 
    
    //user:普通用户 admin:管理员 ban:被封禁;
    public string UserRole { get; set; }= "user";

}