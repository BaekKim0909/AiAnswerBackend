namespace AiAnswerBackend.Common;

public class PageResponse<T>
{
    public long Total { get; set; }
    public List<T> Records { get; set; }

    public PageResponse()
    {
        Records = new List<T>();
    }

    public PageResponse(long total, List<T> records)
    {
        Total = total;
        Records = records;
    }
}