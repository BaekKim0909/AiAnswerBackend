using AiAnswerBackend.Common;

namespace AiAnswerBackend.Dtos.User;

public class UserQueryRequest : PageRequest
{
    public string? Id { get; set; }
    public string? UserAccount { get; set; }
}