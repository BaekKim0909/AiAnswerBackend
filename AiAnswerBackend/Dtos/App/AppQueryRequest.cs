using AiAnswerBackend.Common;

namespace AiAnswerBackend.Dtos.App;

public class AppQueryRequest : PageRequest
{
    public byte ReviewStatus { get; set; }
}