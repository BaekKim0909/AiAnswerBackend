namespace AiAnswerBackend.Dtos.Question;

public class QuestionAddRequest
{
    //APP ID
    public string AppId { get; set; }
    //选项集合(JSON格式)
    public List<QuestionContent> QuestionContent { get; set; }
}