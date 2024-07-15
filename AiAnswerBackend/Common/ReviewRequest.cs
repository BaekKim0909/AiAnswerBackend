namespace AiAnswerBackend.Common;

//审核
public class ReviewRequest
{
    //要审核的APP的ID
    public string Id { get; set; }
    //要提交的状态
    public byte ReviewStatus { get; set; }
    //审核信息
    public string ReviewMessage { get; set; }
}