using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiAnswerBackend.Model.Config;

public class AppConfig:IEntityTypeConfiguration<App>
{
    public void Configure(EntityTypeBuilder<App> builder)
    {
        //设置默认值
        builder.Property(item => item.IsDelete).HasDefaultValue(0);
        builder.Property(item => item.AppType).HasDefaultValue(0);
        builder.Property(item => item.ScoreStrategy).HasDefaultValue(0);
        builder.Property(item => item.ReviewStatus).HasDefaultValue(0);
        //设置索引
        builder.HasIndex(item => item.AppName);
        
        //设置软删除
        builder.HasQueryFilter(item => item.IsDelete == 0);
    }
}