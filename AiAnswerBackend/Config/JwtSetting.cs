namespace AiAnswerBackend.Config;

public class JwtSetting
{
    //密钥
    public string JwtSecretKey { get; set; }
    //过期时间
    public int ExpireSeconds { get; set; }
}