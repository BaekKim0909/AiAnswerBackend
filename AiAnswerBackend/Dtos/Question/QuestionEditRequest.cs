namespace AiAnswerBackend.Dtos.Question;

public class QuestionEditRequest
{
    //Question ID
    public string Id { get; set; }
    //选项集合(JSON格式)
    public List<QuestionContent> QuestionContent { get; set; }
}