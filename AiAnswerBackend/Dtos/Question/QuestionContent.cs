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
        public string Result { get; set; }
        public int Score { get; set; }
        public string Value { get; set; }
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