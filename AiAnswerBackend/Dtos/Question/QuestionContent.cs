namespace AiAnswerBackend.Dtos.Question;

public class QuestionContent
{
    public String Title { get; set; }
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