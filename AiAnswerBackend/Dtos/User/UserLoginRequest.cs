namespace AiAnswerBackend.Dtos.User;

public class UserLoginRequest
{
    public string? UserAccount { get; set; }
    public string? UserPassword { get; set; }
}