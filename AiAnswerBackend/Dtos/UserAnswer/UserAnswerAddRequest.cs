namespace AiAnswerBackend.Dtos.UserAnswer;

public class UserAnswerAddRequest
{
    //应用ID
    public string AppId { get; set; }
    //用户答案
    public List<string> Choices { get; set; }
}