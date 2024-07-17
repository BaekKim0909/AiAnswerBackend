namespace AiAnswerBackend.Dtos.App;

public class AppUpdateRequest
{
    public string Id { get; set; }
    
    public string AppName { get; set; }

    public string AppDesc { get; set; }

    public string AppIcon { get; set; }
    
    public byte AppType { get; set; }
    
    public byte ScoringStrategy { get; set; }
}