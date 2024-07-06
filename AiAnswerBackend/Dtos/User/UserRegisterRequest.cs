namespace AiAnswerBackend.Dtos.User;

public class UserRegisterRequest
{
    public string UserAccount { get; set; }
    
    public string UserPassword { get; set; }
    
    public string CheckedPassword { get; set; }
}