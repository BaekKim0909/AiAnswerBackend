namespace AiAnswerBackend.Dtos.Question;

public class QuestionContent
{
    //题目标题(题目名)
    public String Title { get; set; }
    //题目选项列表
    public List<Option> Options { get; set; }
    
    public QuestionContent(string title, List<Option> options)
    {
        Title = title;
        Options = options;
    }


    public class Option
    {
        //结果(如：I/S/T/P)
        public string Result { get; set; }
        //得分
        public int Score { get; set; }
        //选项内容
        public string Value { get; set; }
        //选项
        public string Key { get; set; }

        public Option(string result, int score, string value, string key)
        {
            Result = result;
            Score = score;
            Value = value;
            Key = key;
        }
    }
}