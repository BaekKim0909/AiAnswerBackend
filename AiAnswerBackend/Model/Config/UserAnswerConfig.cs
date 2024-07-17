using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiAnswerBackend.Model.Config;

public class UserAnswerConfig:IEntityTypeConfiguration<UserAnswer>
{
    public void Configure(EntityTypeBuilder<UserAnswer> builder)
    {
        //设置默认值
        builder.Property(item => item.AppType).HasDefaultValue(0);
        builder.Property(item => item.ScoreStrategy).HasDefaultValue(0);
        builder.Property(item => item.IsDelete).HasDefaultValue(0);
        //设置索引
        builder.HasIndex(item => item.AppId);
        builder.HasIndex(item => item.UserId);
        //设置过滤器
        builder.HasQueryFilter(item => item.IsDelete == 0);
    }
}