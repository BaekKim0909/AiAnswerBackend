using System.Text;
using AiAnswerBackend.Config;
using AiAnswerBackend.Data;
using AiAnswerBackend.Interfaces;
using AiAnswerBackend.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IAppRepository,AppRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
//注册Jwt设置
builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("Jwt"));
//身份验证
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var jwtSetting = builder.Configuration.GetSection("Jwt").Get<JwtSetting>();
    options.TokenValidationParameters = new TokenValidationParameters
    {
        
        ValidateIssuer = false,//验证令牌的发行者是否有效
        ValidateAudience = false,//验证令牌的受众是否有效
        ValidateLifetime = true,//验证令牌的有效期
        ValidateIssuerSigningKey = true,//验证令牌的签名密钥是否有效
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.JwtSecretKey)) // 替换为你的密钥
    };
});
//授权
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("userRole", "admin"));
    options.AddPolicy("User", policy => policy.RequireClaim("userRole", "user"));
    options.AddPolicy("User&Admin",policy => policy.RequireClaim("userRole", "user","admin"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();// 启用身份验证
app.UseAuthorization();// 启用授权


app.UseHttpsRedirection();
app.MapControllers();
app.Run();

