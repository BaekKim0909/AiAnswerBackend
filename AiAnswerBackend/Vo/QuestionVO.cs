using AiAnswerBackend.Dtos.Question;

namespace AiAnswerBackend.Vo;

public class QuestionVO
{
    //题目ID
    public string Id { get; set; }
    
    //题目内容(包含：题目和选项JSON格式)
    public List<QuestionContent> QuestionContent { get; set; }
    
    //题目所属应用ID
    public string AppId { get; set; }
    
    //创建用户ID
    public string CreateUserId { get; set; }
    
    //创建时间
    public DateTime CreateTime { get; set; }
    
    //更新时间
    public DateTime UpdateTime { get; set; }
    
    //创建用户信息
    public UserVO UserVo { get; set; }
}