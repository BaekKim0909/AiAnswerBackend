namespace AiAnswerBackend.Common;
//分页结果
public class PageResult<T>
{
    //数据总数
    public long Total { get; set; }
    //每次分页的结果
    public List<T> Records { get; set; }

    public PageResult(long total, List<T> records)
    {
        Total = total;
        Records = records;
    }
}